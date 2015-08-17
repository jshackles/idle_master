using System.ComponentModel;
using System.Windows.Forms;

namespace IdleMaster
{
    partial class frmSettingsAdvanced
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettingsAdvanced));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSessionID = new System.Windows.Forms.TextBox();
            this.txtSteamLogin = new System.Windows.Forms.TextBox();
            this.txtSteamParental = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.ttHelp = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.ttHelp.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.ttHelp.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.ttHelp.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // txtSessionID
            // 
            resources.ApplyResources(this.txtSessionID, "txtSessionID");
            this.txtSessionID.Name = "txtSessionID";
            this.ttHelp.SetToolTip(this.txtSessionID, resources.GetString("txtSessionID.ToolTip"));
            this.txtSessionID.TextChanged += new System.EventHandler(this.txtSessionID_TextChanged);
            // 
            // txtSteamLogin
            // 
            resources.ApplyResources(this.txtSteamLogin, "txtSteamLogin");
            this.txtSteamLogin.Name = "txtSteamLogin";
            this.ttHelp.SetToolTip(this.txtSteamLogin, resources.GetString("txtSteamLogin.ToolTip"));
            this.txtSteamLogin.TextChanged += new System.EventHandler(this.txtSteamLogin_TextChanged);
            // 
            // txtSteamParental
            // 
            resources.ApplyResources(this.txtSteamParental, "txtSteamParental");
            this.txtSteamParental.Name = "txtSteamParental";
            this.ttHelp.SetToolTip(this.txtSteamParental, resources.GetString("txtSteamParental.ToolTip"));
            this.txtSteamParental.TextChanged += new System.EventHandler(this.txtSteamParental_TextChanged);
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Name = "btnUpdate";
            this.ttHelp.SetToolTip(this.btnUpdate, resources.GetString("btnUpdate.ToolTip"));
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnView
            // 
            resources.ApplyResources(this.btnView, "btnView");
            this.btnView.Image = global::IdleMaster.Properties.Resources.imgView;
            this.btnView.Name = "btnView";
            this.ttHelp.SetToolTip(this.btnView, resources.GetString("btnView.ToolTip"));
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // ttHelp
            // 
            this.ttHelp.AutoPopDelay = 9000;
            this.ttHelp.InitialDelay = 500;
            this.ttHelp.ReshowDelay = 100;
            // 
            // frmSettingsAdvanced
            // 
            this.AcceptButton = this.btnUpdate;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.txtSteamParental);
            this.Controls.Add(this.txtSteamLogin);
            this.Controls.Add(this.txtSessionID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmSettingsAdvanced";
            this.ttHelp.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.frmSettingsAdvanced_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtSessionID;
        private TextBox txtSteamLogin;
        private TextBox txtSteamParental;
        private Button btnView;
        private Button btnUpdate;
        private ToolTip ttHelp;
    }
}