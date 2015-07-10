using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IdleMaster.Properties;

namespace IdleMaster
{
  public class CookieClient : WebClient
  {
    internal CookieContainer Cookie = new CookieContainer();

    internal Uri ResponseUri;

    protected override WebRequest GetWebRequest(Uri address)
    {
      var request = base.GetWebRequest(address);
      if (request is HttpWebRequest)
        (request as HttpWebRequest).CookieContainer = Cookie;
      return request;
    }

    protected override WebResponse GetWebResponse(WebRequest request)
    {
      var baseResponse = base.GetWebResponse(request);
      this.ResponseUri = baseResponse.ResponseUri;
      return baseResponse;
    }

    public static CookieContainer GenerateCookies()
    {
      var cookies = new CookieContainer();
      var target = new Uri("http://steamcommunity.com");
      cookies.Add(new Cookie("sessionid", Settings.Default.sessionid) { Domain = target.Host });
      cookies.Add(new Cookie("steamLogin", Settings.Default.steamLogin) { Domain = target.Host });
      cookies.Add(new Cookie("steamparental", Settings.Default.steamparental) { Domain = target.Host });
      return cookies;
    }

    public static async Task<string> GetHttpAsync(string url)
    {
      var client = new CookieClient();
      var content = string.Empty;
      try
      {
        content = await client.DownloadStringTaskAsync(url);
      }
      catch (Exception ex)
      {
        Logger.Exception(ex, "CookieClient -> GetHttpAsync, for url = " + url);
      }
      return content;
    }

    public CookieClient()
    {
      Cookie = GenerateCookies();
      Encoding = Encoding.UTF8;
    }
  }
}
