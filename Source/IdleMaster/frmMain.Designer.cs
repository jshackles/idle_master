using System.ComponentModel;
using System.Windows.Forms;

namespace IdleMaster
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
        private void InitializeComponent()
        {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      this.lblCookieStatus = new System.Windows.Forms.Label();
      this.tmrCheckCookieData = new System.Windows.Forms.Timer(this.components);
      this.lblSteamStatus = new System.Windows.Forms.Label();
      this.tmrCheckSteam = new System.Windows.Forms.Timer(this.components);
      this.lnkResetCookies = new System.Windows.Forms.LinkLabel();
      this.lnkSignIn = new System.Windows.Forms.LinkLabel();
      this.lblDrops = new System.Windows.Forms.Label();
      this.lblIdle = new System.Windows.Forms.Label();
      this.lblCurrentStatus = new System.Windows.Forms.Label();
      this.lblCurrentRemaining = new System.Windows.Forms.Label();
      this.lblGameName = new System.Windows.Forms.LinkLabel();
      this.mnuTop = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.blacklistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pauseIdlingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.resumeIdlingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.skipGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.blacklistCurrentGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.changelogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.officialGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tmrReadyToGo = new System.Windows.Forms.Timer(this.components);
      this.tmrCardDropCheck = new System.Windows.Forms.Timer(this.components);
      this.ssFooter = new System.Windows.Forms.StatusStrip();
      this.pbIdle = new System.Windows.Forms.ToolStripProgressBar();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.lblTimer = new System.Windows.Forms.ToolStripStatusLabel();
      this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
      this.picReadingPage = new System.Windows.Forms.PictureBox();
      this.btnSkip = new System.Windows.Forms.Button();
      this.picIdleStatus = new System.Windows.Forms.PictureBox();
      this.picCookieStatus = new System.Windows.Forms.PictureBox();
      this.picSteamStatus = new System.Windows.Forms.PictureBox();
      this.picApp = new System.Windows.Forms.PictureBox();
      this.btnPause = new System.Windows.Forms.Button();
      this.btnResume = new System.Windows.Forms.Button();
      this.tmrStartNext = new System.Windows.Forms.Timer(this.components);
      this.tmrBadgeReload = new System.Windows.Forms.Timer(this.components);
      this.lblSignedOnAs = new System.Windows.Forms.Label();
      this.GamesState = new System.Windows.Forms.ListView();
      this.GameName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.Hours = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.mnuTop.SuspendLayout();
      this.ssFooter.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picReadingPage)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picIdleStatus)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picCookieStatus)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picSteamStatus)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picApp)).BeginInit();
      this.SuspendLayout();
      // 
      // lblCookieStatus
      // 
      this.lblCookieStatus.AutoSize = true;
      this.lblCookieStatus.Location = new System.Drawing.Point(41, 71);
      this.lblCookieStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblCookieStatus.Name = "lblCookieStatus";
      this.lblCookieStatus.Size = new System.Drawing.Size(245, 17);
      this.lblCookieStatus.TabIndex = 0;
      this.lblCookieStatus.Text = "Idle Master is not connected to Steam";
      // 
      // tmrCheckCookieData
      // 
      this.tmrCheckCookieData.Enabled = true;
      this.tmrCheckCookieData.Tick += new System.EventHandler(this.tmrCheckCookieData_Tick);
      // 
      // lblSteamStatus
      // 
      this.lblSteamStatus.AutoSize = true;
      this.lblSteamStatus.Location = new System.Drawing.Point(40, 44);
      this.lblSteamStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblSteamStatus.Name = "lblSteamStatus";
      this.lblSteamStatus.Size = new System.Drawing.Size(138, 17);
      this.lblSteamStatus.TabIndex = 3;
      this.lblSteamStatus.Text = "Steam is not running";
      // 
      // tmrCheckSteam
      // 
      this.tmrCheckSteam.Enabled = true;
      this.tmrCheckSteam.Interval = 500;
      this.tmrCheckSteam.Tick += new System.EventHandler(this.tmrCheckSteam_Tick);
      // 
      // lnkResetCookies
      // 
      this.lnkResetCookies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lnkResetCookies.AutoSize = true;
      this.lnkResetCookies.Location = new System.Drawing.Point(259, 71);
      this.lnkResetCookies.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lnkResetCookies.Name = "lnkResetCookies";
      this.lnkResetCookies.Size = new System.Drawing.Size(70, 17);
      this.lnkResetCookies.TabIndex = 4;
      this.lnkResetCookies.TabStop = true;
      this.lnkResetCookies.Text = "(Sign out)";
      this.lnkResetCookies.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkResetCookies_LinkClicked);
      // 
      // lnkSignIn
      // 
      this.lnkSignIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lnkSignIn.AutoSize = true;
      this.lnkSignIn.Location = new System.Drawing.Point(272, 71);
      this.lnkSignIn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lnkSignIn.Name = "lnkSignIn";
      this.lnkSignIn.Size = new System.Drawing.Size(61, 17);
      this.lnkSignIn.TabIndex = 5;
      this.lnkSignIn.TabStop = true;
      this.lnkSignIn.Text = "(Sign in)";
      this.lnkSignIn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSignIn_LinkClicked);
      // 
      // lblDrops
      // 
      this.lblDrops.AutoSize = true;
      this.lblDrops.Location = new System.Drawing.Point(41, 113);
      this.lblDrops.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblDrops.Name = "lblDrops";
      this.lblDrops.Size = new System.Drawing.Size(142, 17);
      this.lblDrops.TabIndex = 9;
      this.lblDrops.Text = "card drops remaining";
      this.lblDrops.Visible = false;
      // 
      // lblIdle
      // 
      this.lblIdle.AutoSize = true;
      this.lblIdle.Location = new System.Drawing.Point(41, 133);
      this.lblIdle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblIdle.Name = "lblIdle";
      this.lblIdle.Size = new System.Drawing.Size(115, 17);
      this.lblIdle.TabIndex = 10;
      this.lblIdle.Text = "games left to idle";
      this.lblIdle.Visible = false;
      // 
      // lblCurrentStatus
      // 
      this.lblCurrentStatus.AutoSize = true;
      this.lblCurrentStatus.Location = new System.Drawing.Point(20, 166);
      this.lblCurrentStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblCurrentStatus.Name = "lblCurrentStatus";
      this.lblCurrentStatus.Size = new System.Drawing.Size(120, 17);
      this.lblCurrentStatus.TabIndex = 11;
      this.lblCurrentStatus.Text = "Currently in-game";
      // 
      // lblCurrentRemaining
      // 
      this.lblCurrentRemaining.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lblCurrentRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCurrentRemaining.ForeColor = System.Drawing.Color.Blue;
      this.lblCurrentRemaining.Location = new System.Drawing.Point(227, 359);
      this.lblCurrentRemaining.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblCurrentRemaining.Name = "lblCurrentRemaining";
      this.lblCurrentRemaining.Size = new System.Drawing.Size(165, 23);
      this.lblCurrentRemaining.TabIndex = 12;
      this.lblCurrentRemaining.Text = "3 card drops remaining";
      this.lblCurrentRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lblCurrentRemaining.Click += new System.EventHandler(this.lblCurrentRemaining_Click);
      // 
      // lblGameName
      // 
      this.lblGameName.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.lblGameName.ForeColor = System.Drawing.Color.DodgerBlue;
      this.lblGameName.Location = new System.Drawing.Point(128, 166);
      this.lblGameName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblGameName.Name = "lblGameName";
      this.lblGameName.Size = new System.Drawing.Size(212, 20);
      this.lblGameName.TabIndex = 16;
      this.lblGameName.TabStop = true;
      this.lblGameName.Text = "Game Name";
      this.lblGameName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblGameName_LinkClicked);
      // 
      // mnuTop
      // 
      this.mnuTop.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.mnuTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gameToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.mnuTop.Location = new System.Drawing.Point(0, 0);
      this.mnuTop.Name = "mnuTop";
      this.mnuTop.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
      this.mnuTop.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.mnuTop.Size = new System.Drawing.Size(405, 28);
      this.mnuTop.TabIndex = 19;
      this.mnuTop.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.blacklistToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
      this.fileToolStripMenuItem.Text = "&File";
      // 
      // settingsToolStripMenuItem
      // 
      this.settingsToolStripMenuItem.Image = global::IdleMaster.Properties.Resources.imgSettings;
      this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
      this.settingsToolStripMenuItem.Size = new System.Drawing.Size(138, 26);
      this.settingsToolStripMenuItem.Text = "&Settings";
      this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
      // 
      // blacklistToolStripMenuItem
      // 
      this.blacklistToolStripMenuItem.Image = global::IdleMaster.Properties.Resources.imgBlacklist;
      this.blacklistToolStripMenuItem.Name = "blacklistToolStripMenuItem";
      this.blacklistToolStripMenuItem.Size = new System.Drawing.Size(138, 26);
      this.blacklistToolStripMenuItem.Text = "&Blacklist";
      this.blacklistToolStripMenuItem.Click += new System.EventHandler(this.blacklistToolStripMenuItem_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(135, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Image = global::IdleMaster.Properties.Resources.imgExit;
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(138, 26);
      this.exitToolStripMenuItem.Text = "E&xit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // gameToolStripMenuItem
      // 
      this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pauseIdlingToolStripMenuItem,
            this.resumeIdlingToolStripMenuItem,
            this.skipGameToolStripMenuItem,
            this.toolStripMenuItem2,
            this.blacklistCurrentGameToolStripMenuItem});
      this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
      this.gameToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
      this.gameToolStripMenuItem.Text = "&Game";
      // 
      // pauseIdlingToolStripMenuItem
      // 
      this.pauseIdlingToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pauseIdlingToolStripMenuItem.Image")));
      this.pauseIdlingToolStripMenuItem.Name = "pauseIdlingToolStripMenuItem";
      this.pauseIdlingToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
      this.pauseIdlingToolStripMenuItem.Text = "&Pause Idling";
      this.pauseIdlingToolStripMenuItem.Click += new System.EventHandler(this.pauseIdlingToolStripMenuItem_Click);
      // 
      // resumeIdlingToolStripMenuItem
      // 
      this.resumeIdlingToolStripMenuItem.Enabled = false;
      this.resumeIdlingToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resumeIdlingToolStripMenuItem.Image")));
      this.resumeIdlingToolStripMenuItem.Name = "resumeIdlingToolStripMenuItem";
      this.resumeIdlingToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
      this.resumeIdlingToolStripMenuItem.Text = "Resume Idling";
      this.resumeIdlingToolStripMenuItem.Click += new System.EventHandler(this.resumeIdlingToolStripMenuItem_Click);
      // 
      // skipGameToolStripMenuItem
      // 
      this.skipGameToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("skipGameToolStripMenuItem.Image")));
      this.skipGameToolStripMenuItem.Name = "skipGameToolStripMenuItem";
      this.skipGameToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
      this.skipGameToolStripMenuItem.Text = "&Skip Current Game";
      this.skipGameToolStripMenuItem.Click += new System.EventHandler(this.skipGameToolStripMenuItem_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(230, 6);
      // 
      // blacklistCurrentGameToolStripMenuItem
      // 
      this.blacklistCurrentGameToolStripMenuItem.Image = global::IdleMaster.Properties.Resources.imgBlacklist;
      this.blacklistCurrentGameToolStripMenuItem.Name = "blacklistCurrentGameToolStripMenuItem";
      this.blacklistCurrentGameToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
      this.blacklistCurrentGameToolStripMenuItem.Text = "&Blacklist Current Game";
      this.blacklistCurrentGameToolStripMenuItem.Click += new System.EventHandler(this.blacklistCurrentGameToolStripMenuItem_Click);
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changelogToolStripMenuItem,
            this.officialGroupToolStripMenuItem,
            this.toolStripMenuItem3,
            this.aboutToolStripMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
      this.helpToolStripMenuItem.Text = "&Help";
      // 
      // changelogToolStripMenuItem
      // 
      this.changelogToolStripMenuItem.Image = global::IdleMaster.Properties.Resources.imgDocument;
      this.changelogToolStripMenuItem.Name = "changelogToolStripMenuItem";
      this.changelogToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
      this.changelogToolStripMenuItem.Text = "&Release Notes";
      this.changelogToolStripMenuItem.Click += new System.EventHandler(this.changelogToolStripMenuItem_Click);
      // 
      // officialGroupToolStripMenuItem
      // 
      this.officialGroupToolStripMenuItem.Image = global::IdleMaster.Properties.Resources.imgGlobe;
      this.officialGroupToolStripMenuItem.Name = "officialGroupToolStripMenuItem";
      this.officialGroupToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
      this.officialGroupToolStripMenuItem.Text = "&Official Group";
      this.officialGroupToolStripMenuItem.Click += new System.EventHandler(this.officialGroupToolStripMenuItem_Click);
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(175, 6);
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.Image = global::IdleMaster.Properties.Resources.imgInfo;
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
      this.aboutToolStripMenuItem.Text = "&About";
      this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
      // 
      // tmrReadyToGo
      // 
      this.tmrReadyToGo.Enabled = true;
      this.tmrReadyToGo.Tick += new System.EventHandler(this.tmrReadyToGo_Tick);
      // 
      // tmrCardDropCheck
      // 
      this.tmrCardDropCheck.Interval = 1000;
      this.tmrCardDropCheck.Tick += new System.EventHandler(this.tmrCardDropCheck_Tick);
      // 
      // ssFooter
      // 
      this.ssFooter.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.ssFooter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbIdle,
            this.toolStripStatusLabel1,
            this.lblTimer});
      this.ssFooter.Location = new System.Drawing.Point(0, 389);
      this.ssFooter.Name = "ssFooter";
      this.ssFooter.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
      this.ssFooter.Size = new System.Drawing.Size(405, 26);
      this.ssFooter.SizingGrip = false;
      this.ssFooter.TabIndex = 20;
      this.ssFooter.Text = "statusStrip1";
      this.ssFooter.Visible = false;
      // 
      // pbIdle
      // 
      this.pbIdle.Name = "pbIdle";
      this.pbIdle.Size = new System.Drawing.Size(251, 20);
      // 
      // toolStripStatusLabel1
      // 
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new System.Drawing.Size(81, 21);
      this.toolStripStatusLabel1.Text = "Next check";
      // 
      // lblTimer
      // 
      this.lblTimer.Name = "lblTimer";
      this.lblTimer.Size = new System.Drawing.Size(44, 21);
      this.lblTimer.Text = "15:00";
      // 
      // notifyIcon1
      // 
      this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
      this.notifyIcon1.Text = "Idle Master";
      this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
      // 
      // picReadingPage
      // 
      this.picReadingPage.Image = global::IdleMaster.Properties.Resources.imgSpin;
      this.picReadingPage.Location = new System.Drawing.Point(20, 111);
      this.picReadingPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.picReadingPage.Name = "picReadingPage";
      this.picReadingPage.Size = new System.Drawing.Size(20, 20);
      this.picReadingPage.TabIndex = 26;
      this.picReadingPage.TabStop = false;
      this.picReadingPage.Visible = false;
      // 
      // btnSkip
      // 
      this.btnSkip.Image = global::IdleMaster.Properties.Resources.imgSkipSmall;
      this.btnSkip.Location = new System.Drawing.Point(365, 166);
      this.btnSkip.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.btnSkip.Name = "btnSkip";
      this.btnSkip.Size = new System.Drawing.Size(20, 20);
      this.btnSkip.TabIndex = 23;
      this.btnSkip.UseVisualStyleBackColor = true;
      this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
      // 
      // picIdleStatus
      // 
      this.picIdleStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.picIdleStatus.Location = new System.Drawing.Point(381, 393);
      this.picIdleStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.picIdleStatus.Name = "picIdleStatus";
      this.picIdleStatus.Size = new System.Drawing.Size(20, 20);
      this.picIdleStatus.TabIndex = 15;
      this.picIdleStatus.TabStop = false;
      // 
      // picCookieStatus
      // 
      this.picCookieStatus.Location = new System.Drawing.Point(20, 70);
      this.picCookieStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.picCookieStatus.Name = "picCookieStatus";
      this.picCookieStatus.Size = new System.Drawing.Size(20, 20);
      this.picCookieStatus.TabIndex = 8;
      this.picCookieStatus.TabStop = false;
      // 
      // picSteamStatus
      // 
      this.picSteamStatus.Location = new System.Drawing.Point(20, 42);
      this.picSteamStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.picSteamStatus.Name = "picSteamStatus";
      this.picSteamStatus.Size = new System.Drawing.Size(20, 20);
      this.picSteamStatus.TabIndex = 7;
      this.picSteamStatus.TabStop = false;
      // 
      // picApp
      // 
      this.picApp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picApp.Location = new System.Drawing.Point(20, 190);
      this.picApp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.picApp.Name = "picApp";
      this.picApp.Size = new System.Drawing.Size(365, 169);
      this.picApp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.picApp.TabIndex = 6;
      this.picApp.TabStop = false;
      this.picApp.Visible = false;
      // 
      // btnPause
      // 
      this.btnPause.Image = global::IdleMaster.Properties.Resources.imgPauseSmall;
      this.btnPause.Location = new System.Drawing.Point(345, 166);
      this.btnPause.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.btnPause.Name = "btnPause";
      this.btnPause.Size = new System.Drawing.Size(20, 20);
      this.btnPause.TabIndex = 22;
      this.btnPause.UseVisualStyleBackColor = true;
      this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
      // 
      // btnResume
      // 
      this.btnResume.Image = global::IdleMaster.Properties.Resources.imgPlaySmall;
      this.btnResume.Location = new System.Drawing.Point(345, 166);
      this.btnResume.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.btnResume.Name = "btnResume";
      this.btnResume.Size = new System.Drawing.Size(20, 20);
      this.btnResume.TabIndex = 24;
      this.btnResume.UseVisualStyleBackColor = true;
      this.btnResume.Visible = false;
      this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
      // 
      // tmrStartNext
      // 
      this.tmrStartNext.Tick += new System.EventHandler(this.tmrStartNext_Tick);
      // 
      // tmrBadgeReload
      // 
      this.tmrBadgeReload.Interval = 1000;
      this.tmrBadgeReload.Tick += new System.EventHandler(this.tmrBadgeReload_Tick);
      // 
      // lblSignedOnAs
      // 
      this.lblSignedOnAs.AutoSize = true;
      this.lblSignedOnAs.Location = new System.Drawing.Point(40, 87);
      this.lblSignedOnAs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblSignedOnAs.Name = "lblSignedOnAs";
      this.lblSignedOnAs.Size = new System.Drawing.Size(91, 17);
      this.lblSignedOnAs.TabIndex = 27;
      this.lblSignedOnAs.Text = "Signed on as";
      this.lblSignedOnAs.Visible = false;
      // 
      // GamesState
      // 
      this.GamesState.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.GameName,
            this.Hours});
      this.GamesState.Location = new System.Drawing.Point(20, 189);
      this.GamesState.Name = "GamesState";
      this.GamesState.Size = new System.Drawing.Size(365, 170);
      this.GamesState.TabIndex = 28;
      this.GamesState.UseCompatibleStateImageBehavior = false;
      this.GamesState.View = System.Windows.Forms.View.Details;
      this.GamesState.Visible = false;
      // 
      // GameName
      // 
      this.GameName.Tag = "";
      this.GameName.Text = "Name";
      this.GameName.Width = 280;
      // 
      // Hours
      // 
      this.Hours.Text = "Hours";
      this.Hours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.Hours.Width = 70;
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(405, 417);
      this.Controls.Add(this.lblSignedOnAs);
      this.Controls.Add(this.picReadingPage);
      this.Controls.Add(this.btnSkip);
      this.Controls.Add(this.picIdleStatus);
      this.Controls.Add(this.lblCurrentRemaining);
      this.Controls.Add(this.lblCurrentStatus);
      this.Controls.Add(this.lblIdle);
      this.Controls.Add(this.lblDrops);
      this.Controls.Add(this.picCookieStatus);
      this.Controls.Add(this.picSteamStatus);
      this.Controls.Add(this.lnkSignIn);
      this.Controls.Add(this.lnkResetCookies);
      this.Controls.Add(this.lblSteamStatus);
      this.Controls.Add(this.lblCookieStatus);
      this.Controls.Add(this.mnuTop);
      this.Controls.Add(this.ssFooter);
      this.Controls.Add(this.btnPause);
      this.Controls.Add(this.btnResume);
      this.Controls.Add(this.lblGameName);
      this.Controls.Add(this.GamesState);
      this.Controls.Add(this.picApp);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuTop;
      this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.MaximizeBox = false;
      this.Name = "frmMain";
      this.Text = "Idle Master";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClose);
      this.Load += new System.EventHandler(this.frmMain_Load);
      this.Resize += new System.EventHandler(this.frmMain_Resize);
      this.mnuTop.ResumeLayout(false);
      this.mnuTop.PerformLayout();
      this.ssFooter.ResumeLayout(false);
      this.ssFooter.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picReadingPage)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picIdleStatus)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picCookieStatus)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picSteamStatus)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picApp)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private Label lblCookieStatus;
        private Timer tmrCheckCookieData;
        private Label lblSteamStatus;
        private Timer tmrCheckSteam;
        private LinkLabel lnkResetCookies;
        private LinkLabel lnkSignIn;
        private PictureBox picApp;
        private PictureBox picSteamStatus;
        private PictureBox picCookieStatus;
        private Label lblDrops;
        private Label lblIdle;
        private Label lblCurrentStatus;
        private Label lblCurrentRemaining;
        private PictureBox picIdleStatus;
        private LinkLabel lblGameName;
        private MenuStrip mnuTop;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private Timer tmrReadyToGo;
        private Timer tmrCardDropCheck;
        private StatusStrip ssFooter;
        private ToolStripProgressBar pbIdle;
        private ToolStripStatusLabel lblTimer;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Button btnPause;
        private Button btnSkip;
        private Button btnResume;
        private ToolStripMenuItem gameToolStripMenuItem;
        private ToolStripMenuItem pauseIdlingToolStripMenuItem;
        private ToolStripMenuItem resumeIdlingToolStripMenuItem;
        private ToolStripMenuItem skipGameToolStripMenuItem;
        private NotifyIcon notifyIcon1;
        private PictureBox picReadingPage;
        private ToolStripMenuItem blacklistToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem blacklistCurrentGameToolStripMenuItem;
        private Timer tmrStartNext;
        private ToolStripMenuItem changelogToolStripMenuItem;
        private ToolStripMenuItem officialGroupToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem3;
        private Timer tmrBadgeReload;
        private Label lblSignedOnAs;
    private ListView GamesState;
    private ColumnHeader GameName;
    private ColumnHeader Hours;
  }
}

