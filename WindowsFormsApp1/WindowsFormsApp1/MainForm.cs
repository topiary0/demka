using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private static bool isProductEditorOpened;
        private DataGridView productsGrid;
        private Button addProductButton;
        private Button deleteProductButton;

        public MainForm()
        {
            InitializeComponent();
            BuildProductGrid();
        }

        public MainForm(DataRow user) : this()
        {
            if (user != null)
            {
                Session.UserLogin = user["login"].ToString();
                Session.UserFullName = user.Table.Columns.Contains("name") ? user["name"].ToString() : user["full_name"].ToString();
                Session.UserRoleId = Convert.ToInt32(user["role"]);
            }
        }

        private void BuildProductGrid()
        {
            Text = "Список товаров";
            Main_Panel.Visible = false;
            productsGrid = new DataGridView
            {
                Location = Main_Panel.Location,
                Size = Main_Panel.Size,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            productsGrid.CellDoubleClick += ProductsGrid_CellDoubleClick;
            productsGrid.RowPrePaint += ProductsGrid_RowPrePaint;
            Controls.Add(productsGrid);

            addProductButton = CreateSideButton("Добавить товар", 270);
            addProductButton.Click += AddProductButton_Click;
            Controls.Add(addProductButton);

            deleteProductButton = CreateSideButton("Удалить товар", 320);
            deleteProductButton.BackColor = Color.FromArgb(190, 60, 60);
            deleteProductButton.Click += DeleteProductButton_Click;
            Controls.Add(deleteProductButton);
        }

        private Button CreateSideButton(string text, int top)
        {
            return new Button
            {
                Text = text,
                Location = new Point(1140, top),
                Size = new Size(200, 40),
                BackColor = Color.FromArgb(50, 120, 220),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
        }

        private void MainForms_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = Session.IsGuest ? "Гость" : Session.UserFullName;
            search_textbox.Visible = Session.IsAdmin || Session.IsManager;
            Sort_combobox.Visible = Session.IsAdmin || Session.IsManager;
            filter_label.Visible = Session.IsAdmin || Session.IsManager;
            filter_supplier_combobox.Visible = Session.IsAdmin || Session.IsManager;
            open_orders_button.Visible = Session.IsAdmin || Session.IsManager;
            addProductButton.Visible = Session.IsAdmin;
            deleteProductButton.Visible = Session.IsAdmin;
            LoadSuppliers();
            RefreshGoods();
        }

        private void LoadSuppliers()
        {
            filter_supplier_combobox.Items.Clear();
            filter_supplier_combobox.Items.Add("Все поставщики");
            try
            {
                foreach (DataRow row in Database.Query("select name from supplier order by name").Rows)
                {
                    filter_supplier_combobox.Items.Add(row["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось загрузить поставщиков. Проверьте подключение к БД.\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            filter_supplier_combobox.SelectedIndex = 0;
        }

        private void RefreshGoods()
        {
            try
            {
                string sql = @"select t.article as [Артикул], t.picture as [Фото], t.name as [Наименование], c.name as [Категория], t.describe as [Описание],
                       m.name as [Производитель], s.name as [Поставщик], t.price as [Цена], t.measurment as [Ед. изм.],
                       t.warehouse as [Количество], t.sale as [Скидка]
                       from tovar t
                       left join category c on c.id = t.category
                       left join manufacture m on m.id = t.manufacture
                       left join supplier s on s.id = t.supplier
                       where (@search = '' or lower(isnull(t.name,'') + ' ' + isnull(t.describe,'') + ' ' + isnull(c.name,'') + ' ' + isnull(m.name,'') + ' ' + isnull(s.name,'')) like @searchLike)
                       and (@supplier = '' or s.name = @supplier)";
                var table = Database.Query(sql,
                    new SqlParameter("@search", search_textbox.Text.Trim().ToLower()),
                    new SqlParameter("@searchLike", "%" + search_textbox.Text.Trim().ToLower() + "%"),
                    new SqlParameter("@supplier", filter_supplier_combobox.SelectedItem?.ToString() == "Все поставщики" ? "" : filter_supplier_combobox.SelectedItem?.ToString() ?? ""));
                table.DefaultView.Sort = GetSort();
                productsGrid.DataSource = table.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось получить список товаров.\n" + ex.Message, "Ошибка загрузки", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetSort()
        {
            string selectedSort = Sort_combobox.SelectedItem?.ToString() ?? "";
            if (selectedSort.Contains("Цене")) return "[Цена] " + (selectedSort.Contains("убывание") ? "DESC" : "ASC");
            if (selectedSort.Contains("Количеству")) return "[Количество] " + (selectedSort.Contains("убывание") ? "DESC" : "ASC");
            return "";
        }

        private void ProductsGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var row = productsGrid.Rows[e.RowIndex];
            int count = Convert.ToInt32(row.Cells["Количество"].Value == DBNull.Value ? 0 : row.Cells["Количество"].Value);
            int sale = Convert.ToInt32(row.Cells["Скидка"].Value == DBNull.Value ? 0 : row.Cells["Скидка"].Value);
            row.DefaultCellStyle.BackColor = count <= 0 ? Color.LightBlue : sale > 17 ? ColorTranslator.FromHtml("#FFDEAD") : Color.White;
            row.Cells["Цена"].Style.ForeColor = sale > 0 ? Color.Red : Color.Black;
            row.Cells["Цена"].Style.Font = sale > 0 ? new Font(productsGrid.Font, FontStyle.Strikeout) : productsGrid.Font;
        }

        private void ProductsGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!Session.IsAdmin || e.RowIndex < 0 || isProductEditorOpened) return;
            isProductEditorOpened = true;
            using (var form = new ProductEditForm(productsGrid.Rows[e.RowIndex].Cells["Артикул"].Value.ToString())) form.ShowDialog();
            isProductEditorOpened = false;
            RefreshGoods();
        }

        private void AddProductButton_Click(object sender, EventArgs e)
        {
            if (isProductEditorOpened) return;
            isProductEditorOpened = true;
            using (var form = new ProductEditForm(null)) form.ShowDialog();
            isProductEditorOpened = false;
            RefreshGoods();
        }

        private void DeleteProductButton_Click(object sender, EventArgs e)
        {
            if (productsGrid.CurrentRow == null) return;
            string article = productsGrid.CurrentRow.Cells["Артикул"].Value.ToString();
            if (Database.Query("select id from orders where article=@article", new SqlParameter("@article", article)).Rows.Count > 0)
            {
                MessageBox.Show("Товар нельзя удалить, потому что он присутствует в заказе.", "Запрещено", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Удалить выбранный товар без возможности восстановления?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Database.Execute("delete from tovar where article=@article", new SqlParameter("@article", article));
                RefreshGoods();
            }
        }

        private void search_textbox_TextChanged(object sender, EventArgs e) => RefreshGoods();
        private void Sort_combobox_SelectedIndexChanged(object sender, EventArgs e) => RefreshGoods();
        private void filter_supplier_combobox_SelectedIndexChanged(object sender, EventArgs e) => RefreshGoods();
        private void open_orders_button_Click(object sender, EventArgs e) { using (var form = new OrdersForm()) form.ShowDialog(); }
        private void BtnLogout_Click(object sender, EventArgs e) { Session.Logout(); new LoginForm().Show(); Close(); }
    }
}
