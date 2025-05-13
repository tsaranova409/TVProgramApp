using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TVProgramApp
{
    public class AdminScheduleForm : Form
    {
        private Panel programsPanel;

        public AdminScheduleForm()
        {
            InitializeComponents();
            LoadPrograms();
        }

        private void InitializeComponents()
        {
            this.Text = "Розклад програм - Адміністратор";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label
            {
                Text = "Розклад програм на тиждень",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };

            Button btnSortByChannel = new Button
            {
                Text = "Сортувати за каналом",
                Location = new Point(20, 60),
                Size = new Size(150, 30)
            };
            btnSortByChannel.Click += (s, e) => SortPrograms("channel");

            Button btnSortByTime = new Button
            {
                Text = "Сортувати за часом",
                Location = new Point(190, 60),
                Size = new Size(150, 30)
            };
            btnSortByTime.Click += (s, e) => SortPrograms("time");

            Button btnBack = new Button
            {
                Text = "Назад",
                Location = new Point(680, 20),
                Size = new Size(80, 30)
            };
            btnBack.Click += (s, e) => Back();

            programsPanel = new Panel
            {
                Location = new Point(20, 100),
                Size = new Size(740, 440),
                AutoScroll = true
            };

            this.Controls.AddRange(new Control[] { lblTitle, btnSortByChannel, btnSortByTime, btnBack, programsPanel });
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
                    Size = new Size(700, 180)
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

                Button btnEdit = new Button
                {
                    Text = "Редагувати",
                    Location = new Point(500, 30),
                    Size = new Size(80, 30),
                    Tag = program
                };
                btnEdit.Click += (s, e) => EditProgram((TVProgram)btnEdit.Tag);

                Button btnDelete = new Button
                {
                    Text = "Видалити",
                    Location = new Point(500, 70),
                    Size = new Size(80, 30),
                    Tag = program
                };
                btnDelete.Click += (s, e) => DeleteProgram((TVProgram)btnDelete.Tag);

                gb.Controls.AddRange(new Control[] { pic, lblChannel, lblTime, lblDay, btnEdit, btnDelete });
                programsPanel.Controls.Add(gb);
                yPos += 190;
            }
        }

        private void SortPrograms(string sortBy)
        {
            MainForm.Programs = sortBy == "channel" ?
                MainForm.Programs.OrderBy(p => p.Channel).ToList() :
                MainForm.Programs.OrderBy(p => p.Time).ToList();

            LoadPrograms();
        }

        private void EditProgram(TVProgram program)
        {
            this.Hide();
            new EditProgramForm(program).Show();
        }

        private void DeleteProgram(TVProgram program)
        {
            if (MessageBox.Show("Видалити програму?", "Підтвердження", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MainForm.Programs.Remove(program);
                LoadPrograms();
            }
        }

        private void Back()
        {
            this.Hide();
            new AdminMainForm().Show();
        }
    }
}