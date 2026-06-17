using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class card : UserControl
    {
        private readonly Color normalBackColor = Color.White;
        private readonly Color saleBackColor = Color.FromArgb(255, 248, 232);
        private bool selected;

        public DataRow CardDDD { get; set; }
        public string Article => GetValue("article");

        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                BorderStyle = selected ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
                BackColor = selected ? Color.FromArgb(232, 242, 255) : GetCardBackColor();
            }
        }

        public card(DataRow DR)
        {
            InitializeComponent();
            CardDDD = DR;
        }

        private void card_Load(object sender, EventArgs e)
        {
            int sale = ToInt(GetValue("sale"));
            decimal price = ToDecimal(GetValue("price"));
            decimal finalPrice = ToDecimal(GetValue("final_price"));

            article_label.Text = "Арт. " + GetValue("article");
            name_label.Text = GetValue("name");
            category_label.Text = GetDisplayValue("category_name", "category");
            describe_label.Text = string.IsNullOrWhiteSpace(GetValue("describe")) ? "Описание не заполнено" : GetValue("describe");
            manuf_label.Text = "Производитель: " + GetDisplayValue("manufacture_name", "manufacture");
            supplier_label.Text = "Поставщик: " + GetDisplayValue("supplier_name", "supplier");
            measure_label.Text = "Ед. изм.: " + GetValue("measurment");
            count_label.Text = "Остаток: " + GetValue("warehouse");
            price_label.Text = sale > 0
                ? string.Format("Цена: {0:0.##} ₽ → {1:0.##} ₽", price, finalPrice)
                : string.Format("Цена: {0:0.##} ₽", price);
            sale_label.Text = sale > 0 ? "-" + sale + "%" : "без скидки";
            sale_label.BackColor = sale > 0 ? Color.FromArgb(230, 82, 82) : Color.FromArgb(235, 238, 245);
            sale_label.ForeColor = sale > 0 ? Color.White : Color.FromArgb(90, 96, 110);
            BackColor = GetCardBackColor();
            LoadImage();
        }

        private string GetDisplayValue(string displayColumn, string fallbackColumn)
        {
            string value = GetValue(displayColumn);
            return string.IsNullOrWhiteSpace(value) ? GetValue(fallbackColumn) : value;
        }

        private string GetValue(string columnName)
        {
            if (CardDDD == null || !CardDDD.Table.Columns.Contains(columnName) || CardDDD[columnName] == DBNull.Value) return string.Empty;
            return CardDDD[columnName].ToString();
        }

        private Color GetCardBackColor()
        {
            return ToInt(GetValue("sale")) >= 17 ? saleBackColor : normalBackColor;
        }

        private int ToInt(string value)
        {
            int result;
            return int.TryParse(value, out result) ? result : 0;
        }

        private decimal ToDecimal(string value)
        {
            decimal result;
            return decimal.TryParse(value, out result) ? result : 0;
        }

        private void LoadImage()
        {
            string picture = GetValue("picture");
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.IsNullOrWhiteSpace(picture) ? "picture.png" : picture);
            if (!File.Exists(fullPath)) fullPath = picture;

            if (File.Exists(fullPath))
            {
                using (var image = Image.FromFile(fullPath))
                {
                    Main_Picture.Image = new Bitmap(image);
                }
            }
            else
            {
                Main_Picture.Image = null;
                Main_Picture.BackColor = Color.FromArgb(236, 239, 244);
            }
        }
    }
}
