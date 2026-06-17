using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        public DataRow UserScope { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(DataRow user)
        {
            InitializeComponent();
            UserScope = user;
        }

        // Имя метода соответствует тому, что указано в дизайнере
        private void MainForms_Load(object sender, EventArgs e)
        {
            // Приветствие
            if (UserScope != null)
            {
                string fullName = UserScope["full_name"]?.ToString() ?? "Пользователь";
                lblWelcome.Text = "Добро пожаловать, " + fullName + "!";
            }
            else
            {
                lblWelcome.Text = "Добро пожаловать!";
            }

            // Если роль = 3 (гость) – скрываем сортировку, поиск, фильтр и заказы
            bool isGuest = (UserScope != null && UserScope["role_id"]?.ToString() == "3");
            if (isGuest)
            {
                search_textbox.Visible = false;
                Sort_combobox.Visible = false;
                open_orders_button.Visible = false;
                filter_label.Visible = false;
                filter_supplier_combobox.Visible = false;
            }

            // Загружаем поставщиков в выпадающий список фильтра
            LoadSuppliers();

            // Первоначальная загрузка товаров
            RefreshGoods();
        }

        /// <summary>
        /// Загружает список поставщиков в выпадающий список фильтра.
        /// Первый элемент - "Все поставщики".
        /// </summary>
        private void LoadSuppliers()
        {
            filter_supplier_combobox.Items.Clear();
            filter_supplier_combobox.Items.Add("Все поставщики");

            try
            {
                var adapter = new user10DataSetTableAdapters.supplierTableAdapter();
                var table = adapter.GetData();
                foreach (DataRow row in table.Rows)
                {
                    string supplierName = row["name"]?.ToString();
                    if (!string.IsNullOrEmpty(supplierName))
                        filter_supplier_combobox.Items.Add(supplierName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки поставщиков: " + ex.Message, "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (filter_supplier_combobox.Items.Count > 0)
                filter_supplier_combobox.SelectedIndex = 0;
        }

        /// <summary>
        /// Обновляет список товаров с учётом текущих фильтра, поиска и сортировки.
        /// </summary>
        private void RefreshGoods()
        {
            Main_Panel.Controls.Clear();

            // Получаем все товары
            var adapter = new user10DataSetTableAdapters.tovarTableAdapter();
            DataTable table = adapter.GetData();

            // ---- ЗАГРУЗКА СПРАВОЧНИКОВ ДЛЯ ПОИСКА И ФИЛЬТРАЦИИ ----
            var categories = new Dictionary<int, string>();
            var manufacturers = new Dictionary<int, string>();
            var suppliers = new Dictionary<int, string>();

            try
            {
                var catAdapter = new user10DataSetTableAdapters.categoryTableAdapter();
                var catTable = catAdapter.GetData();
                foreach (DataRow row in catTable.Rows)
                {
                    int id = Convert.ToInt32(row[0]);
                    string name = row[1]?.ToString() ?? "";
                    categories[id] = name;
                }

                var manAdapter = new user10DataSetTableAdapters.manufactureTableAdapter();
                var manTable = manAdapter.GetData();
                foreach (DataRow row in manTable.Rows)
                {
                    int id = Convert.ToInt32(row[0]);
                    string name = row[1]?.ToString() ?? "";
                    manufacturers[id] = name;
                }

                var supAdapter = new user10DataSetTableAdapters.supplierTableAdapter();
                var supTable = supAdapter.GetData();
                foreach (DataRow row in supTable.Rows)
                {
                    int id = Convert.ToInt32(row[0]);
                    string name = row[1]?.ToString() ?? "";
                    suppliers[id] = name;
                }
            }
            catch (Exception ex)
            {
                // Если справочники не загрузились, поиск и фильтр будут работать только по прямым полям
                MessageBox.Show("Ошибка загрузки справочников: " + ex.Message, "Предупреждение",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // ---- ПОИСК (по всем текстовым полям) ----
            string searchText = search_textbox.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                var rowsWithSearch = table.AsEnumerable().Where(row =>
                {
                    string name = row["name"]?.ToString()?.ToLower() ?? "";
                    string describe = row["describe"]?.ToString()?.ToLower() ?? "";
                    string categoryName = "";
                    string manufacturerName = "";
                    string supplierName = "";

                    int categoryId = 0, manufactureId = 0, supplierId = 0;
                    if (int.TryParse(row["category"]?.ToString(), out categoryId))
                        categories.TryGetValue(categoryId, out categoryName);
                    if (int.TryParse(row["manufacture"]?.ToString(), out manufactureId))
                        manufacturers.TryGetValue(manufactureId, out manufacturerName);
                    if (int.TryParse(row["supplier"]?.ToString(), out supplierId))
                        suppliers.TryGetValue(supplierId, out supplierName);

                    return name.Contains(searchText) ||
                           describe.Contains(searchText) ||
                           categoryName.ToLower().Contains(searchText) ||
                           manufacturerName.ToLower().Contains(searchText) ||
                           supplierName.ToLower().Contains(searchText);
                });

                if (rowsWithSearch.Any())
                    table = rowsWithSearch.CopyToDataTable();
                else
                    table = table.Clone(); // пустая таблица с той же схемой
            }

            // ---- ФИЛЬТР ПО ПОСТАВЩИКУ ----
            string selectedSupplier = filter_supplier_combobox.SelectedItem?.ToString();
            if (selectedSupplier != null && selectedSupplier != "Все поставщики")
            {
                var rowsWithSupplier = table.AsEnumerable().Where(row =>
                {
                    int supplierId = 0;
                    if (int.TryParse(row["supplier"]?.ToString(), out supplierId))
                    {
                        string supplierName = "";
                        suppliers.TryGetValue(supplierId, out supplierName);
                        return supplierName == selectedSupplier;
                    }
                    return false;
                });

                if (rowsWithSupplier.Any())
                    table = rowsWithSupplier.CopyToDataTable();
                else
                    table = table.Clone();
            }

            // ---- СОРТИРОВКА ----
            string sortField = "";
            string sortDirection = "ASC";

            if (Sort_combobox.SelectedIndex >= 0 && Sort_combobox.SelectedItem != null)
            {
                string selectedSort = Sort_combobox.SelectedItem.ToString();
                if (selectedSort.Contains("Цене"))
                {
                    sortField = "price";
                    sortDirection = selectedSort.Contains("убывание") ? "DESC" : "ASC";
                }
                else if (selectedSort.Contains("Количеству"))
                {
                    sortField = "warehouse";
                    sortDirection = selectedSort.Contains("убывание") ? "DESC" : "ASC";
                }
            }

            if (!string.IsNullOrEmpty(sortField))
                table.DefaultView.Sort = sortField + " " + sortDirection;

            // ---- ВЫВОД КАРТОЧЕК ----
            foreach (DataRowView rowView in table.DefaultView)
            {
                card productCard = new card(rowView.Row);
                Main_Panel.Controls.Add(productCard);
            }

            // Если товаров нет – показываем сообщение
            if (Main_Panel.Controls.Count == 0)
            {
                Label noDataLabel = new Label
                {
                    Text = "Товары не найдены",
                    Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.Gray,
                    AutoSize = true,
                    Location = new System.Drawing.Point(50, 50)
                };
                Main_Panel.Controls.Add(noDataLabel);
            }
        }

        // События, которые вызывают обновление списка
        private void search_textbox_TextChanged(object sender, EventArgs e) => RefreshGoods();
        private void Sort_combobox_SelectedIndexChanged(object sender, EventArgs e) => RefreshGoods();
        private void filter_supplier_combobox_SelectedIndexChanged(object sender, EventArgs e) => RefreshGoods();

        // Кнопка "Мои заказы"
        private void open_orders_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Форма заказов будет реализована позже", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Кнопка "Выйти"
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm login = new LoginForm();
            login.Show();
        }
    }
}