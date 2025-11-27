using System;
using System.Web.Services;

namespace HotelProject
{
    /// <summary>
    /// NotificationHelper Web Service
    /// Provides utility methods for formatting notification messages for hotel staff agents
    /// 
    /// Author: Muhammed Hunaid Topiwala
    /// Course: CSE 445 - Distributed Software Development
    /// Assignment: Assignment 5 - Web Service Component
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class NotificationHelper : System.Web.Services.WebService
    {
        /// <summary>
        /// Formats a notification message for hotel staff agents about discount opportunities
        /// </summary>
        /// <param name="hotelName">Name of the hotel (e.g., "Westin")</param>
        /// <param name="discount">Discount percentage as a float (e.g., 20.5 for 20.5%)</param>
        /// <returns>Formatted notification message string</returns>
        /// <example>
        /// Input: hotelName = "Westin", discount = 20.5
        /// Output: "ALERT: 20.5% discount available at Westin Hotel! Book now for your clients."
        /// </example>
        [WebMethod(Description = "Formats a notification message about hotel discounts for staff agents")]
        public string FormatNotificationMessage(string hotelName, float discount)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(hotelName))
            {
                return "Error: Hotel name cannot be empty.";
            }

            if (discount < 0 || discount > 100)
            {
                return "Error: Discount percentage must be between 0 and 100.";
            }

            // Format the notification message with emoji and professional styling
            string message = $"ALERT: {discount}% discount available at {hotelName} Hotel! Book now for your clients.";

            return message;
        }

        /// <summary>
        /// Additional helper method: Validates if a discount percentage is valid
        /// </summary>
        /// <param name="discount">Discount percentage to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        [WebMethod(Description = "Validates if a discount percentage is within acceptable range")]
        public bool ValidateDiscount(float discount)
        {
            return discount >= 0 && discount <= 100;
        }

        /// <summary>
        /// Additional helper method: Generates a formatted timestamp for notifications
        /// </summary>
        /// <returns>Current timestamp in readable format</returns>
        [WebMethod(Description = "Gets current timestamp for notification logging")]
        public string GetNotificationTimestamp()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}