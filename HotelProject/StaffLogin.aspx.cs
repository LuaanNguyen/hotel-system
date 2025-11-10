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
            if(!IsPostBack)
            {
                HttpCookie myCookies = Request.Cookies["myCookieId"];
                if(myCookies != null)
                {
                    CookieUsername.Text = "Entered Username: " + myCookies["Username"];
                    CookiePassword.Text = "Entered Password: " + myCookies["Password"];
                }
            }
        }

        private void LoginControl1_LoginAttempt(object sender, LoginEventArgs e)
        {
            HttpCookie myCookies = new HttpCookie("myCookieId");
            myCookies["Username"] = e.Username;
            myCookies["Password"] = e.Password;
            myCookies.Expires = DateTime.Now.AddMonths(6);
            Response.Cookies.Add(myCookies);

            CookieUsername.Text = "Entered Username: " + myCookies["Username"];
            CookiePassword.Text = "Entered Password: " + myCookies["Password"];
        }
    }
}