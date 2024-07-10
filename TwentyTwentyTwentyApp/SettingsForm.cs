using System;
using System.Drawing;
using System.Windows.Forms;

namespace TwentyTwentyTwentyApp
{
    public partial class SettingsForm : Form
    {
        private int breakInterval;
        private int breakDuration;
        private bool enableSound;
        private bool enableNotifications;
        private bool nightMode;
        private string breakBackgroundImage;
        private string breakSoundFile;
        public event EventHandler<SettingsEventArgs> SettingsSaved;

        public SettingsForm(int currentInterval, int currentDuration, bool currentEnableSound, bool currentEnableNotifications, bool currentNightMode, string currentBreakBackgroundImage, string currentBreakSoundFile)
        {
            breakInterval = currentInterval;
            breakDuration = currentDuration;
            enableSound = currentEnableSound;
            enableNotifications = currentEnableNotifications;
            nightMode = currentNightMode;
            breakBackgroundImage = currentBreakBackgroundImage;
            breakSoundFile = currentBreakSoundFile;
            InitializeComponent();
            ApplyTheme();
        }

        private void InitializeComponent()
        {
            // إعدادات النموذج
            this.Text = "الإعدادات";
            this.Size = new Size(400, 400);

            Label lblBreakInterval = new Label
            {
                Text = "فترة الراحة (دقائق):",
                Location = new Point(10, 10),
                Size = new Size(120, 20)
            };

            NumericUpDown numBreakInterval = new NumericUpDown
            {
                Value = breakInterval,
                Location = new Point(150, 10),
                Size = new Size(100, 20)
            };

            Label lblBreakDuration = new Label
            {
                Text = "مدة الراحة (ثوانٍ):",
                Location = new Point(10, 40),
                Size = new Size(120, 20)
            };

            NumericUpDown numBreakDuration = new NumericUpDown
            {
                Value = breakDuration,
                Location = new Point(150, 40),
                Size = new Size(100, 20)
            };

            CheckBox chkEnableSound = new CheckBox
            {
                Text = "تفعيل الصوت",
                Checked = enableSound,
                Location = new Point(10, 70),
                Size = new Size(150, 20)
            };

            CheckBox chkEnableNotifications = new CheckBox
            {
                Text = "تفعيل الإشعارات",
                Checked = enableNotifications,
                Location = new Point(10, 100),
                Size = new Size(150, 20)
            };

            CheckBox chkNightMode = new CheckBox
            {
                Text = "تفعيل الوضع الليلي",
                Checked = nightMode,
                Location = new Point(10, 130),
                Size = new Size(150, 20)
            };

            Label lblBreakBackgroundImage = new Label
            {
                Text = "صورة خلفية فترة الراحة:",
                Location = new Point(10, 160),
                Size = new Size(150, 20)
            };

            TextBox txtBreakBackgroundImage = new TextBox
            {
                Text = breakBackgroundImage,
                Location = new Point(150, 160),
                Size = new Size(150, 20)
            };

            Button btnBrowseImage = new Button
            {
                Text = "استعراض...",
                Location = new Point(310, 160),
                Size = new Size(75, 23)
            };
            btnBrowseImage.Click += (sender, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                    Title = "اختر صورة خلفية"
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtBreakBackgroundImage.Text = openFileDialog.FileName;
                }
            };

            Label lblBreakSoundFile = new Label
            {
                Text = "ملف صوت فترة الراحة:",
                Location = new Point(10, 190),
                Size = new Size(150, 20)
            };

            TextBox txtBreakSoundFile = new TextBox
            {
                Text = breakSoundFile,
                Location = new Point(150, 190),
                Size = new Size(150, 20)
            };

            Button btnBrowseSound = new Button
            {
                Text = "استعراض...",
                Location = new Point(310, 190),
                Size = new Size(75, 23)
            };
            btnBrowseSound.Click += (sender, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Sound Files|*.wav;*.mp3",
                    Title = "اختر ملف صوت"
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtBreakSoundFile.Text = openFileDialog.FileName;
                }
            };

            Button btnSave = new Button
            {
                Text = "حفظ",
                Location = new Point(10, 230),
                Size = new Size(100, 30)
            };
            btnSave.Click += (sender, e) =>
            {
                SettingsSaved?.Invoke(this, new SettingsEventArgs
                {
                    BreakInterval = (int)numBreakInterval.Value,
                    BreakDuration = (int)numBreakDuration.Value,
                    EnableSound = chkEnableSound.Checked,
                    EnableNotifications = chkEnableNotifications.Checked,
                    NightMode = chkNightMode.Checked,
                    BreakBackgroundImage = txtBreakBackgroundImage.Text,
                    BreakSoundFile = txtBreakSoundFile.Text
                });
                this.Close();
            };

            // إضافة المكونات إلى النموذج
            this.Controls.Add(lblBreakInterval);
            this.Controls.Add(numBreakInterval);
            this.Controls.Add(lblBreakDuration);
            this.Controls.Add(numBreakDuration);
            this.Controls.Add(chkEnableSound);
            this.Controls.Add(chkEnableNotifications);
            this.Controls.Add(chkNightMode);
            this.Controls.Add(lblBreakBackgroundImage);
            this.Controls.Add(txtBreakBackgroundImage);
            this.Controls.Add(btnBrowseImage);
            this.Controls.Add(lblBreakSoundFile);
            this.Controls.Add(txtBreakSoundFile);
            this.Controls.Add(btnBrowseSound);
            this.Controls.Add(btnSave);
        }

        private void ApplyTheme()
        {
            if (nightMode)
            {
                this.BackColor = ThemeColors.NightBackgroundColor;
                foreach (Control control in this.Controls)
                {
                    control.ForeColor = ThemeColors.NightForegroundColor;
                    if (control is Button btn)
                    {
                        btn.BackColor = ThemeColors.NightButtonColor;
                        btn.ForeColor = ThemeColors.NightButtonTextColor;
                    }
                }
            }
            else
            {
                this.BackColor = Color.White;
                foreach (Control control in this.Controls)
                {
                    control.ForeColor = Color.Black;
                    if (control is Button btn)
                    {
                        btn.BackColor = Color.LightGray;
                        btn.ForeColor = Color.Black;
                    }
                }
            }
        }
    }
}
