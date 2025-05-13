using System;
using System.Drawing;
using System.Windows.Forms;

namespace TVProgramApp
{
    public class AdminMainForm : Form
    {
        public AdminMainForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "ТВ Розклад - Адміністратор";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblWelcome = new Label
            {
                Text = $"Адміністратор: {MainForm.CurrentUser.Username}",
                Font = new Font("Arial", 12),
                Location = new Point(20, 20),
                AutoSize = true
            };

            Button btnAddProgram = new Button
            {
                Text = "Додати програму",
                Location = new Point(50, 70),
                Size = new Size(150, 40)
            };
            btnAddProgram.Click += (s, e) => ShowAddProgramForm();

            Button btnViewSchedule = new Button
            {
                Text = "Переглянути розклад",
                Location = new Point(220, 70),
                Size = new Size(150, 40)
            };
            btnViewSchedule.Click += (s, e) => ShowScheduleForm();

            Button btnLogout = new Button
            {
                Text = "Вийти",
                Location = new Point(350, 20),
                Size = new Size(80, 30)
            };
            btnLogout.Click += (s, e) => Logout();

            this.Controls.AddRange(new Control[] { lblWelcome, btnAddProgram, btnViewSchedule, btnLogout });
        }

        private void ShowAddProgramForm()
        {
            this.Hide();
            new AddProgramForm().Show();
        }

        private void ShowScheduleForm()
        {
            this.Hide();
            new AdminScheduleForm().Show();
        }

        private void Logout()
        {
            MainForm.CurrentUser = null;
            this.Hide();
            new MainForm().Show();
        }
    }
}
