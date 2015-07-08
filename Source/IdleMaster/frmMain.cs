using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Xml;
using IdleMaster.Properties;
using Newtonsoft.Json;
using Steamworks;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace IdleMaster
{
  public partial class frmMain : Form
  {
    public List<Process> TwoHoursProcesses = new List<Process>(); // Storage for games process with < 2 hours.
    public Process Idle; // This process handle will control steam-idle.exe
    public List<Badge> Badges = new List<Badge>();
    public bool IsCookieReady;
    public bool IsSteamReady;
    public int TimeLeft = 900;
    public int CardsRemaining { get { return Badges.Sum(b => b.RemainingCard); } }
    public int GamesRemaining { get { return Badges.Count; } }
    public Badge CurrentBadge;

    public CookieContainer GenerateCookies()
    {
      var cookies = new CookieContainer();
      var target = new Uri("http://steamcommunity.com");
      cookies.Add(new Cookie("sessionid", Settings.Default.sessionid) { Domain = target.Host });
      cookies.Add(new Cookie("steamLogin", Settings.Default.steamLogin) { Domain = target.Host });
      cookies.Add(new Cookie("steamparental", Settings.Default.steamparental) { Domain = target.Host });
      return cookies;
    }

    public string GetAppName(string appid)
    {
      var name = "App " + appid;
      try
      {
        var request = WebRequest.Create("http://store.steampowered.com/api/appdetails/?appids=" + appid + "&filters=basic");
        var response = request.GetResponse();
        var dataStream = response.GetResponseStream();
        var reader = new StreamReader(dataStream, Encoding.UTF8);
        var apiRaw = reader.ReadToEnd();
        if (Regex.IsMatch(apiRaw, "\"game\",\"name\":\"(.+?)\""))
        {
          name = Regex.Match(apiRaw, "\"game\",\"name\":\"(.+?)\"").Groups[1].Value;
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

    public string GetUserName(string steamid)
    {
      var userName = "User " + steamid;
      try
      {
        var xmlRaw = new WebClient().DownloadString(string.Format("http://steamcommunity.com/profiles/{0}/?xml=1", steamid));
        var xml = new XmlDocument();
        xml.LoadXml(xmlRaw);
        var nameNode = xml.SelectSingleNode("//steamID");
        if (nameNode != null)
          userName = Regex.Unescape(nameNode.InnerText);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      return userName;
    }

    private void CopyResource(string resourceName, string file)
    {
      using (var resource = GetType().Assembly.GetManifestResourceStream(resourceName))
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

    public void SortBadges(string method)
    {
      lblDrops.Text = "Sorting results based on your settings, please wait...";
      switch (method)
      {
        case "mostcards":
          Badges = Badges.OrderByDescending(b => b.RemainingCard).ToList();
          break;
        case "leastcards":
          Badges = Badges.OrderBy(b => b.RemainingCard).ToList();
          break;
        case "mostvalue":
          var query = string.Format("http://api.enhancedsteam.com/market_data/average_card_prices/im.php?appids={0}",
            string.Join(",", Badges.Select(b => b.AppId)));
          var json = new WebClient().DownloadString(query);
          var convertedJson = JsonConvert.DeserializeObject<EnhancedsteamHelper>(json);
          foreach (var price in convertedJson.Avg_Values)
          {
            var badge = Badges.SingleOrDefault(b => b.AppId == price.AppId);
            if (badge != null)
              badge.AveragePrice = price.Avg_Price;
          }
          Badges = Badges.OrderByDescending(b => b.AveragePrice).ToList();
          break;
        default:
          return;
      }
    }

    public void StartIdle(Badge badge)
    {
      foreach (var badgeLeft in Badges.Where(b => b.HoursPlayed < 2 && !Equals(b, badge)))
      {
        TwoHoursProcesses.Add(badgeLeft.Idle());
      }

      // Place user "In game" for card drops
      Idle = badge.Idle();

      // Update game name
      lblGameName.Visible = true;
      lblGameName.Text = GetAppName(badge.StringId);

      // Update game image
      try
      {
        picApp.Load("http://cdn.akamai.steamstatic.com/steam/apps/" + badge.StringId + "/header_292x136.jpg");
        picApp.Visible = true;
      }
      catch (Exception)
      {

      }

      // Update label controls
      lblCurrentRemaining.Text = badge.RemainingCard + " card drops remaining";
      lblCurrentStatus.Text = "Currently in-game";

      // Set progress bar values and show the footer
      pbIdle.Maximum = badge.RemainingCard;
      pbIdle.Value = 0;
      ssFooter.Visible = true;

      // Start the animated "working" gif
      picIdleStatus.Image = Resources.imgSpin;

      // Set the currentAppID value
      CurrentBadge = badge;

      // Start the timer that will check if drops remain
      tmrCardDropCheck.Enabled = true;

      // Reset the timer
      TimeLeft = pbIdle.Maximum != 1 ? 900 : 300;

      // Set the correct buttons on the form for pause / resume
      btnResume.Visible = false;
      btnPause.Visible = true;
      resumeIdlingToolStripMenuItem.Enabled = false;
      pauseIdlingToolStripMenuItem.Enabled = false;
      skipGameToolStripMenuItem.Enabled = false;

      var graphics = CreateGraphics();
      var scale = graphics.DpiY * 3.86;
      Height = Convert.ToInt32(scale);
    }

    public void StopIdle()
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
        var graphics = CreateGraphics();
        var scale = graphics.DpiY * 1.9583;
        Height = Convert.ToInt32(scale);

        // Kill the idling process
        TwoHoursProcesses.Where(p => !p.HasExited).ToList().ForEach(p => p.Kill());
        if (Idle != null && !Idle.HasExited)
          Idle.Kill();
      }
      catch (Exception)
      {

      }
    }

    public void IdleComplete()
    {
      // Deactivate the timer control and inform the user that the program is finished
      tmrCardDropCheck.Enabled = false;
      lblCurrentStatus.Text = "Idling complete";
    }

    public async Task<string> GetHttpAsync(string url)
    {
      var content = string.Empty;
      try
      {
        var cookies = GenerateCookies();
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
        picIdleStatus.Image = Resources.imgSpin;
      }
      catch (Exception)
      {
        // Try again in 60 seconds
        TimeLeft = 60;
        picIdleStatus.Image = Resources.imgFalse;
      }
      return content;
    }

    public async Task LoadBadgesAsync()
    {
      var response = await GetHttpAsync(Settings.Default.myProfileURL + "/badges/");
      if (string.IsNullOrWhiteSpace(response))
      {
        // badge page didn't load
        picReadingPage.Image = null;
        lblDrops.Text = "Badge page didn't load, will retry in 10 seconds";
        ReloadCount = 10;
        tmrBadgeReload.Enabled = true;
        return;
      }
      var document = new HtmlDocument();
      document.LoadHtml(response);
      var user_avatar = document.DocumentNode.SelectNodes("//div[contains(@class,'user_avatar')]");

      try
      {
        var Count = user_avatar.Count;
      }
      catch (Exception)
      {
        // Invalid cookie data
        IsCookieReady = false;
        lnkResetCookies_LinkClicked(null, null);
        picReadingPage.Visible = false;
        return;
      }

      try
      {
        foreach (var badge in document.DocumentNode.SelectNodes("//div[contains(@class,'badge_title_stats')]"))
        {
          var appid = Regex.Match(badge.InnerHtml, @"card_drop_info_gamebadge_(\d+)_").Groups[1].Value;
          var hours = Regex.Match(badge.ChildNodes[2].InnerText.Replace(",", string.Empty), @"\d+").Value;

          if (badge.ChildNodes.All(n => n.Name != "span"))
            continue;

          var remaining = Regex.Match(badge.ChildNodes["span"].InnerText, @"\d+").Value;

          if (!string.IsNullOrWhiteSpace(remaining) && Badges.All(b => b.StringId != appid))
          {
            var onBlacklist = Settings.Default.blacklist.Contains(appid) || (appid == "368020" || appid == "335590");

            if (!onBlacklist)
              Badges.Add(new Badge(appid, remaining, hours));
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
      var pagelink = document.DocumentNode.SelectSingleNode("//a[contains(@class,'pagelink')][last()]");
      if (pagelink != null)
        try
        {
          // Get number of pages from the last pagelink a element
          var last = int.Parse(Regex.Match(pagelink.Attributes["href"].Value, @"(\d+)").Groups[1].Value);
          var i = 1;
          do
          {
            i++;
            response = await GetHttpAsync(Settings.Default.myProfileURL + "/badges/?p=" + i);
            document = new HtmlDocument();
            document.LoadHtml(response);

            foreach (var badge in document.DocumentNode.SelectNodes("//div[contains(@class,'badge_title_stats')]"))
            {
              var appid = Regex.Match(badge.InnerHtml, @"card_drop_info_gamebadge_(\d+)_").Groups[1].Value;
              var hours = Regex.Match(badge.ChildNodes[0].InnerText.Replace(",", "").Trim(), @"\d+").Value;
              var row = badge.SelectNodes(".//span[contains(@class, 'progress_info_bold')]");
              if (row != null)
              {
                foreach (var data in row)
                {
                  if (data != null)
                  {
                    if (Regex.Match(data.InnerHtml, @"\d").Length > 0)
                    {
                      var remaining = Regex.Match(data.InnerHtml, @"(\d+)").Groups[1].Value;
                      if (Badges.All(b => b.StringId != appid))
                      {
                        var onBlacklist = Settings.Default.blacklist.Cast<string>().Any(blApp => blApp == appid);
                        if (!onBlacklist)
                          Badges.Add(new Badge(appid, remaining, hours));
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

      SortBadges(Settings.Default.sort);

      picReadingPage.Visible = false;
      lblIdle.Text = Badges.Count + " games left to idle";
      lblIdle.Visible = true;
      lblDrops.Text = CardsRemaining + " card drops remaining";
      lblDrops.Visible = true;

      if (CardsRemaining == 0)
      {
        IdleComplete();
      }
    }

    public async Task CheckCardDrops(Badge badge)
    {
      try
      {
        var response = await GetHttpAsync(Settings.Default.myProfileURL + "/gamecards/" + badge.StringId + "/");
        var document = new HtmlDocument();
        document.LoadHtml(response);
        var badgeNode = document.DocumentNode.SelectNodes("//div[contains(@class,'badge_title_stats')]")[0];

        var hours = Regex.Match(badgeNode.ChildNodes[2].InnerText.Replace(",", string.Empty), @"\d+").Value;
        var numDrops = Regex.Match(badgeNode.ChildNodes["span"].InnerText, @"\d+").Value; ;
        lblCurrentRemaining.Text = numDrops + " card drops remaining";
        int intDrops;
        if (int.TryParse(Regex.Match(numDrops, @"\d+").Value, out intDrops))
        {
          badge.RemainingCard = intDrops;
          badge.HoursPlayed = int.Parse(hours);

          // Resets the clock based on the number of remaining drops
          TimeLeft = intDrops == 1 ? 300 : 900;
        }
        else
        {
          Badges.RemoveAll(b => Equals(b, badge));

          // Stop idling the current game
          StopIdle();

          if (Badges.Any())
          {
            // Give the user notification that the next game will start soon
            lblCurrentStatus.Text = "Loading next game...";

            // Make a short but random amount of time pass
            var rand = new Random();
            var wait = rand.Next(3, 9);
            wait = wait * 1000;

            tmrStartNext.Interval = wait;
            tmrStartNext.Enabled = true;
          }
          else
          {
            IdleComplete();
          }
        }
        // Update totals
        lblIdle.Text = GamesRemaining + " games left to idle";
        lblDrops.Text = CardsRemaining + " card drops remaining";
        pbIdle.Value = pbIdle.Maximum - badge.RemainingCard;
      }
      catch (Exception)
      {

      }
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
      if (Settings.Default.updateNeeded)
      {
        Settings.Default.Upgrade();
        Settings.Default.updateNeeded = false;
        Settings.Default.Save();
      }

      // Set the form height
      var graphics = CreateGraphics();
      var scale = graphics.DpiY * 1.625;
      Height = Convert.ToInt32(scale);

      // Set the location of certain elements so that they scale correctly for different DPI settings
      var point = new Point(Convert.ToInt32(graphics.DpiX * 1.14), Convert.ToInt32(lblGameName.Location.Y));
      lblGameName.Location = point;
      point = new Point(Convert.ToInt32(graphics.DpiX * 2.35), Convert.ToInt32(lnkSignIn.Location.Y));
      lnkSignIn.Location = point;
      point = new Point(Convert.ToInt32(graphics.DpiX * 2.15), Convert.ToInt32(lnkResetCookies.Location.Y));
      lnkResetCookies.Location = point;
    }

    private void frmMain_FormClose(object sender, FormClosedEventArgs e)
    {
      try
      {
        StopIdle();
      }
      catch (Exception)
      {

      }
    }

    private void tmrCheckCookieData_Tick(object sender, EventArgs e)
    {
      var connected = !string.IsNullOrWhiteSpace(Settings.Default.sessionid) && !string.IsNullOrWhiteSpace(Settings.Default.steamLogin);

      lblCookieStatus.Text = connected ? "Idle Master is connected to Steam" : "Idle Master is not connected to Steam";
      lblCookieStatus.ForeColor = connected ? Color.Green : Color.Black;
      picCookieStatus.Image = connected ? Resources.imgTrue : Resources.imgFalse;
      lnkSignIn.Visible = !connected;
      lnkResetCookies.Visible = connected;
      IsCookieReady = connected;
    }

    private void tmrCheckSteam_Tick(object sender, EventArgs e)
    {
      var isSteamRunning = SteamAPI.IsSteamRunning() || Settings.Default.ignoreclient;
      lblSteamStatus.Text = isSteamRunning ? (Settings.Default.ignoreclient ? "Steam client status ignored" : "Steam is running") : "Steam is not running";
      lblSteamStatus.ForeColor = isSteamRunning ? Color.Green : Color.Black;
      picSteamStatus.Image = isSteamRunning ? Resources.imgTrue : Resources.imgFalse;
      tmrCheckSteam.Interval = isSteamRunning ? 5000 : 500;
      skipGameToolStripMenuItem.Enabled = isSteamRunning;
      pauseIdlingToolStripMenuItem.Enabled = isSteamRunning;
      IsSteamReady = isSteamRunning;
    }

    private void lblGameName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://store.steampowered.com/app/" + CurrentBadge.StringId);
    }

    private void lnkResetCookies_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      // Clear the settings
      Settings.Default.sessionid = string.Empty;
      Settings.Default.steamLogin = string.Empty;
      Settings.Default.myProfileURL = string.Empty;
      Settings.Default.steamparental = string.Empty;
      Settings.Default.Save();

      // Stop the steam-idle process
      StopIdle();

      // Clear the badges list
      Badges.Clear();

      // Resize the form
      var graphics = CreateGraphics();
      var scale = graphics.DpiY * 1.625;
      Height = Convert.ToInt32(scale);

      // Set timer intervals
      tmrCheckSteam.Interval = 500;
      tmrCheckCookieData.Interval = 500;

      // Hide lblDrops and lblIdle
      lblDrops.Visible = false;
      lblIdle.Visible = false;

      // Set IsCookieReady to false
      IsCookieReady = false;

      // Re-enable tmrReadyToGo
      tmrReadyToGo.Enabled = true;
    }

    private void lnkSignIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      var frm = new frmBrowser();
      frm.ShowDialog();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Close();
    }

    private async void tmrReadyToGo_Tick(object sender, EventArgs e)
    {
      if (!IsCookieReady || !IsSteamReady)
        return;

      // Update the form elements
      if (Settings.Default.showUsername)
      {
        lblSignedOnAs.Text = "Signed in as " + GetUserName(Settings.Default.steamLogin.Substring(0, 17));
        lblSignedOnAs.Visible = true;
      }

      lblDrops.Visible = true;
      lblDrops.Text = "Reading badge page, please wait...";
      lblIdle.Visible = false;
      picReadingPage.Visible = true;

      tmrReadyToGo.Enabled = false;

      // Call the loadBadges() function asynchronously
      await LoadBadgesAsync();

      if (Badges.Any())
        StartIdle(Badges.First());
      else
        IdleComplete();
    }

    private async void tmrCardDropCheck_Tick(object sender, EventArgs e)
    {
      if (TimeLeft <= 0)
      {
        tmrCardDropCheck.Enabled = false;
        await CheckCardDrops(CurrentBadge);

        Badges
          .Where(b => b.IdleProcess != null)
          .ToList()
          .ForEach(async b => b.CheckCardDrops(await GetHttpAsync(Settings.Default.myProfileURL + "/gamecards/" + b.StringId + "/")));
        Badges
          .Where(b => !Equals(b, CurrentBadge) && b.HoursPlayed >= 2 && b.IdleProcess != null)
          .ToList()
          .ForEach(b => b.StopIdle());

        tmrCardDropCheck.Enabled = Badges.Any() && TimeLeft != 0;
      }
      else
      {
        TimeLeft = TimeLeft - 1;
        var minutes = TimeLeft / 60;
        var seconds = TimeLeft - (minutes * 60);
        lblTimer.Text = minutes + ":" + seconds.ToString().PadLeft(2, '0');
      }
    }

    private void btnSkip_Click(object sender, EventArgs e)
    {
      if (!IsSteamReady)
        return;

      Badges.RemoveAll(b => Equals(b, CurrentBadge));
      StopIdle();
      if (Badges.Any())
        StartIdle(Badges.First());
      else
        IdleComplete();
    }

    private void btnPause_Click(object sender, EventArgs e)
    {
      if (!IsSteamReady)
        return;

      // Stop the steam-idle process
      StopIdle();

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

    private void btnResume_Click(object sender, EventArgs e)
    {
      // Resume idling
      if (Badges.Any())
        StartIdle(CurrentBadge);
      else
        IdleComplete();

      pauseIdlingToolStripMenuItem.Enabled = true;
      resumeIdlingToolStripMenuItem.Enabled = false;
    }

    private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // Show the form
      var previous = Settings.Default.sort;
      var frm = new frmSettings();
      frm.ShowDialog();

      if (previous != Settings.Default.sort)
      {
        StopIdle();
        Badges.Clear();
        tmrReadyToGo.Enabled = true;
      }

      if (Settings.Default.showUsername)
        lblSignedOnAs.Text = "Signed in as " + GetUserName(Settings.Default.steamLogin.Substring(0, 17));

      lblSignedOnAs.Visible = Settings.Default.showUsername;
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
      var frm = new frmAbout();
      frm.ShowDialog();
    }

    private void frmMain_Resize(object sender, EventArgs e)
    {
      if (WindowState == FormWindowState.Minimized)
      {
        if (Settings.Default.minToTray)
        {
          notifyIcon1.Visible = true;
          Hide();
        }
      }
      else if (WindowState == FormWindowState.Normal)
      {
        notifyIcon1.Visible = false;
      }
    }

    private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      Show();
      WindowState = FormWindowState.Normal;
    }

    private void lblCurrentRemaining_Click(object sender, EventArgs e)
    {
      if (TimeLeft > 2)
      {
        TimeLeft = 2;
      }
    }

    private void blacklistToolStripMenuItem_Click(object sender, EventArgs e)
    {
      var frm = new frmBlacklist();
      frm.ShowDialog();

      if (Settings.Default.blacklist.Cast<string>().Any(appid => appid == CurrentBadge.StringId))
        btnSkip.PerformClick();
    }

    private void blacklistCurrentGameToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Settings.Default.blacklist.Add(CurrentBadge.StringId);
      Settings.Default.Save();

      btnSkip.PerformClick();
    }

    private void tmrStartNext_Tick(object sender, EventArgs e)
    {
      StartIdle(Badges.First());
      tmrStartNext.Enabled = false;
    }

    private void changelogToolStripMenuItem_Click(object sender, EventArgs e)
    {
      var frm = new frmChangelog();
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
      lblDrops.Text = "Badge page didn't load, will retry in " + ReloadCount + " seconds";

      if (ReloadCount == 0)
      {
        tmrBadgeReload.Enabled = false;
        tmrReadyToGo.Enabled = true;
      }
    }
  }
}