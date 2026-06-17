using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class OrderEditForm : Form
    {
        private readonly int? id;

        public OrderEditForm(int? id)
        {
            this.id = id;
            InitializeComponent();
            Text = id.HasValue ? "Редактирование заказа" : "Добавление заказа";
            saveButton.Click += Save_Click;
            cancelButton.Click += (s, e) => Close();
            LoadDictionaries();
            if (id.HasValue) LoadOrder();
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
            if (deliveryBox.Value.Date < dateBox.Value.Date)
            {
                MessageBox.Show("Дата выдачи не может быть раньше даты заказа.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string sql = id.HasValue ? "update orders set article=@a,status=@s,address=@ad,date=@d,delivery_date=@dd where id=@id" : "insert into orders(article,status,address,date,delivery_date,count,login,code) values(@a,@s,@ad,@d,@dd,1,@login,0)";
            try
            {
                Database.Execute(sql, new SqlParameter("@a", articleBox.SelectedValue), new SqlParameter("@s", statusBox.SelectedValue), new SqlParameter("@ad", addressBox.SelectedValue), new SqlParameter("@d", dateBox.Value), new SqlParameter("@dd", deliveryBox.Value), new SqlParameter("@login", (object)Session.UserLogin ?? DBNull.Value), new SqlParameter("@id", (object)id ?? DBNull.Value));
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить заказ. Проверьте заполнение полей и подключение к БД.\n" + ex.Message, "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
