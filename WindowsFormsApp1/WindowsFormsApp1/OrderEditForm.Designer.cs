namespace WindowsFormsApp1
{
    partial class OrderEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox articleBox;
        private System.Windows.Forms.ComboBox statusBox;
        private System.Windows.Forms.ComboBox addressBox;
        private System.Windows.Forms.DateTimePicker dateBox;
        private System.Windows.Forms.DateTimePicker deliveryBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.articleBox = new System.Windows.Forms.ComboBox();
            this.statusBox = new System.Windows.Forms.ComboBox();
            this.addressBox = new System.Windows.Forms.ComboBox();
            this.dateBox = new System.Windows.Forms.DateTimePicker();
            this.deliveryBox = new System.Windows.Forms.DateTimePicker();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            AddLabel("Артикул", 20, 24); AddControl(this.articleBox, 190, 20, 260, 24);
            AddLabel("Статус", 20, 66); AddControl(this.statusBox, 190, 62, 260, 24);
            AddLabel("Адрес пункта выдачи", 20, 108); AddControl(this.addressBox, 190, 104, 260, 24);
            AddLabel("Дата заказа", 20, 150); AddControl(this.dateBox, 190, 146, 260, 24);
            AddLabel("Дата выдачи", 20, 192); AddControl(this.deliveryBox, 190, 188, 260, 24);
            this.articleBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.addressBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.saveButton.Location = new System.Drawing.Point(190, 240);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(120, 36);
            this.saveButton.Text = "Сохранить";
            this.cancelButton.Location = new System.Drawing.Point(330, 240);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(120, 36);
            this.cancelButton.Text = "Назад";
            this.ClientSize = new System.Drawing.Size(490, 305);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "OrderEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
        }

        private void AddLabel(string text, int left, int top)
        {
            var label = new System.Windows.Forms.Label { Text = text, Location = new System.Drawing.Point(left, top), Size = new System.Drawing.Size(160, 22) };
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
