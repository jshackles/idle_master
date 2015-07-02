using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
