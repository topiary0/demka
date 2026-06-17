using System;
using System.Data;
using System.Windows.Forms;
// ПОДСТАВЬТЕ СВОЁ ПРОСТРАНСТВО ИМЁН ДЛЯ TableAdapter
using WindowsFormsApp1.user10DataSetTableAdapters;

namespace WindowsFormsApp1
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            btnRegister.Click += BtnRegister_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();
            string fullName = txtFullName.Text.Trim();

            // Проверка заполнения
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Используем TableAdapter для проверки существования логина
            usersTableAdapter tb = new usersTableAdapter(); // замените на своё имя адаптера
            var drop = tb.GetData();

            // Проверяем, есть ли уже такой логин
            foreach (DataRow dr in drop)
            {
                if (dr["login"].ToString() == login)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Добавляем нового пользователя
            DataRow newRow = drop.NewRow();
            newRow["login"] = login;
            newRow["password"] = password;
            newRow["name"] = fullName;
            newRow["role"] = 3; // роль "клиент"

            drop.Rows.Add(newRow);
            // Сохраняем изменения в БД через TableAdapter
            tb.Update(drop);

            MessageBox.Show("Регистрация успешна! Вы автоматически вошли.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Автоматический вход
            Session.UserLogin = login;
            Session.UserFullName = fullName;
            Session.UserRoleId = 3;

            this.DialogResult = DialogResult.OK;
            this.Close();

            // Закрываем форму логина (если она открыта) и открываем главную
            foreach (Form f in Application.OpenForms)
            {
                if (f is LoginForm)
                {
                    f.Hide();
                    break;
                }
            }
            MainForm main = new MainForm();
            main.Show();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}