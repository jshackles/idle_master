using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using HtmlAgilityPack;
using Steamworks;
using Newtonsoft.Json;
 
namespace IdleMaster
{
    public partial class frmMain : Form
    {
        public List<Process> TwoHoursProcesses = new List<Process>(); // Storage for games process with < 2 hours.
        public Process Idle; // This process handle will control steam-idle.exe
        public List<Badge> badgesLeft = new List<Badge>();
        public Boolean cookieReady = false;
        public Boolean steamReady = false;
        public int timeLeft = 900;
        public int totalCardsRemaining;
        public int totalGamesRemaining;
        public String currentAppID;
        
        public CookieContainer generateCookies()
        {
            CookieContainer cookies = new CookieContainer();
            Uri target = new Uri("http://steamcommunity.com");
            cookies.Add(new Cookie("sessionid", Properties.Settings.Default.sessionid) { Domain = target.Host });
            cookies.Add(new Cookie("steamLogin", Properties.Settings.Default.steamLogin) { Domain = target.Host });
            cookies.Add(new Cookie("steamparental", Properties.Settings.Default.steamparental) { Domain = target.Host });
            return cookies;
        }

        public string GetAppName(String appid)
        {
            string name = "App " + appid;
            try
            {
                WebRequest request = WebRequest.Create("http://store.steampowered.com/api/appdetails/?appids=" + appid + "&filters=basic");
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);
                string api_raw = reader.ReadToEnd();
                if (Regex.IsMatch(api_raw, "\"game\",\"name\":\"(.+?)\""))
                {
                    name = Regex.Match(api_raw, "\"game\",\"name\":\"(.+?)\"").Groups[1].Value;
                }                
                name = Regex.Unescape(name);
                reader.Close();
                response.Close();                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return name;
        }

        public string GetUserName(String steamid)
        {
            string user_name = "User " + steamid;
            try
            {
                WebRequest request = WebRequest.Create("http://api.enhancedsteam.com/steamapi/GetPlayerSummaries/?steamids=" + steamid);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);
                string api_raw = reader.ReadToEnd();
                if (Regex.IsMatch(api_raw, "\"personaname\": \"(.+?)\""))
                {
                    user_name = Regex.Match(api_raw, "\"personaname\": \"(.+?)\"").Groups[1].Value;
                }
                user_name = Regex.Unescape(user_name);
                reader.Close();
                response.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return user_name;
        }

        private void CopyResource(string resourceName, string file)
        {
            using (Stream resource = GetType().Assembly.GetManifestResourceStream(resourceName))
            {
                if (resource == null)
                {
                    return;
                }
                using (Stream output = File.OpenWrite(file))
                {
                    resource.CopyTo(output);
                }
            }
        }

        public void SortBadges(String method)
        {
            lblDrops.Text = "Sorting results based on your settings, please wait...";
            List<Badge> tempBadgesLeft = new List<Badge>();
            switch (method)
            {
                case "mostcards":
                    tempBadgesLeft = badgesLeft.OrderByDescending(b => b.RemainingCard).ToList();
                    break;
                case "leastcards":
                    tempBadgesLeft = badgesLeft.OrderBy(b => b.RemainingCard).ToList();
                    break;
                case "mostvalue":
                    // Compile the list of appids that need to be idled
                    var appids = string.Join(",", badgesLeft);

                    // Query the API to retrieve the average card values of each appid
                    WebRequest request = WebRequest.Create("http://api.enhancedsteam.com/market_data/average_card_prices/im.php?appids=" + appids);
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);
                    string json = reader.ReadToEnd();
                    reader.Close();
                    response.Close();

                    // Parse the response and sort it appropriately
                    DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(json);
                    DataTable dataTable = dataSet.Tables["avg_values"];
                    DataView dataView = dataTable.DefaultView;
                    dataView.Sort = "avg_price desc";
                    DataTable sorted = dataView.ToTable();

                    foreach (DataRow row in sorted.Rows)
                    {
                        if (row["avg_price"].ToString() != "")
                        {
                            var badge = badgesLeft.FirstOrDefault(b => b.AppId == row["appid"].ToString());
                            if (badge != null)
                            {
                                tempBadgesLeft.Add(badge);
                            }
                        }
                    }

                    break;
                default:
                    return;
            }

            badgesLeft.Clear();
            badgesLeft = tempBadgesLeft;
        }

