using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using IdleMaster.Properties;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

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
        // Assign values to the settings
        Settings.Default.sessionid = txtSessionID.Text.Trim();
        Settings.Default.steamLogin = txtSteamLogin.Text.Trim();
        if (txtSteamLogin.Text.Length > 17)
          Settings.Default.myProfileURL = "http://steamcommunity.com/profiles/" + txtSteamLogin.Text.Substring(0, 17);
        Settings.Default.steamparental = txtSteamParental.Text.Trim();

        // Check to see if data is valid
        var response = await CookieClient.GetHttpAsync("http://steamcommunity.com/profiles/" + txtSteamLogin.Text.Substring(0, 17) + "/badges/");
        var document = new HtmlDocument();
        document.LoadHtml(response);
        document.DocumentNode.SelectNodes("//div[contains(@class,'user_avatar')]").ToString();
        
        // Save all of the data to the program settings file, and close this form
        Settings.Default.Save();

        Close();
      }
      catch (Exception ex)
      {
        Logger.Exception(ex, "frmSettingsAdvanced -> CheckAndSave");
        // Invalid cookie data, reset the form
        btnUpdate.Text = "Update";
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
    }

    private async void btnUpdate_Click(object sender, EventArgs e)
    {
      btnUpdate.Enabled = false;
      txtSessionID.Enabled = false;
      txtSteamLogin.Enabled = false;
      txtSteamParental.Enabled = false;
      btnUpdate.Text = "Validating...";
      await CheckAndSave();
    }
  }
}
