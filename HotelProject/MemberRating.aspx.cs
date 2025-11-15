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
                UsernameTextbox.Text = "";
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
                    UsernameTextbox.Text = "";
                    return;
                }

                // otherwise, if not empty, and the first hotelID == -1, then wrong user
                if (ratedHotels[0].HotelID == "-1")
                {
                    ResultLabel1.Text = "Incorrect credentials. Member with entered username doesn't exist";
                    UsernameTextbox.Text = "";
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

                UsernameTextbox.Text = "";
                RatingDisplay.Text = result.ToString();
           
            }
            catch (Exception ex)
            {
                ResultLabel1.Text = ex.Message;
            }
        }

        protected void AddRating_Click(object sender, EventArgs e)
        {
            // clear out the text, just in case
            ResultLabel2.Text = "";

            // validate inputs
            string username = UsernameTextbox2.Text.Trim();
            string hotelID = HotelIDTextbox.Text.Trim();
            string rating = RatingTextbox.Text.Trim();
            string comment = CommentTextbox.Text.Trim();

            if(string.IsNullOrEmpty(username))
            {
                ResultLabel2.Text = "Result: Enter a username, please.";
            }

            if (string.IsNullOrEmpty(hotelID))
            {
                ResultLabel2.Text = "Result: Enter a hotel ID, please.";
            }

            if (!float.TryParse(rating, out float ratingFloat) || ratingFloat < 0.0f || ratingFloat > 5.0)
            {
                ResultLabel2.Text = "Result: Enter a valid rating from 0.0 to 5.0, please";
            }

            if (string.IsNullOrEmpty(comment)) {
                ResultLabel2.Text = "Result: Enter a comment, please.";
            }

            try
            {
                Service1Client prxy = new Service1Client();


                float convertedRating = float.Parse(rating);
                bool success = prxy.AddHotelRating(username, hotelID, convertedRating, comment);

                if (success)
                {
                    ResultLabel2.Text = "Result: Rating successfully added.\n" +
                    "Please enter the corresponding username in the above TryIt Service to see the newly added rating";

                    UsernameTextbox2.Text = "";
                    HotelIDTextbox.Text = "";
                    RatingTextbox.Text = "";
                    CommentTextbox.Text = "";

                }
                else
                {
                    ResultLabel2.Text = "Result: Failed to add rating";
                }
            }
            catch (Exception ex)
            {
                ResultLabel2.Text = "Result: " + ex.Message;
            }
            
        }
    }
}