using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
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

    public static async Task<string> GetHttpAsync(string url, int count = 3)
    {
      while (true)
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
        
        if (!string.IsNullOrWhiteSpace(content) || count == 0) 
          return content;

        count = count - 1;
      }
    }

    public static async Task<bool> IsLogined()
    {
      var response = await GetHttpAsync(Settings.Default.myProfileURL);
      var document = new HtmlDocument();
      document.LoadHtml(response);
      return document.DocumentNode.SelectSingleNode("//a[@class=\"global_action_link\"]") == null;
    }

    public CookieClient()
    {
      Cookie = GenerateCookies();
      Encoding = Encoding.UTF8;
    }
  }
}
