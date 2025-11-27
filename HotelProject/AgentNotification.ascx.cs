using System;
using System.Text;

namespace HotelProject
{
    /// <summary>
    /// AgentNotification User Control
    /// Displays notification messages for hotel staff agents about discount opportunities
    /// 
    /// Author: Muhammed Hunaid Topiwala
    /// Course: CSE 445 - Distributed Software Development  
    /// Assignment: Assignment 5 - User Control Component
    /// 
    /// Features:
    /// - Displays static notification messages
    /// - Refresh button to reload notifications
    /// - Professional UI with status updates
    /// - Integrates with NotificationHelper web service (for Assignment 6)
    /// </summary>
    public partial class AgentNotification : System.Web.UI.UserControl
    {
        /// <summary>
        /// Page Load event - initializes the control with default notifications
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load initial notifications when page first loads
                LoadNotifications();
                lblStatus.Text = "Last updated: " + DateTime.Now.ToString("hh:mm:ss tt");
            }
        }

        /// <summary>
        /// Refresh Button Click event - reloads notifications
        /// </summary>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            // Reload notifications
            LoadNotifications();

            // Update status with current timestamp
            lblStatus.Text = "Refreshed at: " + DateTime.Now.ToString("hh:mm:ss tt");
        }

        /// <summary>
        /// Loads and displays notification messages
        /// For Assignment 5: Uses static notification data
        /// For Assignment 6: Will integrate with NotificationHelper web service
        /// </summary>
        private void LoadNotifications()
        {
            // Create StringBuilder for efficient string concatenation
            StringBuilder notificationHtml = new StringBuilder();

            // Static notification data (for Assignment 5)
            // These represent typical discount opportunities that staff agents would receive
            string[] notifications = new string[]
            {
                "ALERT: 25% discount available at Westin Hotel! Book now for your clients.",
                "ALERT: 15% discount available at Hampton Inn Phoenix-Airport North! Book now for your clients.",
                "ALERT: 30% discount available at La Quinta Inn! Book now for your clients.",
                "ALERT: 20% discount available at Best Western Plus Chandler Hotel and Suites! Book now for your clients.",
                "ALERT: 18% discount available at Hilton Garden Inn Phoenix Airport! Book now for your clients."
            };

            // Format each notification with styling
            notificationHtml.Append("<div style='font-family: Arial, sans-serif;'>");

            for (int i = 0; i < notifications.Length; i++)
            {
                // Alternate background colors for better readability
                string bgColor = (i % 2 == 0) ? "#f9f9f9" : "#ffffff";

                notificationHtml.Append($@"
                    <div style='padding: 12px; margin-bottom: 10px; 
                                background-color: {bgColor}; 
                                border-left: 4px solid #2196F3; 
                                border-radius: 4px;
                                box-shadow: 0 1px 3px rgba(0,0,0,0.1);'>
                        <div style='font-size: 14px; color: #333; line-height: 1.5;'>
                            {notifications[i]}
                        </div>
                        <div style='font-size: 11px; color: #888; margin-top: 5px;'>
                            Posted: {DateTime.Now.AddMinutes(-i * 15).ToString("MMM dd, yyyy hh:mm tt")}
                        </div>
                    </div>
                ");
            }

            // Add summary at the bottom
            notificationHtml.Append($@"
                <div style='padding: 10px; margin-top: 15px; 
                            background-color: #E8F5E9; 
                            border-radius: 4px; 
                            text-align: center;'>
                    <strong style='color: #2E7D32;'>
                        Total Active Notifications: {notifications.Length}
                    </strong>
                </div>
            ");

            notificationHtml.Append("</div>");

            // Set the HTML content to the label
            lblNotifications.Text = notificationHtml.ToString();
        }

        /// <summary>
        /// Helper method: Gets the count of active notifications
        /// Can be used by parent pages to display notification badges
        /// </summary>
        /// <returns>Number of active notifications</returns>
        public int GetNotificationCount()
        {
            // For Assignment 5: Return static count
            // For Assignment 6: Will return dynamic count from web service
            return 5;
        }

        /// <summary>
        /// Helper method: Clears all displayed notifications
        /// Useful for testing and demonstration purposes
        /// </summary>
        public void ClearNotifications()
        {
            lblNotifications.Text = "<div style='text-align: center; padding: 20px; color: #999;'>" +
                                   "No active notifications at this time.</div>";
            lblStatus.Text = "Notifications cleared at: " + DateTime.Now.ToString("hh:mm:ss tt");
        }
    }
}