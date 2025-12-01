using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace HotelProject
{
    /// <summary>
    /// Global application class for the Hotel Management System
    /// Author: Muhammed Hunaid Topiwala
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Application["TotalVisitors"] = 0;
            Application["ActiveSessions"] = 0;
            Application["ApplicationStartTime"] = DateTime.Now;

            System.Diagnostics.Debug.WriteLine("Hotel Application Started at: " + DateTime.Now);
        }

        /// <summary>
        /// Session_Start event handler
        /// Executes when a new user session begins
        /// </summary>
        protected void Session_Start(object sender, EventArgs e)
        {
            int totalVisitors = (int)Application["TotalVisitors"];
            int activeSessions = (int)Application["ActiveSessions"];


        }
        
        {
        }

        /// <summary>
        /// Application_Error event handler
        /// Executes when an unhandled error occurs in the application
        /// </summary>
        protected void Application_Error(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Application_End event handler
        /// Executes when the application shuts down
        /// </summary>
        protected void Application_End(object sender, EventArgs e)
        {
        }


    }
}