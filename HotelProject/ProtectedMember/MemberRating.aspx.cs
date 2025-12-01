using HotelProject.RatingServiceReference;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelProject
{
    public partial class MemberRating : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DisplayWelcome();
            DisplayRatedHotels();
            LoadAvailableHotels();

        }

        // event listener for the default button
        protected void DefaultButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        // event listener for the browse button
        protected void BrowseButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ProtectedMember/MemberBrowse.aspx");
        }

        // event listener for log out button
        protected void Log_Click(object sender, EventArgs e)
        {
            // clear authentication
            FormsAuthentication.SignOut();

            // clear session (holding the hotel listing info, in this case)
            Session.Clear();
            Session.Abandon();

            // go back to login page
            Response.Redirect("~/MemberLogin.aspx");
        }

        // event listener for change password
        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ProtectedMember/MemberChangePassword.aspx");
        }

        // stores the available hotels to rate as a session variable
        protected List<HotelListing> Hotels
        {
            get { return Session["Hotels"] as List<HotelListing>; }
            set { Session["Hotels"] = value; }
        }

        // load all the hotels stored in the XML file
        protected void LoadAvailableHotels()
        {
            try
            {
                Service1Client prxy = new Service1Client();
                HotelListing[] hotelsArray = prxy.GetAllHotels();

                // the if branch should never happen, but just in case no hotels are in Hotels.xml
                if (hotelsArray == null || hotelsArray.Length == 0)
                {
                    HotelListView.DataSource = null;
                    Hotels = null;
                }
                // otherwise, set the DataSource to be all hotels in Hotels.Xml
                else
                {
                    Hotels = hotelsArray.ToList();
                    HotelListView.DataSource = Hotels;
                }
                HotelListView.DataBind();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        // event listener for list view selection
        protected void HotelListView_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            HotelListView.SelectedIndex = e.NewSelectedIndex;
            HotelListView.DataSource = Hotels;
            HotelListView.DataBind();
        }

        // event listener for the selected hotel changing on the list view
        protected void lvHotels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HotelListView.SelectedIndex >= 0 && Hotels != null && HotelListView.SelectedIndex < Hotels.Count)
            {
                HotelListing selectedHotel = Hotels[HotelListView.SelectedIndex];

                // get the selected hotelID
                hiddenHotelId.Value = selectedHotel.HotelID;
            }
        }

        // display welcome message
        protected void DisplayWelcome()
        {
            string username = User.Identity.Name;
            WelcomeLabel.Text = "Welcome, " + username;
        }

        // display all the hotels
        protected void DisplayRatedHotels()
        {
            string username = User.Identity.Name;

            try
            {
                // create a prxy to call WSDL service to get rated hotels for that user
                Service1Client prxy = new Service1Client();
                RatedHotel[] ratedHotels = prxy.GetRatedHotels(username);

                // this would be the case that the member had no hotels rated
                if (ratedHotels == null || ratedHotels.Length == 0)
                {
                    // display that the user hasn't rated any hotels, but this member does exist
                    RatingTextBox.Text = "This member has not rated any hotels.";
                    return;
                }

                // otherwise, if not empty, and the first hotelID == -1, then we have invalid credentials
                if (ratedHotels[0].HotelID == "-1")
                {
                    // display that the user has incorrect credentials
                    RatingTextBox.Text = "Incorrect credentials. Member with entered username doesn't exist";
                    return;
                }

                // otherwise we can start displaying our result in scrollable textbox
                StringBuilder result = new StringBuilder();

                // first, let's display how many hotels user has rated
                result.AppendLine($"Total Hotels Rated: {ratedHotels.Length}");
                result.AppendLine(new string('=', 75));
                result.AppendLine();

                // then, for each hotel the member has rated...
                foreach (var hotel in ratedHotels)
                {
                    // we're going to display the hotel name, rating, address
                    result.AppendLine($"Hotel: {hotel.HotelName}");
                    result.AppendLine($"Rating: {hotel.Rating}/5");
                    result.AppendLine($"Address: {hotel.HotelAddress.Number} {hotel.HotelAddress.Street}");
                    result.AppendLine($"         {hotel.HotelAddress.City}, {hotel.HotelAddress.Zip}");

                    // we will possibly display the comment (if user entered comment)
                    if (!string.IsNullOrEmpty(hotel.Comment))
                    {
                        result.AppendLine($"Comment: {hotel.Comment}");
                    }

                    result.AppendLine();
                }

                // display the result in large textbox
                RatingTextBox.Text = result.ToString();

            }
            catch (Exception ex)
            {
                RatingTextBox.Text = ex.Message;
            }

        }

        // event listener for rating a hotel
        protected void RateButton_Click(object sender, EventArgs e)
        {
            ResultLabel.Text = "Result: ";
            string username = User.Identity.Name;
            int hotelID;

            // first see if a hotel has been added
            if(!int.TryParse(hiddenHotelId.Value, out hotelID))
            {
                ResultLabel.Text = "Result: Please select a hotel to proceed.";
                return;
            } 

            if(string.IsNullOrEmpty(CommentTextBox.Text))
            {
                ResultLabel.Text = "Result: Please enter a comment before proceeding.";
                return;
            }

            // see if the rating given is a valid number
            float score;
            if(!float.TryParse(RatingEnterTextBox.Text, out score)) {
                ResultLabel.Text = "Result: Enter in a valid floating point number.";
                return;
            }

            if(!(score >= 1 && score <= 5))
            {
                ResultLabel.Text = "Result: We only accept ratings from 1 to 5 inclusive.";
                return;
            }

            // if we passed all that, it's time to actually add the rating
            try
            {
                Service1Client prxy = new Service1Client();
                if (prxy.AddHotelRating(username, hotelID.ToString(), score, CommentTextBox.Text))
                {
                    ResultLabel.Text = "Result: Hotel Successfully Added!";

                    // gotta redisplay the booked hotels now
                    DisplayRatedHotels();
                }
                else
                {
                    ResultLabel.Text = "Result: An unexpected error occurred. Try again.";
                }

            }
            catch (Exception ex)
            {
                ResultLabel.Text = "Result: " + ex.Message;
            }

        }
    }
}