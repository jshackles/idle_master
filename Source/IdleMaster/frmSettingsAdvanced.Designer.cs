﻿using System.ComponentModel;
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettingsAdvanced));
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
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "sessionid:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "steamLogin:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "steamparental:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSessionID
            // 
            this.txtSessionID.Location = new System.Drawing.Point(104, 10);
            this.txtSessionID.Name = "txtSessionID";
            this.txtSessionID.PasswordChar = '*';
            this.txtSessionID.Size = new System.Drawing.Size(299, 20);
            this.txtSessionID.TabIndex = 3;
            this.txtSessionID.TextChanged += new System.EventHandler(this.txtSessionID_TextChanged);
            // 
            // txtSteamLogin
            // 
            this.txtSteamLogin.Location = new System.Drawing.Point(104, 33);
            this.txtSteamLogin.Name = "txtSteamLogin";
            this.txtSteamLogin.PasswordChar = '*';
            this.txtSteamLogin.Size = new System.Drawing.Size(299, 20);
            this.txtSteamLogin.TabIndex = 4;
            this.txtSteamLogin.TextChanged += new System.EventHandler(this.txtSteamLogin_TextChanged);
            // 
            // txtSteamParental
            // 
            this.txtSteamParental.Location = new System.Drawing.Point(104, 57);
            this.txtSteamParental.Name = "txtSteamParental";
            this.txtSteamParental.PasswordChar = '*';
            this.txtSteamParental.Size = new System.Drawing.Size(299, 20);
            this.txtSteamParental.TabIndex = 5;
            this.txtSteamParental.TextChanged += new System.EventHandler(this.txtSteamParental_TextChanged);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(327, 83);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnView
            // 
            this.btnView.Image = global::IdleMaster.Properties.Resources.imgView;
            this.btnView.Location = new System.Drawing.Point(104, 83);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(27, 23);
            this.btnView.TabIndex = 6;
            this.ttHelp.SetToolTip(this.btnView, "Display information above\r\n\r\n[WARNING] \r\nDo not share this information with anyon" +
        "e, \r\nas it could potentially be used by an attacker to log into \r\nyour Steam acc" +
        "ount.");
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 116);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.txtSteamParental);
            this.Controls.Add(this.txtSteamLogin);
            this.Controls.Add(this.txtSessionID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSettingsAdvanced";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Idle Master Authentication Data";
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