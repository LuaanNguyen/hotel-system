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
            // add the event handler
            LoginControl1.LoginAttempt += LoginControl1_LoginAttempt;

            // Fetch the cookie values only when the page isn't loading for the first time
            if (!IsPostBack)
            {
                // if we fetch the cookie, then we can automatically log the user in
                HttpCookie myCookies = Request.Cookies["myCookieId"];
                if (myCookies != null && IsMemberLoggedIn())
                {
                    Response.Redirect("MemberDashboard.aspx");
                }
            }
        }

        // check whether a member is logged in based on stored cookies
        protected bool IsMemberLoggedIn()
        {
            // search for the cookie
            HttpCookie loginCookie = Request.Cookies["myCookieId"];

            // return if the Usernane portion of cookie set (and cookie not null)
            return (loginCookie != null && !string.IsNullOrEmpty(loginCookie["Username"]));
        }

        // event handler for login
        private void LoginControl1_LoginAttempt(object sender, LoginEventArgs e)
        {
            // first get the username and password from LoginEventArgs (because text boxes apart of login control)
            string enteredUsername = e.Username;
            string enteredPassword = e.Password;

            // initialize a cookie, and create proxy to call WSDL login service
            HttpCookie myCookies = new HttpCookie("myCookieId");
            Service1Client prxy = new Service1Client();

            // if we do successfully login...
            if(prxy.LoginMember(enteredUsername, enteredPassword))
            {
                // first, let's set the cookies
                myCookies["Username"] = e.Username;
                myCookies["Password"] = e.Password;
                myCookies.Expires = DateTime.Now.AddMinutes(10);
                Response.Cookies.Add(myCookies);

                // then, let's redirect to right page
                Response.Redirect("MemberDashboard.aspx");
            }
            // otherwise, inform users of incorrect credentials
            else
            {
                ResultLabel.Text = "Result: Invalid Credentials.";
            }

        }
    }
}