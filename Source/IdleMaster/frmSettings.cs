using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (radIdleDefault.Checked == true)
            {
                Properties.Settings.Default.sort = "default";
            }
            if (radIdleLeastDrops.Checked == true)
            {
                Properties.Settings.Default.sort = "leastcards";
            }
            if (radIdleMostDrops.Checked == true)
            {
                Properties.Settings.Default.sort = "mostcards";
            }
            if (radIdleMostValue.Checked == true)
            {
                Properties.Settings.Default.sort = "mostvalue";
            }
            
            if (chkMinToTray.Checked == true)
            {
                Properties.Settings.Default.minToTray = true;
            }
            else
            {
                Properties.Settings.Default.minToTray = false;
            }

            Properties.Settings.Default.Save();
            this.Close();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            switch (Properties.Settings.Default.sort)
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
                default:
                    break;
            }

            if (Properties.Settings.Default.minToTray == true)
            {
                chkMinToTray.Checked = true;
            }
        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            frmSettingsAdvanced frm = new frmSettingsAdvanced();
            frm.ShowDialog();
        }
    }
}
