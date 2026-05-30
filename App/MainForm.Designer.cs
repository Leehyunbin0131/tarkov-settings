namespace tarkov_settings
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.layoutTablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.SideMenu = new System.Windows.Forms.ToolStrip();
            this.MiscsButton = new System.Windows.Forms.ToolStripButton();
            this.ColorButton = new System.Windows.Forms.ToolStripButton();
            this.ColorPanel = new System.Windows.Forms.Panel();
            this.HotkeyHelpLabel = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.PresetLabel = new System.Windows.Forms.Label();
            this.DisplayLabel = new System.Windows.Forms.Label();
            this.SavePresetButton = new System.Windows.Forms.Button();
            this.LanguageLabel = new System.Windows.Forms.Label();
            this.LanguageCombo = new System.Windows.Forms.ComboBox();
            this.ProfileCombo = new System.Windows.Forms.ComboBox();
            this.enableHotkeysCheckBox = new System.Windows.Forms.CheckBox();
            this.startWithWindowsCheckBox = new System.Windows.Forms.CheckBox();
            this.minimizeStartCheckBox = new System.Windows.Forms.CheckBox();
            this.DisplayCombo = new System.Windows.Forms.ComboBox();
            this.DVLGroupBox = new System.Windows.Forms.GroupBox();
            this.DVLPanel = new System.Windows.Forms.Panel();
            this.DVLHelpLabel = new System.Windows.Forms.Label();
            this.DVLLabel = new System.Windows.Forms.Label();
            this.DVLBar = new System.Windows.Forms.TrackBar();
            this.DVLText = new System.Windows.Forms.TextBox();
            this.colorGroupBox = new System.Windows.Forms.GroupBox();
            this.colorTablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.brightnessPanel = new System.Windows.Forms.Panel();
            this.BrightnessHelpLabel = new System.Windows.Forms.Label();
            this.BrightnessBar = new System.Windows.Forms.TrackBar();
            this.BrightnessLabel = new System.Windows.Forms.Label();
            this.BrightnessText = new System.Windows.Forms.TextBox();
            this.contrastPanel = new System.Windows.Forms.Panel();
            this.ContrastHelpLabel = new System.Windows.Forms.Label();
            this.ContrastBar = new System.Windows.Forms.TrackBar();
            this.ContrastText = new System.Windows.Forms.TextBox();
            this.ContrastLabel = new System.Windows.Forms.Label();
            this.gammaPanel = new System.Windows.Forms.Panel();
            this.GammaHelpLabel = new System.Windows.Forms.Label();
            this.GammaText = new System.Windows.Forms.TextBox();
            this.GammaBar = new System.Windows.Forms.TrackBar();
            this.GammaLabel = new System.Windows.Forms.Label();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.enableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightnessToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contrastToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.gammaToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dvlToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.layoutTablePanel.SuspendLayout();
            this.SideMenu.SuspendLayout();
            this.ColorPanel.SuspendLayout();
            this.DVLGroupBox.SuspendLayout();
            this.DVLPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DVLBar)).BeginInit();
            this.colorGroupBox.SuspendLayout();
            this.colorTablePanel.SuspendLayout();
            this.brightnessPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessBar)).BeginInit();
            this.contrastPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContrastBar)).BeginInit();
            this.gammaPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GammaBar)).BeginInit();
            this.trayMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutTablePanel
            // 
            this.layoutTablePanel.ColumnCount = 2;
            this.layoutTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.37594F));
            this.layoutTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.62406F));
            this.layoutTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTablePanel.Controls.Add(this.SideMenu, 0, 0);
            this.layoutTablePanel.Controls.Add(this.ColorPanel, 1, 0);
            this.layoutTablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTablePanel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutTablePanel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.layoutTablePanel.Location = new System.Drawing.Point(0, 0);
            this.layoutTablePanel.Name = "layoutTablePanel";
            this.layoutTablePanel.RowCount = 1;
            this.layoutTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.4669F));
            this.layoutTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.5331F));
            this.layoutTablePanel.Size = new System.Drawing.Size(734, 440);
            this.layoutTablePanel.TabIndex = 0;
            // 
            // SideMenu
            // 
            this.SideMenu.AutoSize = false;
            this.SideMenu.BackColor = System.Drawing.Color.AliceBlue;
            this.SideMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SideMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.SideMenu.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.SideMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiscsButton,
            this.ColorButton});
            this.SideMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.SideMenu.Location = new System.Drawing.Point(0, 5);
            this.SideMenu.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.SideMenu.Name = "SideMenu";
            this.SideMenu.Size = new System.Drawing.Size(76, 430);
            this.SideMenu.TabIndex = 1;
            this.SideMenu.Text = "colorSettings";
            // 
            // MiscsButton
            // 
            this.MiscsButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MiscsButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MiscsButton.Image = global::tarkov_settings.Properties.Resources.nikita;
            this.MiscsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MiscsButton.Name = "MiscsButton";
            this.MiscsButton.Size = new System.Drawing.Size(73, 74);
            this.MiscsButton.Text = "Miscs";
            this.MiscsButton.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.MiscsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.MiscsButton.Click += new System.EventHandler(this.ShowMiscsMode);
            // 
            // ColorButton
            // 
            this.ColorButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColorButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ColorButton.Image = global::tarkov_settings.Properties.Resources.nikita_rainbow;
            this.ColorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ColorButton.Name = "ColorButton";
            this.ColorButton.Size = new System.Drawing.Size(73, 74);
            this.ColorButton.Text = "Color";
            this.ColorButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ColorButton.Click += new System.EventHandler(this.ShowColorMode);
            // 
            // ColorPanel
            // 
            this.ColorPanel.Controls.Add(this.SavePresetButton);
            this.ColorPanel.Controls.Add(this.LanguageCombo);
            this.ColorPanel.Controls.Add(this.LanguageLabel);
            this.ColorPanel.Controls.Add(this.DisplayLabel);
            this.ColorPanel.Controls.Add(this.PresetLabel);
            this.ColorPanel.Controls.Add(this.HotkeyHelpLabel);
            this.ColorPanel.Controls.Add(this.StatusLabel);
            this.ColorPanel.Controls.Add(this.ProfileCombo);
            this.ColorPanel.Controls.Add(this.enableHotkeysCheckBox);
            this.ColorPanel.Controls.Add(this.startWithWindowsCheckBox);
            this.ColorPanel.Controls.Add(this.minimizeStartCheckBox);
            this.ColorPanel.Controls.Add(this.DisplayCombo);
            this.ColorPanel.Controls.Add(this.DVLGroupBox);
            this.ColorPanel.Controls.Add(this.colorGroupBox);
            this.ColorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ColorPanel.Location = new System.Drawing.Point(79, 3);
            this.ColorPanel.Name = "ColorPanel";
            this.ColorPanel.Size = new System.Drawing.Size(652, 434);
            this.ColorPanel.TabIndex = 2;
            //
            // HotkeyHelpLabel
            //
            this.HotkeyHelpLabel.AutoSize = true;
            this.HotkeyHelpLabel.Font = new System.Drawing.Font("Consolas", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HotkeyHelpLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.HotkeyHelpLabel.Location = new System.Drawing.Point(24, 116);
            this.HotkeyHelpLabel.Name = "HotkeyHelpLabel";
            this.HotkeyHelpLabel.Size = new System.Drawing.Size(248, 102);
            this.HotkeyHelpLabel.TabIndex = 21;
            this.HotkeyHelpLabel.Text = "Ctrl+Alt+T: Toggle\r\nCtrl+Alt+R: Reset\r\nCtrl+Alt+1: Preset 1\r\nCtrl+Alt+2: Preset 2\r\nCtrl+Alt+3: Preset 3\r\nCtrl+Alt+4: Preset 4";
            //
            // StatusLabel
            //
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.StatusLabel.Location = new System.Drawing.Point(3, 329);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(160, 22);
            this.StatusLabel.TabIndex = 20;
            this.StatusLabel.Text = "Status: Loading";
            //
            // PresetLabel
            //
            this.PresetLabel.AutoSize = true;
            this.PresetLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.PresetLabel.Location = new System.Drawing.Point(286, 303);
            this.PresetLabel.Name = "PresetLabel";
            this.PresetLabel.Size = new System.Drawing.Size(70, 22);
            this.PresetLabel.TabIndex = 22;
            this.PresetLabel.Text = "Preset";
            //
            // DisplayLabel
            //
            this.DisplayLabel.AutoSize = true;
            this.DisplayLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DisplayLabel.Location = new System.Drawing.Point(502, 303);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(80, 22);
            this.DisplayLabel.TabIndex = 23;
            this.DisplayLabel.Text = "Display";
            //
            // SavePresetButton
            //
            this.SavePresetButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SavePresetButton.Location = new System.Drawing.Point(412, 325);
            this.SavePresetButton.Name = "SavePresetButton";
            this.SavePresetButton.Size = new System.Drawing.Size(80, 32);
            this.SavePresetButton.TabIndex = 24;
            this.SavePresetButton.Text = "Save";
            this.SavePresetButton.UseVisualStyleBackColor = true;
            this.SavePresetButton.Click += new System.EventHandler(this.SavePresetClicked);
            //
            // LanguageLabel
            //
            this.LanguageLabel.AutoSize = true;
            this.LanguageLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LanguageLabel.Location = new System.Drawing.Point(3, 280);
            this.LanguageLabel.Name = "LanguageLabel";
            this.LanguageLabel.Size = new System.Drawing.Size(90, 22);
            this.LanguageLabel.TabIndex = 25;
            this.LanguageLabel.Text = "Language";
            //
            // LanguageCombo
            //
            this.LanguageCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LanguageCombo.FormattingEnabled = true;
            this.LanguageCombo.Location = new System.Drawing.Point(3, 305);
            this.LanguageCombo.Name = "LanguageCombo";
            this.LanguageCombo.Size = new System.Drawing.Size(180, 30);
            this.LanguageCombo.TabIndex = 26;
            this.LanguageCombo.SelectedValueChanged += new System.EventHandler(this.LanguageCombo_SelectedValueChanged);
            //
            // ProfileCombo
            //
            this.ProfileCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProfileCombo.FormattingEnabled = true;
            this.ProfileCombo.Location = new System.Drawing.Point(286, 326);
            this.ProfileCombo.Name = "ProfileCombo";
            this.ProfileCombo.Size = new System.Drawing.Size(120, 30);
            this.ProfileCombo.TabIndex = 19;
            this.ProfileCombo.SelectedValueChanged += new System.EventHandler(this.ProfileCombo_SelectedValueChanged);
            //
            // enableHotkeysCheckBox
            //
            this.enableHotkeysCheckBox.AutoSize = true;
            this.enableHotkeysCheckBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.enableHotkeysCheckBox.Location = new System.Drawing.Point(3, 87);
            this.enableHotkeysCheckBox.Name = "enableHotkeysCheckBox";
            this.enableHotkeysCheckBox.Size = new System.Drawing.Size(106, 26);
            this.enableHotkeysCheckBox.TabIndex = 18;
            this.enableHotkeysCheckBox.Text = "Hotkeys";
            this.enableHotkeysCheckBox.UseVisualStyleBackColor = true;
            this.enableHotkeysCheckBox.CheckedChanged += new System.EventHandler(this.CheckOnEnableHotkeys);
            //
            // startWithWindowsCheckBox
            //
            this.startWithWindowsCheckBox.AutoSize = true;
            this.startWithWindowsCheckBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.startWithWindowsCheckBox.Location = new System.Drawing.Point(3, 51);
            this.startWithWindowsCheckBox.Name = "startWithWindowsCheckBox";
            this.startWithWindowsCheckBox.Size = new System.Drawing.Size(206, 26);
            this.startWithWindowsCheckBox.TabIndex = 17;
            this.startWithWindowsCheckBox.Text = "Start with Windows";
            this.startWithWindowsCheckBox.UseVisualStyleBackColor = true;
            this.startWithWindowsCheckBox.CheckedChanged += new System.EventHandler(this.CheckOnStartWithWindows);
            // 
            // minimizeStartCheckBox
            // 
            this.minimizeStartCheckBox.AutoSize = true;
            this.minimizeStartCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.minimizeStartCheckBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.minimizeStartCheckBox.Location = new System.Drawing.Point(3, 230);
            this.minimizeStartCheckBox.Name = "minimizeStartCheckBox";
            this.minimizeStartCheckBox.Size = new System.Drawing.Size(286, 26);
            this.minimizeStartCheckBox.TabIndex = 16;
            this.minimizeStartCheckBox.Text = "Minimize to Tray on Start";
            this.minimizeStartCheckBox.UseVisualStyleBackColor = false;
            this.minimizeStartCheckBox.CheckedChanged += new System.EventHandler(this.CheckOnMinimizeToTray);
            // 
            // DisplayCombo
            // 
            this.DisplayCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DisplayCombo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.DisplayCombo.FormattingEnabled = true;
            this.DisplayCombo.Location = new System.Drawing.Point(502, 326);
            this.DisplayCombo.Name = "DisplayCombo";
            this.DisplayCombo.Size = new System.Drawing.Size(139, 30);
            this.DisplayCombo.TabIndex = 15;
            this.DisplayCombo.SelectedValueChanged += new System.EventHandler(this.DisplayCombo_SelectedValueChanged);
            // 
            // DVLGroupBox
            // 
            this.DVLGroupBox.Controls.Add(this.DVLPanel);
            this.DVLGroupBox.Location = new System.Drawing.Point(499, 9);
            this.DVLGroupBox.Name = "DVLGroupBox";
            this.DVLGroupBox.Size = new System.Drawing.Size(145, 307);
            this.DVLGroupBox.TabIndex = 13;
            this.DVLGroupBox.TabStop = false;
            this.DVLGroupBox.Text = "DVL";
            // 
            // DVLPanel
            // 
            this.DVLPanel.Controls.Add(this.DVLHelpLabel);
            this.DVLPanel.Controls.Add(this.DVLLabel);
            this.DVLPanel.Controls.Add(this.DVLBar);
            this.DVLPanel.Controls.Add(this.DVLText);
            this.DVLPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DVLPanel.Location = new System.Drawing.Point(3, 25);
            this.DVLPanel.Name = "DVLPanel";
            this.DVLPanel.Size = new System.Drawing.Size(139, 279);
            this.DVLPanel.TabIndex = 0;
            // 
            // DVLHelpLabel
            // 
            this.DVLHelpLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DVLHelpLabel.Cursor = System.Windows.Forms.Cursors.Help;
            this.DVLHelpLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.DVLHelpLabel.Location = new System.Drawing.Point(104, 11);
            this.DVLHelpLabel.Name = "DVLHelpLabel";
            this.DVLHelpLabel.Size = new System.Drawing.Size(18, 18);
            this.DVLHelpLabel.TabIndex = 12;
            this.DVLHelpLabel.Text = "?";
            this.DVLHelpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dvlToolTip.SetToolTip(this.DVLHelpLabel, "Digital Vibrance / Saturation changes color intensity.\r\nHigher values make colors more vivid and easier to distinguish.\r\nThis setting uses NVIDIA NvAPI and is disabled on unsupported GPUs.\r\nDouble-click the setting label to reset it.");
            // 
            // DVLLabel
            // 
            this.DVLLabel.AutoSize = true;
            this.DVLLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DVLLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DVLLabel.Location = new System.Drawing.Point(13, 11);
            this.DVLLabel.Name = "DVLLabel";
            this.DVLLabel.Size = new System.Drawing.Size(170, 44);
            this.DVLLabel.TabIndex = 10;
            this.DVLLabel.Text = "Digital Vibrance\r\n(Saturation)\r\n";
            this.DVLLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dvlToolTip.SetToolTip(this.DVLLabel, "Double-click to reset");
            this.DVLLabel.DoubleClick += new System.EventHandler(this.ColorLabel_DClick);
            // 
            // DVLBar
            // 
            this.DVLBar.Location = new System.Drawing.Point(56, 42);
            this.DVLBar.Maximum = 63;
            this.DVLBar.Name = "DVLBar";
            this.DVLBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.DVLBar.Size = new System.Drawing.Size(69, 184);
            this.DVLBar.TabIndex = 9;
            this.DVLBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.DVLBar.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
            // 
            // DVLText
            // 
            this.DVLText.Location = new System.Drawing.Point(46, 232);
            this.DVLText.Name = "DVLText";
            this.DVLText.ReadOnly = true;
            this.DVLText.Size = new System.Drawing.Size(41, 29);
            this.DVLText.TabIndex = 11;
            this.DVLText.Text = "0";
            this.DVLText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // colorGroupBox
            // 
            this.colorGroupBox.Controls.Add(this.colorTablePanel);
            this.colorGroupBox.Location = new System.Drawing.Point(3, 9);
            this.colorGroupBox.Name = "colorGroupBox";
            this.colorGroupBox.Size = new System.Drawing.Size(490, 307);
            this.colorGroupBox.TabIndex = 12;
            this.colorGroupBox.TabStop = false;
            this.colorGroupBox.Text = "Color";
            // 
            // colorTablePanel
            // 
            this.colorTablePanel.ColumnCount = 1;
            this.colorTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.colorTablePanel.Controls.Add(this.brightnessPanel, 0, 0);
            this.colorTablePanel.Controls.Add(this.contrastPanel, 0, 1);
            this.colorTablePanel.Controls.Add(this.gammaPanel, 0, 2);
            this.colorTablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorTablePanel.Location = new System.Drawing.Point(3, 25);
            this.colorTablePanel.Name = "colorTablePanel";
            this.colorTablePanel.RowCount = 3;
            this.colorTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.colorTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.colorTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.colorTablePanel.Size = new System.Drawing.Size(484, 279);
            this.colorTablePanel.TabIndex = 1;
            // 
            // brightnessPanel
            // 
            this.brightnessPanel.Controls.Add(this.BrightnessHelpLabel);
            this.brightnessPanel.Controls.Add(this.BrightnessBar);
            this.brightnessPanel.Controls.Add(this.BrightnessLabel);
            this.brightnessPanel.Controls.Add(this.BrightnessText);
            this.brightnessPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brightnessPanel.Location = new System.Drawing.Point(3, 3);
            this.brightnessPanel.Name = "brightnessPanel";
            this.brightnessPanel.Size = new System.Drawing.Size(478, 87);
            this.brightnessPanel.TabIndex = 0;
            // 
            // BrightnessHelpLabel
            // 
            this.BrightnessHelpLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BrightnessHelpLabel.Cursor = System.Windows.Forms.Cursors.Help;
            this.BrightnessHelpLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.BrightnessHelpLabel.Location = new System.Drawing.Point(136, 10);
            this.BrightnessHelpLabel.Name = "BrightnessHelpLabel";
            this.BrightnessHelpLabel.Size = new System.Drawing.Size(18, 18);
            this.BrightnessHelpLabel.TabIndex = 25;
            this.BrightnessHelpLabel.Text = "?";
            this.BrightnessHelpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.brightnessToolTip.SetToolTip(this.BrightnessHelpLabel, "Brightness shifts the whole gamma ramp brighter or darker.\r\nUse it when dark areas are too hard to see or the image is washed out.\r\nDefault is 0.50. Double-click the setting label to reset it.");
            // 
            // BrightnessBar
            // 
            this.BrightnessBar.Location = new System.Drawing.Point(13, 27);
            this.BrightnessBar.Maximum = 100;
            this.BrightnessBar.Minimum = -100;
            this.BrightnessBar.Name = "BrightnessBar";
            this.BrightnessBar.Size = new System.Drawing.Size(397, 69);
            this.BrightnessBar.TabIndex = 18;
            this.BrightnessBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.BrightnessBar.Value = 50;
            this.BrightnessBar.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
            // 
            // BrightnessLabel
            // 
            this.BrightnessLabel.AutoSize = true;
            this.BrightnessLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BrightnessLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BrightnessLabel.Location = new System.Drawing.Point(20, 10);
            this.BrightnessLabel.Name = "BrightnessLabel";
            this.BrightnessLabel.Size = new System.Drawing.Size(110, 22);
            this.BrightnessLabel.TabIndex = 21;
            this.BrightnessLabel.Text = "Brightness";
            this.brightnessToolTip.SetToolTip(this.BrightnessLabel, "Double-click to reset");
            this.BrightnessLabel.DoubleClick += new System.EventHandler(this.ColorLabel_DClick);
            // 
            // BrightnessText
            // 
            this.BrightnessText.Location = new System.Drawing.Point(424, 27);
            this.BrightnessText.Name = "BrightnessText";
            this.BrightnessText.ReadOnly = true;
            this.BrightnessText.Size = new System.Drawing.Size(41, 29);
            this.BrightnessText.TabIndex = 24;
            this.BrightnessText.Text = "0.50";
            this.BrightnessText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // contrastPanel
            // 
            this.contrastPanel.Controls.Add(this.ContrastHelpLabel);
            this.contrastPanel.Controls.Add(this.ContrastBar);
            this.contrastPanel.Controls.Add(this.ContrastText);
            this.contrastPanel.Controls.Add(this.ContrastLabel);
            this.contrastPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contrastPanel.Location = new System.Drawing.Point(3, 96);
            this.contrastPanel.Name = "contrastPanel";
            this.contrastPanel.Size = new System.Drawing.Size(478, 87);
            this.contrastPanel.TabIndex = 1;
            // 
            // ContrastHelpLabel
            // 
            this.ContrastHelpLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContrastHelpLabel.Cursor = System.Windows.Forms.Cursors.Help;
            this.ContrastHelpLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.ContrastHelpLabel.Location = new System.Drawing.Point(116, 22);
            this.ContrastHelpLabel.Name = "ContrastHelpLabel";
            this.ContrastHelpLabel.Size = new System.Drawing.Size(18, 18);
            this.ContrastHelpLabel.TabIndex = 26;
            this.ContrastHelpLabel.Text = "?";
            this.ContrastHelpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.contrastToolTip.SetToolTip(this.ContrastHelpLabel, "Contrast increases or decreases the gap between dark and bright colors.\r\nHigher values make edges pop but can crush details.\r\nDefault is 0.50. Double-click the setting label to reset it.");
            // 
            // ContrastBar
            // 
            this.ContrastBar.Location = new System.Drawing.Point(13, 39);
            this.ContrastBar.Maximum = 100;
            this.ContrastBar.Minimum = -100;
            this.ContrastBar.Name = "ContrastBar";
            this.ContrastBar.Size = new System.Drawing.Size(397, 69);
            this.ContrastBar.TabIndex = 19;
            this.ContrastBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ContrastBar.Value = 50;
            this.ContrastBar.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
            // 
            // ContrastText
            // 
            this.ContrastText.Location = new System.Drawing.Point(424, 39);
            this.ContrastText.Name = "ContrastText";
            this.ContrastText.ReadOnly = true;
            this.ContrastText.Size = new System.Drawing.Size(41, 29);
            this.ContrastText.TabIndex = 25;
            this.ContrastText.Text = "0.50";
            this.ContrastText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ContrastLabel
            // 
            this.ContrastLabel.AutoSize = true;
            this.ContrastLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ContrastLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ContrastLabel.Location = new System.Drawing.Point(20, 22);
            this.ContrastLabel.Name = "ContrastLabel";
            this.ContrastLabel.Size = new System.Drawing.Size(90, 22);
            this.ContrastLabel.TabIndex = 22;
            this.ContrastLabel.Text = "Contrast";
            this.contrastToolTip.SetToolTip(this.ContrastLabel, "Double-click to reset");
            this.ContrastLabel.DoubleClick += new System.EventHandler(this.ColorLabel_DClick);
            // 
            // gammaPanel
            // 
            this.gammaPanel.Controls.Add(this.GammaHelpLabel);
            this.gammaPanel.Controls.Add(this.GammaText);
            this.gammaPanel.Controls.Add(this.GammaBar);
            this.gammaPanel.Controls.Add(this.GammaLabel);
            this.gammaPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gammaPanel.Location = new System.Drawing.Point(3, 189);
            this.gammaPanel.Name = "gammaPanel";
            this.gammaPanel.Size = new System.Drawing.Size(478, 87);
            this.gammaPanel.TabIndex = 2;
            // 
            // GammaHelpLabel
            // 
            this.GammaHelpLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GammaHelpLabel.Cursor = System.Windows.Forms.Cursors.Help;
            this.GammaHelpLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.GammaHelpLabel.Location = new System.Drawing.Point(86, 23);
            this.GammaHelpLabel.Name = "GammaHelpLabel";
            this.GammaHelpLabel.Size = new System.Drawing.Size(18, 18);
            this.GammaHelpLabel.TabIndex = 27;
            this.GammaHelpLabel.Text = "?";
            this.GammaHelpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.gammaToolTip.SetToolTip(this.GammaHelpLabel, "Gamma changes mid-tone brightness without moving pure black and white as much.\r\nLower values darken mid-tones; higher values brighten them.\r\nDefault is 1.00. Double-click the setting label to reset it.");
            // 
            // GammaText
            // 
            this.GammaText.Location = new System.Drawing.Point(424, 40);
            this.GammaText.Name = "GammaText";
            this.GammaText.ReadOnly = true;
            this.GammaText.Size = new System.Drawing.Size(41, 29);
            this.GammaText.TabIndex = 26;
            this.GammaText.Text = "1.00";
            this.GammaText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GammaBar
            // 
            this.GammaBar.Location = new System.Drawing.Point(13, 40);
            this.GammaBar.Maximum = 280;
            this.GammaBar.Minimum = 40;
            this.GammaBar.Name = "GammaBar";
            this.GammaBar.Size = new System.Drawing.Size(397, 69);
            this.GammaBar.TabIndex = 20;
            this.GammaBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.GammaBar.Value = 100;
            this.GammaBar.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
            // 
            // GammaLabel
            // 
            this.GammaLabel.AutoSize = true;
            this.GammaLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GammaLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.GammaLabel.Location = new System.Drawing.Point(20, 23);
            this.GammaLabel.Name = "GammaLabel";
            this.GammaLabel.Size = new System.Drawing.Size(60, 22);
            this.GammaLabel.TabIndex = 23;
            this.GammaLabel.Text = "Gamma";
            this.gammaToolTip.SetToolTip(this.GammaLabel, "Double-click to reset");
            this.GammaLabel.DoubleClick += new System.EventHandler(this.ColorLabel_DClick);
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.trayMenuStrip;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "Tarkov Settings";
            this.trayIcon.Visible = true;
            this.trayIcon.DoubleClick += new System.EventHandler(this.ShowForm);
            // 
            // trayMenuStrip
            // 
            this.trayMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.trayMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableToolStripMenuItem,
            this.resetColorsToolStripMenuItem,
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.trayMenuStrip.Name = "trayMenuStrip";
            this.trayMenuStrip.Size = new System.Drawing.Size(178, 132);
            // 
            // enableToolStripMenuItem
            // 
            this.enableToolStripMenuItem.Checked = true;
            this.enableToolStripMenuItem.CheckOnClick = true;
            this.enableToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableToolStripMenuItem.Name = "enableToolStripMenuItem";
            this.enableToolStripMenuItem.Size = new System.Drawing.Size(177, 32);
            this.enableToolStripMenuItem.Text = "Enable";
            this.enableToolStripMenuItem.CheckedChanged += new System.EventHandler(this.EnableToolStripMenuItem_CheckedChanged);
            //
            // resetColorsToolStripMenuItem
            //
            this.resetColorsToolStripMenuItem.Name = "resetColorsToolStripMenuItem";
            this.resetColorsToolStripMenuItem.Size = new System.Drawing.Size(177, 32);
            this.resetColorsToolStripMenuItem.Text = "Reset Colors";
            this.resetColorsToolStripMenuItem.Click += new System.EventHandler(this.ResetColorsClicked);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(177, 32);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.ShowForm);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(177, 32);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitFormClicked);
            // 
            // brightnessToolTip
            // 
            this.brightnessToolTip.IsBalloon = true;
            this.brightnessToolTip.ShowAlways = true;
            this.brightnessToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.brightnessToolTip.ToolTipTitle = "Brightness";
            // 
            // contrastToolTip
            // 
            this.contrastToolTip.IsBalloon = true;
            this.contrastToolTip.ShowAlways = true;
            this.contrastToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.contrastToolTip.ToolTipTitle = "Contrast";
            // 
            // gammaToolTip
            // 
            this.gammaToolTip.IsBalloon = true;
            this.gammaToolTip.ShowAlways = true;
            this.gammaToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.gammaToolTip.ToolTipTitle = "Gamma";
            // 
            // dvlToolTip
            // 
            this.dvlToolTip.IsBalloon = true;
            this.dvlToolTip.ShowAlways = true;
            this.dvlToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.dvlToolTip.ToolTipTitle = "Saturation";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(734, 440);
            this.Controls.Add(this.layoutTablePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Tarkov Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.layoutTablePanel.ResumeLayout(false);
            this.SideMenu.ResumeLayout(false);
            this.SideMenu.PerformLayout();
            this.ColorPanel.ResumeLayout(false);
            this.ColorPanel.PerformLayout();
            this.DVLGroupBox.ResumeLayout(false);
            this.DVLPanel.ResumeLayout(false);
            this.DVLPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DVLBar)).EndInit();
            this.colorGroupBox.ResumeLayout(false);
            this.colorTablePanel.ResumeLayout(false);
            this.brightnessPanel.ResumeLayout(false);
            this.brightnessPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessBar)).EndInit();
            this.contrastPanel.ResumeLayout(false);
            this.contrastPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContrastBar)).EndInit();
            this.gammaPanel.ResumeLayout(false);
            this.gammaPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GammaBar)).EndInit();
            this.trayMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel layoutTablePanel;
        private System.Windows.Forms.ToolStrip SideMenu;
        private System.Windows.Forms.ToolStripButton MiscsButton;
        private System.Windows.Forms.ToolStripButton ColorButton;
        private System.Windows.Forms.Panel ColorPanel;
        
        

        private System.Windows.Forms.GroupBox colorGroupBox;
        private System.Windows.Forms.TextBox DVLText;
        private System.Windows.Forms.Label DVLLabel;
        private System.Windows.Forms.TrackBar DVLBar;
        private System.Windows.Forms.TextBox GammaText;
        private System.Windows.Forms.TextBox ContrastText;
        private System.Windows.Forms.TextBox BrightnessText;
        private System.Windows.Forms.Label GammaLabel;
        private System.Windows.Forms.Label ContrastLabel;
        private System.Windows.Forms.Label BrightnessLabel;
        private System.Windows.Forms.TrackBar GammaBar;
        private System.Windows.Forms.TrackBar ContrastBar;
        private System.Windows.Forms.TrackBar BrightnessBar;
        private System.Windows.Forms.TableLayoutPanel colorTablePanel;
        private System.Windows.Forms.Panel brightnessPanel;
        private System.Windows.Forms.Panel contrastPanel;
        private System.Windows.Forms.Panel gammaPanel;
        private System.Windows.Forms.GroupBox DVLGroupBox;
        private System.Windows.Forms.Panel DVLPanel;
        private System.Windows.Forms.ComboBox DisplayCombo;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem enableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetColorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.CheckBox minimizeStartCheckBox;
        private System.Windows.Forms.CheckBox startWithWindowsCheckBox;
        private System.Windows.Forms.CheckBox enableHotkeysCheckBox;
        private System.Windows.Forms.Label HotkeyHelpLabel;
        private System.Windows.Forms.Label PresetLabel;
        private System.Windows.Forms.Label DisplayLabel;
        private System.Windows.Forms.Button SavePresetButton;
        private System.Windows.Forms.Label LanguageLabel;
        private System.Windows.Forms.ComboBox LanguageCombo;
        private System.Windows.Forms.ComboBox ProfileCombo;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.ToolTip dvlToolTip;
        private System.Windows.Forms.ToolTip brightnessToolTip;
        private System.Windows.Forms.ToolTip contrastToolTip;
        private System.Windows.Forms.ToolTip gammaToolTip;
        private System.Windows.Forms.Label BrightnessHelpLabel;
        private System.Windows.Forms.Label ContrastHelpLabel;
        private System.Windows.Forms.Label GammaHelpLabel;
        private System.Windows.Forms.Label DVLHelpLabel;
    }
}

