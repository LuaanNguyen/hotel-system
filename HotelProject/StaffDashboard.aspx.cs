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
            if (!IsPostBack)
            {
                string currUsername = GetCurrUsername();
                WelcomeLabel.Text = $"Welcome, {currUsername}";
                HttpCookie myCookies = Request.Cookies["myCookieId2"];
                if (myCookies != null)
                {
                    CookieUsername.Text = "Entered Username: " + myCookies["Username"];
                    CookiePassword.Text = "Entered Password: " + myCookies["Password"];
                }
            }
        }

        protected string GetCurrUsername()
        {
            string username = Session["UsernameStaff"] as string;

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