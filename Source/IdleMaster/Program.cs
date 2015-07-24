using System;
using System.Windows.Forms;

namespace IdleMaster
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.ThreadException += (o, a) => Logger.Exception(a.Exception);
      Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new frmMain());
    }
  }
}
