using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelProject
{
    public partial class StaffDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // if it's not the first time entering the page...
            if (!IsPostBack)
            {
                // fetch the current username via cookies
                string currUsername = GetCurrUsername();

                // display username with welcome message
                WelcomeLabel.Text = $"Welcome, {currUsername}";

                // set your username and password for cookie TryIt display
                HttpCookie myCookies = Request.Cookies["myCookieId2"];
                if (myCookies != null)
                {
                    CookieUsername.Text = "Entered Username: " + myCookies["Username"];
                    CookiePassword.Text = "Entered Password: " + myCookies["Password"];
                }
            }
        }

        // gets the username for display purposes
        protected string GetCurrUsername()
        {
            // we first look at the session state
            string username = Session["UsernameStaff"] as string;

            // and if the session state isn't set, fetch from the cookie
            if (string.IsNullOrEmpty(username))
            {
                HttpCookie loginCookie = Request.Cookies["myCookieId2"];
                if (loginCookie != null)
                {
                    username = loginCookie["Username"];
                    Session["UsernameStaff"] = username;
                }

            }
            return username;
        }
    }
}