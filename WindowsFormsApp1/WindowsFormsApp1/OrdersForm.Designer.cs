namespace WindowsFormsApp1
{
    partial class OrdersForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Button addOrderButton;
        private System.Windows.Forms.Button deleteOrderButton;
        private System.Windows.Forms.Button backButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grid = new System.Windows.Forms.DataGridView();
            this.addOrderButton = new System.Windows.Forms.Button();
            this.deleteOrderButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            this.grid.Location = new System.Drawing.Point(20, 20);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(900, 470);
            this.grid.ReadOnly = true;
            this.grid.AllowUserToAddRows = false;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.MultiSelect = false;
            this.addOrderButton.Location = new System.Drawing.Point(20, 510);
            this.addOrderButton.Name = "addOrderButton";
            this.addOrderButton.Size = new System.Drawing.Size(150, 36);
            this.addOrderButton.Text = "Добавить заказ";
            this.deleteOrderButton.Location = new System.Drawing.Point(190, 510);
            this.deleteOrderButton.Name = "deleteOrderButton";
            this.deleteOrderButton.Size = new System.Drawing.Size(150, 36);
            this.deleteOrderButton.Text = "Удалить заказ";
            this.backButton.Location = new System.Drawing.Point(800, 510);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(120, 36);
            this.backButton.Text = "Назад";
            this.ClientSize = new System.Drawing.Size(950, 575);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.addOrderButton);
            this.Controls.Add(this.deleteOrderButton);
            this.Controls.Add(this.backButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "OrdersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Заказы";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
