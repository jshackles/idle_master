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

      Settings.Default.minToTray = chkMinToTray.Checked;

      Settings.Default.ignoreclient = chkIgnoreClientStatus.Checked;

      Settings.Default.showUsername = chkShowUsername.Checked;

      Settings.Default.Save();
      Close();
    }

    private void frmSettings_Load(object sender, EventArgs e)
    {
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
        default:
          break;
      }

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
    }

    private void btnAdvanced_Click(object sender, EventArgs e)
    {
      var frm = new frmSettingsAdvanced();
      frm.ShowDialog();
    }
  }
}
