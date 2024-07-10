using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Newtonsoft.Json;
using NAudio.Wave;

namespace TwentyTwentyTwentyApp
{
    public partial class Form1 : Form
    {
        private Timer breakTimer;
        private int breakInterval = 20; // فترة الراحة كل 20 دقيقة
        private int breakDuration = 20; // مدة الراحة كل 20 ثانية
        private bool enableSound = true;
        private bool enableNotifications = true;
        private bool nightMode = false;
        private string breakBackgroundImage = null;
        private string breakSoundFile = null;
        private int breaksTaken = 0;
        private bool autoStart = false;
        private Form fullScreenForm;
        private WaveOutEvent waveOut;
        private AudioFileReader audioFileReader;
        private const string SettingsFilePath = "settings.json";
        private const string AutoStartRegistryKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        public Form1()
        {
            InitializeComponent();
            LoadSettings();
            InitializeBreakTimer();
            ApplyTheme();
            SetAutoStart(autoStart);
        }

        private void InitializeBreakTimer()
        {
            breakTimer = new Timer
            {
                Interval = breakInterval * 60 * 1000 // تحويل الدقائق إلى ميلي ثانية
            };
            breakTimer.Tick += BreakTimer_Tick;
        }

        private void BreakTimer_Tick(object sender, EventArgs e)
        {
            StartBreak();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            breakTimer.Start();
            MessageBox.Show("تم بدء التذكيرات بفترات الراحة.", "بدء التذكيرات", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            // فتح نافذة الإعدادات
            SettingsForm settingsForm = new SettingsForm(breakInterval, breakDuration, enableSound, enableNotifications, nightMode, breakBackgroundImage, breakSoundFile, autoStart)
            {
                Owner = this
            };
            settingsForm.SettingsSaved += OnSettingsSaved;
            settingsForm.ShowDialog();
        }

        private void OnSettingsSaved(object sender, SettingsEventArgs e)
        {
            UpdateSettings(e.BreakInterval, e.BreakDuration, e.EnableSound, e.EnableNotifications, e.NightMode, e.BreakBackgroundImage, e.BreakSoundFile, e.AutoStart);
            SaveSettings();
        }

        private async void StartBreak()
        {
            // إخفاء النموذج الحالي وعرض نافذة ملء الشاشة
            this.Hide();
            fullScreenForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                WindowState = FormWindowState.Maximized,
                BackColor = nightMode ? ThemeColors.NightBackgroundColor : Color.White
            };

            if (!string.IsNullOrEmpty(breakBackgroundImage) && File.Exists(breakBackgroundImage))
            {
                fullScreenForm.BackgroundImage = Image.FromFile(breakBackgroundImage);
                fullScreenForm.BackgroundImageLayout = ImageLayout.Stretch;
            }

            Label lblBreak = new Label
            {
                Text = "حان وقت الراحة! انظر بعيدًا لمدة 20 ثانية.",
                ForeColor = nightMode ? ThemeColors.NightLabelColor : Color.Black,
                Font = new Font("Arial", 24, FontStyle.Bold),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            fullScreenForm.Controls.Add(lblBreak);
            fullScreenForm.Show();

            if (!string.IsNullOrEmpty(breakSoundFile) && File.Exists(breakSoundFile))
            {
                PlaySound(breakSoundFile);
            }

            await Task.Delay(breakDuration * 1000); // الانتظار لمدة مدة الراحة

            StopSound();

            fullScreenForm.Close();
            this.Show();

            if (enableNotifications)
            {
                MessageBox.Show("تم الانتهاء من فترة الراحة.", "نهاية الراحة", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            breaksTaken++;
            UpdateBreaksTaken();
        }

        private void PlaySound(string fileName)
        {
            try
            {
                waveOut = new WaveOutEvent();
                audioFileReader = new AudioFileReader(fileName);
                waveOut.Init(audioFileReader);
                waveOut.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing sound: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StopSound()
        {
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }

            if (audioFileReader != null)
            {
                audioFileReader.Dispose();
                audioFileReader = null;
            }
        }

        private void UpdateBreaksTaken()
        {
            lblBreaksTaken.Text = $"عدد فترات الراحة التي تم أخذها: {breaksTaken}";
        }

        public void UpdateSettings(int newInterval, int newDuration, bool newEnableSound, bool newEnableNotifications, bool newNightMode, string newBreakBackgroundImage, string newBreakSoundFile, bool newAutoStart)
        {
            breakInterval = newInterval;
            breakDuration = newDuration;
            enableSound = newEnableSound;
            enableNotifications = newEnableNotifications;
            nightMode = newNightMode;
            breakBackgroundImage = newBreakBackgroundImage;
            breakSoundFile = newBreakSoundFile;
            autoStart = newAutoStart;
            breakTimer.Interval = breakInterval * 60 * 1000;
            ApplyTheme();
            SetAutoStart(autoStart);
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

        private void LoadSettings()
        {
            if (File.Exists(SettingsFilePath))
            {
                var settingsJson = File.ReadAllText(SettingsFilePath);
                var settings = JsonConvert.DeserializeObject<Settings>(settingsJson);
                breakInterval = settings.BreakInterval;
                breakDuration = settings.BreakDuration;
                enableSound = settings.EnableSound;
                enableNotifications = settings.EnableNotifications;
                nightMode = settings.NightMode;
                breakBackgroundImage = settings.BreakBackgroundImage;
                breakSoundFile = settings.BreakSoundFile;
                breaksTaken = settings.BreaksTaken;
                autoStart = settings.AutoStart;
                UpdateBreaksTaken();
            }
        }

        private void SaveSettings()
        {
            var settings = new Settings
            {
                BreakInterval = breakInterval,
                BreakDuration = breakDuration,
                EnableSound = enableSound,
                EnableNotifications = enableNotifications,
                NightMode = nightMode,
                BreakBackgroundImage = breakBackgroundImage,
                BreakSoundFile = breakSoundFile,
                BreaksTaken = breaksTaken,
                AutoStart = autoStart
            };
            var settingsJson = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(SettingsFilePath, settingsJson);
        }

        private void SetAutoStart(bool autoStart)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(AutoStartRegistryKey, true))
            {
                if (autoStart)
                {
                    key.SetValue("TwentyTwentyTwentyApp", Application.ExecutablePath);
                }
                else
                {
                    key.DeleteValue("TwentyTwentyTwentyApp", false);
                }
            }
        }
    }

    public class Settings
    {
        public int BreakInterval { get; set; }
        public int BreakDuration { get; set; }
        public bool EnableSound { get; set; }
        public bool EnableNotifications { get; set; }
        public bool NightMode { get; set; }
        public string BreakBackgroundImage { get; set; }
        public string BreakSoundFile { get; set; }
        public int BreaksTaken { get; set; }
        public bool AutoStart { get; set; }
    }

    public class SettingsEventArgs : EventArgs
    {
        public int BreakInterval { get; set; }
        public int BreakDuration { get; set; }
        public bool EnableSound { get; set; }
        public bool EnableNotifications { get; set; }
        public bool NightMode { get; set; }
        public string BreakBackgroundImage { get; set; }
        public string BreakSoundFile { get; set; }
        public bool AutoStart { get; set; }
    }
}
