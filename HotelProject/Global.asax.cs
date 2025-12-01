using System;
using System.Web;

namespace HotelProject
{
    /// <summary>
    /// Global application class for the Hotel Management System
    /// Author: Muhammed Hunaid Topiwala
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Application_Start event handler
        /// Executes when the application starts for the first time
        /// </summary>
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
            
            Application["TotalVisitors"] = totalVisitors + 1;
            Application["ActiveSessions"] = activeSessions + 1;
            
            Session["SessionStartTime"] = DateTime.Now;
        }

        /// <summary>
        /// Session_End event handler
        /// Executes when a user session ends or times out
        /// </summary>
        protected void Session_End(object sender, EventArgs e)
        {
            int activeSessions = (int)Application["ActiveSessions"];
            Application["ActiveSessions"] = Math.Max(0, activeSessions - 1);
        }

        /// <summary>
        /// Application_Error event handler
        /// Executes when an unhandled error occurs in the application
        /// </summary>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            System.Diagnostics.Debug.WriteLine("Application Error: " + exception?.Message);
        }

        /// <summary>
        /// Application_End event handler
        /// Executes when the application shuts down
        /// </summary>
        protected void Application_End(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Hotel Application Ended at: " + DateTime.Now);
        }
    }
}
