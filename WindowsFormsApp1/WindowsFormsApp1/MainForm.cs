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
        private string selectedArticle;
        private card selectedCard;

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
            productsGrid.Visible = false;
            Main_Panel.Visible = true;
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
                string sql = @"select t.article, t.picture, t.name, t.category, c.name as category_name, t.describe,
                       t.manufacture, m.name as manufacture_name, t.supplier, s.name as supplier_name, t.price, t.measurment,
                       t.warehouse, t.sale,
                       case when isnull(t.sale,0) > 0 then t.price * (100 - t.sale) / 100 else t.price end as final_price
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
                FillProductCards(table.DefaultView);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось получить список товаров.\n" + ex.Message, "Ошибка загрузки", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillProductCards(DataView goods)
        {
            Main_Panel.SuspendLayout();
            Main_Panel.Controls.Clear();
            selectedArticle = null;
            selectedCard = null;

            foreach (DataRowView view in goods)
            {
                var productCard = new card(view.Row) { Margin = new Padding(0, 0, 0, 16), Width = Main_Panel.ClientSize.Width - 35 };
                productCard.Click += ProductCard_Click;
                foreach (Control control in productCard.Controls) control.Click += ProductCard_Click;
                Main_Panel.Controls.Add(productCard);
            }

            Main_Panel.ResumeLayout();
        }

        private void ProductCard_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;
            while (control != null && !(control is card)) control = control.Parent;
            var clickedCard = control as card;
            if (clickedCard == null) return;
            if (selectedCard != null) selectedCard.Selected = false;
            selectedCard = clickedCard;
            selectedCard.Selected = true;
            selectedArticle = clickedCard.Article;
        }

        private string GetSort()
        {
            string selectedSort = Sort_combobox.SelectedItem?.ToString() ?? "";
            if (selectedSort.Contains("Цене")) return "price " + (selectedSort.Contains("убывание") ? "DESC" : "ASC");
            if (selectedSort.Contains("Количеству")) return "warehouse " + (selectedSort.Contains("убывание") ? "DESC" : "ASC");
            return "";
        }

        private void ProductsGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e) { }

        private void ProductsGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            selectedArticle = productsGrid.Rows[e.RowIndex].Cells["article"].Value.ToString();
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

        private void EditProductButton_Click(object sender, EventArgs e) => EditSelectedProduct();

        private void EditSelectedProduct()
        {
            if (!Session.IsAdmin) { MessageBox.Show("Редактировать товары может только администратор.", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (string.IsNullOrWhiteSpace(selectedArticle)) { MessageBox.Show("Выберите карточку товара для редактирования.", "Редактирование товара", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (isProductEditorOpened) { MessageBox.Show("Уже открыто окно редактирования товара. Сначала закройте его.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            isProductEditorOpened = true;
            using (var form = new ProductEditForm(selectedArticle)) form.ShowDialog();
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
            if (string.IsNullOrWhiteSpace(selectedArticle)) { MessageBox.Show("Выберите карточку товара для удаления.", "Удаление товара", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            string article = selectedArticle;
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
