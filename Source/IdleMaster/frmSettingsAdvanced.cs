using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            if (Properties.Settings.Default.sessionid != "")
            {
                txtSessionID.Text = Properties.Settings.Default.sessionid.ToString();
                txtSessionID.Enabled = false;
            }
            else
            {
                txtSessionID.PasswordChar = '\0';
            }

            if (Properties.Settings.Default.steamLogin != "")
            {
                txtSteamLogin.Text = Properties.Settings.Default.steamLogin.ToString();
                txtSteamLogin.Enabled = false;
            }
            else
            {
                txtSteamLogin.PasswordChar = '\0';
            }

            if (Properties.Settings.Default.steamparental != "")
            {
                txtSteamParental.Text = Properties.Settings.Default.steamparental.ToString();
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
                string response = await GetHttpAsync("http://steamcommunity.com/profiles/" + txtSteamLogin.Text.Substring(0, 17) + "/badges/");
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(response);
                HtmlNodeCollection user_avatar = document.DocumentNode.SelectNodes("//div[contains(@class,'user_avatar')]");
            
                int Count = user_avatar.Count;

                // Assign values to the settings
                Properties.Settings.Default.sessionid = txtSessionID.Text.Trim();
                Properties.Settings.Default.steamLogin = txtSteamLogin.Text.Trim();
                if (txtSteamLogin.Text.Length > 17)
                {
                    Properties.Settings.Default.myProfileURL = "http://steamcommunity.com/profiles/" + txtSteamLogin.Text.Substring(0, 17);
                }
                Properties.Settings.Default.steamparental = txtSteamParental.Text.Trim();

                // Save all of the data to the program settings file, and close this form
                Properties.Settings.Default.Save();

                this.Close();
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

        public async Task<string> GetHttpAsync(String url)
        {
            String content = "";
            try
            {
                CookieContainer cookies = new CookieContainer();
                Uri target = new Uri("http://steamcommunity.com");
                cookies.Add(new Cookie("sessionid", txtSessionID.Text.Trim()) { Domain = target.Host });
                cookies.Add(new Cookie("steamLogin", txtSteamLogin.Text.Trim()) { Domain = target.Host });
                cookies.Add(new Cookie("steamparental", txtSteamParental.Text.Trim()) { Domain = target.Host });
                HttpWebRequest r = (HttpWebRequest)WebRequest.Create(url);
                r.Method = "GET";
                r.CookieContainer = cookies;            
                HttpWebResponse res = (HttpWebResponse)await r.GetResponseAsync();
                if (res != null)
                {
                    if (res.StatusCode == HttpStatusCode.OK)
                    {
                        Stream stream = res.GetResponseStream();
                        using (StreamReader reader = new StreamReader(stream))
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
