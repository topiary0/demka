using System;
using System.Data;
using System.Windows.Forms;
// ПОДСТАВЬТЕ СВОЁ ПРОСТРАНСТВО ИМЁН ДЛЯ TableAdapter
using WindowsFormsApp1.user10DataSetTableAdapters;

namespace WindowsFormsApp1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            // Привязываем обработчики (если не назначены в дизайнере)
            btnLogin.Click += BtnLogin_Click;
            btnGuest.Click += BtnGuest_Click;
            btnRegister.Click += BtnRegister_Click;
        }

        // ---------- ВХОД ПО ЛОГИНУ/ПАРОЛЮ (как в Exam) ----------
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Введите логин и пароль из базы данных.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                DataTable users = Database.Query(@"select u.login, u.password, u.name, u.role, r.name as role_name
                    from users u left join role r on r.id = u.role
                    where u.login = @login and u.password = @password",
                    new System.Data.SqlClient.SqlParameter("@login", txtLogin.Text.Trim()),
                    new System.Data.SqlClient.SqlParameter("@password", txtPassword.Text));

                if (users.Rows.Count == 0)
                {
                    MessageBox.Show("Неправильный логин и/или пароль. Проверьте введенные данные и повторите попытку.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow user = users.Rows[0];
                Session.UserLogin = user["login"].ToString();
                Session.UserFullName = user["name"].ToString();
                Session.UserRoleId = Convert.ToInt32(user["role"]);
                Session.UserRoleName = user["role_name"].ToString();

                MessageBox.Show("Успешная авторизация!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MainForm main = new MainForm();
                main.Show();
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось выполнить авторизацию. Проверьте подключение к базе данных.\n" + ex.Message, "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- ВХОД КАК ГОСТЬ ----------
        private void BtnGuest_Click(object sender, EventArgs e)
        {
            Session.Logout(); // очищаем сессию
            MainForm main = new MainForm();
            main.Show();
            this.Hide();
        }

        // ---------- РЕГИСТРАЦИЯ ----------
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm reg = new RegisterForm();
            reg.ShowDialog();
        }
    }
}