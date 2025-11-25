using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelProject
{
    public partial class Login : System.Web.UI.Page
    {
        // This page's sole purpose is to simply redirect the unauthenticated users to the correct login page 
        // (determines correct login page based on where the user was originally trying to go--either a member protected page, or staff protected page)
        protected void Page_Load(object sender, EventArgs e)
        {
            string attemptedAccessUrl = Request.QueryString["ReturnUrl"];

            //"ReturnUrl value: " + (attemptedAccessUrl ?? "NULL") + "<br/>");

            // user was trying to access the protected staff dashboard
            if (attemptedAccessUrl != null && attemptedAccessUrl.ToLower().Contains("staff")) {
                // (if login attempt successful, the returned url should be the page where the user was originally trying to go to)
                // (the Default.aspx is set as default in web.config) 
                Response.Redirect("~/StaffLogin.aspx?ReturnUrl=" + Server.UrlEncode(attemptedAccessUrl));
            }
            // otherwise, they were trying to access a protected member page (rating/browse) so go to Member login
            else
            {
                Response.Redirect("~/MemberLogin.aspx?ReturnUrl=" + Server.UrlEncode(attemptedAccessUrl));
            }
        }
    }
}