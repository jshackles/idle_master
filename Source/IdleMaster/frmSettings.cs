using System;
using System.Windows.Forms;
using IdleMaster.Properties;
using Microsoft.Win32;          //For RegistryKey

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

      Settings.Default.OnlyOneGameIdle = OneGameOnly.Checked && !ManyThenOne.Checked;

      Settings.Default.minToTray = chkMinToTray.Checked;

      Settings.Default.ignoreclient = chkIgnoreClientStatus.Checked;

      Settings.Default.showUsername = chkShowUsername.Checked;

      setStartOnBoot(chkStartOnBoot.Checked);
      
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

      OneGameOnly.Checked = Settings.Default.OnlyOneGameIdle;
      ManyThenOne.Checked = !Settings.Default.OnlyOneGameIdle;

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
            if (getSteamStartOnBoot())
            {
                chkStartOnBoot.Enabled = true;
                buttonHelpStartOnBoot.Enabled = false;
                buttonHelpStartOnBoot.Visible = false;
            }
            else
            {
                //Check if user did not disable start steam on boot
                if (getStartOnBoot()) setStartOnBoot(false);
                chkStartOnBoot.Enabled = false;
                buttonHelpStartOnBoot.Enabled = true;
                buttonHelpStartOnBoot.Visible = true;
                ttHints.SetToolTip(buttonHelpStartOnBoot, "IdleMaster needs Steam running, to start on boot you have to make steam start on boot first\n"+
                                                          "(Steam-Settings-Interface-Run Steam when my computer starts)");
            }
      if (getStartOnBoot())
      {
        chkStartOnBoot.Checked = true;
      }
    }

    private void btnAdvanced_Click(object sender, EventArgs e)
    {
      var frm = new frmSettingsAdvanced();
      frm.ShowDialog();
    }

    private void setStartOnBoot(bool value)
    {
       RegistryKey registryRunAtStartup = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
       if (chkStartOnBoot.Checked)
       {
          registryRunAtStartup.SetValue("IdleMaster", Application.ExecutablePath.ToString());
       }
       else if(registryRunAtStartup.GetValue("IdleMaster")!=null)
       {
          registryRunAtStartup.DeleteValue("IdleMaster", false);
       }
    }
        private bool getStartOnBoot()
        {
            RegistryKey registryRunAtStartup = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (registryRunAtStartup.GetValue("IdleMaster") != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool getSteamStartOnBoot()
        {
            RegistryKey registryRunAtStartup = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (registryRunAtStartup.GetValue("Steam") != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void buttonHelpStartOnBoot_MouseEnter(object sender, EventArgs e)
        {
            
        }
    }
}
