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
      if (Settings.Default.sessionid != "")
      {
        txtSessionID.Text = Settings.Default.sessionid;
        txtSessionID.Enabled = false;
      }
      else
      {
        txtSessionID.PasswordChar = '\0';
      }

      if (Settings.Default.steamLogin != "")
      {
        txtSteamLogin.Text = Settings.Default.steamLogin;
        txtSteamLogin.Enabled = false;
      }
      else
      {
        txtSteamLogin.PasswordChar = '\0';
      }

      if (Settings.Default.steamparental != "")
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
        // Check to see if data is valid
        var response = await GetHttpAsync("http://steamcommunity.com/profiles/" + txtSteamLogin.Text.Substring(0, 17) + "/badges/");
        var document = new HtmlDocument();
        document.LoadHtml(response);
        var user_avatar = document.DocumentNode.SelectNodes("//div[contains(@class,'user_avatar')]");
        var Count = user_avatar.Count;

        // Assign values to the settings
        Settings.Default.sessionid = txtSessionID.Text.Trim();
        Settings.Default.steamLogin = txtSteamLogin.Text.Trim();
        if (txtSteamLogin.Text.Length > 17)
        {
          Settings.Default.myProfileURL = "http://steamcommunity.com/profiles/" + txtSteamLogin.Text.Substring(0, 17);
        }
        Settings.Default.steamparental = txtSteamParental.Text.Trim();

        // Save all of the data to the program settings file, and close this form
        Settings.Default.Save();

        Close();
      }
      catch (Exception)
      {
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

    public async Task<string> GetHttpAsync(string url)
    {
      var content = string.Empty;
      try
      {
        var cookies = new CookieContainer();
        var target = new Uri("http://steamcommunity.com");
        cookies.Add(new Cookie("sessionid", txtSessionID.Text.Trim()) { Domain = target.Host });
        cookies.Add(new Cookie("steamLogin", txtSteamLogin.Text.Trim()) { Domain = target.Host });
        cookies.Add(new Cookie("steamparental", txtSteamParental.Text.Trim()) { Domain = target.Host });
        var r = (HttpWebRequest)WebRequest.Create(url);
        r.Method = "GET";
        r.CookieContainer = cookies;
        var res = (HttpWebResponse)await r.GetResponseAsync();
        if (res != null)
        {
          if (res.StatusCode == HttpStatusCode.OK)
          {
            var stream = res.GetResponseStream();
            using (var reader = new StreamReader(stream))
            {
              content = reader.ReadToEnd();
            }
          }
        }
      }
      catch (Exception)
      {

      }
      return content;
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
