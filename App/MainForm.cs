using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using tarkov_settings.Setting;
using tarkov_settings.GPU;

namespace tarkov_settings
{
    public partial class MainForm : Form
    {
        private ProcessMonitor pMonitor = ProcessMonitor.Instance;
        private IGPU gpu = GPUDevice.Instance;
        private AppSetting appSetting;

        private bool minimizeOnStart = false;
        private bool forceMinimized = false;
        private bool isExiting = false;
        private bool shutdownFinalized = false;
        private bool isLoadingProfile = false;
        private bool hotkeysRegistered = false;

        private const int HOTKEY_TOGGLE_ENABLE = 1001;
        private const int HOTKEY_RESET_COLORS = 1002;
        private const int WM_HOTKEY = 0x0312;
        private const uint MOD_ALT = 0x0001;
        private const uint MOD_CONTROL = 0x0002;
        private const string StartupRegistryValueName = "Tarkov Settings";
        private const string StartupRegistryPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public MainForm(string startupProfile = null, bool forceMinimized = false)
        {
            InitializeComponent();
            this.forceMinimized = forceMinimized;

            #region Load App Settings
            // Load Settings
            appSetting = AppSetting.Load();
            if (!string.IsNullOrWhiteSpace(startupProfile))
                appSetting.SetActiveProfile(startupProfile);

            LoadProfiles();
            LoadActiveProfileIntoControls();

            minimizeOnStart = appSetting.minimizeOnStart || forceMinimized;
            this.minimizeStartCheckBox.Checked = minimizeOnStart;
            this.startWithWindowsCheckBox.Checked = appSetting.startWithWindows;
            this.enableHotkeysCheckBox.Checked = appSetting.enableHotkeys;
            #endregion
            
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = String.Format("Tarkov Settings {0}", version);
            _ = new UpdateNotifier(version);

            // Saturation Initialize
            if (!gpu.SupportsSaturation)
            {
                DVLGroupBox.Enabled = false;
                DVLGroupBox.Text = "DVL (Unsupported)";
                dvlToolTip.SetToolTip(DVLLabel, $"{gpu.Vendor} GPU does not support Digital Vibrance control in this app.");
            }

            #region Initialize Display
            // Initialize Display Dropdown
            foreach (string display in Display.displays)
            {
                DisplayCombo.Items.Add(display);
            }
            
            if(DisplayCombo.FindString(appSetting.display) != -1)
                DisplayCombo.SelectedIndex = DisplayCombo.FindString(appSetting.display);
            else if (DisplayCombo.Items.Count > 0)
                DisplayCombo.SelectedIndex = 0;

            if (DisplayCombo.SelectedItem != null)
                Display.Primary = (string)DisplayCombo.SelectedItem;
            #endregion

            // Initialize Process Monitor
            pMonitor.Parent = this;
            foreach (string pTarget in appSetting.pTargets)
            {
                pMonitor.Add(pTarget.ToLower());
            }
            pMonitor.Init();
            ShowColorMode(this, EventArgs.Empty);
            UpdateRuntimeStatus("Desktop");
        }

        #region BCGS Getter/Setter
        public double Brightness
        {
            get => BrightnessBar.Value / 100.0;
            set => BrightnessBar.Value = ClampToTrackBar(BrightnessBar, (int)(value * 100));
        }

        public double Contrast
        {
            get => ContrastBar.Value / 100.0;
            set => ContrastBar.Value = ClampToTrackBar(ContrastBar, (int)(value * 100));
        }

        public double Gamma
        {
            get => GammaBar.Value / 100.0;
            set => GammaBar.Value = ClampToTrackBar(GammaBar, (int)(value * 100));
        }

        public int DVL
        {
            get => DVLBar.Value;
            set => DVLBar.Value = ClampToTrackBar(DVLBar, value);
        }

        public (double, double, double, int) GetColorValue()
        {
            return (
                BrightnessBar.Value / 100.0,
                ContrastBar.Value / 100.0,
                GammaBar.Value / 100.0,
                DVLBar.Value
                );
        }
        #endregion

        public bool IsEnabled { get=> this.enableToolStripMenuItem.Checked;}

        private void MainForm_Load(object sender, EventArgs e)
        {
            RegisterAppHotkeys();

            if (minimizeOnStart)
            {
                this.Visible = false;
                this.ShowInTaskbar = false;
                this.trayIcon.ShowBalloonTip(
                    2500,
                    "Tarkov Settings Initailized!",
                    "Check out tray to modify your color setting",
                    ToolTipIcon.Info
                    );
            }
        }

