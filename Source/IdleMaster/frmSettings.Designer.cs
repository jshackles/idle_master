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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkShowUsername = new System.Windows.Forms.CheckBox();
            this.chkIgnoreClientStatus = new System.Windows.Forms.CheckBox();
            this.chkMinToTray = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radIdleMostValue = new System.Windows.Forms.RadioButton();
            this.radIdleLeastDrops = new System.Windows.Forms.RadioButton();
            this.radIdleMostDrops = new System.Windows.Forms.RadioButton();
            this.radIdleDefault = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.ttHints = new System.Windows.Forms.ToolTip(this.components);
            this.btnAdvanced = new System.Windows.Forms.Button();
            this.IdlingQuantity = new System.Windows.Forms.GroupBox();
            this.ManyThenOne = new System.Windows.Forms.RadioButton();
            this.OneGameOnly = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.IdlingQuantity.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.chkShowUsername);
            this.groupBox1.Controls.Add(this.chkIgnoreClientStatus);
            this.groupBox1.Controls.Add(this.chkMinToTray);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.ttHints.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // chkShowUsername
            // 
            resources.ApplyResources(this.chkShowUsername, "chkShowUsername");
            this.chkShowUsername.Name = "chkShowUsername";
            this.ttHints.SetToolTip(this.chkShowUsername, resources.GetString("chkShowUsername.ToolTip"));
            this.chkShowUsername.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreClientStatus
            // 
            resources.ApplyResources(this.chkIgnoreClientStatus, "chkIgnoreClientStatus");
            this.chkIgnoreClientStatus.Name = "chkIgnoreClientStatus";
            this.ttHints.SetToolTip(this.chkIgnoreClientStatus, resources.GetString("chkIgnoreClientStatus.ToolTip"));
            this.chkIgnoreClientStatus.UseVisualStyleBackColor = true;
            // 
            // chkMinToTray
            // 
            resources.ApplyResources(this.chkMinToTray, "chkMinToTray");
            this.chkMinToTray.Name = "chkMinToTray";
            this.ttHints.SetToolTip(this.chkMinToTray, resources.GetString("chkMinToTray.ToolTip"));
            this.chkMinToTray.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.radIdleMostValue);
            this.groupBox2.Controls.Add(this.radIdleLeastDrops);
            this.groupBox2.Controls.Add(this.radIdleMostDrops);
            this.groupBox2.Controls.Add(this.radIdleDefault);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.ttHints.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // radIdleMostValue
            // 
            resources.ApplyResources(this.radIdleMostValue, "radIdleMostValue");
            this.radIdleMostValue.Name = "radIdleMostValue";
            this.radIdleMostValue.TabStop = true;
            this.ttHints.SetToolTip(this.radIdleMostValue, resources.GetString("radIdleMostValue.ToolTip"));
            this.radIdleMostValue.UseVisualStyleBackColor = true;
            // 
            // radIdleLeastDrops
            // 
            resources.ApplyResources(this.radIdleLeastDrops, "radIdleLeastDrops");
            this.radIdleLeastDrops.Name = "radIdleLeastDrops";
            this.ttHints.SetToolTip(this.radIdleLeastDrops, resources.GetString("radIdleLeastDrops.ToolTip"));
            this.radIdleLeastDrops.UseVisualStyleBackColor = true;
            // 
            // radIdleMostDrops
            // 
            resources.ApplyResources(this.radIdleMostDrops, "radIdleMostDrops");
            this.radIdleMostDrops.Name = "radIdleMostDrops";
            this.ttHints.SetToolTip(this.radIdleMostDrops, resources.GetString("radIdleMostDrops.ToolTip"));
            this.radIdleMostDrops.UseVisualStyleBackColor = true;
            // 
            // radIdleDefault
            // 
            resources.ApplyResources(this.radIdleDefault, "radIdleDefault");
            this.radIdleDefault.Checked = true;
            this.radIdleDefault.Name = "radIdleDefault";
            this.radIdleDefault.TabStop = true;
            this.ttHints.SetToolTip(this.radIdleDefault, resources.GetString("radIdleDefault.ToolTip"));
            this.radIdleDefault.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.ttHints.SetToolTip(this.btnCancel, resources.GetString("btnCancel.ToolTip"));
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.ttHints.SetToolTip(this.btnOK, resources.GetString("btnOK.ToolTip"));
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnAdvanced
            // 
            resources.ApplyResources(this.btnAdvanced, "btnAdvanced");
            this.btnAdvanced.Image = global::IdleMaster.Properties.Resources.imgLock;
            this.btnAdvanced.Name = "btnAdvanced";
            this.ttHints.SetToolTip(this.btnAdvanced, resources.GetString("btnAdvanced.ToolTip"));
            this.btnAdvanced.UseVisualStyleBackColor = true;
            this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
            // 
            // IdlingQuantity
            // 
            resources.ApplyResources(this.IdlingQuantity, "IdlingQuantity");
            this.IdlingQuantity.Controls.Add(this.ManyThenOne);
            this.IdlingQuantity.Controls.Add(this.OneGameOnly);
            this.IdlingQuantity.Name = "IdlingQuantity";
            this.IdlingQuantity.TabStop = false;
            this.ttHints.SetToolTip(this.IdlingQuantity, resources.GetString("IdlingQuantity.ToolTip"));
            // 
            // ManyThenOne
            // 
            resources.ApplyResources(this.ManyThenOne, "ManyThenOne");
            this.ManyThenOne.Name = "ManyThenOne";
            this.ManyThenOne.TabStop = true;
            this.ttHints.SetToolTip(this.ManyThenOne, resources.GetString("ManyThenOne.ToolTip"));
            this.ManyThenOne.UseVisualStyleBackColor = true;
            // 
            // OneGameOnly
            // 
            resources.ApplyResources(this.OneGameOnly, "OneGameOnly");
            this.OneGameOnly.Checked = true;
            this.OneGameOnly.Name = "OneGameOnly";
            this.OneGameOnly.TabStop = true;
            this.ttHints.SetToolTip(this.OneGameOnly, resources.GetString("OneGameOnly.ToolTip"));
            this.OneGameOnly.UseVisualStyleBackColor = true;
            // 
            // frmSettings
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.IdlingQuantity);
            this.Controls.Add(this.btnAdvanced);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmSettings";
            this.ttHints.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.IdlingQuantity.ResumeLayout(false);
            this.IdlingQuantity.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private CheckBox chkMinToTray;
        private GroupBox groupBox2;
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
    private GroupBox IdlingQuantity;
    private RadioButton ManyThenOne;
    private RadioButton OneGameOnly;
  }
}