using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TVProgramApp
{
    public class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Реєстрація";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label
            {
                Text = "Реєстрація",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };

            Label lblUsername = new Label
            {
                Text = "Ім'я користувача:",
                Location = new Point(50, 70),
                AutoSize = true
            };

            TextBox txtUsername = new TextBox
            {
                Location = new Point(50, 90),
                Size = new Size(200, 20)
            };

            Label lblPassword = new Label
            {
                Text = "Пароль:",
                Location = new Point(50, 120),
                AutoSize = true
            };

            TextBox txtPassword = new TextBox
            {
                Location = new Point(50, 140),
                Size = new Size(200, 20),
                PasswordChar = '*'
            };

            Label lblRole = new Label
            {
                Text = "Роль:",
                Location = new Point(50, 170),
                AutoSize = true
            };

            ComboBox cmbRole = new ComboBox
            {
                Location = new Point(50, 190),
                Size = new Size(200, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbRole.Items.AddRange(new object[] { "Користувач", "Адміністратор" });
            cmbRole.SelectedIndex = 0;

            Button btnRegister = new Button
            {
                Text = "Зареєструватися",
                Location = new Point(50, 230),
                Size = new Size(150, 30)
            };
            btnRegister.Click += (s, e) => Register(txtUsername.Text, txtPassword.Text, cmbRole.SelectedIndex == 1);

            Button btnBack = new Button
            {
                Text = "Назад",
                Location = new Point(220, 230),
                Size = new Size(80, 30)
            };
            btnBack.Click += (s, e) => BackToLogin();

            this.Controls.AddRange(new Control[] { lblTitle, lblUsername, txtUsername, lblPassword,
                txtPassword, lblRole, cmbRole, btnRegister, btnBack });
        }

        private void Register(string username, string password, bool isAdmin)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заповніть всі поля");
                return;
            }

            if (User.Users.Any(u => u.Username == username))
            {
                MessageBox.Show("Користувач з таким іменем вже існує");
                return;
            }

            User newUser = new User(username, password, isAdmin);
            User.Users.Add(newUser);
            MessageBox.Show("Реєстрація успішна!");
            BackToLogin();
        }

        private void BackToLogin()
        {
            this.Hide();
            new MainForm().Show();
        }
    }
}