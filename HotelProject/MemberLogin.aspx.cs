using HotelProject.RatingServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelProject
{
    public partial class MemberLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // add event handler
            LoginControl1.LoginAttempt += LoginControl1_LoginAttempt;

            // Fetch the cookie values only when the page isn't loading for the first time
            if (!IsPostBack)
            {
                // see if the staff is logged in from cookies. If so, redirect to the dashboard
                HttpCookie myCookies = Request.Cookies["myCookieId"];
                if (myCookies != null && IsMemberLoggedIn())
                {
                    Response.Redirect("~/ProtectedMember/MemberBrowse.aspx");
                }
            }
        }

        // check if staff is logged in with cookies
        protected bool IsMemberLoggedIn()
        {
            // fetch the cookie, check if not null & username set
            HttpCookie loginCookie = Request.Cookies["myCookieId"];
            return (loginCookie != null && !string.IsNullOrEmpty(loginCookie["Username"]));
        }

        private void LoginControl1_LoginAttempt(object sender, LoginEventArgs e)
        {
            // first, fetch the login details from the event
            string enteredUsername = e.Username;
            string enteredPassword = e.Password;

            // initialize the cookie + proxy to call service
            HttpCookie myCookies = new HttpCookie("myCookieId");
            Service1Client prxy = new Service1Client();

            // if the credentials are correct...
            if (prxy.LoginMember(enteredUsername, enteredPassword))
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                     1,
                     e.Username,
                     DateTime.Now,
                     DateTime.Now.AddMinutes(10),
                     false,
                     "Member",
                     FormsAuthentication.FormsCookiePath
                 );

                string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(cookie);

                // then redirect to wherever the user was originally trying to go
                string attemptedAccess = Request.QueryString["ReturnUrl"];
                if(!string.IsNullOrEmpty(attemptedAccess))
                {
                    Response.Redirect(attemptedAccess);
                }
                else
                {
                    Response.Redirect("~/ProtectedMember/MemberBrowse.aspx");
                }
            }
            else
            {
                // otherwise, just go back to incorrect credentials
                // ResultLabel.Text = "Result: Invalid Credentials.";
            }
        }
    }
}