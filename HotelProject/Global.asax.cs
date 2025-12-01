using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace HotelProject
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }
        
        // for forms authentication + role checking
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            // check if authenticated first
            if (User != null && User.Identity.IsAuthenticated)
            {
                // create a cookie
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                // verify cookie information
                if (authCookie != null)
                {
                    try
                    {
                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                        string[] roles = ticket.UserData.Split(',');

                        System.Security.Principal.GenericIdentity identity =
                            new System.Security.Principal.GenericIdentity(ticket.Name, "Forms");
                        System.Security.Principal.GenericPrincipal principal =
                            new System.Security.Principal.GenericPrincipal(identity, roles);

                        Context.User = principal;
                    }
                    catch { }
                }
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }


    }
}