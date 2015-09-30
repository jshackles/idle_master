﻿using System;
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

        protected override WebResponse GetWebResponse(WebRequest request, System.IAsyncResult result)
        {
            try
            {
                var baseResponse = base.GetWebResponse(request);

                var cookies = (baseResponse as HttpWebResponse).Cookies;

                // Check, if cookie should be deleted. This means that sessionID is now invalid and user has to log in again.
                // Maybe this shoud be done other way (authenticate exception), but because of shared settings and timers in frmMain...
                if (cookies.Count > 0)
                {
                    if (cookies["steamLogin"].Value == "deleted")
                    {
                        Settings.Default.sessionid = string.Empty;
                        Settings.Default.steamLogin = string.Empty;
                        Settings.Default.steamparental = string.Empty;
                        Settings.Default.steamMachineAuth = string.Empty;
                        Settings.Default.steamRememberLogin = string.Empty;
                        Settings.Default.Save();
                    }
                }

                this.ResponseUri = baseResponse.ResponseUri;
                return baseResponse;                
            }
            catch (Exception)
            {

            }
            return null;
        }

        public static CookieContainer GenerateCookies()
        {
            var cookies = new CookieContainer();
            var target = new Uri("http://steamcommunity.com");
            cookies.Add(new Cookie("sessionid", Settings.Default.sessionid) { Domain = target.Host });
            cookies.Add(new Cookie("steamLogin", Settings.Default.steamLogin) { Domain = target.Host });
            cookies.Add(new Cookie("steamparental", Settings.Default.steamparental) { Domain = target.Host });
            cookies.Add(new Cookie("steamRememberLogin", Settings.Default.steamRememberLogin) { Domain = target.Host });
            cookies.Add(new Cookie(GetSteamMachineAuthCookieName(), Settings.Default.steamMachineAuth) { Domain = target.Host });
            return cookies;
        }

        public static string GetSteamMachineAuthCookieName()
        {
            if (Settings.Default.steamLogin != null && Settings.Default.steamLogin.Length > 17)
                return string.Format("steamMachineAuth{0}", Settings.Default.steamLogin.Substring(0, 17));
            return "steamMachineAuth";
        }

        public static async Task<string> GetHttpAsync(string url, int count = 3)
        {
            while (true)
            {
                var client = new CookieClient();
                var content = string.Empty;
                try
                {
                    // If user is NOT authenticated (cookie got deleted in GetWebResponse()), return empty result
                    if (String.IsNullOrEmpty(Settings.Default.sessionid))
                    {
                        return string.Empty;
                    }

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
