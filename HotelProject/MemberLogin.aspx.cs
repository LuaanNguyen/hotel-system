using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelProject.RatingServiceReference;

namespace HotelProject
{
    public partial class MemberLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginControl1.LoginAttempt += LoginControl1_LoginAttempt;

            // Fetch the cookie values only when the page isn't loading for the first time
            if (!IsPostBack)
            {
                HttpCookie myCookies = Request.Cookies["myCookieId"];
                if (myCookies != null && IsMemberLoggedIn())
                {
                    Response.Redirect("MemberDashboard.aspx");
                }
            }
        }

        protected bool IsMemberLoggedIn()
        {
            HttpCookie loginCookie = Request.Cookies["myCookieId"];
            return (loginCookie != null && !string.IsNullOrEmpty(loginCookie["Username"]));
        }

        private void LoginControl1_LoginAttempt(object sender, LoginEventArgs e)
        {
            string enteredUsername = e.Username;
            string enteredPassword = e.Password;

            HttpCookie myCookies = new HttpCookie("myCookieId");
            Service1Client prxy = new Service1Client();

            if(prxy.LoginMember(enteredUsername, enteredPassword))
            {
                // first, let's set the cookies
                myCookies["Username"] = e.Username;
                myCookies["Password"] = e.Password;
                myCookies.Expires = DateTime.Now.AddMinutes(10);
                Response.Cookies.Add(myCookies);

                Response.Redirect("MemberDashboard.aspx");
            }
            else
            {
                ResultLabel.Text = "Result: Invalid Credentials.";
            }

        }
    }
}