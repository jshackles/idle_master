using System;
using System.Windows.Forms;
using IdleMaster.Properties;

namespace IdleMaster
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Settings.Default.minToTray = chkMinToTray.Checked;

            Settings.Default.ignoreclient = chkIgnoreClientStatus.Checked;

            Settings.Default.showUsername = chkShowUsername.Checked;

            if (radIdleDefault.Checked)
            {
                Settings.Default.sort = "default";
            }
            if (radIdleLeastDrops.Checked)
            {
                Settings.Default.sort = "leastcards";
            }
            if (radIdleMostDrops.Checked)
            {
                Settings.Default.sort = "mostcards";
            }
            if (radIdleMostValue.Checked)
            {
                Settings.Default.sort = "mostvalue";
            }

            if (radCompletionDefault.Checked)
            {
                Settings.Default.completion = "default";
            }
            if (radCompletionExit.Checked)
            {
                Settings.Default.completion = "exit";
            }
            if (radCompletionShutdown.Checked)
            {
                Settings.Default.completion = "shutdown";
            }

            Settings.Default.Save();
            Close();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            if (Settings.Default.minToTray)
            {
                chkMinToTray.Checked = true;
            }

            if (Settings.Default.ignoreclient)
            {
                chkIgnoreClientStatus.Checked = true;
            }

            if (Settings.Default.showUsername)
            {
                chkShowUsername.Checked = true;
            }

            switch (Settings.Default.sort)
            {
                case "leastcards":
                    radIdleLeastDrops.Checked = true;
                    break;
                case "mostcards":
                    radIdleMostDrops.Checked = true;
                    break;
                case "mostvalue":
                    radIdleMostValue.Checked = true;
                    break;
            }

            switch (Settings.Default.completion)
            {
                case "exit":
                    radCompletionExit.Checked = true;
                    break;
                case "shutdown":
                    radCompletionShutdown.Checked = true;
                    break;
            }
        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            frmSettingsAdvanced frm = new frmSettingsAdvanced();
            frm.ShowDialog();
        }
    }
}
