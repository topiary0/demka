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
            ((System.ComponentModel.ISupportInitialize)(this.productPictureBox)).BeginInit();
            this.SuspendLayout();
            AddLabel("Артикул", 20, 24); AddControl(this.articleBox, 170, 20, 300, 22);
            AddLabel("Наименование", 20, 62); AddControl(this.nameBox, 170, 58, 300, 22);
            AddLabel("Категория", 20, 100); AddControl(this.categoryBox, 170, 96, 300, 24);
            AddLabel("Описание", 20, 138); AddControl(this.descriptionBox, 170, 134, 300, 68);
            this.descriptionBox.Multiline = true;
            AddLabel("Производитель", 20, 220); AddControl(this.manufactureBox, 170, 216, 300, 24);
            AddLabel("Поставщик", 20, 258); AddControl(this.supplierBox, 170, 254, 300, 24);
            AddLabel("Цена", 20, 296); AddControl(this.priceBox, 170, 292, 300, 22);
            AddLabel("Ед. измерения", 20, 334); AddControl(this.measureBox, 170, 330, 300, 22);
            AddLabel("Количество", 20, 372); AddControl(this.countBox, 170, 368, 300, 22);
            AddLabel("Скидка, %", 20, 410); AddControl(this.saleBox, 170, 406, 300, 22);
            AddLabel("Фото", 20, 448); AddControl(this.imageBox, 170, 444, 300, 22);
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

        private void AddLabel(string text, int left, int top)
        {
            var label = new System.Windows.Forms.Label { Text = text, Location = new System.Drawing.Point(left, top), Size = new System.Drawing.Size(135, 22) };
            this.Controls.Add(label);
        }

        private void AddControl(System.Windows.Forms.Control control, int left, int top, int width, int height)
        {
            control.Location = new System.Drawing.Point(left, top);
            control.Size = new System.Drawing.Size(width, height);
            this.Controls.Add(control);
        }
    }
}
