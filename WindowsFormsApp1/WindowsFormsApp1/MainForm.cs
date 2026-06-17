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
                if (user.Table.Columns.Contains("role_name")) Session.UserRoleName = user["role_name"].ToString();
            }
        }

        private void BuildProductGrid()
        {
            Text = "Список товаров";
        }

        private void MainForms_Load(object sender, EventArgs e)
        {
            string roleText = Session.IsGuest ? "гость" : string.IsNullOrWhiteSpace(Session.UserRoleName) ? "роль ID " + Session.UserRoleId : Session.UserRoleName;
            lblWelcome.Text = "Пользователь: " + Session.DisplayName + " | Роль: " + roleText;
            role_label.Text = GetRoleHint();
            search_textbox.Visible = Session.IsAdmin || Session.IsManager;
            search_label.Visible = Session.IsAdmin || Session.IsManager;
            Sort_combobox.Visible = Session.IsAdmin || Session.IsManager;
            sort_label.Visible = Session.IsAdmin || Session.IsManager;
            filter_label.Visible = Session.IsAdmin || Session.IsManager;
            filter_supplier_combobox.Visible = Session.IsAdmin || Session.IsManager;
            open_orders_button.Visible = Session.IsAdmin || Session.IsManager;
            open_orders_button.Text = "Заказы";
            addProductButton.Visible = Session.IsAdmin;
            editProductButton.Visible = Session.IsAdmin;
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
                       t.warehouse as [Количество], t.sale as [Скидка],
                       case when isnull(t.sale,0) > 0 then t.price * (100 - t.sale) / 100 else t.price end as [Итоговая цена]
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
            int count = ToInt(row.Cells["Количество"].Value);
            int sale = ToInt(row.Cells["Скидка"].Value);
            row.DefaultCellStyle.BackColor = count <= 0 ? Color.LightBlue : sale > 17 ? ColorTranslator.FromHtml("#FFDEAD") : Color.White;
            row.Cells["Цена"].Style.ForeColor = sale > 0 ? Color.Red : Color.Black;
            row.Cells["Цена"].Style.Font = sale > 0 ? new Font(productsGrid.Font, FontStyle.Strikeout) : productsGrid.Font;
        }

        private int ToInt(object value)
        {
            if (value == null || value == DBNull.Value) return 0;
            int result;
            return int.TryParse(value.ToString(), out result) ? result : 0;
        }

        private void ProductsGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            EditSelectedProduct();
        }

        private void AddProductButton_Click(object sender, EventArgs e)
        {
            if (!Session.IsAdmin) { MessageBox.Show("Добавлять товары может только администратор.", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (isProductEditorOpened) { MessageBox.Show("Уже открыто окно редактирования товара. Сначала закройте его.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            isProductEditorOpened = true;
            using (var form = new ProductEditForm(null)) form.ShowDialog();
            isProductEditorOpened = false;
            RefreshGoods();
        }

        private void EditProductButton_Click(object sender, EventArgs e)
        {
            EditSelectedProduct();
        }

        private void EditSelectedProduct()
        {
            if (!Session.IsAdmin) { MessageBox.Show("Редактировать товары может только администратор.", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (productsGrid.CurrentRow == null) { MessageBox.Show("Выберите товар в таблице для редактирования.", "Редактирование товара", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (isProductEditorOpened) { MessageBox.Show("Уже открыто окно редактирования товара. Сначала закройте его.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            isProductEditorOpened = true;
            using (var form = new ProductEditForm(productsGrid.CurrentRow.Cells["Артикул"].Value.ToString())) form.ShowDialog();
            isProductEditorOpened = false;
            RefreshGoods();
        }

        private string GetRoleHint()
        {
            if (Session.IsAdmin) return "Доступ: товары (просмотр/поиск/сортировка/фильтр/добавление/редактирование/удаление), заказы (просмотр/добавление/редактирование/удаление).";
            if (Session.IsManager) return "Доступ: товары (просмотр/поиск/сортировка/фильтр), заказы (просмотр).";
            if (Session.IsClient) return "Доступ: просмотр списка товаров.";
            return "Доступ: гостевой просмотр списка товаров.";
        }

        private void DeleteProductButton_Click(object sender, EventArgs e)
        {
            if (!Session.IsAdmin) { MessageBox.Show("Удалять товары может только администратор.", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
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
