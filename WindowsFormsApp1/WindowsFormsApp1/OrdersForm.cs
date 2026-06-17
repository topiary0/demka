using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class OrdersForm : Form
    {
        public OrdersForm()
        {
            InitializeComponent();
            addOrderButton.Visible = Session.IsAdmin;
            deleteOrderButton.Visible = Session.IsAdmin;
            addOrderButton.Click += Add_Click;
            deleteOrderButton.Click += Delete_Click;
            backButton.Click += (s, e) => Close();
            grid.CellDoubleClick += Grid_CellDoubleClick;
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

        private void Add_Click(object sender, EventArgs e)
        {
            if (!Session.IsAdmin) { ShowAdminOnly("Добавлять заказы"); return; }
            using (var form = new OrderEditForm(null)) form.ShowDialog();
            RefreshOrders();
        }

        private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (!Session.IsAdmin) { ShowAdminOnly("Редактировать заказы"); return; }
            using (var form = new OrderEditForm(Convert.ToInt32(grid.Rows[e.RowIndex].Cells["ID"].Value))) form.ShowDialog();
            RefreshOrders();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (!Session.IsAdmin) { ShowAdminOnly("Удалять заказы"); return; }
            if (grid.CurrentRow == null) return;
            if (MessageBox.Show("Удалить заказ без возможности восстановления?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Database.Execute("delete from orders where id=@id", new SqlParameter("@id", grid.CurrentRow.Cells["ID"].Value));
                RefreshOrders();
            }
        }

        private void ShowAdminOnly(string action)
        {
            MessageBox.Show(action + " может только администратор.", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
