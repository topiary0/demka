namespace WindowsFormsApp1
{
    partial class ProductEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox articleBox;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.ComboBox categoryBox;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.ComboBox manufactureBox;
        private System.Windows.Forms.ComboBox supplierBox;
        private System.Windows.Forms.TextBox priceBox;
        private System.Windows.Forms.TextBox measureBox;
        private System.Windows.Forms.TextBox countBox;
        private System.Windows.Forms.TextBox saleBox;
        private System.Windows.Forms.TextBox imageBox;
        private System.Windows.Forms.Button chooseImageButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.PictureBox productPictureBox;
        private System.Windows.Forms.Label articleLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label manufactureLabel;
        private System.Windows.Forms.Label supplierLabel;
        private System.Windows.Forms.Label priceLabel;
        private System.Windows.Forms.Label measureLabel;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.Label saleLabel;
        private System.Windows.Forms.Label imageLabel;
        private System.Windows.Forms.Label autoArticleLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.articleBox = new System.Windows.Forms.TextBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.categoryBox = new System.Windows.Forms.ComboBox();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.manufactureBox = new System.Windows.Forms.ComboBox();
            this.supplierBox = new System.Windows.Forms.ComboBox();
            this.priceBox = new System.Windows.Forms.TextBox();
            this.measureBox = new System.Windows.Forms.TextBox();
            this.countBox = new System.Windows.Forms.TextBox();
            this.saleBox = new System.Windows.Forms.TextBox();
            this.imageBox = new System.Windows.Forms.TextBox();
            this.chooseImageButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.productPictureBox = new System.Windows.Forms.PictureBox();
            this.articleLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.manufactureLabel = new System.Windows.Forms.Label();
            this.supplierLabel = new System.Windows.Forms.Label();
            this.priceLabel = new System.Windows.Forms.Label();
            this.measureLabel = new System.Windows.Forms.Label();
            this.countLabel = new System.Windows.Forms.Label();
            this.saleLabel = new System.Windows.Forms.Label();
            this.imageLabel = new System.Windows.Forms.Label();
            this.autoArticleLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.productPictureBox)).BeginInit();
            this.SuspendLayout();
            ConfigureLabel(this.articleLabel, "Артикул", 20, 24); ConfigureControl(this.articleBox, 170, 20, 300, 22);
            ConfigureLabel(this.nameLabel, "Наименование", 20, 62); ConfigureControl(this.nameBox, 170, 58, 300, 22);
            ConfigureLabel(this.categoryLabel, "Категория", 20, 100); ConfigureControl(this.categoryBox, 170, 96, 300, 24);
            ConfigureLabel(this.descriptionLabel, "Описание", 20, 138); ConfigureControl(this.descriptionBox, 170, 134, 300, 68);
            this.descriptionBox.Multiline = true;
            ConfigureLabel(this.manufactureLabel, "Производитель", 20, 220); ConfigureControl(this.manufactureBox, 170, 216, 300, 24);
            ConfigureLabel(this.supplierLabel, "Поставщик", 20, 258); ConfigureControl(this.supplierBox, 170, 254, 300, 24);
            ConfigureLabel(this.priceLabel, "Цена", 20, 296); ConfigureControl(this.priceBox, 170, 292, 300, 22);
            ConfigureLabel(this.measureLabel, "Ед. измерения", 20, 334); ConfigureControl(this.measureBox, 170, 330, 300, 22);
            ConfigureLabel(this.countLabel, "Количество", 20, 372); ConfigureControl(this.countBox, 170, 368, 300, 22);
            ConfigureLabel(this.saleLabel, "Скидка, %", 20, 410); ConfigureControl(this.saleBox, 170, 406, 300, 22);
            ConfigureLabel(this.imageLabel, "Фото", 20, 448); ConfigureControl(this.imageBox, 170, 444, 300, 22);
            this.autoArticleLabel.Location = new System.Drawing.Point(170, 20);
            this.autoArticleLabel.Name = "autoArticleLabel";
            this.autoArticleLabel.Size = new System.Drawing.Size(360, 22);
            this.autoArticleLabel.Text = "Артикул будет создан автоматически при сохранении";
            this.autoArticleLabel.Visible = false;
            this.productPictureBox.Location = new System.Drawing.Point(500, 20);
            this.productPictureBox.Name = "productPictureBox";
            this.productPictureBox.Size = new System.Drawing.Size(300, 200);
            this.productPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.productPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chooseImageButton.Location = new System.Drawing.Point(500, 235);
            this.chooseImageButton.Name = "chooseImageButton";
            this.chooseImageButton.Size = new System.Drawing.Size(140, 32);
            this.chooseImageButton.Text = "Выбрать фото";
            this.chooseImageButton.UseVisualStyleBackColor = true;
            this.saveButton.Location = new System.Drawing.Point(170, 495);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(130, 36);
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.cancelButton.Location = new System.Drawing.Point(320, 495);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(130, 36);
            this.cancelButton.Text = "Назад";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.categoryBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.manufactureBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.supplierBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ClientSize = new System.Drawing.Size(830, 560);
            this.Controls.Add(this.autoArticleLabel);
            this.Controls.Add(this.imageLabel);
            this.Controls.Add(this.saleLabel);
            this.Controls.Add(this.countLabel);
            this.Controls.Add(this.measureLabel);
            this.Controls.Add(this.priceLabel);
            this.Controls.Add(this.supplierLabel);
            this.Controls.Add(this.manufactureLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.categoryLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.articleLabel);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.saleBox);
            this.Controls.Add(this.countBox);
            this.Controls.Add(this.measureBox);
            this.Controls.Add(this.priceBox);
            this.Controls.Add(this.supplierBox);
            this.Controls.Add(this.manufactureBox);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.categoryBox);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.articleBox);
            this.Controls.Add(this.productPictureBox);
            this.Controls.Add(this.chooseImageButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ProductEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.productPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ConfigureLabel(System.Windows.Forms.Label label, string text, int left, int top)
        {
            label.Text = text;
            label.Location = new System.Drawing.Point(left, top);
            label.Size = new System.Drawing.Size(135, 22);
        }

        private void ConfigureControl(System.Windows.Forms.Control control, int left, int top, int width, int height)
        {
            control.Location = new System.Drawing.Point(left, top);
            control.Size = new System.Drawing.Size(width, height);
        }
    }
}