        #region Control Event Handlers
        private void ColorLabel_DClick(object sender, EventArgs e)
        {
            var label = sender as Label;
            
            if (label.Equals(BrightnessLabel))
            {
                BrightnessBar.Value = 50;
            }
            else if (label.Equals(ContrastLabel))
            {
                ContrastBar.Value = 50;
            }
            else if (label.Equals(GammaLabel))
            {
                GammaBar.Value = 100;
            }
            else if (label.Equals(DVLLabel))
            {
                DVLBar.Value = 0;
            }
        }
        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            var trackBar = sender as TrackBar;

            if (trackBar.Equals(BrightnessBar))
            {
                BrightnessText.Text = (BrightnessBar.Value / 100.0).ToString("0.00");
            }
            else if (trackBar.Equals(ContrastBar))
            {
                ContrastText.Text = (ContrastBar.Value / 100.0).ToString("0.00");
            }
            else if (trackBar.Equals(GammaBar))
            {
                GammaText.Text = (GammaBar.Value / 100.0).ToString("0.00");
            }
            else if (trackBar.Equals(DVLBar))
            {
                DVLText.Text = DVLBar.Value.ToString();
            }
        }
        private void DisplayCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (DisplayCombo.SelectedItem == null)
                return;

            string selectedDisplay = (string)DisplayCombo.SelectedItem;
            Display.Primary = selectedDisplay;

