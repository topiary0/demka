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
            // Создаём адаптер для таблицы users
            usersTableAdapter tb = new usersTableAdapter(); // замените на свой TableAdapter
            // Получаем все данные из таблицы
            var drop = tb.GetData();
            bool successful_login = false;

            // Перебираем все строки
            foreach (DataRow dr in drop)
            {
                // Проверяем логин (dr[2]) и пароль (dr[3]) – индексы могут отличаться!
                // Лучше использовать имена колонок: dr["login"] и dr["password"]
                if (dr["login"].ToString() == txtLogin.Text && dr["password"].ToString() == txtPassword.Text)
                {
                    successful_login = true;

                    // Сохраняем данные пользователя в сессии
                    Session.UserLogin = dr["login"].ToString();
                    Session.UserFullName = dr["name"].ToString();   // имя колонки с ФИО
                    Session.UserRoleId = Convert.ToInt32(dr["role"]);

                    MessageBox.Show("Успешная авторизация!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Открываем главную форму (или форму товаров)
                    MainForm main = new MainForm();
                    main.Show();
                    this.Hide(); // скрываем форму логина
                    break;
                }
            }

            if (!successful_login)
            {
                MessageBox.Show("Неправильный логин и/или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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