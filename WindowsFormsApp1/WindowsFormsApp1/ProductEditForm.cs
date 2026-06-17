using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class ProductEditForm : Form
    {
        private readonly string article;
        private TextBox articleBox = new TextBox(), nameBox = new TextBox(), measureBox = new TextBox(), priceBox = new TextBox(), countBox = new TextBox(), saleBox = new TextBox(), imageBox = new TextBox();
        private TextBox descriptionBox = new TextBox();
        private ComboBox categoryBox = new ComboBox(), manufactureBox = new ComboBox(), supplierBox = new ComboBox();

        public ProductEditForm(string article)
        {
            this.article = article;
            Text = article == null ? "Добавление товара" : "Редактирование товара";
            Size = new Size(650, 620);
            StartPosition = FormStartPosition.CenterParent;
            BuildForm();
            LoadDictionaries();
            if (article != null) LoadProduct();
        }

        private void BuildForm()
        {
            int y = 20;
            AddRow("Артикул", articleBox, ref y);
            AddRow("Наименование", nameBox, ref y);
            AddRow("Категория", categoryBox, ref y);
            AddRow("Описание", descriptionBox, ref y); descriptionBox.Height = 60; y += 35;
            AddRow("Производитель", manufactureBox, ref y);
            AddRow("Поставщик", supplierBox, ref y);
            AddRow("Цена", priceBox, ref y);
            AddRow("Ед. измерения", measureBox, ref y);
            AddRow("Количество", countBox, ref y);
            AddRow("Скидка", saleBox, ref y);
            AddRow("Фото", imageBox, ref y);
            var choose = new Button { Text = "Выбрать фото", Location = new Point(460, y - 35), Size = new Size(120, 28) };
            choose.Click += Choose_Click;
            Controls.Add(choose);
            var save = new Button { Text = "Сохранить", Location = new Point(260, y + 20), Size = new Size(120, 36) };
            save.Click += Save_Click;
            Controls.Add(save);
            articleBox.ReadOnly = article != null;
            categoryBox.DropDownStyle = manufactureBox.DropDownStyle = supplierBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void AddRow(string label, Control control, ref int y)
        {
            Controls.Add(new Label { Text = label, Location = new Point(20, y + 4), Size = new Size(130, 24) });
            control.Location = new Point(160, y);
            control.Size = new Size(280, 26);
            Controls.Add(control);
            y += 38;
        }

        private void LoadDictionaries()
        {
            FillCombo(categoryBox, "select id, name from category order by name");
            FillCombo(manufactureBox, "select id, name from manufacture order by name");
            FillCombo(supplierBox, "select id, name from supplier order by name");
        }

        private void FillCombo(ComboBox combo, string sql)
        {
            combo.DataSource = Database.Query(sql);
            combo.DisplayMember = "name";
            combo.ValueMember = "id";
        }

        private void LoadProduct()
        {
            var row = Database.Query("select * from tovar where article=@article", new SqlParameter("@article", article)).Rows[0];
            articleBox.Text = row["article"].ToString();
            nameBox.Text = row["name"].ToString();
            descriptionBox.Text = row["describe"].ToString();
            priceBox.Text = row["price"].ToString();
            measureBox.Text = row["measurment"].ToString();
            countBox.Text = row["warehouse"].ToString();
            saleBox.Text = row["sale"].ToString();
            imageBox.Text = row["picture"].ToString();
            categoryBox.SelectedValue = row["category"];
            manufactureBox.SelectedValue = row["manufacture"];
            supplierBox.SelectedValue = row["supplier"];
        }

        private void Choose_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog { Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images"));
                    string target = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", Path.GetFileName(dialog.FileName));
                    using (var image = Image.FromFile(dialog.FileName))
                    using (var resized = new Bitmap(image, new Size(300, 200))) resized.Save(target);
                    imageBox.Text = Path.Combine("Images", Path.GetFileName(dialog.FileName));
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(articleBox.Text) || string.IsNullOrWhiteSpace(nameBox.Text)) { MessageBox.Show("Заполните артикул и наименование.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (!decimal.TryParse(priceBox.Text, out decimal price) || price < 0 || !int.TryParse(countBox.Text, out int count) || count < 0 || !int.TryParse(saleBox.Text, out int sale) || sale < 0) { MessageBox.Show("Цена, количество и скидка должны быть неотрицательными числами.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            string sql = article == null ? "insert into tovar(article,name,category,describe,manufacture,supplier,price,measurment,warehouse,sale,picture) values(@a,@n,@c,@d,@m,@s,@p,@me,@w,@sale,@pic)" : "update tovar set name=@n,category=@c,describe=@d,manufacture=@m,supplier=@s,price=@p,measurment=@me,warehouse=@w,sale=@sale,picture=@pic where article=@a";
            Database.Execute(sql, new SqlParameter("@a", articleBox.Text), new SqlParameter("@n", nameBox.Text), new SqlParameter("@c", categoryBox.SelectedValue), new SqlParameter("@d", descriptionBox.Text), new SqlParameter("@m", manufactureBox.SelectedValue), new SqlParameter("@s", supplierBox.SelectedValue), new SqlParameter("@p", price), new SqlParameter("@me", measureBox.Text), new SqlParameter("@w", count), new SqlParameter("@sale", sale), new SqlParameter("@pic", imageBox.Text));
            DialogResult = DialogResult.OK;
        }
    }
}
