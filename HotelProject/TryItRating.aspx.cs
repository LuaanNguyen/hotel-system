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
    public partial class TryItRating : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Event handler for getting all the ratings for a user
        protected void GetRatings_Click(object sender, EventArgs e)
        {
            // clear previous results
            RatingDisplay.Text = "";
            ResultLabel1.Text = "";

            // get username
            string username = UsernameTextbox.Text;

            // validate the username
            if (string.IsNullOrEmpty(username))
            {
                ResultLabel1.Text = "Result: Enter a username.";
                UsernameTextbox.Text = "";
                return;
            }

            try
            {
                // create a prxy to call WSDL service to get rated hotels for that user
                Service1Client prxy = new Service1Client();
                RatedHotel[] ratedHotels = prxy.GetRatedHotels(username);

                // this would be the case that the member had no hotels rated
                if (ratedHotels == null || ratedHotels.Length == 0)
                {
                    // display that the user hasn't rated any hotels, but this member does exist
                    RatingDisplay.Text = "This member has not rated any hotels.";
                    ResultLabel1.Text = string.Format("Member '{0}' exists!", username);
                    UsernameTextbox.Text = "";
                    return;
                }

                // otherwise, if not empty, and the first hotelID == -1, then we have invalid credentials
                if (ratedHotels[0].HotelID == "-1")
                {
                    // display that the user has incorrect credentials
                    ResultLabel1.Text = "Incorrect credentials. Member with entered username doesn't exist";
                    UsernameTextbox.Text = "";
                    return;
                }

                // otherwise we can start displaying our result in scrollable textbox
                StringBuilder result = new StringBuilder();

                // first, let's display how many hotels user has rated
                result.AppendLine(string.Format("Total Hotels Rated: {0}", ratedHotels.Length));
                result.AppendLine(new string('=', 75));
                result.AppendLine();

                // then, for each hotel the member has rated...
                foreach (var hotel in ratedHotels)
                {
                    // we're going to display the hotel name, rating, address
                    result.AppendLine(string.Format("Hotel: {0}", hotel.HotelName));
                    result.AppendLine(string.Format("Rating: {0}/5", hotel.Rating));
                    result.AppendLine(string.Format("Address: {0} {1}", hotel.HotelAddress.Number, hotel.HotelAddress.Street));
                    result.AppendLine(string.Format("         {0}, {1}", hotel.HotelAddress.City, hotel.HotelAddress.Zip));

                    // we will possibly display the comment (if user entered comment)
                    if (!string.IsNullOrEmpty(hotel.Comment))
                    {
                        result.AppendLine(string.Format("Comment: {0}", hotel.Comment));
                    }

                    result.AppendLine();
                }

                // clear out the username textbox
                UsernameTextbox.Text = "";

                // display the result in large textbox
                RatingDisplay.Text = result.ToString();

            }
            catch (Exception ex)
            {
                ResultLabel1.Text = ex.Message;
            }
        }

        // Event listener for button to add a rating
        protected void AddRating_Click(object sender, EventArgs e)
        {
            // clear out the text, just in case
            ResultLabel2.Text = "";

            // validate inputs
            string username = UsernameTextbox2.Text.Trim();
            string hotelID = HotelIDTextbox.Text.Trim();
            string rating = RatingTextbox.Text.Trim();
            string comment = CommentTextbox.Text.Trim();

            // checking (mostly) if all fields nonempty (except rating, which needs to be float between 0 and 5)
            if (string.IsNullOrEmpty(username))
            {
                ResultLabel2.Text = "Result: Enter a username, please.";
                return;
            }

            if (string.IsNullOrEmpty(hotelID))
            {
                ResultLabel2.Text = "Result: Enter a hotel ID, please.";
                return;
            }

            if (!float.TryParse(rating, out float ratingFloat) || ratingFloat < 0.0f || ratingFloat > 5.0f)
            {
                ResultLabel2.Text = "Result: Enter a valid rating from 0.0 to 5.0, please";
                return;
            }

            if (string.IsNullOrEmpty(comment))
            {
                ResultLabel2.Text = "Result: Enter a comment, please.";
                return;
            }

            try
            {
                // now create a proxy to use the WSDL service
                Service1Client prxy = new Service1Client();

                // service takes float as arg, so first parse the rating
                float convertedRating = float.Parse(rating);

                // now call the service (method)
                bool success = prxy.AddHotelRating(username, hotelID, convertedRating, comment);

                // if successfully added, indicate so
                if (success)
                {
                    ResultLabel2.Text = "Result: Rating successfully added.\n" +
                    "Please enter the corresponding username in the above TryIt Service to see the newly added rating";

                    // clear out all the inputs
                    UsernameTextbox2.Text = "";
                    HotelIDTextbox.Text = "";
                    RatingTextbox.Text = "";
                    CommentTextbox.Text = "";

                }
                // otherwise, notify of exceptions
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