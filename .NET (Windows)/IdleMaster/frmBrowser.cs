using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace IdleMaster
{
    public partial class frmBrowser : Form
    {

        public int seconds_waiting = 20;

        public frmBrowser()
        {
            // This initializes the components on the form
            InitializeComponent();
        }

        private void frmBrowser_Load(object sender, EventArgs e)
        {
            // When the form is loaded, navigate to the Steam login page using the web browser control
            wbAuth.Navigate("https://steamcommunity.com/login/home/?goto=my/profile");            
        }

        // This code block executes each time a new document is loaded into the web browser control
        private void wbAuth_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {   
            // Find the page header, and remove it.  This gives the login form a more streamlined look.
            dynamic htmldoc = wbAuth.Document.DomDocument as dynamic;
            dynamic global_header = htmldoc.GetElementById("global_header") as dynamic;
            if (global_header != null)
            {
                global_header.parentNode.removeChild(global_header);
            }

            // Get the URL of the page that just finished loading
            string url = wbAuth.Url.AbsoluteUri;

            // If the page it just finished loading isn't the login page
            if (url != "https://steamcommunity.com/login/home/?goto=my/profile" && url != "https://store.steampowered.com//login/transfer" && url.StartsWith("javascript:") == false && url.StartsWith("about:") == false)
            {
                // Get a list of cookies from the current page
                CookieContainer container = GetUriCookieContainer(wbAuth.Url);
                var cookies = container.GetCookies(wbAuth.Url);

                // Go through the cookie data so that we can extract the cookies we are looking for
                foreach (Cookie cookie in cookies)
                {
                    // Save the "sessionid" cookie
                    if (cookie.Name == "sessionid")
                    {                        
                        Properties.Settings.Default.sessionid = cookie.Value;
                    }

                    // Save the "steamLogin" cookie and construct and save the user's profile link
                    else if (cookie.Name == "steamLogin")
                    {
                        Properties.Settings.Default.steamLogin = cookie.Value;
                        Properties.Settings.Default.myProfileURL = "http://steamcommunity.com/profiles/" + cookie.Value.Substring(0,17);
                    }

                    // Save the "steamparental" cookie"
                    else if (cookie.Name == "steamparental")
                    {
                        Properties.Settings.Default.steamparental = cookie.Value;
                    }
                }

                // Save all of the data to the program settings file, and close this form
                Properties.Settings.Default.Save();
                this.Close();
            }
        }

        // Imports the InternetGetCookieEx function from wininet.dll which allows the application to access the cookie data from the web browser control
        // Reference: http://stackoverflow.com/questions/3382498/is-it-possible-to-transfer-authentication-from-webbrowser-to-webrequest
        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetGetCookieEx(
            string url,
            string cookieName,
            StringBuilder cookieData,
            ref int size,
            Int32 dwFlags,
            IntPtr lpReserved);

        // Assigns the hex value for the DLL flag that allows Idle Master to be able to access cookie data marked as "HTTP Only"
        private const Int32 InternetCookieHttponly = 0x2000;

        // This function returns cookie data based on a uniform resource identifier
        public static CookieContainer GetUriCookieContainer(Uri uri)
        {
            // First, create a null cookie container
            CookieContainer cookies = null;

            // Determine the size of the cookie
            int datasize = 8192 * 16;
            StringBuilder cookieData = new StringBuilder(datasize);

            // Call InternetGetCookieEx from wininet.dll
            if (!InternetGetCookieEx(uri.ToString(), null, cookieData, ref datasize, InternetCookieHttponly, IntPtr.Zero))
            {
                if (datasize < 0)
                    return null;
                // Allocate stringbuilder large enough to hold the cookie
                cookieData = new StringBuilder(datasize);
                if (!InternetGetCookieEx(
                    uri.ToString(),
                    null, cookieData,
                    ref datasize,
                    InternetCookieHttponly,
                    IntPtr.Zero))
                    return null;
            }

            // If the cookie contains data, add it to the cookie container
            if (cookieData.Length > 0)
            {
                cookies = new CookieContainer();
                cookies.SetCookies(uri, cookieData.ToString().Replace(';', ','));
            }

            // Return the cookie container
            return cookies;
        }

        // This code executes each time the web browser control is in the process of navigating
        private void wbAuth_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            // Get the url that's being navigated to
            String url = e.Url.AbsoluteUri;

            // Check to see if the page it's navigating to isn't the Steam login page or related calls
            if (url != "https://steamcommunity.com/login/home/?goto=my/profile" && url != "https://store.steampowered.com//login/transfer" && url.StartsWith("javascript:") == false && url.StartsWith("about:") == false)
            {
                // start the sanity check timer
                tmrCheck.Enabled = true;

                // If it's navigating to a page other than the Steam login page, hide the browser control and resize the form
                wbAuth.Visible = false;
                this.Height = 81;
                this.Width = 272;
            }            
        }

        private void tmrCheck_Tick(object sender, EventArgs e)
        {
            // Prevents the application from "saving" for more than 30 seconds and will attempt to save the cookie data after that time
            if (seconds_waiting > 0)
            {
                seconds_waiting = seconds_waiting - 1;
            }
            else
            {
                tmrCheck.Enabled = false;
                wbAuth_DocumentCompleted(null, null);
            }
        }
    }
}
