using System.ComponentModel;
using System.Windows.Forms;

namespace IdleMaster
{
    partial class frmChangelog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangelog));
            this.rtbChangelog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbChangelog
            // 
            resources.ApplyResources(this.rtbChangelog, "rtbChangelog");
            this.rtbChangelog.BackColor = System.Drawing.Color.White;
            this.rtbChangelog.Name = "rtbChangelog";
            this.rtbChangelog.ReadOnly = true;
            // 
            // frmChangelog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtbChangelog);
            this.Name = "frmChangelog";
            this.Load += new System.EventHandler(this.frmChangelog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private RichTextBox rtbChangelog;
    }
}