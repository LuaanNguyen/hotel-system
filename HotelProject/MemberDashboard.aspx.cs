using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelProject
{
    public partial class MemberDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // if not the first time it's being loaded...
            if (!IsPostBack)
            {
                // fetch the username
                string currUsername = GetCurrUsername();

                // display it with welcome message...
                WelcomeLabel.Text = $"Welcome, {currUsername}";

                // This is for the TryIt cookie section, where I display the values fetched from cookie
                HttpCookie myCookies = Request.Cookies["myCookieId"];
                if (myCookies != null)
                {
                    CookieUsername.Text = "Entered Username: " + myCookies["Username"];
                    CookiePassword.Text = "Entered Password: " + myCookies["Password"];
                }
            }
        }

        // get username of current member for display purposes
        protected string GetCurrUsername()
        {
            // first, I'll check the Session variable for anything
            string username = Session["UsernameMember"] as string;
            
            // if I can't pull it from session, I'll pull it from the cookie
            if(string.IsNullOrEmpty(username))
            {
                // get username from the cookie...
                HttpCookie loginCookie = Request.Cookies["myCookieId"];
                if (loginCookie != null)
                {
                    username = loginCookie["Username"];
                    Session["UsernameMember"] = username;
                }

            }
            
            return username;
        }
    }
}