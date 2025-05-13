using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TVProgramApp
{
    public class MainForm : Form
    {
        public static List<TVProgram> Programs = new List<TVProgram>();
        public static User CurrentUser;
        private Panel mainPanel;

        public MainForm()
        {
            InitializeComponents();
            LoadSampleData();
        }

        private void InitializeComponents()
        {
            this.Text = "ТВ Розклад";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            mainPanel = new Panel { Dock = DockStyle.Fill };
            this.Controls.Add(mainPanel);

            Label lblTitle = new Label
            {
                Text = "ТВ Розклад",
                Font = new Font("Arial", 20, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                Parent = mainPanel
            };

            Label lblUsername = new Label
            {
                Text = "Ім'я користувача:",
                Location = new Point(50, 80),
                AutoSize = true,
                Parent = mainPanel
            };

            TextBox txtUsername = new TextBox
            {
                Location = new Point(50, 100),
                Size = new Size(200, 20),
                Parent = mainPanel
            };

            Label lblPassword = new Label
            {
                Text = "Пароль:",
                Location = new Point(50, 130),
                AutoSize = true,
                Parent = mainPanel
            };

            TextBox txtPassword = new TextBox
            {
                Location = new Point(50, 150),
                Size = new Size(200, 20),
                PasswordChar = '*',
                Parent = mainPanel
            };

            Button btnLogin = new Button
            {
                Text = "Увійти",
                Location = new Point(50, 190),
                Size = new Size(100, 30),
                Parent = mainPanel
            };
            btnLogin.Click += (s, e) => Login(txtUsername.Text, txtPassword.Text);

            LinkLabel linkRegister = new LinkLabel
            {
                Text = "Не маєте акаунту? Зареєструйтесь",
                Location = new Point(50, 230),
                AutoSize = true,
                Parent = mainPanel
            };
            linkRegister.Click += (s, e) => ShowRegisterForm();
        }

        private void LoadSampleData()
        {
            User.Users.Add(new User("admin", "admin123", true));
            User.Users.Add(new User("user", "user123", false));

            Programs.Add(new TVProgram
            {
                Title = "Новини",
                Channel = "1+1",
                Time = "19:00",
                DayOfWeek = "Понеділок"
            });
        }

        private void Login(string username, string password)
        {
            var user = User.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                MessageBox.Show("Невірний логін або пароль");
                return;
            }

            CurrentUser = user;
            this.Hide();

            if (user.IsAdmin)
                new AdminMainForm().Show();
            else
                new UserScheduleForm().Show();
        }

        private void ShowRegisterForm()
        {
            this.Hide();
            new RegisterForm().Show();
        }
    }
}