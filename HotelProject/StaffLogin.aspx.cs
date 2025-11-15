using HotelProject.RatingServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelProject
{
    public partial class StaffLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginControl1.LoginAttempt += LoginControl1_LoginAttempt;

            // Fetch the cookie values only when the page isn't loading for the first time
            if (!IsPostBack)
            {
                HttpCookie myCookies = Request.Cookies["myCookieId2"];
                if (myCookies != null && IsStaffLoggedIn())
                {
                    Response.Redirect("StaffDashboard.aspx");
                }
            }
        }

        protected bool IsStaffLoggedIn()
        {
            HttpCookie loginCookie = Request.Cookies["myCookieId2"];
            return (loginCookie != null && !string.IsNullOrEmpty(loginCookie["Username"]));
        }

        private void LoginControl1_LoginAttempt(object sender, LoginEventArgs e)
        {
            string enteredUsername = e.Username;
            string enteredPassword = e.Password;

            HttpCookie myCookies = new HttpCookie("myCookieId2");
            Service1Client prxy = new Service1Client();

            if (prxy.LoginStaff(enteredUsername, enteredPassword))
            {
                // first, let's set the cookies
                myCookies["Username"] = e.Username;
                myCookies["Password"] = e.Password;
                myCookies.Expires = DateTime.Now.AddMinutes(10);
                Response.Cookies.Add(myCookies);

                Response.Redirect("StaffDashboard.aspx");
            }
            else
            {
                ResultLabel.Text = "Result: Invalid Credentials.";
            }

        }
    }
}