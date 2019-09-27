namespace mGeek
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// 
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            mTxtBox = new System.Windows.Forms.RichTextBox();
            this.ListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.searchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshLogsListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.mGeekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.codingFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataDumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.driveDataFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ListLogs = new System.Windows.Forms.ListBox();
            this.SearchLogsButton = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.TimeDebug = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LogSearchTextBox = new System.Windows.Forms.TextBox();
            this.MonthSelectLogs = new System.Windows.Forms.ComboBox();
            this.contextLogsMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VersionTxt = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.LRButton = new System.Windows.Forms.Button();
            this.MercButton = new System.Windows.Forms.Button();
            this.BMWButton = new System.Windows.Forms.Button();
            this.BMWPanel = new System.Windows.Forms.Panel();
            this.MERCPanel = new System.Windows.Forms.Panel();
            this.LRPanel = new System.Windows.Forms.Panel();
            this.ListContextMenu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextLogsMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mTxtBox
            // 
            mTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            mTxtBox.BackColor = System.Drawing.Color.DarkSlateGray;
            mTxtBox.ContextMenuStrip = this.ListContextMenu;
            mTxtBox.DetectUrls = false;
            mTxtBox.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            mTxtBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            mTxtBox.Location = new System.Drawing.Point(0, 254);
            mTxtBox.Name = "mTxtBox";
            mTxtBox.RightMargin = 50000;
            mTxtBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            mTxtBox.ShowSelectionMargin = true;
            mTxtBox.Size = new System.Drawing.Size(1512, 603);
            mTxtBox.TabIndex = 0;
            mTxtBox.Text = "0|";
            mTxtBox.WordWrap = false;
            mTxtBox.KeyDown += mTxtBox_KeyDown;
            mTxtBox.MouseDown += MTxtBox_MouseDown;
            // 
            // ListContextMenu
            // 
            this.ListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem1});
            this.ListContextMenu.Name = "ListContextMenu";
            this.ListContextMenu.Size = new System.Drawing.Size(110, 26);
            // 
            // searchToolStripMenuItem1
            // 
            this.searchToolStripMenuItem1.Name = "searchToolStripMenuItem1";
            this.searchToolStripMenuItem1.Size = new System.Drawing.Size(109, 22);
            this.searchToolStripMenuItem1.Text = "Search";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "All Files (*.*)|*.*";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog1_FileOk);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2,
            this.toolStripDropDownButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1524, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.refreshLogsListToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(38, 22);
            this.toolStripDropDownButton1.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // refreshLogsListToolStripMenuItem
            // 
            this.refreshLogsListToolStripMenuItem.Name = "refreshLogsListToolStripMenuItem";
            this.refreshLogsListToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.refreshLogsListToolStripMenuItem.Text = "Refresh Logs list";
            this.refreshLogsListToolStripMenuItem.Click += new System.EventHandler(this.refreshLogsListToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mGeekToolStripMenuItem,
            this.debugTimeToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(47, 22);
            this.toolStripDropDownButton2.Text = "Tools";
            // 
            // mGeekToolStripMenuItem
            // 
            this.mGeekToolStripMenuItem.Checked = true;
            this.mGeekToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mGeekToolStripMenuItem.Name = "mGeekToolStripMenuItem";
            this.mGeekToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.mGeekToolStripMenuItem.Text = "mGeek Syntax";
            this.mGeekToolStripMenuItem.Click += new System.EventHandler(this.MGeekToolStripMenuItem_Click);
            // 
            // debugTimeToolStripMenuItem
            // 
            this.debugTimeToolStripMenuItem.Name = "debugTimeToolStripMenuItem";
            this.debugTimeToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.debugTimeToolStripMenuItem.Text = "Debug Time";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.searchToolStripMenuItem.Text = "Search";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.codingFilesToolStripMenuItem,
            this.driveDataFilesToolStripMenuItem});
            this.toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton3.Image")));
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(91, 22);
            this.toolStripDropDownButton3.Text = "File Shortcuts";
            // 
            // codingFilesToolStripMenuItem
            // 
            this.codingFilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.datasToolStripMenuItem,
            this.dataDumpToolStripMenuItem});
            this.codingFilesToolStripMenuItem.Name = "codingFilesToolStripMenuItem";
            this.codingFilesToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.codingFilesToolStripMenuItem.Text = "Coding Files";
            // 
            // datasToolStripMenuItem
            // 
            this.datasToolStripMenuItem.Name = "datasToolStripMenuItem";
            this.datasToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.datasToolStripMenuItem.Text = "Datas";
            this.datasToolStripMenuItem.Click += new System.EventHandler(this.datasToolStripMenuItem_Click);
            // 
            // dataDumpToolStripMenuItem
            // 
            this.dataDumpToolStripMenuItem.Name = "dataDumpToolStripMenuItem";
            this.dataDumpToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.dataDumpToolStripMenuItem.Text = "Data Dump";
            this.dataDumpToolStripMenuItem.Click += new System.EventHandler(this.dataDumpToolStripMenuItem_Click);
            // 
            // driveDataFilesToolStripMenuItem
            // 
            this.driveDataFilesToolStripMenuItem.Name = "driveDataFilesToolStripMenuItem";
            this.driveDataFilesToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.driveDataFilesToolStripMenuItem.Text = "Drive Data Files";
            this.driveDataFilesToolStripMenuItem.Click += new System.EventHandler(this.driveDataFilesToolStripMenuItem_Click);
            // 
            // ListLogs
            // 
            this.ListLogs.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ListLogs.Font = new System.Drawing.Font("Bahnschrift", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListLogs.ForeColor = System.Drawing.Color.Coral;
            this.ListLogs.FormattingEnabled = true;
            this.ListLogs.ItemHeight = 16;
            this.ListLogs.Location = new System.Drawing.Point(-1, 102);
            this.ListLogs.Name = "ListLogs";
            this.ListLogs.ScrollAlwaysVisible = true;
            this.ListLogs.Size = new System.Drawing.Size(278, 148);
            this.ListLogs.TabIndex = 10;
            this.ListLogs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListLogs_MouseDoubleClick);
            this.ListLogs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListLogs_MouseDown);
            this.ListLogs.MouseLeave += new System.EventHandler(this.ListLogs_MouseLeave);
            this.ListLogs.MouseHover += new System.EventHandler(this.ListLogs_MouseHover);
            // 
            // SearchLogsButton
            // 
            this.SearchLogsButton.Location = new System.Drawing.Point(248, 55);
            this.SearchLogsButton.Name = "SearchLogsButton";
            this.SearchLogsButton.Size = new System.Drawing.Size(29, 25);
            this.SearchLogsButton.TabIndex = 12;
            this.SearchLogsButton.Text = ">>";
            this.SearchLogsButton.UseVisualStyleBackColor = true;
            this.SearchLogsButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(283, 202);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(157, 25);
            this.button4.TabIndex = 13;
            this.button4.Text = "LOG 2 SDF";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // TimeDebug
            // 
            this.TimeDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeDebug.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.TimeDebug.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeDebug.Location = new System.Drawing.Point(1243, 2);
            this.TimeDebug.Name = "TimeDebug";
            this.TimeDebug.Size = new System.Drawing.Size(274, 20);
            this.TimeDebug.TabIndex = 17;
            this.TimeDebug.Text = "Time Took To Load:";
            this.TimeDebug.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(1322, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 55);
            this.label1.TabIndex = 18;
            this.label1.Text = "mGeek";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(1477, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "0.01";
            // 
            // LogSearchTextBox
            // 
            this.LogSearchTextBox.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogSearchTextBox.Location = new System.Drawing.Point(0, 55);
            this.LogSearchTextBox.Name = "LogSearchTextBox";
            this.LogSearchTextBox.Size = new System.Drawing.Size(247, 25);
            this.LogSearchTextBox.TabIndex = 11;
            this.LogSearchTextBox.TextChanged += new System.EventHandler(this.LogSearchTextBox_TextChanged);
            this.LogSearchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LogSearchList_KeyDown);
            // 
            // MonthSelectLogs
            // 
            this.MonthSelectLogs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MonthSelectLogs.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MonthSelectLogs.FormattingEnabled = true;
            this.MonthSelectLogs.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.MonthSelectLogs.Location = new System.Drawing.Point(0, 29);
            this.MonthSelectLogs.Name = "MonthSelectLogs";
            this.MonthSelectLogs.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MonthSelectLogs.Size = new System.Drawing.Size(277, 24);
            this.MonthSelectLogs.TabIndex = 21;
            // 
            // contextLogsMenuStrip1
            // 
            this.contextLogsMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadToolStripMenuItem});
            this.contextLogsMenuStrip1.Name = "contextLogsMenuStrip1";
            this.contextLogsMenuStrip1.Size = new System.Drawing.Size(111, 26);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.ReloadToolStripMenuItem_Click);
            // 
            // VersionTxt
            // 
            this.VersionTxt.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.VersionTxt.Enabled = false;
            this.VersionTxt.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionTxt.Location = new System.Drawing.Point(923, 231);
            this.VersionTxt.MaxLength = 100;
            this.VersionTxt.Name = "VersionTxt";
            this.VersionTxt.Size = new System.Drawing.Size(145, 25);
            this.VersionTxt.TabIndex = 22;
            this.VersionTxt.Text = "Version:";
            this.VersionTxt.WordWrap = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 82);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(277, 20);
            this.tabControl1.TabIndex = 23;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(269, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Logs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(269, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Car Logs";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(269, 0);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "CIP Logs";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // LRButton
            // 
            this.LRButton.BackColor = System.Drawing.Color.Transparent;
            this.LRButton.BackgroundImage = global::mGeek.Properties.Resources.LR;
            this.LRButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LRButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LRButton.FlatAppearance.BorderSize = 0;
            this.LRButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.LRButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.LRButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LRButton.Location = new System.Drawing.Point(648, 58);
            this.LRButton.Name = "LRButton";
            this.LRButton.Size = new System.Drawing.Size(125, 120);
            this.LRButton.TabIndex = 25;
            this.LRButton.UseVisualStyleBackColor = false;
            this.LRButton.Click += new System.EventHandler(this.Button2_Click);
            this.LRButton.MouseEnter += new System.EventHandler(this.Button2_MouseEnter);
            this.LRButton.MouseLeave += new System.EventHandler(this.Button2_MouseLeave);
            // 
            // MercButton
            // 
            this.MercButton.BackColor = System.Drawing.Color.Transparent;
            this.MercButton.BackgroundImage = global::mGeek.Properties.Resources.MERC;
            this.MercButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MercButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MercButton.FlatAppearance.BorderSize = 0;
            this.MercButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.MercButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.MercButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MercButton.Location = new System.Drawing.Point(465, 58);
            this.MercButton.Name = "MercButton";
            this.MercButton.Size = new System.Drawing.Size(125, 120);
            this.MercButton.TabIndex = 24;
            this.MercButton.UseVisualStyleBackColor = false;
            this.MercButton.Click += new System.EventHandler(this.Button1_Click_1);
            this.MercButton.MouseEnter += new System.EventHandler(this.Button1_MouseEnter);
            this.MercButton.MouseLeave += new System.EventHandler(this.Button1_MouseLeave);
            // 
            // BMWButton
            // 
            this.BMWButton.BackColor = System.Drawing.Color.Transparent;
            this.BMWButton.BackgroundImage = global::mGeek.Properties.Resources.CBMW;
            this.BMWButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BMWButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BMWButton.FlatAppearance.BorderSize = 0;
            this.BMWButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BMWButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BMWButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BMWButton.Location = new System.Drawing.Point(294, 58);
            this.BMWButton.Name = "BMWButton";
            this.BMWButton.Size = new System.Drawing.Size(125, 120);
            this.BMWButton.TabIndex = 20;
            this.BMWButton.UseVisualStyleBackColor = false;
            this.BMWButton.Click += new System.EventHandler(this.Button6_Click);
            this.BMWButton.MouseEnter += new System.EventHandler(this.Button6_MouseEnter);
            this.BMWButton.MouseLeave += new System.EventHandler(this.Button6_MouseLeave);
            // 
            // BMWPanel
            // 
            this.BMWPanel.BackColor = System.Drawing.Color.OrangeRed;
            this.BMWPanel.Location = new System.Drawing.Point(294, 185);
            this.BMWPanel.Name = "BMWPanel";
            this.BMWPanel.Size = new System.Drawing.Size(125, 2);
            this.BMWPanel.TabIndex = 26;
            this.BMWPanel.Visible = false;
            // 
            // MERCPanel
            // 
            this.MERCPanel.BackColor = System.Drawing.Color.OrangeRed;
            this.MERCPanel.Location = new System.Drawing.Point(465, 184);
            this.MERCPanel.Name = "MERCPanel";
            this.MERCPanel.Size = new System.Drawing.Size(125, 2);
            this.MERCPanel.TabIndex = 27;
            this.MERCPanel.Visible = false;
            // 
            // LRPanel
            // 
            this.LRPanel.BackColor = System.Drawing.Color.OrangeRed;
            this.LRPanel.Location = new System.Drawing.Point(648, 184);
            this.LRPanel.Name = "LRPanel";
            this.LRPanel.Size = new System.Drawing.Size(125, 2);
            this.LRPanel.TabIndex = 28;
            this.LRPanel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1524, 869);
            this.Controls.Add(this.ListLogs);
            this.Controls.Add(this.LRPanel);
            this.Controls.Add(this.MERCPanel);
            this.Controls.Add(this.BMWPanel);
            this.Controls.Add(this.LRButton);
            this.Controls.Add(this.MercButton);
            this.Controls.Add(this.SearchLogsButton);
            this.Controls.Add(this.LogSearchTextBox);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(mTxtBox);
            this.Controls.Add(this.VersionTxt);
            this.Controls.Add(this.MonthSelectLogs);
            this.Controls.Add(this.BMWButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TimeDebug);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "mGeek";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ListContextMenu.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextLogsMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem mGeekToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ListBox ListLogs;
        private System.Windows.Forms.Button SearchLogsButton;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ToolStripMenuItem debugTimeToolStripMenuItem;
        private System.Windows.Forms.Label TimeDebug;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BMWButton;
        private System.Windows.Forms.TextBox LogSearchTextBox;
        private System.Windows.Forms.ComboBox MonthSelectLogs;
        private System.Windows.Forms.ContextMenuStrip contextLogsMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.TextBox VersionTxt;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button MercButton;
        private System.Windows.Forms.Button LRButton;
        private System.Windows.Forms.Panel BMWPanel;
        private System.Windows.Forms.Panel MERCPanel;
        private System.Windows.Forms.Panel LRPanel;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ListContextMenu;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem refreshLogsListToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem codingFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem driveDataFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem datasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataDumpToolStripMenuItem;
        public static System.Windows.Forms.RichTextBox mTxtBox;
    }
}

