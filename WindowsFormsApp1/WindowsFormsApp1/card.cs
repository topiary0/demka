using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class card : UserControl
    {
        public DataRow CardDDD { get; set; }

        public card(DataRow DR)
        {
            InitializeComponent();
            CardDDD = DR;
        }

        private void card_Load(object sender, EventArgs e)
        {
            // Заполняем данные
            name_label.Text = "Наименование: " + CardDDD["name"].ToString();
            measure_label.Text = CardDDD["measurment"].ToString();
            describe_label.Text = CardDDD["describe"].ToString();
            count_label.Text = CardDDD["warehouse"].ToString();
            price_label.Text = CardDDD["price"].ToString();
            sale_label.Text = CardDDD["sale"].ToString();

            // Категория
            try
            {
                var category_dr = new user10DataSetTableAdapters.categoryTableAdapter();
                var category_table = category_dr.GetData();
                foreach (DataRow row in category_table)
                {
                    if (row[0].ToString() == CardDDD["category"].ToString())
                    {
                        category_label.Text = row[1].ToString();
                        break;
                    }
                }
            }
            catch { category_label.Text = "—"; }

            // Производитель
            try
            {
                var manufact_dr = new user10DataSetTableAdapters.manufactureTableAdapter();
                var manufacturer_table = manufact_dr.GetData();
                foreach (DataRow row in manufacturer_table)
                {
                    if (row[0].ToString() == CardDDD["manufacturer"].ToString())
                    {
                        manuf_label.Text = row[1].ToString();
                        break;
                    }
                }
            }
            catch { manuf_label.Text = "—"; }

            // Поставщик
            try
            {
                var supplier_dr = new user10DataSetTableAdapters.supplierTableAdapter();
                var supplier_table = supplier_dr.GetData();
                foreach (DataRow row in supplier_table)
                {
                    if (row[0].ToString() == CardDDD["supplier"].ToString())
                    {
                        supplier_label.Text = row[1].ToString();
                        break;
                    }
                }
            }
            catch { supplier_label.Text = "—"; }

            // Картинка
            string imagePath = "C:\\Users\\Иван\\OneDrive\\Рабочий стол\\ЗАДАНИЕ ДЭ\\Модуль 1\\import\\" + CardDDD["picture"].ToString();
            if (System.IO.File.Exists(imagePath))
            {
                using (var st = new System.IO.FileStream(imagePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    Main_Picture.Image = Image.FromStream(st);
                }
            }
            else
            {
                Main_Picture.Image = null;
                Main_Picture.BackColor = Color.LightGray;
            }

            // Скидка
            if (int.TryParse(CardDDD["sale"].ToString(), out int sale) && sale >= 17)
            {
                this.BackColor = Color.FromArgb(200, 220, 255); // светло-синий
            }
        }
    }
}