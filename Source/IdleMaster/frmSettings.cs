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

      Settings.Default.OnlyOneGameIdle = radOneGameOnly.Checked && !radManyThenOne.Checked;

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

      // Load translation
      this.Text = localization.strings.idle_master_settings;
      grpGeneral.Text = localization.strings.general;
      grpIdlingQuantity.Text = localization.strings.idling_behavior;
      grpPriority.Text = localization.strings.idling_order;
      btnOK.Text = localization.strings.accept;
      btnCancel.Text = localization.strings.cancel;
      ttHints.SetToolTip(btnAdvanced, localization.strings.advanced_auth);
      chkMinToTray.Text = localization.strings.minimize_to_tray;
      chkIgnoreClientStatus.Text = localization.strings.ignore_client_status;
      chkShowUsername.Text = localization.strings.show_username;
      radOneGameOnly.Text = localization.strings.idle_individual;
      radManyThenOne.Text = localization.strings.idle_simultaneous;
      radIdleDefault.Text = localization.strings.order_default;
      radIdleMostValue.Text = localization.strings.order_value;
      radIdleMostDrops.Text = localization.strings.order_most;
      radIdleLeastDrops.Text = localization.strings.order_least;

      radOneGameOnly.Checked = Settings.Default.OnlyOneGameIdle;
      radManyThenOne.Checked = !Settings.Default.OnlyOneGameIdle;

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