        public void startIdle(String appid)
        {
            foreach (var badge in badgesLeft.Where(b => b.HoursPlayed < 2 && b.AppId != appid))
            {
              // Place user "In game" for card drops
              TwoHoursProcesses.Add(Process.Start(new ProcessStartInfo("steam-idle.exe", badge.AppId) { WindowStyle = ProcessWindowStyle.Hidden }));
            }
            
            // Place user "In game" for card drops
            ProcessStartInfo startInfo = new ProcessStartInfo("steam-idle.exe", appid);
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Idle = Process.Start(startInfo);
            
            // Update game name
            lblGameName.Visible = true;
            lblGameName.Text = GetAppName(appid);

            // Update game image
            try
            {
                picApp.Load("http://cdn.akamai.steamstatic.com/steam/apps/" + appid + "/header_292x136.jpg");
                picApp.Visible = true;
            }
            catch (Exception)
            {

            }

            // Update label controls
            var remained = badgesLeft.Single(b => b.AppId == appid).RemainingCard;
            lblCurrentRemaining.Text = remained + " card drops remaining";
            lblCurrentStatus.Text = "Currently in-game";

            // Set progress bar values and show the footer
            pbIdle.Maximum = Int32.Parse(remained);
            pbIdle.Value = 0;
            ssFooter.Visible = true;

            // Start the animated "working" gif
            picIdleStatus.Image = Properties.Resources.imgSpin;

            // Set the currentAppID value
            currentAppID = appid;

            // Start the timer that will check if drops remain
            tmrCardDropCheck.Enabled = true;

            // Reset the timer
            if (pbIdle.Maximum != 1)
            {
                timeLeft = 900;
            }
            else
            {
                timeLeft = 300;
            } 

            // Set the correct buttons on the form for pause / resume
            btnResume.Visible = false;
            btnPause.Visible = true;
            resumeIdlingToolStripMenuItem.Enabled = false;
            pauseIdlingToolStripMenuItem.Enabled = false;
            skipGameToolStripMenuItem.Enabled = false;

            Graphics graphics = this.CreateGraphics();
            double scale = graphics.DpiY * 3.86;
            this.Height = Convert.ToInt32(scale);
        }

        public void stopIdle()
        {
            try 
            {
                lblGameName.Visible = false;
                picApp.Image = null;
                picApp.Visible = false;
                lblCurrentStatus.Text = "Not in game";
                picIdleStatus.Image = null;

                // Stop the card drop check timer
                tmrCardDropCheck.Enabled = false;

                // Hide the status bar
                ssFooter.Visible = false;

                // Resize the form
                Graphics graphics = this.CreateGraphics();
                double scale = graphics.DpiY * 1.9583;
                this.Height = Convert.ToInt32(scale);

                // Kill the idling process
                TwoHoursProcesses.ForEach(p => p.Kill());
                Idle.Kill();
            }
            catch (Exception)
            {

            }
        }

        public void idleComplete()
        {
            // Deactivate the timer control and inform the user that the program is finished
            tmrCardDropCheck.Enabled = false;
            lblCurrentStatus.Text = "Idling complete";
        }

        public async Task<string> GetHttpAsync(String url)
        {
            String content = "";
            try
            {
                CookieContainer cookies = generateCookies();
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
                picIdleStatus.Image = Properties.Resources.imgSpin;
            }
            catch (Exception)
            {
                // Try again in 60 seconds
                timeLeft = 60;
                picIdleStatus.Image = Properties.Resources.imgFalse;
            }
            return content;
        }

