namespace WindowsFormsApp1
{
    partial class card
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox Main_Picture;
        private System.Windows.Forms.Label category_label;
        private System.Windows.Forms.Label name_label;
        private System.Windows.Forms.Label describe_label;
        private System.Windows.Forms.Label manuf_label;
        private System.Windows.Forms.Label supplier_label;
        private System.Windows.Forms.Label price_label;
        private System.Windows.Forms.Label measure_label;
        private System.Windows.Forms.Label count_label;
        private System.Windows.Forms.Label sale_label;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Main_Picture = new System.Windows.Forms.PictureBox();
            this.category_label = new System.Windows.Forms.Label();
            this.name_label = new System.Windows.Forms.Label();
            this.describe_label = new System.Windows.Forms.Label();
            this.manuf_label = new System.Windows.Forms.Label();
            this.supplier_label = new System.Windows.Forms.Label();
            this.price_label = new System.Windows.Forms.Label();
            this.measure_label = new System.Windows.Forms.Label();
            this.count_label = new System.Windows.Forms.Label();
            this.sale_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Main_Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // Main_Picture
            // 
            this.Main_Picture.Location = new System.Drawing.Point(18, 54);
            this.Main_Picture.Name = "Main_Picture";
            this.Main_Picture.Size = new System.Drawing.Size(305, 304);
            this.Main_Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Main_Picture.TabIndex = 0;
            this.Main_Picture.TabStop = false;
            // 
            // category_label
            // 
            this.category_label.AutoSize = true;
            this.category_label.Location = new System.Drawing.Point(342, 21);
            this.category_label.Name = "category_label";
            this.category_label.Size = new System.Drawing.Size(89, 20);
            this.category_label.TabIndex = 1;
            this.category_label.Text = "Категория";
            // 
            // name_label
            // 
            this.name_label.Location = new System.Drawing.Point(624, 21);
            this.name_label.Name = "name_label";
            this.name_label.Size = new System.Drawing.Size(304, 72);
            this.name_label.TabIndex = 2;
            this.name_label.Text = "Название";
            // 
            // describe_label
            // 
            this.describe_label.Location = new System.Drawing.Point(342, 107);
            this.describe_label.Name = "describe_label";
            this.describe_label.Size = new System.Drawing.Size(586, 136);
            this.describe_label.TabIndex = 3;
            this.describe_label.Text = "Описание";
            // 
            // manuf_label
            // 
            this.manuf_label.AutoSize = true;
            this.manuf_label.Location = new System.Drawing.Point(342, 256);
            this.manuf_label.Name = "manuf_label";
            this.manuf_label.Size = new System.Drawing.Size(131, 20);
            this.manuf_label.TabIndex = 4;
            this.manuf_label.Text = "Производитель";
            // 
            // supplier_label
            // 
            this.supplier_label.AutoSize = true;
            this.supplier_label.Location = new System.Drawing.Point(342, 287);
            this.supplier_label.Name = "supplier_label";
            this.supplier_label.Size = new System.Drawing.Size(95, 20);
            this.supplier_label.TabIndex = 5;
            this.supplier_label.Text = "Поставщик";
            // 
            // price_label
            // 
            this.price_label.AutoSize = true;
            this.price_label.Location = new System.Drawing.Point(342, 317);
            this.price_label.Name = "price_label";
            this.price_label.Size = new System.Drawing.Size(48, 20);
            this.price_label.TabIndex = 6;
            this.price_label.Text = "Цена";
            // 
            // measure_label
            // 
            this.measure_label.AutoSize = true;
            this.measure_label.Location = new System.Drawing.Point(342, 347);
            this.measure_label.Name = "measure_label";
            this.measure_label.Size = new System.Drawing.Size(73, 20);
            this.measure_label.TabIndex = 7;
            this.measure_label.Text = "Ед. Изм.";
            // 
            // count_label
            // 
            this.count_label.AutoSize = true;
            this.count_label.Location = new System.Drawing.Point(342, 380);
            this.count_label.Name = "count_label";
            this.count_label.Size = new System.Drawing.Size(73, 20);
            this.count_label.TabIndex = 8;
            this.count_label.Text = "Остаток";
            // 
            // sale_label
            // 
            this.sale_label.AutoSize = true;
            this.sale_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F);
            this.sale_label.Location = new System.Drawing.Point(1002, 159);
            this.sale_label.Name = "sale_label";
            this.sale_label.Size = new System.Drawing.Size(206, 59);
            this.sale_label.TabIndex = 9;
            this.sale_label.Text = "Скидка";
            // 
            // card
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.sale_label);
            this.Controls.Add(this.count_label);
            this.Controls.Add(this.measure_label);
            this.Controls.Add(this.price_label);
            this.Controls.Add(this.supplier_label);
            this.Controls.Add(this.manuf_label);
            this.Controls.Add(this.describe_label);
            this.Controls.Add(this.name_label);
            this.Controls.Add(this.category_label);
            this.Controls.Add(this.Main_Picture);
            this.Name = "card";
            this.Size = new System.Drawing.Size(1245, 419);
            this.Load += new System.EventHandler(this.card_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Main_Picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}