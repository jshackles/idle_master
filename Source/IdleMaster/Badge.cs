using System.Diagnostics;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace IdleMaster
{
  public class Badge
  {
    public double AveragePrice { get; set; }

    public int AppId { get; set; }

    public string StringId
    {
      get { return AppId.ToString(); }
      set { AppId = string.IsNullOrWhiteSpace(value) ? 0 : int.Parse(value); }
    }

    public int RemainingCard { get; set; }

    public int HoursPlayed { get; set; }


    public Process IdleProcess;

    public Process Idle()
    {
      IdleProcess = Process.Start(new ProcessStartInfo("steam-idle.exe", AppId.ToString()) { WindowStyle = ProcessWindowStyle.Hidden });
      return IdleProcess;
    }

    public void StopIdle()
    {
      if (IdleProcess != null && !IdleProcess.HasExited)
        IdleProcess.Kill();
    }

    public void CheckCardDrops(string badgePage)
    {
      try
      {
        var document = new HtmlDocument();
        document.LoadHtml(badgePage);
        var badgeNode = document.DocumentNode.SelectNodes("//div[contains(@class,'badge_title_stats')]")[0];

        var hours = Regex.Match(badgeNode.ChildNodes[2].InnerText.Replace(",", string.Empty), @"\d+").Value;
        var numDrops = Regex.Match(badgeNode.ChildNodes["span"].InnerText, @"\d+").Value; ;
        int intDrops;
        if (int.TryParse(Regex.Match(numDrops, @"\d+").Value, out intDrops))
          RemainingCard = intDrops;
        HoursPlayed = int.Parse(hours);
      }
      catch { }
    }

    public override bool Equals(object obj)
    {
      var badge = obj as Badge;
      return badge != null && Equals(AppId, badge.AppId);
    }

    public override int GetHashCode()
    {
      return AppId.GetHashCode();
    }

    public Badge(string id, string remaining, string hours)
      : this()
    {
      StringId = id;
      RemainingCard = string.IsNullOrWhiteSpace(remaining) ? 0 : int.Parse(remaining);
      HoursPlayed = string.IsNullOrWhiteSpace(hours) ? 0 : int.Parse(hours);
    }

    public Badge()
    { }
  }
}
