using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TVProgramApp
{
    public class EditProgramForm : Form
    {
        private readonly TVProgram program;
        private string newImagePath;

        public EditProgramForm(TVProgram programToEdit)
        {
            program = programToEdit;
            newImagePath = program.ImagePath;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Редагувати програму";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label
            {
                Text = "Редагування програми",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };

            Button btnSelectImage = new Button
            {
                Text = "Змінити заставку",
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
            if (!string.IsNullOrEmpty(program.ImagePath) && File.Exists(program.ImagePath))
            {
                pictureBox.Image = Image.FromFile(program.ImagePath);
            }

            Label lblProgramTitle = new Label
            {
                Text = "Назва програми:",
                Location = new Point(300, 60),
                AutoSize = true
            };

            TextBox txtTitle = new TextBox
            {
                Location = new Point(300, 80),
                Size = new Size(200, 20),
                Text = program.Title
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
                Size = new Size(200, 20),
                Text = program.Channel
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
                Size = new Size(200, 20),
                Text = program.Time
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
            cmbDay.SelectedItem = program.DayOfWeek;

            Button btnSave = new Button
            {
                Text = "Зберегти",
                Location = new Point(300, 270),
                Size = new Size(100, 30)
            };
            btnSave.Click += (s, e) => SaveChanges(txtTitle.Text, txtChannel.Text, txtTime.Text, cmbDay.SelectedItem.ToString());

            Button btnCancel = new Button
            {
                Text = "Скасувати",
                Location = new Point(420, 270),
                Size = new Size(100, 30)
            };
            btnCancel.Click += (s, e) => Cancel();

            this.Controls.AddRange(new Control[] { lblTitle, btnSelectImage, pictureBox, lblProgramTitle,
                txtTitle, lblChannel, txtChannel, lblTime, txtTime, lblDay, cmbDay, btnSave, btnCancel });
        }

        private void SelectImage(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                newImagePath = openFileDialog.FileName;
                ((PictureBox)this.Controls[2]).Image = Image.FromFile(newImagePath);
            }
        }

        private void SaveChanges(string title, string channel, string time, string day)
        {
            program.Title = title;
            program.Channel = channel;
            program.Time = time;
            program.DayOfWeek = day;
            program.ImagePath = newImagePath;

            MessageBox.Show("Зміни збережено!");
            Cancel();
        }

        private void Cancel()
        {
            this.Hide();
            new AdminScheduleForm().Show();
        }
    }
}
