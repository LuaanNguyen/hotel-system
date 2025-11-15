using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HotelProject.RatingServiceReference;
using System.CodeDom.Compiler;

namespace HotelProject
{
    public partial class MemberRating : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GetRatings_Click(object sender, EventArgs e)
        {
            // clear previous results
            RatingDisplay.Text = "";
            ResultLabel1.Text = "";

            // get username
            string username = UsernameTextbox.Text;

            if (string.IsNullOrEmpty(username))
            {
                ResultLabel1.Text = "Result: Enter a username.";
                return;
            }

            try
            {
                Service1Client prxy = new Service1Client();
                RatedHotel[] ratedHotels = prxy.GetRatedHotels(username);

                // this would be the case that the member had no hotels rated
                if (ratedHotels == null || ratedHotels.Length == 0)
                {
                    RatingDisplay.Text = "This member has not rated any hotels.";
                    ResultLabel1.Text = $"Member '{username}' exists!";
                    return;
                }

                // otherwise, if not empty, and the first hotelID == -1, then wrong user
                if (ratedHotels[0].HotelID == "-1")
                {
                    ResultLabel1.Text = "Incorrect credentials. Member with entered username doesn't exist";
                    return;
                }

                StringBuilder result = new StringBuilder();
                result.AppendLine($"Total Hotels Rated: {ratedHotels.Length}");
                result.AppendLine(new string('=', 75));
                result.AppendLine();

                foreach (var hotel in ratedHotels)
                {
                    result.AppendLine($"Hotel: {hotel.HotelName}");
                    result.AppendLine($"Rating: {hotel.Rating}/5");
                    result.AppendLine($"Address: {hotel.HotelAddress.Number} {hotel.HotelAddress.Street}");
                    result.AppendLine($"         {hotel.HotelAddress.City}, {hotel.HotelAddress.Zip}");

                    if (!string.IsNullOrEmpty(hotel.Comment))
                    {
                        result.AppendLine($"Comment: {hotel.Comment}");
                    }

                    result.AppendLine();
                }

                RatingDisplay.Text = result.ToString();
           
            }
            catch (Exception ex)
            {
                ResultLabel1.Text = ex.Message;
            }
        }
    }
}