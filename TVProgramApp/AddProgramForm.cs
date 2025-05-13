using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TVProgramApp
{
    public class AddProgramForm : Form
    {
        private string imagePath = "";

        public AddProgramForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Додати програму";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label
            {
                Text = "Додати нову програму",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };

            Button btnSelectImage = new Button
            {
                Text = "Вибрати заставку",
                Location = new Point(50, 60),
                Size = new Size(150, 30)
            };
            btnSelectImage.Click += SelectImage;

            PictureBox pictureBox = new PictureBox
            {
                Location = new Point(50, 100),
                Size = new Size(200, 150),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            Label lblProgramTitle = new Label
            {
                Text = "Назва програми:",
                Location = new Point(300, 60),
                AutoSize = true
            };

            TextBox txtTitle = new TextBox
            {
                Location = new Point(300, 80),
                Size = new Size(200, 20)
            };

            Label lblChannel = new Label
            {
                Text = "Канал:",
                Location = new Point(300, 110),
                AutoSize = true
            };

            TextBox txtChannel = new TextBox
            {
                Location = new Point(300, 130),
                Size = new Size(200, 20)
            };

            Label lblTime = new Label
            {
                Text = "Час показу:",
                Location = new Point(300, 160),
                AutoSize = true
            };

            TextBox txtTime = new TextBox
            {
                Location = new Point(300, 180),
                Size = new Size(200, 20)
            };

            Label lblDay = new Label
            {
                Text = "День тижня:",
                Location = new Point(300, 210),
                AutoSize = true
            };

            ComboBox cmbDay = new ComboBox
            {
                Location = new Point(300, 230),
                Size = new Size(200, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbDay.Items.AddRange(new object[] { "Понеділок", "Вівторок", "Середа", "Четвер", "П'ятниця", "Субота", "Неділя" });
            cmbDay.SelectedIndex = 0;

            Button btnAdd = new Button
            {
                Text = "Додати",
                Location = new Point(300, 270),
                Size = new Size(100, 30)
            };
            btnAdd.Click += (s, e) => AddProgram(txtTitle.Text, txtChannel.Text, txtTime.Text, cmbDay.SelectedItem.ToString());

            Button btnCancel = new Button
            {
                Text = "Скасувати",
                Location = new Point(420, 270),
                Size = new Size(100, 30)
            };
            btnCancel.Click += (s, e) => Cancel();

            this.Controls.AddRange(new Control[] { lblTitle, btnSelectImage, pictureBox, lblProgramTitle,
                txtTitle, lblChannel, txtChannel, lblTime, txtTime, lblDay, cmbDay, btnAdd, btnCancel });
        }

        private void SelectImage(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imagePath = openFileDialog.FileName;
                ((PictureBox)this.Controls[2]).Image = Image.FromFile(imagePath);
            }
        }

        private void AddProgram(string title, string channel, string time, string day)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(channel) || string.IsNullOrWhiteSpace(time))
            {
                MessageBox.Show("Заповніть обов'язкові поля");
                return;
            }

            MainForm.Programs.Add(new TVProgram
            {
                Title = title,
                Channel = channel,
                Time = time,
                DayOfWeek = day,
                ImagePath = imagePath
            });

            MessageBox.Show("Програму додано!");
            Cancel();
        }

        private void Cancel()
        {
            this.Hide();
            new AdminMainForm().Show();
        }
    }
}