            if(Display.Primary != selectedDisplay)
            {
                DisplayCombo.SelectedIndex = DisplayCombo.FindString(Display.Primary);
            }
        }

        private void ProfileCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (isLoadingProfile || ProfileCombo.SelectedItem == null)
                return;

            SaveActiveProfileValues();
            appSetting.SetActiveProfile(ProfileCombo.SelectedItem.ToString());
            LoadActiveProfileIntoControls();
            SaveSettings();
            UpdateRuntimeStatus("Profile Loaded", appSetting.activeProfile);
        }
        #endregion

        private void ShowForm(object sender, EventArgs e)
        {
            this.Visible = true;
            this.ShowInTaskbar = true;
        }

        private void ShowColorMode(object sender, EventArgs e)
        {
            colorGroupBox.Visible = true;
            DVLGroupBox.Visible = true;
            ProfileCombo.Visible = true;
            DisplayCombo.Visible = true;

            startWithWindowsCheckBox.Visible = false;
            enableHotkeysCheckBox.Visible = false;
            HotkeyHelpLabel.Visible = false;
            minimizeStartCheckBox.Visible = false;

            StatusLabel.Location = new System.Drawing.Point(3, 329);
            ColorButton.Checked = true;
            MiscsButton.Checked = false;
        }

        private void ShowMiscsMode(object sender, EventArgs e)
        {
            colorGroupBox.Visible = false;
            DVLGroupBox.Visible = false;
            ProfileCombo.Visible = false;
            DisplayCombo.Visible = false;

            startWithWindowsCheckBox.Visible = true;
            enableHotkeysCheckBox.Visible = true;
            HotkeyHelpLabel.Visible = true;
            minimizeStartCheckBox.Visible = true;

            StatusLabel.Location = new System.Drawing.Point(3, 17);
            ColorButton.Checked = false;
            MiscsButton.Checked = true;
        }

        private void ExitFormClicked(object sender, EventArgs e)
        {
            isExiting = true;
            SaveSettings();
            FinalizeShutdown();

            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !isExiting)
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                SaveSettings();
                FinalizeShutdown();
            }
        }

        private void CheckOnMinimizeToTray(object sender, EventArgs e)
        {
            this.minimizeOnStart = this.minimizeStartCheckBox.Checked;
        }

        private void CheckOnStartWithWindows(object sender, EventArgs e)
        {
            appSetting.startWithWindows = this.startWithWindowsCheckBox.Checked;
            ApplyStartupRegistration(appSetting.startWithWindows);
            SaveSettings();
        }

        private void CheckOnEnableHotkeys(object sender, EventArgs e)
        {
            appSetting.enableHotkeys = this.enableHotkeysCheckBox.Checked;
            if (appSetting.enableHotkeys)
                RegisterAppHotkeys();
            else
                UnregisterAppHotkeys();
            SaveSettings();
        }

        private void EnableToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsEnabled)
            {
                ResetColorsClicked(sender, e);
                return;
            }

            pMonitor.RefreshCurrentFocus();
        }

        private void ResetColorsClicked(object sender, EventArgs e)
        {
            UpdateRuntimeStatus("Resetting");
            pMonitor.ResetColors();
            UpdateRuntimeStatus(IsEnabled ? "Desktop" : "Disabled");
        }

        public void UpdateRuntimeStatus(string status, string activeProcess = null)
        {
            if (IsDisposed)
                return;

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => UpdateRuntimeStatus(status, activeProcess)));
                return;
            }

            var detail = string.IsNullOrWhiteSpace(activeProcess) ? string.Empty : $" ({activeProcess})";
            StatusLabel.Text = $"Status: {status}{detail}";

            var trayText = $"Tarkov Settings - {status}";
            trayIcon.Text = trayText.Length > 63 ? trayText.Substring(0, 63) : trayText;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                switch (m.WParam.ToInt32())
                {
                    case HOTKEY_TOGGLE_ENABLE:
                        enableToolStripMenuItem.Checked = !enableToolStripMenuItem.Checked;
                        break;
                    case HOTKEY_RESET_COLORS:
                        ResetColorsClicked(this, EventArgs.Empty);
                        break;
                }
            }

            base.WndProc(ref m);
        }

        private void LoadProfiles()
        {
            isLoadingProfile = true;
            ProfileCombo.Items.Clear();
            foreach (var profileName in appSetting.profiles.Keys)
            {
                ProfileCombo.Items.Add(profileName);
            }
            ProfileCombo.SelectedItem = appSetting.activeProfile;
            isLoadingProfile = false;
        }

        private void LoadActiveProfileIntoControls()
        {
            var profile = appSetting.GetActiveProfile();
            Brightness = profile.brightness;
            Contrast = profile.contrast;
            Gamma = profile.gamma;
            DVL = profile.saturation;
            appSetting.display = profile.display;
        }

        private void SaveActiveProfileValues()
        {
            appSetting.UpdateActiveProfile(
                Brightness,
                Contrast,
                Gamma,
                DVL,
                DisplayCombo.SelectedItem?.ToString() ?? appSetting.display);
        }

        private void SaveSettings()
        {
            SaveActiveProfileValues();
            appSetting.minimizeOnStart = minimizeOnStart;
            appSetting.startWithWindows = startWithWindowsCheckBox.Checked;
            appSetting.enableHotkeys = enableHotkeysCheckBox.Checked;
            appSetting.Save();
        }

        private void FinalizeShutdown()
        {
            if (shutdownFinalized)
                return;

            shutdownFinalized = true;
            Console.WriteLine("[mainForm] Closing pMonitor");
            UnregisterAppHotkeys();
            pMonitor.Close();

            if (this.trayIcon != null)
                this.trayIcon.Dispose();
        }

        private void RegisterAppHotkeys()
        {
            if (hotkeysRegistered || appSetting == null || !appSetting.enableHotkeys || !IsHandleCreated)
                return;

            hotkeysRegistered = true;
            RegisterHotKey(Handle, HOTKEY_TOGGLE_ENABLE, MOD_CONTROL | MOD_ALT, (uint)Keys.T);
            RegisterHotKey(Handle, HOTKEY_RESET_COLORS, MOD_CONTROL | MOD_ALT, (uint)Keys.R);
        }

        private void UnregisterAppHotkeys()
        {
            if (!hotkeysRegistered || !IsHandleCreated)
                return;

            UnregisterHotKey(Handle, HOTKEY_TOGGLE_ENABLE);
            UnregisterHotKey(Handle, HOTKEY_RESET_COLORS);
            hotkeysRegistered = false;
        }

        private void ApplyStartupRegistration(bool enabled)
        {
            try
            {
                using (var key = Registry.CurrentUser.CreateSubKey(StartupRegistryPath))
                {
                    if (enabled)
                        key.SetValue(StartupRegistryValueName, $"\"{Application.ExecutablePath}\" --minimized");
                    else
                        key.DeleteValue(StartupRegistryValueName, false);
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error("Failed to update startup registration", ex);
                MessageBox.Show("Could not update Windows startup registration.", "Startup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                startWithWindowsCheckBox.Checked = false;
            }
        }

        private static int ClampToTrackBar(TrackBar trackBar, int value)
        {
            return Math.Min(Math.Max(value, trackBar.Minimum), trackBar.Maximum);
        }
    }
}
