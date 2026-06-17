using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class OrderEditForm : Form
    {
        private readonly int? id;
        private ComboBox articleBox = new ComboBox(), statusBox = new ComboBox(), addressBox = new ComboBox();
        private DateTimePicker dateBox = new DateTimePicker(), deliveryBox = new DateTimePicker();
        public OrderEditForm(int? id)
        {
            this.id = id;
            Text = id.HasValue ? "Редактирование заказа" : "Добавление заказа";
            Size = new Size(520, 330);
            StartPosition = FormStartPosition.CenterParent;
            Build();
            LoadDictionaries();
            if (id.HasValue) LoadOrder();
        }

        private void Build()
        {
            int y = 20;
            AddRow("Артикул", articleBox, ref y);
            AddRow("Статус", statusBox, ref y);
            AddRow("Адрес пункта выдачи", addressBox, ref y);
            AddRow("Дата заказа", dateBox, ref y);
            AddRow("Дата выдачи", deliveryBox, ref y);
            var save = new Button { Text = "Сохранить", Location = new Point(200, y + 10), Size = new Size(120, 36) };
            save.Click += Save_Click;
            Controls.Add(save);
            articleBox.DropDownStyle = statusBox.DropDownStyle = addressBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void AddRow(string label, Control control, ref int y)
        {
            Controls.Add(new Label { Text = label, Location = new Point(20, y + 4), Size = new Size(160, 24) });
            control.Location = new Point(190, y);
            control.Size = new Size(260, 26);
            Controls.Add(control);
            y += 42;
        }

        private void LoadDictionaries()
        {
            articleBox.DataSource = Database.Query("select article from tovar order by article"); articleBox.DisplayMember = articleBox.ValueMember = "article";
            statusBox.DataSource = Database.Query("select id,name from status order by name"); statusBox.DisplayMember = "name"; statusBox.ValueMember = "id";
            addressBox.DataSource = Database.Query("select id,address from address order by address"); addressBox.DisplayMember = "address"; addressBox.ValueMember = "id";
        }

        private void LoadOrder()
        {
            var row = Database.Query("select * from orders where id=@id", new SqlParameter("@id", id.Value)).Rows[0];
            articleBox.SelectedValue = row["article"];
            statusBox.SelectedValue = row["status"];
            addressBox.SelectedValue = row["address"];
            if (row["date"] != DBNull.Value) dateBox.Value = Convert.ToDateTime(row["date"]);
            if (row["delivery_date"] != DBNull.Value) deliveryBox.Value = Convert.ToDateTime(row["delivery_date"]);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            string sql = id.HasValue ? "update orders set article=@a,status=@s,address=@ad,date=@d,delivery_date=@dd where id=@id" : "insert into orders(article,status,address,date,delivery_date,count,login,code) values(@a,@s,@ad,@d,@dd,1,@login,0)";
            Database.Execute(sql, new SqlParameter("@a", articleBox.SelectedValue), new SqlParameter("@s", statusBox.SelectedValue), new SqlParameter("@ad", addressBox.SelectedValue), new SqlParameter("@d", dateBox.Value), new SqlParameter("@dd", deliveryBox.Value), new SqlParameter("@login", (object)Session.UserLogin ?? DBNull.Value), new SqlParameter("@id", (object)id ?? DBNull.Value));
            DialogResult = DialogResult.OK;
        }
    }
}
