using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IdleMaster
{
  public class Badge
  {
    public int AppId { get; set; }
    public string StringId
    {
      get { return this.AppId.ToString(); }
      set { this.AppId = string.IsNullOrWhiteSpace(value) ? 0 : int.Parse(value); }
    }
    public int RemainingCard { get; set; }
    public int HoursPlayed { get; set; }

    public Process IdleProcess;

    public Process Idle()
    {
      this.IdleProcess = Process.Start(new ProcessStartInfo("steam-idle.exe", this.AppId.ToString()) { WindowStyle = ProcessWindowStyle.Hidden });
      return this.IdleProcess;
    }

    public void StopIdle()
    {
      if (this.IdleProcess != null && !this.IdleProcess.HasExited)
        this.IdleProcess.Kill();
    }

    public void CheckCardDrops(string badgePage)
    {
      try
      {
        HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
        document.LoadHtml(badgePage);
        var badgeNode = document.DocumentNode.SelectNodes("//div[contains(@class,'badge_title_stats')]")[0];

        string hours = Regex.Match(badgeNode.ChildNodes[2].InnerText.Replace(",", string.Empty), @"\d+").Value;
        string numDrops = Regex.Match(badgeNode.ChildNodes["span"].InnerText, @"\d+").Value; ;
        int intDrops;
        if (Int32.TryParse(Regex.Match(numDrops, @"\d+").Value, out intDrops))
          this.RemainingCard = intDrops;
        this.HoursPlayed = int.Parse(hours);
      }
      catch (Exception)
      {

      }
      return;
    }

    public override bool Equals(object obj)
    {
      if (obj is Badge)
        return Equals(this.AppId, ((Badge)obj).AppId);
      return false;
    }

    public override int GetHashCode()
    {
      return this.AppId.GetHashCode();
    }

    public Badge(string id, string remaining, string hours) : this()
    {
      this.StringId = id;
      this.RemainingCard = string.IsNullOrWhiteSpace(remaining) ? 0 : int.Parse(remaining);
    //this.HoursPlayed = string.IsNullOrWhiteSpace(hours) ? 0 : int.Parse(hours);
      this.HoursPlayed = 1;
    }

    public Badge()
    { }
  }
}
