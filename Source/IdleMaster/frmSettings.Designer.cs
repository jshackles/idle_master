using System.ComponentModel;
using System.Windows.Forms;

namespace IdleMaster
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.grpGeneral = new System.Windows.Forms.GroupBox();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.chkShowUsername = new System.Windows.Forms.CheckBox();
            this.chkIgnoreClientStatus = new System.Windows.Forms.CheckBox();
            this.chkMinToTray = new System.Windows.Forms.CheckBox();
            this.grpPriority = new System.Windows.Forms.GroupBox();
            this.radIdleMostValue = new System.Windows.Forms.RadioButton();
            this.radIdleLeastDrops = new System.Windows.Forms.RadioButton();
            this.radIdleMostDrops = new System.Windows.Forms.RadioButton();
            this.radIdleDefault = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.ttHints = new System.Windows.Forms.ToolTip(this.components);
            this.btnAdvanced = new System.Windows.Forms.Button();
            this.grpIdlingQuantity = new System.Windows.Forms.GroupBox();
            this.radManyThenOne = new System.Windows.Forms.RadioButton();
            this.radOneGameOnly = new System.Windows.Forms.RadioButton();
            this.grpGeneral.SuspendLayout();
            this.grpPriority.SuspendLayout();
            this.grpIdlingQuantity.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpGeneral
            // 
            this.grpGeneral.Controls.Add(this.cboLanguage);
            this.grpGeneral.Controls.Add(this.lblLanguage);
            this.grpGeneral.Controls.Add(this.chkShowUsername);
            this.grpGeneral.Controls.Add(this.chkIgnoreClientStatus);
            this.grpGeneral.Controls.Add(this.chkMinToTray);
            this.grpGeneral.Location = new System.Drawing.Point(13, 13);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(322, 106);
            this.grpGeneral.TabIndex = 0;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // cboLanguage
            // 
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Items.AddRange(new object[] {
            "English",
            "Bulgarian",
            "Chinese (Simplified, China)",
            "Chinese (Traditional, China)",
            "Czech",
            "Danish",
            "Dutch",
            "Finnish",
            "French",
            "German",
            "Greek",
            "Hungarian",
            "Italian",
            "Japanese",
            "Korean",
            "Norwegian",
            "Polish",
            "Portuguese",
            "Portuguese (Brazil)",
            "Romanian",
            "Russian",
            "Spanish",
            "Swedish",
            "Thai",
            "Turkish",
            "Ukrainian"});
            this.cboLanguage.Location = new System.Drawing.Point(133, 77);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(180, 21);
            this.cboLanguage.TabIndex = 4;
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(26, 79);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(103, 13);
            this.lblLanguage.TabIndex = 3;
            this.lblLanguage.Text = "Interface Language:";
            // 
            // chkShowUsername
            // 
            this.chkShowUsername.AutoSize = true;
            this.chkShowUsername.Location = new System.Drawing.Point(8, 57);
            this.chkShowUsername.Name = "chkShowUsername";
            this.chkShowUsername.Size = new System.Drawing.Size(219, 17);
            this.chkShowUsername.TabIndex = 2;
            this.chkShowUsername.Text = "Show Steam username of signed on user";
            this.chkShowUsername.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreClientStatus
            // 
            this.chkIgnoreClientStatus.AutoSize = true;
            this.chkIgnoreClientStatus.Location = new System.Drawing.Point(8, 38);
            this.chkIgnoreClientStatus.Name = "chkIgnoreClientStatus";
            this.chkIgnoreClientStatus.Size = new System.Drawing.Size(148, 17);
            this.chkIgnoreClientStatus.TabIndex = 1;
            this.chkIgnoreClientStatus.Text = "Ignore Steam client status";
            this.chkIgnoreClientStatus.UseVisualStyleBackColor = true;
            // 
            // chkMinToTray
            // 
            this.chkMinToTray.AutoSize = true;
            this.chkMinToTray.Location = new System.Drawing.Point(8, 19);
            this.chkMinToTray.Name = "chkMinToTray";
            this.chkMinToTray.Size = new System.Drawing.Size(188, 17);
            this.chkMinToTray.TabIndex = 0;
            this.chkMinToTray.Text = "Minimize Idle Master to system tray";
            this.chkMinToTray.UseVisualStyleBackColor = true;
            // 
            // grpPriority
            // 
            this.grpPriority.Controls.Add(this.radIdleMostValue);
            this.grpPriority.Controls.Add(this.radIdleLeastDrops);
            this.grpPriority.Controls.Add(this.radIdleMostDrops);
            this.grpPriority.Controls.Add(this.radIdleDefault);
            this.grpPriority.Location = new System.Drawing.Point(13, 193);
            this.grpPriority.Name = "grpPriority";
            this.grpPriority.Size = new System.Drawing.Size(322, 92);
            this.grpPriority.TabIndex = 1;
            this.grpPriority.TabStop = false;
            this.grpPriority.Text = "Idling Order";
            // 
            // radIdleMostValue
            // 
            this.radIdleMostValue.AutoSize = true;
            this.radIdleMostValue.Location = new System.Drawing.Point(7, 35);
            this.radIdleMostValue.Name = "radIdleMostValue";
            this.radIdleMostValue.Size = new System.Drawing.Size(233, 17);
            this.radIdleMostValue.TabIndex = 3;
            this.radIdleMostValue.TabStop = true;
            this.radIdleMostValue.Text = "Prioritize games with the highest card values";
            this.radIdleMostValue.UseVisualStyleBackColor = true;
            // 
            // radIdleLeastDrops
            // 
            this.radIdleLeastDrops.AutoSize = true;
            this.radIdleLeastDrops.Location = new System.Drawing.Point(7, 69);
            this.radIdleLeastDrops.Name = "radIdleLeastDrops";
            this.radIdleLeastDrops.Size = new System.Drawing.Size(295, 17);
            this.radIdleLeastDrops.TabIndex = 2;
            this.radIdleLeastDrops.Text = "Prioritize games with the lowest number of available drops";
            this.radIdleLeastDrops.UseVisualStyleBackColor = true;
            // 
            // radIdleMostDrops
            // 
            this.radIdleMostDrops.AutoSize = true;
            this.radIdleMostDrops.Location = new System.Drawing.Point(7, 52);
            this.radIdleMostDrops.Name = "radIdleMostDrops";
            this.radIdleMostDrops.Size = new System.Drawing.Size(299, 17);
            this.radIdleMostDrops.TabIndex = 1;
            this.radIdleMostDrops.Text = "Prioritize games with the highest number of available drops";
            this.radIdleMostDrops.UseVisualStyleBackColor = true;
            // 
            // radIdleDefault
            // 
            this.radIdleDefault.AutoSize = true;
            this.radIdleDefault.Checked = true;
            this.radIdleDefault.Location = new System.Drawing.Point(7, 18);
            this.radIdleDefault.Name = "radIdleDefault";
            this.radIdleDefault.Size = new System.Drawing.Size(155, 17);
            this.radIdleDefault.TabIndex = 0;
            this.radIdleDefault.TabStop = true;
            this.radIdleDefault.Text = "Default (Alphabetical Order)";
            this.radIdleDefault.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(261, 295);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(180, 295);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "&Accept";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnAdvanced
            // 
            this.btnAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdvanced.Image = global::IdleMaster.Properties.Resources.imgLock;
            this.btnAdvanced.Location = new System.Drawing.Point(12, 295);
            this.btnAdvanced.Name = "btnAdvanced";
            this.btnAdvanced.Size = new System.Drawing.Size(25, 23);
            this.btnAdvanced.TabIndex = 4;
            this.ttHints.SetToolTip(this.btnAdvanced, "Display advanced authentication information");
            this.btnAdvanced.UseVisualStyleBackColor = true;
            this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
            // 
            // grpIdlingQuantity
            // 
            this.grpIdlingQuantity.Controls.Add(this.radManyThenOne);
            this.grpIdlingQuantity.Controls.Add(this.radOneGameOnly);
            this.grpIdlingQuantity.Location = new System.Drawing.Point(13, 124);
            this.grpIdlingQuantity.Margin = new System.Windows.Forms.Padding(2);
            this.grpIdlingQuantity.Name = "grpIdlingQuantity";
            this.grpIdlingQuantity.Padding = new System.Windows.Forms.Padding(2);
            this.grpIdlingQuantity.Size = new System.Drawing.Size(322, 64);
            this.grpIdlingQuantity.TabIndex = 5;
            this.grpIdlingQuantity.TabStop = false;
            this.grpIdlingQuantity.Text = "Idling Behavior";
            // 
            // radManyThenOne
            // 
            this.radManyThenOne.AutoSize = true;
            this.radManyThenOne.Location = new System.Drawing.Point(7, 35);
            this.radManyThenOne.Name = "radManyThenOne";
            this.radManyThenOne.Size = new System.Drawing.Size(293, 17);
            this.radManyThenOne.TabIndex = 5;
            this.radManyThenOne.TabStop = true;
            this.radManyThenOne.Text = "Idle games simultaneously up to 2 hours, then individually";
            this.radManyThenOne.UseVisualStyleBackColor = true;
            // 
            // radOneGameOnly
            // 
            this.radOneGameOnly.AutoSize = true;
            this.radOneGameOnly.Checked = true;
            this.radOneGameOnly.Location = new System.Drawing.Point(7, 18);
            this.radOneGameOnly.Name = "radOneGameOnly";
            this.radOneGameOnly.Size = new System.Drawing.Size(152, 17);
            this.radOneGameOnly.TabIndex = 4;
            this.radOneGameOnly.TabStop = true;
            this.radOneGameOnly.Text = "Idle each game individually";
            this.radOneGameOnly.UseVisualStyleBackColor = true;
            // 
            // frmSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(348, 330);
            this.Controls.Add(this.grpIdlingQuantity);
            this.Controls.Add(this.btnAdvanced);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpPriority);
            this.Controls.Add(this.grpGeneral);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Idle Master Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.grpPriority.ResumeLayout(false);
            this.grpPriority.PerformLayout();
            this.grpIdlingQuantity.ResumeLayout(false);
            this.grpIdlingQuantity.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox grpGeneral;
        private CheckBox chkMinToTray;
        private GroupBox grpPriority;
        private RadioButton radIdleLeastDrops;
        private RadioButton radIdleMostDrops;
        private RadioButton radIdleDefault;
        private Button btnCancel;
        private Button btnOK;
        private RadioButton radIdleMostValue;
        private Button btnAdvanced;
        private ToolTip ttHints;
        private CheckBox chkIgnoreClientStatus;
        private CheckBox chkShowUsername;
    private GroupBox grpIdlingQuantity;
    private RadioButton radManyThenOne;
    private RadioButton radOneGameOnly;
    private ComboBox cboLanguage;
    private Label lblLanguage;
  }
}