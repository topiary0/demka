using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class OrdersForm : Form
    {
        private DataGridView grid = new DataGridView();
        public OrdersForm()
        {
            Text = "Заказы";
            Size = new Size(950, 600);
            StartPosition = FormStartPosition.CenterParent;
            grid.Dock = DockStyle.Top;
            grid.Height = 470;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.CellDoubleClick += Grid_CellDoubleClick;
            Controls.Add(grid);
            var add = new Button { Text = "Добавить заказ", Location = new Point(20, 500), Size = new Size(140, 36), Visible = Session.IsAdmin };
            add.Click += (s, e) => { if (!Session.IsAdmin) { MessageBox.Show("Добавлять заказы может только администратор.", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; } using (var form = new OrderEditForm(null)) form.ShowDialog(); RefreshOrders(); };
            Controls.Add(add);
            var del = new Button { Text = "Удалить заказ", Location = new Point(180, 500), Size = new Size(140, 36), Visible = Session.IsAdmin };
            del.Click += Delete_Click;
            Controls.Add(del);
            var back = new Button { Text = "Назад", Location = new Point(780, 500), Size = new Size(120, 36) };
            back.Click += (s, e) => Close();
            Controls.Add(back);
            RefreshOrders();
        }

        private void RefreshOrders()
        {
            try
            {
                grid.DataSource = Database.Query(@"select o.id as [ID], o.article as [Артикул], st.name as [Статус], a.address as [Адрес пункта выдачи], o.date as [Дата заказа], o.delivery_date as [Дата выдачи]
                    from orders o left join status st on st.id=o.status left join address a on a.id=o.address order by o.id desc");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось загрузить список заказов. Проверьте подключение к БД.\n" + ex.Message, "Ошибка загрузки", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (!Session.IsAdmin) { MessageBox.Show("Редактировать заказы может только администратор.", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            using (var form = new OrderEditForm(Convert.ToInt32(grid.Rows[e.RowIndex].Cells["ID"].Value))) form.ShowDialog();
            RefreshOrders();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (!Session.IsAdmin) { MessageBox.Show("Удалять заказы может только администратор.", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (grid.CurrentRow == null) return;
            if (MessageBox.Show("Удалить заказ без возможности восстановления?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Database.Execute("delete from orders where id=@id", new SqlParameter("@id", grid.CurrentRow.Cells["ID"].Value));
                RefreshOrders();
            }
        }
    }
}
