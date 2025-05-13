using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TVProgramApp
{
    public class UserScheduleForm : Form
    {
        private Panel programsPanel;

        public UserScheduleForm()
        {
            InitializeComponents();
            LoadPrograms();
        }

        private void InitializeComponents()
        {
            this.Text = "ТВ Розклад - Користувач";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblWelcome = new Label
            {
                Text = $"Користувач: {MainForm.CurrentUser.Username}",
                Font = new Font("Arial", 12),
                Location = new Point(20, 20),
                AutoSize = true
            };

            Label lblTitle = new Label
            {
                Text = "Розклад програм на тиждень",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(20, 60),
                AutoSize = true
            };

            Button btnSortByChannel = new Button
            {
                Text = "Сортувати за каналом",
                Location = new Point(20, 100),
                Size = new Size(150, 30)
            };
            btnSortByChannel.Click += (s, e) => SortPrograms("channel");

            Button btnSortByTime = new Button
            {
                Text = "Сортувати за часом",
                Location = new Point(190, 100),
                Size = new Size(150, 30)
            };
            btnSortByTime.Click += (s, e) => SortPrograms("time");

            Button btnLogout = new Button
            {
                Text = "Вийти",
                Location = new Point(680, 20),
                Size = new Size(80, 30)
            };
            btnLogout.Click += (s, e) => Logout();

            programsPanel = new Panel
            {
                Location = new Point(20, 140),
                Size = new Size(740, 400),
                AutoScroll = true
            };

            this.Controls.AddRange(new Control[] { lblWelcome, lblTitle, btnSortByChannel, btnSortByTime, btnLogout, programsPanel });
        }

        private void LoadPrograms()
        {
            programsPanel.Controls.Clear();

            int yPos = 10;
            foreach (var program in MainForm.Programs)
            {
                GroupBox gb = new GroupBox
                {
                    Text = program.Title,
                    Location = new Point(10, yPos),
                    Size = new Size(700, 150)
                };

                PictureBox pic = new PictureBox
                {
                    Location = new Point(10, 20),
                    Size = new Size(150, 100),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle
                };
                if (!string.IsNullOrEmpty(program.ImagePath) && File.Exists(program.ImagePath))
                {
                    pic.Image = Image.FromFile(program.ImagePath);
                }

                Label lblChannel = new Label
                {
                    Text = $"Канал: {program.Channel}",
                    Location = new Point(170, 30),
                    AutoSize = true
                };

                Label lblTime = new Label
                {
                    Text = $"Час: {program.Time}",
                    Location = new Point(170, 60),
                    AutoSize = true
                };

                Label lblDay = new Label
                {
                    Text = $"День: {program.DayOfWeek}",
                    Location = new Point(170, 90),
                    AutoSize = true
                };

                gb.Controls.AddRange(new Control[] { pic, lblChannel, lblTime, lblDay });
                programsPanel.Controls.Add(gb);
                yPos += 160;
            }
        }

        private void SortPrograms(string sortBy)
        {
            MainForm.Programs = sortBy == "channel" ?
                MainForm.Programs.OrderBy(p => p.Channel).ToList() :
                MainForm.Programs.OrderBy(p => p.Time).ToList();

            LoadPrograms();
        }

        private void Logout()
        {
            MainForm.CurrentUser = null;
            this.Hide();
            new MainForm().Show();
        }
    }
}