        public async Task LoadBadgesAsync()
        {
            string response = await GetHttpAsync(Properties.Settings.Default.myProfileURL + "/badges/");
            if (response == "")
            {
                // badge page didn't load
                picReadingPage.Image = null;
                lblDrops.Text = "Badge page didn't load, will retry in 10 seconds";
                ReloadCount = 10;
                tmrBadgeReload.Enabled = true;
                return;
            }
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(response);
            HtmlNodeCollection user_avatar = document.DocumentNode.SelectNodes("//div[contains(@class,'user_avatar')]");

            try
            {
                int Count = user_avatar.Count;
            }
            catch (Exception)
            {
                // Invalid cookie data
                cookieReady = false;
                lnkResetCookies_LinkClicked(null, null);
                picReadingPage.Visible = false;
                return;
            }

            int totaldrops = 0;

            try
            {
                foreach (HtmlNode badge in document.DocumentNode.SelectNodes("//div[contains(@class,'badge_title_stats')]"))
                {
                    string appid = Regex.Match(badge.InnerHtml, @"card_drop_info_gamebadge_(\d+)_").Groups[1].Value;
                    string hours = Regex.Match(badge.ChildNodes[0].InnerText.Replace(",", "").Trim(), @"\d+").Value;
                    int hour = int.Parse(string.IsNullOrWhiteSpace(hours) ? "0" : hours);
                    HtmlNodeCollection row = badge.SelectNodes(".//span[contains(@class, 'progress_info_bold')]");
                    if (row != null)
                    {
                        foreach (HtmlNode data in row)
                        {
                            if (data != null)
                            {
                                if (Regex.Match(data.InnerHtml, @"\d").Length > 0)
                                {
                                    string remaining = Regex.Match(data.InnerHtml, @"(\d+)").Groups[1].Value;
                                    totaldrops = totaldrops + Convert.ToInt16(remaining);
                                    if (!badgesLeft.Any(b => b.AppId == appid))
                                    {
                                        Boolean onBlacklist = false; ;
                                        foreach (String blApp in Properties.Settings.Default.blacklist)
                                        {
                                            if (blApp == appid)
                                            {
                                                onBlacklist = true;
                                            }                                            
                                        }
                                        if (appid == "368020" || appid == "335590")
                                        {
                                            onBlacklist = true;
                                        }
                                        if (onBlacklist == false)
                                        {
                                            badgesLeft.Add(new Badge() { AppId = appid, RemainingCard = remaining, HoursPlayed = hour});
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // badge page didn't load
                picReadingPage.Image = null;
                lblDrops.Text = "Badge page didn't load, will retry in 10 seconds";
                ReloadCount = 10;
                tmrBadgeReload.Enabled = true;
                return;
            }

            // Detect if the user has multiple badge pages
            HtmlNode pagelink = document.DocumentNode.SelectSingleNode("//a[contains(@class,'pagelink')][last()]");
            try
            {
                // Get number of pages from the last pagelink a element
                int last = Int32.Parse(Regex.Match(pagelink.Attributes["href"].Value, @"(\d+)").Groups[1].Value);
                int i = 1;
                do
                {
                    i++;
                    response = await GetHttpAsync(Properties.Settings.Default.myProfileURL + "/badges/?p=" + i);
                    document = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(response);

                    foreach (HtmlNode badge in document.DocumentNode.SelectNodes("//div[contains(@class,'badge_title_stats')]"))
                    {
                        string appid = Regex.Match(badge.InnerHtml, @"card_drop_info_gamebadge_(\d+)_").Groups[1].Value;
                        string hours = Regex.Match(badge.ChildNodes[0].InnerText.Replace(",", "").Trim(), @"\d+").Value;
                        int hour = int.Parse(string.IsNullOrWhiteSpace(hours) ? "0" : hours);
                        HtmlNodeCollection row = badge.SelectNodes(".//span[contains(@class, 'progress_info_bold')]");
                        if (row != null)
                        {
                            foreach (HtmlNode data in row)
                            {
                                if (data != null)
                                {
                                    if (Regex.Match(data.InnerHtml, @"\d").Length > 0)
                                    {
                                        string remaining = Regex.Match(data.InnerHtml, @"(\d+)").Groups[1].Value;
                                        totaldrops = totaldrops + Convert.ToInt16(remaining);
                                        if (!badgesLeft.Any(b => b.AppId == appid))
                                        {
                                            Boolean onBlacklist = false; ;
                                            foreach (String blApp in Properties.Settings.Default.blacklist)
                                            {
                                                if (blApp == appid)
                                                {
                                                    onBlacklist = true;
                                                }
                                            }
                                            if (onBlacklist == false)
                                            {
                                                badgesLeft.Add(new Badge() { AppId = appid, RemainingCard = remaining, HoursPlayed = hour});
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                } while (i < last);
            }
            catch (Exception)
            {
                // Only one page, we're good to go!
            }

            SortBadges(Properties.Settings.Default.sort);

            picReadingPage.Visible = false;
            lblIdle.Text = badgesLeft.Count + " games left to idle";
            lblIdle.Visible = true;
            lblDrops.Text = totaldrops.ToString() + " card drops remaining";
            lblDrops.Visible = true;
            
            // Set global variable values
            totalCardsRemaining = totaldrops;
            totalGamesRemaining = badgesLeft.Count;

            if (totaldrops == 0)
            {
                idleComplete();
            }
        }

        public async Task checkCardDrops(String appid)
        {
            try
            {
                string response = await GetHttpAsync(Properties.Settings.Default.myProfileURL + "/gamecards/" + appid + "/");
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(response);
                HtmlNodeCollection drops = document.DocumentNode.SelectNodes("//span[contains(@class,'progress_info_bold')]");
            
                String numDrops = drops[0].InnerText;
                lblCurrentRemaining.Text = numDrops;
                int intDrops;
                if (Int32.TryParse(Regex.Match(numDrops, @"(\d+)").Groups[1].Value, out intDrops)) {

                    // Determine if the drop count has changed
                    int dropsSoFar = Int32.Parse(badgesLeft.Single(b => b.AppId == appid).RemainingCard) - intDrops;
                    int dropsBefore = pbIdle.Value;

                    if (dropsBefore != dropsSoFar)
                    {
                        totalCardsRemaining = totalCardsRemaining - (dropsSoFar - dropsBefore);
                        lblDrops.Text = totalCardsRemaining + " card drops remaining";
                        pbIdle.Value = dropsSoFar;
                    }

                    // Resets the clock based on the number of remaining drops
                    if (intDrops == 1)
                    {
                        timeLeft = 300;
                    }
                    else
                    {
                        timeLeft = 900;
                    }
                    
                }
                else
                {
                    badgesLeft.RemoveAll(b => b.AppId == appid);

                    // Update totals
                    totalCardsRemaining = totalCardsRemaining - 1;
                    totalGamesRemaining = totalGamesRemaining - 1;
                    lblIdle.Text = totalGamesRemaining + " games left to idle";
                    lblDrops.Text = totalCardsRemaining + " card drops remaining";

                    // Stop idling the current game
                    stopIdle();

                    if (badgesLeft.Count != 0)
                    {
                        // Give the user notification that the next game will start soon
                        lblCurrentStatus.Text = "Loading next game...";

                        // Make a short but random amount of time pass
                        Random rand = new Random();
                        int wait = rand.Next(3, 9);
                        wait = wait * 1000;

                        tmrStartNext.Interval = wait;
                        tmrStartNext.Enabled = true;
                    }
                    else
                    {
                        idleComplete();
                    }
                }
            }
            catch (Exception)
            {
                
            }
            return;
        }

        public frmMain()
        {            
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Copy external references to the output directory.  This allows ClickOnce install.
            if (File.Exists(Environment.CurrentDirectory + "\\steam_api.dll") == false)
            {
                CopyResource("IdleMaster.Resources.steam_api.dll", Environment.CurrentDirectory + @"\steam_api.dll");
            }
            if (File.Exists(Environment.CurrentDirectory + "\\CSteamworks.dll") == false)
            {
                CopyResource("IdleMaster.Resources.CSteamworks.dll", Environment.CurrentDirectory + @"\CSteamworks.dll");
            }
            if (File.Exists(Environment.CurrentDirectory + "\\steam-idle.exe") == false)
            {
                CopyResource("IdleMaster.Resources.steam-idle.exe", Environment.CurrentDirectory + @"\steam-idle.exe");
            }

            // Update the settings, if needed.  When the application updates, settings will persist.
            if (Properties.Settings.Default.updateNeeded)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.updateNeeded = false;
                Properties.Settings.Default.Save();
            }

            // Set the form height
            Graphics graphics = this.CreateGraphics();
            double scale = graphics.DpiY * 1.625;
            this.Height = Convert.ToInt32(scale);

            // Set the location of certain elements so that they scale correctly for different DPI settings
            double lblGameName_scale = graphics.DpiX * 1.14;
            double lnkSignIn_scale = graphics.DpiX * 2.35;
            double lnkSignOut_scale = graphics.DpiX * 2.15;
            Point point = new Point(Convert.ToInt32(lblGameName_scale), Convert.ToInt32(lblGameName.Location.Y));
            lblGameName.Location = point;
            point = new Point(Convert.ToInt32(lnkSignIn_scale), Convert.ToInt32(lnkSignIn.Location.Y));
            lnkSignIn.Location = point;
            point = new Point(Convert.ToInt32(lnkSignOut_scale), Convert.ToInt32(lnkResetCookies.Location.Y));
            lnkResetCookies.Location = point;
        }

        private void frmMain_FormClose(object sender, FormClosedEventArgs e)
        {
            try 
            {
                stopIdle();
            }
            catch (Exception)
            {

            }
        }

        private void tmrCheckCookieData_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.sessionid != "" && Properties.Settings.Default.steamLogin != "")
            {
                lblCookieStatus.Text = "Idle Master is connected to Steam";
                lblCookieStatus.ForeColor = System.Drawing.Color.Green;
                picCookieStatus.Image = Properties.Resources.imgTrue;
                lnkSignIn.Visible = false;
                lnkResetCookies.Visible = true;
                cookieReady = true;
            }
            else
            {
                lblCookieStatus.Text = "Idle Master is not connected to Steam";
                lblCookieStatus.ForeColor = System.Drawing.Color.Black;
                picCookieStatus.Image = Properties.Resources.imgFalse;
                lnkSignIn.Visible = true;
                lnkResetCookies.Visible = false;
                cookieReady = false;
            }
        }

        private void tmrCheckSteam_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ignoreclient == true)
            {
                lblSteamStatus.Text = "Steam client status ignored";
                lblSteamStatus.ForeColor = System.Drawing.Color.Green;
                picSteamStatus.Image = Properties.Resources.imgTrue;
                tmrCheckSteam.Interval = 5000;
                skipGameToolStripMenuItem.Enabled = true;
                pauseIdlingToolStripMenuItem.Enabled = true;
                steamReady = true;
            }
            else
            {
                if (SteamAPI.IsSteamRunning() == true)
                {
                    lblSteamStatus.Text = "Steam is running";
                    lblSteamStatus.ForeColor = System.Drawing.Color.Green;
                    picSteamStatus.Image = Properties.Resources.imgTrue;
                    tmrCheckSteam.Interval = 5000;
                    skipGameToolStripMenuItem.Enabled = true;
                    pauseIdlingToolStripMenuItem.Enabled = true;
                    steamReady = true;
                }
                else
                {
                    lblSteamStatus.Text = "Steam is not running";
                    lblSteamStatus.ForeColor = System.Drawing.Color.Black;
                    picSteamStatus.Image = Properties.Resources.imgFalse;
                    tmrCheckSteam.Interval = 500;
                    skipGameToolStripMenuItem.Enabled = false;
                    pauseIdlingToolStripMenuItem.Enabled = false;
                    steamReady = false;
                }
            }
        }

        private void lblGameName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://store.steampowered.com/app/" + currentAppID);
        }

        private void lnkResetCookies_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Clear the settings
            Properties.Settings.Default.sessionid = "";
            Properties.Settings.Default.steamLogin = "";
            Properties.Settings.Default.myProfileURL = "";
            Properties.Settings.Default.steamparental = "";
            Properties.Settings.Default.Save();

            // Stop the steam-idle process
            stopIdle();

            // Clear the badges list
            badgesLeft.Clear();

            // Resize the form
            Graphics graphics = this.CreateGraphics();
            double scale = graphics.DpiY * 1.625;
            this.Height = Convert.ToInt32(scale);

            // Set timer intervals
            tmrCheckSteam.Interval = 500;
            tmrCheckCookieData.Interval = 500;

            // Hide lblDrops and lblIdle
            lblDrops.Visible = false;
            lblIdle.Visible = false;

            // Set cookieReady to false
            cookieReady = false;

            // Re-enable tmrReadyToGo
            tmrReadyToGo.Enabled = true;            
        }

        private void lnkSignIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {            
            frmBrowser frm = new frmBrowser();
            frm.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void tmrReadyToGo_Tick(object sender, EventArgs e)
        {
            if (cookieReady && steamReady)
            {
                // Update the form elements
                if (Properties.Settings.Default.showUsername)
                {
                    lblSignedOnAs.Text = "Signed in as " + GetUserName(Properties.Settings.Default.steamLogin.Substring(0, 17));
                    lblSignedOnAs.Visible = true;
                }

                lblDrops.Visible = true;
                lblDrops.Text = "Reading badge page, please wait...";
                lblIdle.Visible = false;
                picReadingPage.Visible = true;

                tmrReadyToGo.Enabled = false;

                // Call the loadBadges() function asynchronously
                await LoadBadgesAsync();

                if (badgesLeft.Count != 0)
                {
                    startIdle(badgesLeft.First().AppId);
                }
                else
                {
                    idleComplete();
                }
            }
        }

        private async void tmrCardDropCheck_Tick(object sender, EventArgs e)
        {
            if (timeLeft <= 0)
            {
                tmrCardDropCheck.Enabled = false;
                await checkCardDrops(currentAppID);
                if (badgesLeft.Count != 0 && timeLeft != 0)
                {
                    tmrCardDropCheck.Enabled = true;
                }
            }
            else
            {
                timeLeft = timeLeft - 1;
                int minutes = timeLeft / 60;
                int seconds = timeLeft - (minutes * 60);
                if (seconds < 10)
                {
                    lblTimer.Text = minutes + ":0" + seconds;
                }
                else
                {
                    lblTimer.Text = minutes + ":" + seconds;
                }
            }            
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
          if (steamReady)
          {
            badgesLeft.RemoveAll(b => b.AppId == currentAppID);
            stopIdle();
            if (badgesLeft.Count != 0)
            {
              startIdle(badgesLeft.First().AppId);
            }
            else
            {
              idleComplete();
            }
          }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
          if (steamReady)
          {
            // Stop the steam-idle process
            stopIdle();

            // Indicate to the user that idling has been paused
            lblCurrentStatus.Text = "Idling paused";

            // Set the correct button visibility
            btnResume.Visible = true;
            btnPause.Visible = false;
            pauseIdlingToolStripMenuItem.Enabled = false;
            resumeIdlingToolStripMenuItem.Enabled = true;

            // Focus the resume button
            btnResume.Focus();
          }
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            // Resume idling
            if (badgesLeft.Count != 0)
            {
                startIdle(currentAppID);
            }
            else
            {
                idleComplete();
            }

            pauseIdlingToolStripMenuItem.Enabled = true;
            resumeIdlingToolStripMenuItem.Enabled = false;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show the form
            String previous = Properties.Settings.Default.sort;
            frmSettings frm = new frmSettings();
            frm.ShowDialog();

            if (previous != Properties.Settings.Default.sort)
            {
                stopIdle();
                badgesLeft.Clear();
                tmrReadyToGo.Enabled = true;
            }

            if (Properties.Settings.Default.showUsername)
            {
                lblSignedOnAs.Text = "Signed in as " + GetUserName(Properties.Settings.Default.steamLogin.Substring(0, 17));
                lblSignedOnAs.Visible = true;
            }
            else
            {
                lblSignedOnAs.Visible = false;
            }
        }

        private void pauseIdlingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnPause.PerformClick();
        }

        private void resumeIdlingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnResume.PerformClick();
        }

        private void skipGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSkip.PerformClick();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.ShowDialog();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                if (Properties.Settings.Default.minToTray == true)
                {
                    notifyIcon1.Visible = true;
                    this.Hide();
                }
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void lblCurrentRemaining_Click(object sender, EventArgs e)
        {
            if (timeLeft > 2)
            {
                timeLeft = 2;
            }            
        }

        private void blacklistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBlacklist frm = new frmBlacklist();
            frm.ShowDialog();

            foreach (String appid in Properties.Settings.Default.blacklist)
            {   
                if (appid == currentAppID)
                {
                    btnSkip.PerformClick();
                }
            }
        }

        private void blacklistCurrentGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Collections.Specialized.StringCollection blacklist = new System.Collections.Specialized.StringCollection();
            foreach (String appid in Properties.Settings.Default.blacklist)
            {
                blacklist.Add(appid);
            }
            blacklist.Add(currentAppID);
            Properties.Settings.Default.blacklist = blacklist;
            Properties.Settings.Default.Save();

            btnSkip.PerformClick();
        }

        private void tmrStartNext_Tick(object sender, EventArgs e)
        {
            startIdle(badgesLeft.First().AppId);
            tmrStartNext.Enabled = false;
        }

        private void changelogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangelog frm = new frmChangelog();
            frm.Show();
        }

        private void officialGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://steamcommunity.com/groups/idlemastery");
        }

        int ReloadCount = 10;
        private void tmrBadgeReload_Tick(object sender, EventArgs e)
        {
            ReloadCount = ReloadCount - 1;
            lblDrops.Text = "Badge page didn't load, will retry in " +  ReloadCount + " seconds";

            if (ReloadCount == 0)
            {
                tmrBadgeReload.Enabled = false;
                tmrReadyToGo.Enabled = true;
            }            
        }
    }
}