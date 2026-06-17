namespace WindowsFormsApp1
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel Main_Panel;
        private System.Windows.Forms.ComboBox Sort_combobox;
        private System.Windows.Forms.TextBox search_textbox;
        private System.Windows.Forms.Button open_orders_button;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label filter_label;
        private System.Windows.Forms.ComboBox filter_supplier_combobox;
        private System.Windows.Forms.DataGridView productsGrid;
        private System.Windows.Forms.Button addProductButton;
        private System.Windows.Forms.Button deleteProductButton;
        private System.Windows.Forms.Label search_label;
        private System.Windows.Forms.Label sort_label;
        private System.Windows.Forms.Label role_label;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Main_Panel = new System.Windows.Forms.FlowLayoutPanel();
            this.Sort_combobox = new System.Windows.Forms.ComboBox();
            this.search_textbox = new System.Windows.Forms.TextBox();
            this.open_orders_button = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.filter_label = new System.Windows.Forms.Label();
            this.filter_supplier_combobox = new System.Windows.Forms.ComboBox();
            this.productsGrid = new System.Windows.Forms.DataGridView();
            this.addProductButton = new System.Windows.Forms.Button();
            this.deleteProductButton = new System.Windows.Forms.Button();
            this.search_label = new System.Windows.Forms.Label();
            this.sort_label = new System.Windows.Forms.Label();
            this.role_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.productsGrid)).BeginInit();
            this.SuspendLayout();

            // Main_Panel
            this.Main_Panel.AutoScroll = true;
            this.Main_Panel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.Main_Panel.Location = new System.Drawing.Point(20, 70);
            this.Main_Panel.Name = "Main_Panel";
            this.Main_Panel.Padding = new System.Windows.Forms.Padding(10);
            this.Main_Panel.Size = new System.Drawing.Size(1100, 700);
            this.Main_Panel.TabIndex = 0;
            this.Main_Panel.Visible = false;
            this.Main_Panel.WrapContents = true;

            // productsGrid
            this.productsGrid.Location = new System.Drawing.Point(20, 70);
            this.productsGrid.Name = "productsGrid";
            this.productsGrid.Size = new System.Drawing.Size(1100, 700);
            this.productsGrid.ReadOnly = true;
            this.productsGrid.AllowUserToAddRows = false;
            this.productsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.productsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.productsGrid.MultiSelect = false;
            this.productsGrid.TabIndex = 8;
            this.productsGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProductsGrid_CellDoubleClick);
            this.productsGrid.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.ProductsGrid_RowPrePaint);

            // sort_label
            this.sort_label.AutoSize = true;
            this.sort_label.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.sort_label.Location = new System.Drawing.Point(1140, 50);
            this.sort_label.Name = "sort_label";
            this.sort_label.Size = new System.Drawing.Size(91, 19);
            this.sort_label.Text = "Сортировка:";

            // Sort_combobox
            this.Sort_combobox.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.Sort_combobox.FormattingEnabled = true;
            this.Sort_combobox.Items.AddRange(new object[] {
                "Цене (убывание)",
                "Цене (возрастание)",
                "Количеству (убывание)",
                "Количеству (возрастание)"});
            this.Sort_combobox.Location = new System.Drawing.Point(1140, 70);
            this.Sort_combobox.Name = "Sort_combobox";
            this.Sort_combobox.Size = new System.Drawing.Size(200, 28);
            this.Sort_combobox.TabIndex = 1;
            this.Sort_combobox.SelectedIndexChanged += new System.EventHandler(this.Sort_combobox_SelectedIndexChanged);

            // search_label
            this.search_label.AutoSize = true;
            this.search_label.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.search_label.Location = new System.Drawing.Point(1140, 105);
            this.search_label.Name = "search_label";
            this.search_label.Size = new System.Drawing.Size(55, 19);
            this.search_label.Text = "Поиск:";

            // search_textbox
            this.search_textbox.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.search_textbox.Location = new System.Drawing.Point(1140, 128);
            this.search_textbox.Name = "search_textbox";
            this.search_textbox.Size = new System.Drawing.Size(200, 27);
            this.search_textbox.TabIndex = 2;
            this.search_textbox.TextChanged += new System.EventHandler(this.search_textbox_TextChanged);

            // open_orders_button
            this.open_orders_button.BackColor = System.Drawing.Color.FromArgb(50, 120, 220);
            this.open_orders_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.open_orders_button.FlatAppearance.BorderSize = 0;
            this.open_orders_button.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.open_orders_button.ForeColor = System.Drawing.Color.White;
            this.open_orders_button.Location = new System.Drawing.Point(1140, 300);
            this.open_orders_button.Name = "open_orders_button";
            this.open_orders_button.Size = new System.Drawing.Size(200, 40);
            this.open_orders_button.TabIndex = 3;
            this.open_orders_button.Text = "Заказы";
            this.open_orders_button.UseVisualStyleBackColor = false;
            this.open_orders_button.Click += new System.EventHandler(this.open_orders_button_Click);

            // btnLogout
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(220, 50, 50);
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(1140, 20);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(200, 30);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Выйти";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);

            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(40, 40, 50);
            this.lblWelcome.Location = new System.Drawing.Point(20, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(0, 30);
            this.lblWelcome.TabIndex = 5;

            // filter_label
            this.filter_label.AutoSize = true;
            this.filter_label.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.filter_label.Location = new System.Drawing.Point(1140, 170);
            this.filter_label.Name = "filter_label";
            this.filter_label.Size = new System.Drawing.Size(89, 23);
            this.filter_label.TabIndex = 6;
            this.filter_label.Text = "Поставщик:";

            // filter_supplier_combobox
            this.filter_supplier_combobox.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.filter_supplier_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filter_supplier_combobox.Location = new System.Drawing.Point(1140, 200);
            this.filter_supplier_combobox.Name = "filter_supplier_combobox";
            this.filter_supplier_combobox.Size = new System.Drawing.Size(200, 28);
            this.filter_supplier_combobox.TabIndex = 7;
            this.filter_supplier_combobox.SelectedIndexChanged += new System.EventHandler(this.filter_supplier_combobox_SelectedIndexChanged);


            // addProductButton
            this.addProductButton.BackColor = System.Drawing.Color.FromArgb(50, 120, 220);
            this.addProductButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addProductButton.FlatAppearance.BorderSize = 0;
            this.addProductButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.addProductButton.ForeColor = System.Drawing.Color.White;
            this.addProductButton.Location = new System.Drawing.Point(1140, 350);
            this.addProductButton.Name = "addProductButton";
            this.addProductButton.Size = new System.Drawing.Size(200, 40);
            this.addProductButton.Text = "Добавить товар";
            this.addProductButton.UseVisualStyleBackColor = false;
            this.addProductButton.Click += new System.EventHandler(this.AddProductButton_Click);

            // deleteProductButton
            this.deleteProductButton.BackColor = System.Drawing.Color.FromArgb(190, 60, 60);
            this.deleteProductButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteProductButton.FlatAppearance.BorderSize = 0;
            this.deleteProductButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.deleteProductButton.ForeColor = System.Drawing.Color.White;
            this.deleteProductButton.Location = new System.Drawing.Point(1140, 400);
            this.deleteProductButton.Name = "deleteProductButton";
            this.deleteProductButton.Size = new System.Drawing.Size(200, 40);
            this.deleteProductButton.Text = "Удалить товар";
            this.deleteProductButton.UseVisualStyleBackColor = false;
            this.deleteProductButton.Click += new System.EventHandler(this.DeleteProductButton_Click);

            // role_label
            this.role_label.AutoSize = true;
            this.role_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.role_label.ForeColor = System.Drawing.Color.FromArgb(80, 80, 90);
            this.role_label.Location = new System.Drawing.Point(20, 50);
            this.role_label.Name = "role_label";
            this.role_label.Size = new System.Drawing.Size(0, 19);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.ClientSize = new System.Drawing.Size(1380, 800);
            this.Controls.Add(this.role_label);
            this.Controls.Add(this.deleteProductButton);
            this.Controls.Add(this.addProductButton);
            this.Controls.Add(this.sort_label);
            this.Controls.Add(this.search_label);
            this.Controls.Add(this.productsGrid);
            this.Controls.Add(this.filter_supplier_combobox);
            this.Controls.Add(this.filter_label);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.open_orders_button);
            this.Controls.Add(this.search_textbox);
            this.Controls.Add(this.Sort_combobox);
            this.Controls.Add(this.Main_Panel);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главная";
            this.Load += new System.EventHandler(this.MainForms_Load);
            ((System.ComponentModel.ISupportInitialize)(this.productsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}