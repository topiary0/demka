namespace WindowsFormsApp1
{
    partial class card
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox Main_Picture;
        private System.Windows.Forms.Label article_label;
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
            if (disposing && (components != null)) components.Dispose();
            if (disposing && Main_Picture.Image != null) Main_Picture.Image.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Main_Picture = new System.Windows.Forms.PictureBox();
            this.article_label = new System.Windows.Forms.Label();
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
            this.Main_Picture.BackColor = System.Drawing.Color.FromArgb(236, 239, 244);
            this.Main_Picture.Location = new System.Drawing.Point(20, 20);
            this.Main_Picture.Name = "Main_Picture";
            this.Main_Picture.Size = new System.Drawing.Size(190, 150);
            this.Main_Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Main_Picture.TabIndex = 0;
            this.Main_Picture.TabStop = false;
            //
            // article_label
            //
            this.article_label.AutoSize = true;
            this.article_label.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.article_label.ForeColor = System.Drawing.Color.FromArgb(93, 103, 120);
            this.article_label.Location = new System.Drawing.Point(230, 18);
            this.article_label.Name = "article_label";
            this.article_label.Size = new System.Drawing.Size(56, 20);
            this.article_label.TabIndex = 1;
            this.article_label.Text = "Арт.";
            //
            // category_label
            //
            this.category_label.AutoSize = true;
            this.category_label.BackColor = System.Drawing.Color.FromArgb(231, 240, 255);
            this.category_label.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.category_label.ForeColor = System.Drawing.Color.FromArgb(38, 88, 170);
            this.category_label.Location = new System.Drawing.Point(230, 45);
            this.category_label.Name = "category_label";
            this.category_label.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.category_label.Size = new System.Drawing.Size(98, 26);
            this.category_label.TabIndex = 2;
            this.category_label.Text = "Категория";
            //
            // name_label
            //
            this.name_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.name_label.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.name_label.ForeColor = System.Drawing.Color.FromArgb(33, 37, 45);
            this.name_label.Location = new System.Drawing.Point(230, 80);
            this.name_label.Name = "name_label";
            this.name_label.Size = new System.Drawing.Size(590, 42);
            this.name_label.TabIndex = 3;
            this.name_label.Text = "Название товара";
            //
            // describe_label
            //
            this.describe_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.describe_label.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.describe_label.ForeColor = System.Drawing.Color.FromArgb(75, 82, 95);
            this.describe_label.Location = new System.Drawing.Point(230, 126);
            this.describe_label.Name = "describe_label";
            this.describe_label.Size = new System.Drawing.Size(590, 52);
            this.describe_label.TabIndex = 4;
            this.describe_label.Text = "Описание";
            //
            // manuf_label
            //
            this.manuf_label.AutoSize = true;
            this.manuf_label.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.manuf_label.ForeColor = System.Drawing.Color.FromArgb(75, 82, 95);
            this.manuf_label.Location = new System.Drawing.Point(230, 188);
            this.manuf_label.Name = "manuf_label";
            this.manuf_label.Size = new System.Drawing.Size(139, 21);
            this.manuf_label.TabIndex = 5;
            this.manuf_label.Text = "Производитель";
            //
            // supplier_label
            //
            this.supplier_label.AutoSize = true;
            this.supplier_label.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.supplier_label.ForeColor = System.Drawing.Color.FromArgb(75, 82, 95);
            this.supplier_label.Location = new System.Drawing.Point(510, 188);
            this.supplier_label.Name = "supplier_label";
            this.supplier_label.Size = new System.Drawing.Size(98, 21);
            this.supplier_label.TabIndex = 6;
            this.supplier_label.Text = "Поставщик";
            //
            // price_label
            //
            this.price_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.price_label.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.price_label.ForeColor = System.Drawing.Color.FromArgb(25, 124, 76);
            this.price_label.Location = new System.Drawing.Point(840, 72);
            this.price_label.Name = "price_label";
            this.price_label.Size = new System.Drawing.Size(230, 76);
            this.price_label.TabIndex = 7;
            this.price_label.Text = "Цена";
            this.price_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // measure_label
            //
            this.measure_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.measure_label.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.measure_label.ForeColor = System.Drawing.Color.FromArgb(75, 82, 95);
            this.measure_label.Location = new System.Drawing.Point(850, 155);
            this.measure_label.Name = "measure_label";
            this.measure_label.Size = new System.Drawing.Size(220, 24);
            this.measure_label.TabIndex = 8;
            this.measure_label.Text = "Ед. изм.";
            this.measure_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // count_label
            //
            this.count_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.count_label.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.count_label.ForeColor = System.Drawing.Color.FromArgb(75, 82, 95);
            this.count_label.Location = new System.Drawing.Point(850, 188);
            this.count_label.Name = "count_label";
            this.count_label.Size = new System.Drawing.Size(220, 24);
            this.count_label.TabIndex = 9;
            this.count_label.Text = "Остаток";
            this.count_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // sale_label
            //
            this.sale_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sale_label.BackColor = System.Drawing.Color.FromArgb(230, 82, 82);
            this.sale_label.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.sale_label.ForeColor = System.Drawing.Color.White;
            this.sale_label.Location = new System.Drawing.Point(925, 20);
            this.sale_label.Name = "sale_label";
            this.sale_label.Size = new System.Drawing.Size(145, 42);
            this.sale_label.TabIndex = 10;
            this.sale_label.Text = "-0%";
            this.sale_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // card
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
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
            this.Controls.Add(this.article_label);
            this.Controls.Add(this.Main_Picture);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "card";
            this.Size = new System.Drawing.Size(1090, 230);
            this.Load += new System.EventHandler(this.card_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Main_Picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
