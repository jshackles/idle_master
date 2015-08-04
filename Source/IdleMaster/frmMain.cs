using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
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
    public List<Badge> AllBadges { get; set; }

    public IEnumerable<Badge> CanIdleBadges
    {
      get { return AllBadges.Where(b => b.RemainingCard != 0); }
    }

    public bool IsCookieReady;
    public bool IsSteamReady;
    public int TimeLeft = 900;
    public int CardsRemaining { get { return CanIdleBadges.Sum(b => b.RemainingCard); } }
    public int GamesRemaining { get { return CanIdleBadges.Count(); } }
    public Badge CurrentBadge;

    internal void UpdateStateInfo()
    {
      // Update totals
      lblIdle.Text = string.Format("{0} games left to idle, {1} idle now.", GamesRemaining, CanIdleBadges.Count(b => b.InIdle));
      lblDrops.Text = CardsRemaining + " card drops remaining";
      lblIdle.Visible = GamesRemaining != 0;
      lblDrops.Visible = CardsRemaining != 0;
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
          AllBadges = AllBadges.OrderByDescending(b => b.RemainingCard).ToList();
          break;
        case "leastcards":
          AllBadges = AllBadges.OrderBy(b => b.RemainingCard).ToList();
          break;
        case "mostvalue":
          var query = string.Format("http://api.enhancedsteam.com/market_data/average_card_prices/im.php?appids={0}",
            string.Join(",", AllBadges.Select(b => b.AppId)));
          var json = new WebClient() { Encoding = Encoding.UTF8 }.DownloadString(query);
          var convertedJson = JsonConvert.DeserializeObject<EnhancedsteamHelper>(json);
          foreach (var price in convertedJson.Avg_Values)
          {
            var badge = AllBadges.SingleOrDefault(b => b.AppId == price.AppId);
            if (badge != null)
              badge.AveragePrice = price.Avg_Price;
          }
          AllBadges = AllBadges.OrderByDescending(b => b.AveragePrice).ToList();
          break;
        default:
          return;
      }
    }

    public void UpdateIdleProcesses()
    {
      foreach (var badge in CanIdleBadges.Where(b => !Equals(b, CurrentBadge)))
      {
        if (badge.HoursPlayed >= 2 && badge.InIdle)
          badge.StopIdle();

        if (badge.HoursPlayed < 2 && CanIdleBadges.Count(b => b.InIdle) < 30)
          badge.Idle();
      }

      RefreshGamesStateListView();

      if (!CanIdleBadges.Any(b => b.InIdle))
        NextIdle();

      UpdateStateInfo();
    }

    private void NextIdle()
    {
      // Stop idling the current game
      StopIdle();

      if (CanIdleBadges.Any())
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

    private void StartIdle()
    {
      if (CanIdleBadges.Any())
      {
        if (Settings.Default.OnlyOneGameIdle)
          StartSoloIdle(CanIdleBadges.First());
        else
        {
            var multi = CanIdleBadges.Where(b => b.HoursPlayed < 2);
            if (multi.Any())
            {
                StartMultipleIdle();
            }
            else
            {
                StartSoloIdle(CanIdleBadges.First());
            }
        }
      }
      else
        IdleComplete();

      UpdateStateInfo();
    }

    public void StartSoloIdle(Badge badge)
    {
      // Set the currentAppID value
      CurrentBadge = badge;

      // Place user "In game" for card drops
      CurrentBadge.Idle();

      // Update game name
      lblGameName.Visible = true;
      lblGameName.Text = CurrentBadge.Name;

      GamesState.Visible = false;

      // Update game image
      try
      {
        picApp.Load("http://cdn.akamai.steamstatic.com/steam/apps/" + CurrentBadge.StringId + "/header_292x136.jpg");
        picApp.Visible = true;
      }
      catch (Exception ex)
      {
        Logger.Exception(ex, "frmMain -> StartIdle -> load pic, for id = " + CurrentBadge.AppId);
      }

      // Update label controls
      lblCurrentRemaining.Text = CurrentBadge.RemainingCard + " card drops remaining";
      lblCurrentStatus.Text = "Currently in-game";

      // Set progress bar values and show the footer
      pbIdle.Maximum = CurrentBadge.RemainingCard;
      pbIdle.Value = 0;
      ssFooter.Visible = true;

      // Start the animated "working" gif
      picIdleStatus.Image = Resources.imgSpin;

      // Start the timer that will check if drops remain
      tmrCardDropCheck.Enabled = true;

      // Reset the timer
      TimeLeft = CurrentBadge.RemainingCard == 1 ? 300 : 900;

      // Set the correct buttons on the form for pause / resume
      btnResume.Visible = false;
      btnPause.Visible = true;
      btnSkip.Visible = true;
      resumeIdlingToolStripMenuItem.Enabled = false;
      pauseIdlingToolStripMenuItem.Enabled = false;
      skipGameToolStripMenuItem.Enabled = false;

      var scale = CreateGraphics().DpiY * 3.9;
      Height = Convert.ToInt32(scale);
    }

    public void StartMultipleIdle()
    {
      UpdateIdleProcesses();

      // Update label controls
      lblCurrentRemaining.Text = "Update games status";
      lblCurrentStatus.Text = "Currently in-game";

      lblGameName.Visible = false;
      ssFooter.Visible = true;

      // Start the animated "working" gif
      picIdleStatus.Image = Resources.imgSpin;

      // Start the timer that will check if drops remain
      tmrCardDropCheck.Enabled = true;

      // Reset the timer
      TimeLeft = 360;

      // Show game
      GamesState.Visible = true;
      picApp.Visible = false;
      RefreshGamesStateListView();

      // Set the correct buttons on the form for pause / resume
      btnResume.Visible = false;
      btnPause.Visible = false;
      btnSkip.Visible = false;
      resumeIdlingToolStripMenuItem.Enabled = false;
      pauseIdlingToolStripMenuItem.Enabled = false;
      skipGameToolStripMenuItem.Enabled = false;

      var scale = CreateGraphics().DpiY * 3.86;
      Height = Convert.ToInt32(scale);
    }

    private void RefreshGamesStateListView()
    {
      GamesState.Items.Clear();
      foreach (var badge in CanIdleBadges.Where(b => b.InIdle))
      {
        var line = new ListViewItem(badge.Name);
        line.SubItems.Add(badge.HoursPlayed.ToString());
        GamesState.Items.Add(line);
      }
    }

    public void StopIdle()
    {
      try
      {
        lblGameName.Visible = false;
        picApp.Image = null;
        picApp.Visible = false;
        GamesState.Visible = false;
        btnPause.Visible = false;
        btnSkip.Visible = false;
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
        foreach (var badge in AllBadges.Where(b => b.InIdle))
          badge.StopIdle();
      }
      catch (Exception ex)
      {
        Logger.Exception(ex, "frmMain -> StopIdle");
      }
    }

    public void IdleComplete()
    {
      // Deactivate the timer control and inform the user that the program is finished
      tmrCardDropCheck.Enabled = false;
      lblCurrentStatus.Text = "Idling complete";
    }


    public async Task LoadBadgesAsync()
    {
      var profileLink = Settings.Default.myProfileURL + "/badges";
      var document = new HtmlDocument();
      var pages = new List<string>() { "?p=1" };

      try
      {
        for (var i = 0; i < pages.Count; i++)
        {
          var response = await CookieClient.GetHttpAsync(profileLink + pages[i]);
          document.LoadHtml(response);

          var pageNodes = document.DocumentNode.SelectNodes("//a[@class=\"pagelink\"]");
          if (pageNodes != null)
          {
            pages.AddRange(pageNodes.Select(p => p.Attributes["href"].Value).Distinct());
            pages = pages.Distinct().ToList();
          }

          foreach (var badge in document.DocumentNode.SelectNodes("//div[@class=\"badge_row is_link\"]"))
          {
            var appIdNode = badge.SelectSingleNode(".//a[@class=\"badge_row_overlay\"]").Attributes["href"].Value;
            var appid = Regex.Match(appIdNode, @"gamecards/(\d+)/").Groups[1].Value;

            if (string.IsNullOrWhiteSpace(appid) || Settings.Default.blacklist.Contains(appid) || appid == "368020" || appid == "335590")
              continue;

            var hoursNode = badge.SelectSingleNode(".//div[@class=\"badge_title_stats_playtime\"]");
            var hours = hoursNode == null ? string.Empty : Regex.Match(hoursNode.InnerText, @"[0-9\.,]+").Value;

            var nameNode = badge.SelectSingleNode(".//div[@class=\"badge_title\"]");
            var name = WebUtility.HtmlDecode(nameNode.FirstChild.InnerText).Trim();

            var cardNode = badge.SelectSingleNode(".//span[@class=\"progress_info_bold\"]");
            var cards = cardNode == null ? string.Empty : Regex.Match(cardNode.InnerText, @"[0-9]+").Value;

            var badgeInMemory = AllBadges.FirstOrDefault(b => b.StringId == appid);
            if (badgeInMemory != null)
              badgeInMemory.UpdateStats(cards, hours);
            else
              AllBadges.Add(new Badge(appid, name, cards, hours));
          }
        }
      }
      catch (Exception ex)
      {
        Logger.Exception(ex, "Badge -> LoadBadgesAsync, for profile = " + Settings.Default.myProfileURL);
        // badge page didn't load
        picReadingPage.Image = null;
        lblDrops.Text = "Badge page didn't load, will retry in 10 seconds";
        ReloadCount = 10;
        tmrBadgeReload.Enabled = true;
        return;
      }

      SortBadges(Settings.Default.sort);

      picReadingPage.Visible = false;
      UpdateStateInfo();

      if (CardsRemaining == 0)
      {
        IdleComplete();
      }
    }

    public async Task CheckCardDrops(Badge badge)
    {
      if (!await badge.CanCardDrops())
        NextIdle();
      else
      {
        // Resets the clock based on the number of remaining drops
        TimeLeft = badge.RemainingCard == 1 ? 300 : 900;
      }

      lblCurrentRemaining.Text = badge.RemainingCard + " card drops remaining";
      pbIdle.Value = pbIdle.Maximum - badge.RemainingCard;
      UpdateStateInfo();
    }

    public frmMain()
    {
      InitializeComponent();
      AllBadges = new List<Badge>();
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
      StopIdle();
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
      Process.Start("http://store.steampowered.com/app/" + CurrentBadge.AppId);
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
      AllBadges.Clear();

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
        lblSignedOnAs.Text = SteamProfile.GetSignedAs();
        lblSignedOnAs.Visible = true;
      }

      lblDrops.Visible = true;
      lblDrops.Text = "Reading badge page, please wait...";
      lblIdle.Visible = false;
      picReadingPage.Visible = true;

      tmrReadyToGo.Enabled = false;

      // Call the loadBadges() function asynchronously
      await LoadBadgesAsync();

      StartIdle();
    }


    private async void tmrCardDropCheck_Tick(object sender, EventArgs e)
    {
      if (TimeLeft <= 0)
      {
        tmrCardDropCheck.Enabled = false;
        if (CurrentBadge != null)
        {
          CurrentBadge.Idle();
          await CheckCardDrops(CurrentBadge);
        }

        var isMultipleIdle = CanIdleBadges.Any(b => !Equals(b, CurrentBadge) && b.InIdle);
        if (isMultipleIdle)
        {
          await LoadBadgesAsync();
          UpdateIdleProcesses();

          isMultipleIdle = CanIdleBadges.Any(b => b.HoursPlayed < 2 && b.InIdle);
          if (isMultipleIdle)
            TimeLeft = 360;
        }

        tmrCardDropCheck.Enabled = CanIdleBadges.Any() && TimeLeft != 0;
      }
      else
      {
        TimeLeft = TimeLeft - 1;
        lblTimer.Text = TimeSpan.FromSeconds(TimeLeft).ToString(@"mm\:ss");
      }
    }

    private void btnSkip_Click(object sender, EventArgs e)
    {
      if (!IsSteamReady)
        return;

      StopIdle();
      AllBadges.RemoveAll(b => Equals(b, CurrentBadge));
      StartIdle();
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
      StartIdle();

      pauseIdlingToolStripMenuItem.Enabled = true;
      resumeIdlingToolStripMenuItem.Enabled = false;
    }

    private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // Show the form
      String previous = Settings.Default.sort;
      Boolean previous_behavior = Settings.Default.OnlyOneGameIdle;
      Form frm = new frmSettings();
      frm.ShowDialog();

      if (previous != Settings.Default.sort || previous_behavior != Settings.Default.OnlyOneGameIdle)
      {
        StopIdle();
        AllBadges.Clear();
        tmrReadyToGo.Enabled = true;
      }

      if (Settings.Default.showUsername)
        lblSignedOnAs.Text = SteamProfile.GetSignedAs();

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
      tmrStartNext.Enabled = false;
      StartIdle();
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