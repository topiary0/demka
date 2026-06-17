using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ProductEditForm : Form
    {
        private readonly string article;
        private string originalImagePath;

        public ProductEditForm(string article)
        {
            this.article = article;
            InitializeComponent();
            Text = article == null ? "Добавление товара" : "Редактирование товара";
            chooseImageButton.Click += Choose_Click;
            saveButton.Click += Save_Click;
            cancelButton.Click += (s, e) => Close();
            LoadDictionaries();

            if (article != null)
            {
                LoadProduct();
            }
            else
            {
                articleBox.Visible = false;
                autoArticleLabel.Visible = true;
            }
        }

        private void LoadDictionaries()
        {
            try
            {
                FillCombo(categoryBox, "select id, name from category order by name");
                FillCombo(manufactureBox, "select id, name from manufacture order by name");
                FillCombo(supplierBox, "select id, name from supplier order by name");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось загрузить справочники товара. Проверьте подключение к БД.\n" + ex.Message, "Ошибка загрузки", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillCombo(ComboBox combo, string sql)
        {
            combo.DataSource = Database.Query(sql);
            combo.DisplayMember = "name";
            combo.ValueMember = "id";
        }

        private void LoadProduct()
        {
            try
            {
                DataTable table = Database.Query("select * from tovar where article=@article", new SqlParameter("@article", article));
                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("Выбранный товар не найден в базе данных.", "Ошибка загрузки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                DataRow row = table.Rows[0];
                articleBox.Text = row["article"].ToString();
                articleBox.ReadOnly = true;
                nameBox.Text = row["name"].ToString();
                descriptionBox.Text = row["describe"].ToString();
                priceBox.Text = row["price"].ToString();
                measureBox.Text = row["measurment"].ToString();
                countBox.Text = row["warehouse"].ToString();
                saleBox.Text = row["sale"].ToString();
                imageBox.Text = row["picture"].ToString();
                originalImagePath = imageBox.Text;
                categoryBox.SelectedValue = row["category"];
                manufactureBox.SelectedValue = row["manufacture"];
                supplierBox.SelectedValue = row["supplier"];
                LoadPreview(imageBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось загрузить товар.\n" + ex.Message, "Ошибка загрузки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void Choose_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog { Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp" })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images"));
                string fileName = Path.GetFileNameWithoutExtension(dialog.FileName) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(dialog.FileName);
                string target = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", fileName);
                using (var image = Image.FromFile(dialog.FileName))
                using (var resized = new Bitmap(image, new Size(300, 200)))
                {
                    resized.Save(target);
                }

                imageBox.Text = Path.Combine("Images", fileName);
                LoadPreview(imageBox.Text);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (!ValidateProduct(out decimal price, out int count, out int sale)) return;

            string currentArticle = article ?? GetNextArticle();
            string sql = article == null
                ? "insert into tovar(article,name,category,describe,manufacture,supplier,price,measurment,warehouse,sale,picture) values(@a,@n,@c,@d,@m,@s,@p,@me,@w,@sale,@pic)"
                : "update tovar set name=@n,category=@c,describe=@d,manufacture=@m,supplier=@s,price=@p,measurment=@me,warehouse=@w,sale=@sale,picture=@pic where article=@a";

            try
            {
                Database.Execute(sql,
                    new SqlParameter("@a", currentArticle),
                    new SqlParameter("@n", nameBox.Text.Trim()),
                    new SqlParameter("@c", categoryBox.SelectedValue),
                    new SqlParameter("@d", descriptionBox.Text.Trim()),
                    new SqlParameter("@m", manufactureBox.SelectedValue),
                    new SqlParameter("@s", supplierBox.SelectedValue),
                    new SqlParameter("@p", price),
                    new SqlParameter("@me", measureBox.Text.Trim()),
                    new SqlParameter("@w", count),
                    new SqlParameter("@sale", sale),
                    new SqlParameter("@pic", string.IsNullOrWhiteSpace(imageBox.Text) ? "picture.png" : imageBox.Text.Trim()));
                DeleteOldImageIfReplaced();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить товар. Проверьте заполнение полей и подключение к БД.\n" + ex.Message, "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateProduct(out decimal price, out int count, out int sale)
        {
            price = 0;
            count = 0;
            sale = 0;
            if (string.IsNullOrWhiteSpace(nameBox.Text))
            {
                MessageBox.Show("Заполните наименование товара.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!decimal.TryParse(priceBox.Text, out price) || price < 0 || !int.TryParse(countBox.Text, out count) || count < 0 || !int.TryParse(saleBox.Text, out sale) || sale < 0)
            {
                MessageBox.Show("Цена, количество и скидка должны быть неотрицательными числами.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private string GetNextArticle()
        {
            object value = Database.Query("select isnull(max(try_convert(int, article)), 0) + 1 as next_article from tovar").Rows[0]["next_article"];
            return value.ToString();
        }

        private void LoadPreview(string relativePath)
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.IsNullOrWhiteSpace(relativePath) ? "picture.png" : relativePath);
            if (productPictureBox.Image != null)
            {
                productPictureBox.Image.Dispose();
                productPictureBox.Image = null;
            }
            if (File.Exists(fullPath))
            {
                using (var image = Image.FromFile(fullPath))
                {
                    productPictureBox.Image = new Bitmap(image);
                }
            }
        }

        private void DeleteOldImageIfReplaced()
        {
            if (string.IsNullOrWhiteSpace(originalImagePath) || originalImagePath == imageBox.Text || originalImagePath == "picture.png") return;
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, originalImagePath);
            if (File.Exists(fullPath)) File.Delete(fullPath);
        }
    }
}
