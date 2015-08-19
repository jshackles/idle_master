using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using IdleMaster.Properties;

namespace IdleMaster
{
  public partial class frmSettingsAdvanced : Form
  {
    public frmSettingsAdvanced()
    {
      InitializeComponent();
    }

    private void btnView_Click(object sender, EventArgs e)
    {
      txtSessionID.PasswordChar = '\0';
      txtSteamLogin.PasswordChar = '\0';
      txtSteamParental.PasswordChar = '\0';

      txtSessionID.Enabled = true;
      txtSteamLogin.Enabled = true;
      txtSteamParental.Enabled = true;

      btnView.Visible = false;
    }

    private void frmSettingsAdvanced_Load(object sender, EventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(Settings.Default.sessionid))
      {
        txtSessionID.Text = Settings.Default.sessionid;
        txtSessionID.Enabled = false;
      }
      else
      {
        txtSessionID.PasswordChar = '\0';
      }

      if (!string.IsNullOrWhiteSpace(Settings.Default.steamLogin))
      {
        txtSteamLogin.Text = Settings.Default.steamLogin;
        txtSteamLogin.Enabled = false;
      }
      else
      {
        txtSteamLogin.PasswordChar = '\0';
      }

      if (!string.IsNullOrWhiteSpace(Settings.Default.steamparental))
      {
        txtSteamParental.Text = Settings.Default.steamparental;
        txtSteamParental.Enabled = false;
      }
      else
      {
        txtSteamParental.PasswordChar = '\0';
      }

      if (txtSessionID.Enabled && txtSteamLogin.Enabled && txtSteamParental.Enabled)
      {
        btnView.Visible = false;
      }

      btnUpdate.Enabled = false;
    }

    private void txtSessionID_TextChanged(object sender, EventArgs e)
    {
      btnUpdate.Enabled = true;
    }

    private void txtSteamLogin_TextChanged(object sender, EventArgs e)
    {
      btnUpdate.Enabled = true;
    }

    private void txtSteamParental_TextChanged(object sender, EventArgs e)
    {
      btnUpdate.Enabled = true;
    }

    private async Task CheckAndSave()
    {
      try
      {
        Settings.Default.sessionid = txtSessionID.Text.Trim();
        Settings.Default.steamLogin = txtSteamLogin.Text.Trim();
        Settings.Default.myProfileURL = SteamProfile.GetSteamUrl();
        Settings.Default.steamparental = txtSteamParental.Text.Trim();

        if (await CookieClient.IsLogined())
        {
          Settings.Default.Save();
          Close();
          return;
        }
      }
      catch (Exception ex)
      {
        Logger.Exception(ex, "frmSettingsAdvanced -> CheckAndSave");
      }

      // Invalid cookie data, reset the form
      btnUpdate.Text = Resources.Update;
      txtSessionID.Text = "";
      txtSteamLogin.Text = "";
      txtSteamParental.Text = "";
      txtSessionID.PasswordChar = '\0';
      txtSteamLogin.PasswordChar = '\0';
      txtSteamParental.PasswordChar = '\0';
      txtSessionID.Enabled = true;
      txtSteamLogin.Enabled = true;
      txtSteamParental.Enabled = true;
      txtSessionID.Focus();
      MessageBox.Show("The data you've entered isn't valid.  Please try again.");
      btnUpdate.Enabled = true;
    }

    private async void btnUpdate_Click(object sender, EventArgs e)
    {
      btnUpdate.Enabled = false;
      txtSessionID.Enabled = false;
      txtSteamLogin.Enabled = false;
      txtSteamParental.Enabled = false;
      btnUpdate.Text = Resources.Validating;
      await CheckAndSave();
    }
  }
}
