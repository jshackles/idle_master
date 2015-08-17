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
            this.lblHoursPlayed = new System.Windows.Forms.Label();
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
            resources.ApplyResources(this.lblCookieStatus, "lblCookieStatus");
            this.lblCookieStatus.Name = "lblCookieStatus";
            // 
            // tmrCheckCookieData
            // 
            this.tmrCheckCookieData.Enabled = true;
            this.tmrCheckCookieData.Tick += new System.EventHandler(this.tmrCheckCookieData_Tick);
            // 
            // lblSteamStatus
            // 
            resources.ApplyResources(this.lblSteamStatus, "lblSteamStatus");
            this.lblSteamStatus.Name = "lblSteamStatus";
            // 
            // tmrCheckSteam
            // 
            this.tmrCheckSteam.Enabled = true;
            this.tmrCheckSteam.Interval = 500;
            this.tmrCheckSteam.Tick += new System.EventHandler(this.tmrCheckSteam_Tick);
            // 
            // lnkResetCookies
            // 
            resources.ApplyResources(this.lnkResetCookies, "lnkResetCookies");
            this.lnkResetCookies.Name = "lnkResetCookies";
            this.lnkResetCookies.TabStop = true;
            this.lnkResetCookies.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkResetCookies_LinkClicked);
            // 
            // lnkSignIn
            // 
            resources.ApplyResources(this.lnkSignIn, "lnkSignIn");
            this.lnkSignIn.Name = "lnkSignIn";
            this.lnkSignIn.TabStop = true;
            this.lnkSignIn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSignIn_LinkClicked);
            // 
            // lblDrops
            // 
            resources.ApplyResources(this.lblDrops, "lblDrops");
            this.lblDrops.Name = "lblDrops";
            // 
            // lblIdle
            // 
            resources.ApplyResources(this.lblIdle, "lblIdle");
            this.lblIdle.Name = "lblIdle";
            // 
            // lblCurrentStatus
            // 
            resources.ApplyResources(this.lblCurrentStatus, "lblCurrentStatus");
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            // 
            // lblCurrentRemaining
            // 
            resources.ApplyResources(this.lblCurrentRemaining, "lblCurrentRemaining");
            this.lblCurrentRemaining.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCurrentRemaining.ForeColor = System.Drawing.Color.Blue;
            this.lblCurrentRemaining.Name = "lblCurrentRemaining";
            this.lblCurrentRemaining.Click += new System.EventHandler(this.lblCurrentRemaining_Click);
            // 
            // lblGameName
            // 
            resources.ApplyResources(this.lblGameName, "lblGameName");
            this.lblGameName.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblGameName.Name = "lblGameName";
            this.lblGameName.TabStop = true;
            this.lblGameName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblGameName_LinkClicked);
            // 
            // mnuTop
            // 
            resources.ApplyResources(this.mnuTop, "mnuTop");
            this.mnuTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gameToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mnuTop.Name = "mnuTop";
            this.mnuTop.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // fileToolStripMenuItem
            // 
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.blacklistToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            // 
            // settingsToolStripMenuItem
            // 
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            this.settingsToolStripMenuItem.Image = global::IdleMaster.Properties.Resources.imgSettings;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // blacklistToolStripMenuItem
            // 
            resources.ApplyResources(this.blacklistToolStripMenuItem, "blacklistToolStripMenuItem");
            this.blacklistToolStripMenuItem.Image = global::IdleMaster.Properties.Resources.imgBlacklist;
            this.blacklistToolStripMenuItem.Name = "blacklistToolStripMenuItem";
            this.blacklistToolStripMenuItem.Click += new System.EventHandler(this.blacklistToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // exitToolStripMenuItem
            // 
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Image = global::IdleMaster.Properties.Resources.imgExit;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // gameToolStripMenuItem
            // 
            resources.ApplyResources(this.gameToolStripMenuItem, "gameToolStripMenuItem");
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pauseIdlingToolStripMenuItem,
            this.resumeIdlingToolStripMenuItem,
            this.skipGameToolStripMenuItem,
            this.toolStripMenuItem2,
            this.blacklistCurrentGameToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            // 
            // pauseIdlingToolStripMenuItem
            // 
            resources.ApplyResources(this.pauseIdlingToolStripMenuItem, "pauseIdlingToolStripMenuItem");
            this.pauseIdlingToolStripMenuItem.Name = "pauseIdlingToolStripMenuItem";
            this.pauseIdlingToolStripMenuItem.Click += new System.EventHandler(this.pauseIdlingToolStripMenuItem_Click);
            // 
            // resumeIdlingToolStripMenuItem
            // 
            resources.ApplyResources(this.resumeIdlingToolStripMenuItem, "resumeIdlingToolStripMenuItem");
            this.resumeIdlingToolStripMenuItem.Name = "resumeIdlingToolStripMenuItem";
            this.resumeIdlingToolStripMenuItem.Click += new System.EventHandler(this.resumeIdlingToolStripMenuItem_Click);
            // 
            // skipGameToolStripMenuItem
            // 
            resources.ApplyResources(this.skipGameToolStripMenuItem, "skipGameToolStripMenuItem");
            this.skipGameToolStripMenuItem.Name = "skipGameToolStripMenuItem";
            this.skipGameToolStripMenuItem.Click += new System.EventHandler(this.skipGameToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            // 
            // blacklistCurrentGameToolStripMenuItem
            // 
            resources.ApplyResources(this.blacklistCurrentGameToolStripMenuItem, "blacklistCurrentGameToolStripMenuItem");
            this.blacklistCurrentGameToolStripMenuItem.Image = global::IdleMaster.Properties.Resources.imgBlacklist;
            this.blacklistCurrentGameToolStripMenuItem.Name = "blacklistCurrentGameToolStripMenuItem";
            this.blacklistCurrentGameToolStripMenuItem.Click += new System.EventHandler(this.blacklistCurrentGameToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changelogToolStripMenuItem,
            this.officialGroupToolStripMenuItem,
            this.toolStripMenuItem3,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            // 
            // changelogToolStripMenuItem
            // 
            resources.ApplyResources(this.changelogToolStripMenuItem, "changelogToolStripMenuItem");
            this.changelogToolStripMenuItem.Image = global::IdleMaster.Properties.Resources.imgDocument;
            this.changelogToolStripMenuItem.Name = "changelogToolStripMenuItem";
            this.changelogToolStripMenuItem.Click += new System.EventHandler(this.changelogToolStripMenuItem_Click);
            // 
            // officialGroupToolStripMenuItem
            // 
            resources.ApplyResources(this.officialGroupToolStripMenuItem, "officialGroupToolStripMenuItem");
            this.officialGroupToolStripMenuItem.Image = global::IdleMaster.Properties.Resources.imgGlobe;
            this.officialGroupToolStripMenuItem.Name = "officialGroupToolStripMenuItem";
            this.officialGroupToolStripMenuItem.Click += new System.EventHandler(this.officialGroupToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            // 
            // aboutToolStripMenuItem
            // 
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Image = global::IdleMaster.Properties.Resources.imgInfo;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
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
            resources.ApplyResources(this.ssFooter, "ssFooter");
            this.ssFooter.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ssFooter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbIdle,
            this.toolStripStatusLabel1,
            this.lblTimer});
            this.ssFooter.Name = "ssFooter";
            this.ssFooter.SizingGrip = false;
            // 
            // pbIdle
            // 
            resources.ApplyResources(this.pbIdle, "pbIdle");
            this.pbIdle.Name = "pbIdle";
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // lblTimer
            // 
            resources.ApplyResources(this.lblTimer, "lblTimer");
            this.lblTimer.Name = "lblTimer";
            // 
            // notifyIcon1
            // 
            resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // picReadingPage
            // 
            resources.ApplyResources(this.picReadingPage, "picReadingPage");
            this.picReadingPage.Image = global::IdleMaster.Properties.Resources.imgSpin;
            this.picReadingPage.Name = "picReadingPage";
            this.picReadingPage.TabStop = false;
            // 
            // btnSkip
            // 
            resources.ApplyResources(this.btnSkip, "btnSkip");
            this.btnSkip.Image = global::IdleMaster.Properties.Resources.imgSkipSmall;
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // picIdleStatus
            // 
            resources.ApplyResources(this.picIdleStatus, "picIdleStatus");
            this.picIdleStatus.Name = "picIdleStatus";
            this.picIdleStatus.TabStop = false;
            // 
            // picCookieStatus
            // 
            resources.ApplyResources(this.picCookieStatus, "picCookieStatus");
            this.picCookieStatus.Name = "picCookieStatus";
            this.picCookieStatus.TabStop = false;
            // 
            // picSteamStatus
            // 
            resources.ApplyResources(this.picSteamStatus, "picSteamStatus");
            this.picSteamStatus.Name = "picSteamStatus";
            this.picSteamStatus.TabStop = false;
            // 
            // picApp
            // 
            resources.ApplyResources(this.picApp, "picApp");
            this.picApp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picApp.Name = "picApp";
            this.picApp.TabStop = false;
            // 
            // btnPause
            // 
            resources.ApplyResources(this.btnPause, "btnPause");
            this.btnPause.Image = global::IdleMaster.Properties.Resources.imgPauseSmall;
            this.btnPause.Name = "btnPause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnResume
            // 
            resources.ApplyResources(this.btnResume, "btnResume");
            this.btnResume.Image = global::IdleMaster.Properties.Resources.imgPlaySmall;
            this.btnResume.Name = "btnResume";
            this.btnResume.UseVisualStyleBackColor = true;
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
            resources.ApplyResources(this.lblSignedOnAs, "lblSignedOnAs");
            this.lblSignedOnAs.Name = "lblSignedOnAs";
            // 
            // GamesState
            // 
            resources.ApplyResources(this.GamesState, "GamesState");
            this.GamesState.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.GameName,
            this.Hours});
            this.GamesState.Name = "GamesState";
            this.GamesState.UseCompatibleStateImageBehavior = false;
            this.GamesState.View = System.Windows.Forms.View.Details;
            // 
            // GameName
            // 
            this.GameName.Tag = "";
            resources.ApplyResources(this.GameName, "GameName");
            // 
            // Hours
            // 
            resources.ApplyResources(this.Hours, "Hours");
            // 
            // lblHoursPlayed
            // 
            resources.ApplyResources(this.lblHoursPlayed, "lblHoursPlayed");
            this.lblHoursPlayed.Name = "lblHoursPlayed";
            // 
            // frmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lblHoursPlayed);
            this.Controls.Add(this.GamesState);
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
            this.Controls.Add(this.picApp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mnuTop;
            this.MaximizeBox = false;
            this.Name = "frmMain";
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
    private Label lblHoursPlayed;
  }
}

