using System;
using System.Collections.Generic;
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
        private bool isLoadingLanguage = false;
        private bool hotkeysRegistered = false;

        private const int HOTKEY_TOGGLE_ENABLE = 1001;
        private const int HOTKEY_RESET_COLORS = 1002;
        private const int HOTKEY_PRESET_1 = 1011;
        private const int HOTKEY_PRESET_2 = 1012;
        private const int HOTKEY_PRESET_3 = 1013;
        private const int HOTKEY_PRESET_4 = 1014;
        private const int WM_HOTKEY = 0x0312;
        private const uint MOD_ALT = 0x0001;
        private const uint MOD_CONTROL = 0x0002;
        private const string StartupRegistryValueName = "Tarkov Settings";
        private const string StartupRegistryPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private class LanguageOption
        {
            public string Code { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        public MainForm(string startupProfile = null, bool forceMinimized = false)
        {
            InitializeComponent();
            this.forceMinimized = forceMinimized;

            #region Load App Settings
            // Load Settings
            appSetting = AppSetting.Load();
            if (!string.IsNullOrWhiteSpace(startupProfile))
                appSetting.SetActiveProfile(startupProfile);

            LoadLanguageOptions();
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
            ApplyLocalization();
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
                    T("trayInitTitle"),
                    T("trayInitMessage"),
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

            SwitchPreset(ProfileCombo.SelectedItem.ToString(), false);
        }

        private void LanguageCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (isLoadingLanguage || appSetting == null)
                return;

            appSetting.language = GetSelectedLanguageCode();
            ApplyLocalization();
            SaveSettings();
            UpdateRuntimeStatus("Language Changed");
        }
        #endregion

        private void SavePresetClicked(object sender, EventArgs e)
        {
            SaveSettings();
            pMonitor.RefreshCurrentFocus();
            UpdateRuntimeStatus("Preset Saved", appSetting.activeProfile);
        }

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
            PresetLabel.Visible = true;
            SavePresetButton.Visible = true;
            DisplayCombo.Visible = true;
            DisplayLabel.Visible = true;
            LanguageCombo.Visible = false;
            LanguageLabel.Visible = false;

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
            PresetLabel.Visible = false;
            SavePresetButton.Visible = false;
            DisplayCombo.Visible = false;
            DisplayLabel.Visible = false;
            LanguageCombo.Visible = true;
            LanguageLabel.Visible = true;

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
            var localizedStatus = TStatus(status);
            StatusLabel.Text = $"{T("statusPrefix")}: {localizedStatus}{detail}";

            var trayText = $"Tarkov Settings - {localizedStatus}";
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
                    case HOTKEY_PRESET_1:
                        SwitchPreset(AppSetting.PresetNames[0], true);
                        break;
                    case HOTKEY_PRESET_2:
                        SwitchPreset(AppSetting.PresetNames[1], true);
                        break;
                    case HOTKEY_PRESET_3:
                        SwitchPreset(AppSetting.PresetNames[2], true);
                        break;
                    case HOTKEY_PRESET_4:
                        SwitchPreset(AppSetting.PresetNames[3], true);
                        break;
                }
            }

            base.WndProc(ref m);
        }

        private void LoadProfiles()
        {
            isLoadingProfile = true;
            ProfileCombo.Items.Clear();
            foreach (var profileName in AppSetting.PresetNames)
            {
                ProfileCombo.Items.Add(profileName);
            }
            ProfileCombo.SelectedItem = appSetting.activeProfile;
            isLoadingProfile = false;
        }

        private void LoadLanguageOptions()
        {
            isLoadingLanguage = true;
            LanguageCombo.Items.Clear();
            LanguageCombo.Items.Add(new LanguageOption { Code = "en", Name = "English" });
            LanguageCombo.Items.Add(new LanguageOption { Code = "ko", Name = "한국어" });
            LanguageCombo.Items.Add(new LanguageOption { Code = "ja", Name = "日本語" });
            LanguageCombo.Items.Add(new LanguageOption { Code = "zh", Name = "中文" });

            foreach (LanguageOption option in LanguageCombo.Items)
            {
                if (option.Code == appSetting.language)
                {
                    LanguageCombo.SelectedItem = option;
                    break;
                }
            }

            if (LanguageCombo.SelectedItem == null && LanguageCombo.Items.Count > 0)
                LanguageCombo.SelectedIndex = 0;

            isLoadingLanguage = false;
        }

        private void LoadActiveProfileIntoControls()
        {
            var profile = appSetting.GetActiveProfile();
            Brightness = profile.brightness;
            Contrast = profile.contrast;
            Gamma = profile.gamma;
            DVL = profile.saturation;
            appSetting.display = profile.display;

            if (DisplayCombo.Items.Count > 0 && DisplayCombo.FindStringExact(profile.display) != -1)
            {
                DisplayCombo.SelectedItem = profile.display;
                Display.Primary = profile.display;
            }
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
            appSetting.language = GetSelectedLanguageCode();
            appSetting.Save();
        }

        private string GetSelectedLanguageCode()
        {
            var selectedLanguage = LanguageCombo.SelectedItem as LanguageOption;
            return selectedLanguage == null ? "en" : selectedLanguage.Code;
        }

        private void SwitchPreset(string presetName, bool fromHotkey)
        {
            if (string.IsNullOrWhiteSpace(presetName))
                return;

            SaveActiveProfileValues();
            appSetting.SetActiveProfile(presetName);
            LoadActiveProfileIntoControls();

            isLoadingProfile = true;
            ProfileCombo.SelectedItem = appSetting.activeProfile;
            isLoadingProfile = false;

            SaveSettings();
            pMonitor.RefreshCurrentFocus();
            UpdateRuntimeStatus(fromHotkey ? "Preset Hotkey" : "Preset Loaded", appSetting.activeProfile);
        }

        private void ApplyLocalization()
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = string.Format("Tarkov Settings {0}", version);

            MiscsButton.Text = T("miscs");
            ColorButton.Text = T("color");
            colorGroupBox.Text = T("color");
            DVLGroupBox.Text = gpu.SupportsSaturation ? T("dvlGroup") : T("dvlUnsupportedGroup");

            BrightnessLabel.Text = T("brightness");
            ContrastLabel.Text = T("contrast");
            GammaLabel.Text = T("gamma");
            DVLLabel.Text = T("dvlLabel");
            PresetLabel.Text = T("preset");
            DisplayLabel.Text = T("display");
            SavePresetButton.Text = T("save");
            LanguageLabel.Text = T("language");

            startWithWindowsCheckBox.Text = T("startWithWindows");
            enableHotkeysCheckBox.Text = T("hotkeys");
            minimizeStartCheckBox.Text = T("minimizeToTray");
            HotkeyHelpLabel.Text = T("hotkeyHelp");

            enableToolStripMenuItem.Text = T("enable");
            resetColorsToolStripMenuItem.Text = T("resetColors");
            showToolStripMenuItem.Text = T("show");
            exitToolStripMenuItem.Text = T("exit");

            brightnessToolTip.ToolTipTitle = T("brightness");
            contrastToolTip.ToolTipTitle = T("contrast");
            gammaToolTip.ToolTipTitle = T("gamma");
            dvlToolTip.ToolTipTitle = T("saturation");

            brightnessToolTip.SetToolTip(BrightnessHelpLabel, T("brightnessHelp"));
            contrastToolTip.SetToolTip(ContrastHelpLabel, T("contrastHelp"));
            gammaToolTip.SetToolTip(GammaHelpLabel, T("gammaHelp"));
            dvlToolTip.SetToolTip(DVLHelpLabel, T("dvlHelp"));

            brightnessToolTip.SetToolTip(BrightnessLabel, T("doubleClickReset"));
            contrastToolTip.SetToolTip(ContrastLabel, T("doubleClickReset"));
            gammaToolTip.SetToolTip(GammaLabel, T("doubleClickReset"));
            dvlToolTip.SetToolTip(DVLLabel, gpu.SupportsSaturation ? T("doubleClickReset") : T("unsupportedSaturation"));
        }

        private string TStatus(string status)
        {
            switch (status)
            {
                case "Desktop":
                    return T("statusDesktop");
                case "Disabled":
                    return T("statusDisabled");
                case "Resetting":
                    return T("statusResetting");
                case "Preset Saved":
                    return T("statusPresetSaved");
                case "Preset Loaded":
                case "Profile Loaded":
                    return T("statusPresetLoaded");
                case "Preset Hotkey":
                    return T("statusPresetHotkey");
                case "Language Changed":
                    return T("statusLanguageChanged");
                case "Tarkov Active":
                    return T("statusTarkovActive");
                case "Loading":
                    return T("statusLoading");
                default:
                    return status;
            }
        }

        private string T(string key)
        {
            var language = appSetting == null ? "en" : appSetting.language;
            switch (language)
            {
                case "ko":
                    return Ko(key);
                case "ja":
                    return Ja(key);
                case "zh":
                    return Zh(key);
                default:
                    return En(key);
            }
        }

        private string En(string key)
        {
            switch (key)
            {
                case "miscs": return "Miscs";
                case "color": return "Color";
                case "dvlGroup": return "DVL";
                case "dvlUnsupportedGroup": return "DVL (Unsupported)";
                case "brightness": return "Brightness";
                case "contrast": return "Contrast";
                case "gamma": return "Gamma";
                case "dvlLabel": return "Digital Vibrance\r\n(Saturation)\r\n";
                case "saturation": return "Saturation";
                case "preset": return "Preset";
                case "display": return "Display";
                case "save": return "Save";
                case "language": return "Language";
                case "startWithWindows": return "Start with Windows";
                case "hotkeys": return "Hotkeys";
                case "minimizeToTray": return "Minimize to Tray on Start";
                case "hotkeyHelp": return "Ctrl+Alt+T: Toggle\r\nCtrl+Alt+R: Reset\r\nCtrl+Alt+1: Preset 1\r\nCtrl+Alt+2: Preset 2\r\nCtrl+Alt+3: Preset 3\r\nCtrl+Alt+4: Preset 4";
                case "enable": return "Enable";
                case "resetColors": return "Reset Colors";
                case "show": return "Show";
                case "exit": return "Exit";
                case "statusPrefix": return "Status";
                case "statusDesktop": return "Desktop";
                case "statusDisabled": return "Disabled";
                case "statusResetting": return "Resetting";
                case "statusPresetSaved": return "Preset Saved";
                case "statusPresetLoaded": return "Preset Loaded";
                case "statusPresetHotkey": return "Preset Hotkey";
                case "statusLanguageChanged": return "Language Changed";
                case "statusTarkovActive": return "Tarkov Active";
                case "statusLoading": return "Loading";
                case "doubleClickReset": return "Double-click to reset";
                case "unsupportedSaturation": return string.Format("{0} GPU does not support Digital Vibrance control in this app.", gpu.Vendor);
                case "brightnessHelp": return "Brightness shifts the whole gamma ramp brighter or darker.\r\nUse it when dark areas are too hard to see or the image is washed out.\r\nDefault is 0.50. Double-click the setting label to reset it.";
                case "contrastHelp": return "Contrast increases or decreases the gap between dark and bright colors.\r\nHigher values make edges pop but can crush details.\r\nDefault is 0.50. Double-click the setting label to reset it.";
                case "gammaHelp": return "Gamma changes mid-tone brightness without moving pure black and white as much.\r\nLower values darken mid-tones; higher values brighten them.\r\nDefault is 1.00. Double-click the setting label to reset it.";
                case "dvlHelp": return "Digital Vibrance / Saturation changes color intensity.\r\nHigher values make colors more vivid and easier to distinguish.\r\nThis setting uses NVIDIA NvAPI and is disabled on unsupported GPUs.\r\nDouble-click the setting label to reset it.";
                case "trayInitTitle": return "Tarkov Settings Initialized!";
                case "trayInitMessage": return "Check the tray to modify your color settings.";
                case "startupErrorTitle": return "Startup";
                case "startupErrorMessage": return "Could not update Windows startup registration.";
                default: return key;
            }
        }

        private string Ko(string key)
        {
            switch (key)
            {
                case "miscs": return "기타";
                case "color": return "색상";
                case "dvlGroup": return "DVL";
                case "dvlUnsupportedGroup": return "DVL (미지원)";
                case "brightness": return "밝기";
                case "contrast": return "대비";
                case "gamma": return "감마";
                case "dvlLabel": return "디지털 바이브런스\r\n(채도)\r\n";
                case "saturation": return "채도";
                case "preset": return "프리셋";
                case "display": return "디스플레이";
                case "save": return "저장";
                case "language": return "언어";
                case "startWithWindows": return "Windows 시작 시 실행";
                case "hotkeys": return "단축키";
                case "minimizeToTray": return "시작 시 트레이로 최소화";
                case "hotkeyHelp": return "Ctrl+Alt+T: 켜기/끄기\r\nCtrl+Alt+R: 색상 초기화\r\nCtrl+Alt+1: 프리셋 1\r\nCtrl+Alt+2: 프리셋 2\r\nCtrl+Alt+3: 프리셋 3\r\nCtrl+Alt+4: 프리셋 4";
                case "enable": return "활성화";
                case "resetColors": return "색상 초기화";
                case "show": return "보기";
                case "exit": return "종료";
                case "statusPrefix": return "상태";
                case "statusDesktop": return "데스크톱";
                case "statusDisabled": return "비활성화됨";
                case "statusResetting": return "초기화 중";
                case "statusPresetSaved": return "프리셋 저장됨";
                case "statusPresetLoaded": return "프리셋 불러옴";
                case "statusPresetHotkey": return "프리셋 단축키";
                case "statusLanguageChanged": return "언어 변경됨";
                case "statusTarkovActive": return "Tarkov 활성화";
                case "statusLoading": return "불러오는 중";
                case "doubleClickReset": return "더블클릭하면 초기화됩니다";
                case "unsupportedSaturation": return string.Format("{0} GPU는 이 앱에서 디지털 바이브런스 제어를 지원하지 않습니다.", gpu.Vendor);
                case "brightnessHelp": return "밝기는 전체 감마 램프를 더 밝거나 어둡게 이동합니다.\r\n어두운 영역이 잘 안 보이거나 화면이 과하게 밝을 때 조정합니다.\r\n기본값은 0.50입니다. 설정 라벨을 더블클릭하면 초기화됩니다.";
                case "contrastHelp": return "대비는 어두운 색과 밝은 색 사이의 차이를 키우거나 줄입니다.\r\n값을 높이면 윤곽이 선명해지지만 디테일이 뭉개질 수 있습니다.\r\n기본값은 0.50입니다. 설정 라벨을 더블클릭하면 초기화됩니다.";
                case "gammaHelp": return "감마는 순수한 검정/흰색보다 중간 톤의 밝기를 주로 바꿉니다.\r\n값을 낮추면 중간 톤이 어두워지고, 높이면 밝아집니다.\r\n기본값은 1.00입니다. 설정 라벨을 더블클릭하면 초기화됩니다.";
                case "dvlHelp": return "디지털 바이브런스/채도는 색의 강도를 조정합니다.\r\n값을 높이면 색이 더 선명해져 구분하기 쉬워집니다.\r\nNVIDIA NvAPI를 사용하며 지원하지 않는 GPU에서는 비활성화됩니다.\r\n설정 라벨을 더블클릭하면 초기화됩니다.";
                case "trayInitTitle": return "Tarkov Settings 초기화 완료!";
                case "trayInitMessage": return "트레이에서 색상 설정을 수정할 수 있습니다.";
                case "startupErrorTitle": return "시작 프로그램";
                case "startupErrorMessage": return "Windows 시작 프로그램 등록을 수정할 수 없습니다.";
                default: return key;
            }
        }

        private string Ja(string key)
        {
            switch (key)
            {
                case "miscs": return "その他";
                case "color": return "カラー";
                case "dvlGroup": return "DVL";
                case "dvlUnsupportedGroup": return "DVL (未対応)";
                case "brightness": return "明るさ";
                case "contrast": return "コントラスト";
                case "gamma": return "ガンマ";
                case "dvlLabel": return "デジタルバイブランス\r\n(彩度)\r\n";
                case "saturation": return "彩度";
                case "preset": return "プリセット";
                case "display": return "ディスプレイ";
                case "save": return "保存";
                case "language": return "言語";
                case "startWithWindows": return "Windows 起動時に実行";
                case "hotkeys": return "ホットキー";
                case "minimizeToTray": return "起動時にトレイへ最小化";
                case "hotkeyHelp": return "Ctrl+Alt+T: 有効/無効\r\nCtrl+Alt+R: 色をリセット\r\nCtrl+Alt+1: プリセット 1\r\nCtrl+Alt+2: プリセット 2\r\nCtrl+Alt+3: プリセット 3\r\nCtrl+Alt+4: プリセット 4";
                case "enable": return "有効";
                case "resetColors": return "色をリセット";
                case "show": return "表示";
                case "exit": return "終了";
                case "statusPrefix": return "状態";
                case "statusDesktop": return "デスクトップ";
                case "statusDisabled": return "無効";
                case "statusResetting": return "リセット中";
                case "statusPresetSaved": return "プリセット保存済み";
                case "statusPresetLoaded": return "プリセット読み込み済み";
                case "statusPresetHotkey": return "プリセットホットキー";
                case "statusLanguageChanged": return "言語変更済み";
                case "statusTarkovActive": return "Tarkov アクティブ";
                case "statusLoading": return "読み込み中";
                case "doubleClickReset": return "ダブルクリックでリセット";
                case "unsupportedSaturation": return string.Format("{0} GPU はこのアプリでデジタルバイブランス制御に対応していません。", gpu.Vendor);
                case "brightnessHelp": return "明るさはガンマランプ全体を明るく、または暗くします。\r\n暗部が見えにくい時や画面が白っぽい時に調整します。\r\n既定値は 0.50 です。設定ラベルをダブルクリックするとリセットします。";
                case "contrastHelp": return "コントラストは暗い色と明るい色の差を増減します。\r\n高くすると輪郭が強くなりますが、細部がつぶれる場合があります。\r\n既定値は 0.50 です。設定ラベルをダブルクリックするとリセットします。";
                case "gammaHelp": return "ガンマは黒と白よりも中間トーンの明るさを主に変更します。\r\n低くすると中間トーンが暗くなり、高くすると明るくなります。\r\n既定値は 1.00 です。設定ラベルをダブルクリックするとリセットします。";
                case "dvlHelp": return "デジタルバイブランス/彩度は色の強さを変更します。\r\n高くすると色が鮮やかになり、見分けやすくなります。\r\nNVIDIA NvAPI を使用し、未対応 GPU では無効になります。\r\n設定ラベルをダブルクリックするとリセットします。";
                case "trayInitTitle": return "Tarkov Settings 初期化完了!";
                case "trayInitMessage": return "トレイからカラー設定を変更できます。";
                case "startupErrorTitle": return "スタートアップ";
                case "startupErrorMessage": return "Windows スタートアップ登録を更新できませんでした。";
                default: return key;
            }
        }

        private string Zh(string key)
        {
            switch (key)
            {
                case "miscs": return "其他";
                case "color": return "颜色";
                case "dvlGroup": return "DVL";
                case "dvlUnsupportedGroup": return "DVL (不支持)";
                case "brightness": return "亮度";
                case "contrast": return "对比度";
                case "gamma": return "伽马";
                case "dvlLabel": return "数字鲜艳度\r\n(饱和度)\r\n";
                case "saturation": return "饱和度";
                case "preset": return "预设";
                case "display": return "显示器";
                case "save": return "保存";
                case "language": return "语言";
                case "startWithWindows": return "随 Windows 启动";
                case "hotkeys": return "快捷键";
                case "minimizeToTray": return "启动时最小化到托盘";
                case "hotkeyHelp": return "Ctrl+Alt+T: 开关\r\nCtrl+Alt+R: 重置颜色\r\nCtrl+Alt+1: 预设 1\r\nCtrl+Alt+2: 预设 2\r\nCtrl+Alt+3: 预设 3\r\nCtrl+Alt+4: 预设 4";
                case "enable": return "启用";
                case "resetColors": return "重置颜色";
                case "show": return "显示";
                case "exit": return "退出";
                case "statusPrefix": return "状态";
                case "statusDesktop": return "桌面";
                case "statusDisabled": return "已禁用";
                case "statusResetting": return "正在重置";
                case "statusPresetSaved": return "预设已保存";
                case "statusPresetLoaded": return "预设已加载";
                case "statusPresetHotkey": return "预设快捷键";
                case "statusLanguageChanged": return "语言已更改";
                case "statusTarkovActive": return "Tarkov 已激活";
                case "statusLoading": return "正在加载";
                case "doubleClickReset": return "双击可重置";
                case "unsupportedSaturation": return string.Format("{0} GPU 不支持此应用的数字鲜艳度控制。", gpu.Vendor);
                case "brightnessHelp": return "亮度会让整个伽马曲线变亮或变暗。\r\n当暗部难以看清或画面过亮时使用。\r\n默认值为 0.50。双击设置标签可重置。";
                case "contrastHelp": return "对比度会增加或减少暗色与亮色之间的差异。\r\n较高的值会让边缘更明显，但可能损失细节。\r\n默认值为 0.50。双击设置标签可重置。";
                case "gammaHelp": return "伽马会主要改变中间色调亮度，而不是纯黑和纯白。\r\n较低值会压暗中间调，较高值会提亮中间调。\r\n默认值为 1.00。双击设置标签可重置。";
                case "dvlHelp": return "数字鲜艳度/饱和度会改变颜色强度。\r\n较高值会让颜色更鲜明，更容易区分。\r\n此设置使用 NVIDIA NvAPI，不支持的 GPU 会禁用。\r\n双击设置标签可重置。";
                case "trayInitTitle": return "Tarkov Settings 已初始化!";
                case "trayInitMessage": return "可在托盘中修改颜色设置。";
                case "startupErrorTitle": return "启动项";
                case "startupErrorMessage": return "无法更新 Windows 启动项注册。";
                default: return key;
            }
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
            RegisterHotKey(Handle, HOTKEY_PRESET_1, MOD_CONTROL | MOD_ALT, (uint)Keys.D1);
            RegisterHotKey(Handle, HOTKEY_PRESET_2, MOD_CONTROL | MOD_ALT, (uint)Keys.D2);
            RegisterHotKey(Handle, HOTKEY_PRESET_3, MOD_CONTROL | MOD_ALT, (uint)Keys.D3);
            RegisterHotKey(Handle, HOTKEY_PRESET_4, MOD_CONTROL | MOD_ALT, (uint)Keys.D4);
        }

        private void UnregisterAppHotkeys()
        {
            if (!hotkeysRegistered || !IsHandleCreated)
                return;

            UnregisterHotKey(Handle, HOTKEY_TOGGLE_ENABLE);
            UnregisterHotKey(Handle, HOTKEY_RESET_COLORS);
            UnregisterHotKey(Handle, HOTKEY_PRESET_1);
            UnregisterHotKey(Handle, HOTKEY_PRESET_2);
            UnregisterHotKey(Handle, HOTKEY_PRESET_3);
            UnregisterHotKey(Handle, HOTKEY_PRESET_4);
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
                MessageBox.Show(T("startupErrorMessage"), T("startupErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                startWithWindowsCheckBox.Checked = false;
            }
        }

        private static int ClampToTrackBar(TrackBar trackBar, int value)
        {
            return Math.Min(Math.Max(value, trackBar.Minimum), trackBar.Maximum);
        }
    }
}
