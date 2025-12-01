using HotelProject.RatingServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelProject
{
    public partial class MemberBrowse : System.Web.UI.Page
    {
        string selectedHotelId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string username = User.Identity.Name;
                WelcomeLabel.Text = "Welcome, " + username;
                LoadAvailableHotels();
                LoadBookedHotels();
                displayBalance();
            }
        }

        // event listener for log out button
        protected void LogOut_Click(object sender, EventArgs e)
        {
            // clear authentication
            FormsAuthentication.SignOut();

            // clear session (holding the hotel listing info, in this case)
            Session.Clear();
            Session.Abandon();

            // go back to login page
            Response.Redirect("~/MemberLogin.aspx");
        }

        // event listener for default button
        protected void DefaultButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        // event listener for member button
        protected void RateButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ProtectedMember/MemberRating.aspx");
        }

        // event listener for changing password
        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ProtectedMember/MemberChangePassword.aspx");
        }


        // set/get HotelListings from Session
        protected List<HotelListing> Hotels
        {
            get { return Session["Hotels"] as List<HotelListing>; }
            set { Session["Hotels"] = value; }
        }

        // Load all hotels available for booking
        protected void LoadAvailableHotels()
        {
            HotelDetailTextBox.Text = "Select a hotel to view further details...";

            try
            {
                // use a proxy to call the service
                Service1Client prxy = new Service1Client();
                HotelListing[] hotelsArray = prxy.BrowseHotels();

                // if hotelsArray's length is 0, then no hotels have been posted by the staff
                if (hotelsArray == null || hotelsArray.Length == 0)
                {
                    HotelDetailTextBox.Text = "No hotels have been booked + posted by an agent yet.";
                    HotelListView.DataSource = null;
                    Hotels = null;
                }
                // otherwise, set the data source of the ListView
                else
                {
                    Hotels = hotelsArray.ToList();
                    HotelListView.DataSource = Hotels;
                }
                HotelListView.DataBind();
            }
            catch (Exception ex)
            {
                HotelDetailTextBox.Text = ex.Message;
            }
        }

        // Event listener for an item on the HotelListView being selected
        protected void HotelListView_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            HotelListView.SelectedIndex = e.NewSelectedIndex;
            HotelListView.DataSource = Hotels;
            HotelListView.DataBind();
        }

        // Event listener for item on HotelListView being changed
        protected void lvHotels_SelectedIndexChanged(object sender, EventArgs e)
        {
            // display details of the seleced hotel
            if (HotelListView.SelectedIndex >= 0 && Hotels != null && HotelListView.SelectedIndex < Hotels.Count)
            {
                HotelListing selectedHotel = Hotels[HotelListView.SelectedIndex];
                DisplayHotelDetails(selectedHotel);

                // get the selected hotelID aNd store it in the hidden field for future reference
                hiddenHotelId.Value = selectedHotel.HotelID;
            }
        }

        // display the balance as a label
        protected void displayBalance()
        {
            string username = User.Identity.Name;

            Service1Client prxy = new Service1Client();
            float userBalance = prxy.getBalance(username);

            CurrBalanceLabel.Text = "Current Balance: " + userBalance.ToString("F2") ;

        }

        // add funds to a user's account
        protected void AddMoney_Click(object sender, EventArgs e)
        {

            string username = User.Identity.Name;

            // first check if the input field not empty
            if (string.IsNullOrEmpty(AddMoneyTextBox.Text))
            {
                ErrorLabel.Text = "Error: Fill in all fields before adding money";
                return;
            }

            // check if the value can be parsed into a float
            float addedBalance;
            if(!float.TryParse(AddMoneyTextBox.Text, out addedBalance))
            {
                ErrorLabel.Text = "Error: You must enter in a float";
                return;
            }

            // finally check if user entered in a postive amount
            if (addedBalance <= 0) {
                ErrorLabel.Text = "Error: You must enter in a postive amount";
                return;
            }

            // otherwise, if all error checks passed, continue with adding money to the user's account
            Service1Client prxy = new Service1Client();
            if(!prxy.addBalance(username, addedBalance))
            {
                ErrorLabel.Text = "Error: An unexpected error occured. Try again";
                return;
            }
            else
            {
                ErrorLabel.Text = "Error: ";
            }
            // redisplay the balance and reset the input field
            displayBalance();
            AddMoneyTextBox.Text = "";
        }

        // load up all the hotels that a user has previously booked
        protected void LoadBookedHotels()
        {
            string username = User.Identity.Name;

            // get the prxy to call the service
            Service1Client prxy = new Service1Client();

            try
            { 
               HotelBooking[] bookedHotelsTemp = prxy.GetBookedHotels(username);
                if(bookedHotelsTemp == null)
                {
                    BookedHotelsTextBox.Text = "This user has not booked any hotels yet.";
                    return;
                }

                // use the string builder to format how the booked hotel is displayed
                StringBuilder details = new StringBuilder();
                for (int i = 0; i < bookedHotelsTemp.Length; i++)
                {
                    details.AppendLine(bookedHotelsTemp[i].HotelName.ToString());
                    details.AppendLine("=========================");
                    details.AppendLine(string.Format("Booked Hotel ID: {0}", bookedHotelsTemp[i].HotelID.ToString()));
                    details.AppendLine(string.Format("Start Date: {0}", bookedHotelsTemp[i].Start_Date.ToString()));
                    details.AppendLine(string.Format("End Date: {0}", bookedHotelsTemp[i].End_Date.ToString()));
                    details.AppendLine(string.Format("Price: ${0}", bookedHotelsTemp[i].Price.ToString()));
                    details.AppendLine("-------------------------");
                    details.AppendLine();
                }

                // display the booked hotels in the textbox
                BookedHotelsTextBox.Text = details.ToString();

            }
            catch (Exception ex)
            {
                BookedHotelsTextBox.Text = "Error: " + ex.Message;
            }

        } 

        // displays hotel details of available hotels
        protected void DisplayHotelDetails(HotelListing hotelListing)
        {
            // use string builder to format the string going into the display text box
            StringBuilder details = new StringBuilder();

            // list out the hotel's addess, nearest airport, its price, and the # of rooms available
            details.AppendLine(hotelListing.Name);
            details.AppendLine("=========================");
            details.AppendLine(string.Format("Hotel ID: {0}", hotelListing.HotelID));
            details.AppendLine("Address: ");
            details.AppendLine(string.Format("{0} {1}", hotelListing.HotelAddress.Number, hotelListing.HotelAddress.Street));
            details.AppendLine(string.Format("{0}, {1}, {2}", hotelListing.HotelAddress.City, hotelListing.HotelAddress.State, hotelListing.HotelAddress.Zip));
            details.AppendLine(string.Format("Nearest Airport: {0}", hotelListing.NearestAirport));
            details.AppendLine(string.Format("Quantity of Rooms: {0}", hotelListing.BookedRooms));
            details.AppendLine(string.Format("Price per Hotel Room: ${0:F2}", hotelListing.Price));

            // convert it all to a string and display
            HotelDetailTextBox.Text = details.ToString();
        }

        // event listener for pushing the book hotel button
        protected void BookHotel_Click(object sender, EventArgs e)
        {
            // first gather all inputs
            string username = User.Identity.Name;
            int hotelId;

            if (!int.TryParse(hiddenHotelId.Value, out hotelId))
            {
                BookHotelResult.Text = "Book Hotel Result: Must select a hotel from the list view first.";
                return;
            }

            string startDateStr = txtStartDate.Text; 
            string endDateStr = txtEndDate.Text;

            // Check if the entered in dates are valid
            // Parse dates
            DateTime startDate;
            DateTime endDate;

            // see if entered in start_date and end_date are valid
            if (!DateTime.TryParse(txtStartDate.Text, out startDate))
            {
                BookHotelResult.Text = "Book Hotel Result: Please enter a valid start date";
                return;
            }

            if (!DateTime.TryParse(txtEndDate.Text, out endDate))
            {
                BookHotelResult.Text = "Book Hote Result: Please enter a valid end date";
                return;
            }
            // Check if start date is today or in the future
            if (startDate.Date < DateTime.Today)
            {
                BookHotelResult.Text = "Book Hotel Result: Start date must be today or in the future";
                return;
            }

            // Check if end date is after start date
            if (endDate <= startDate)
            {
                BookHotelResult.Text = "Book Hotel Result: End date must be after start date";
                return;
            }

            // after all checks passed, call the service using a proxy
            Service1Client prxy = new Service1Client();

            try
            {
                int result = prxy.BookHotel(username, hotelId, startDateStr, endDateStr);

                // depending on the result, display the error tag accordingly
                switch (result)
                {
                    case 0:
                        // if the hotel was booked succesfully, then I have to refresh BookedHotels
                        // also have to refresh available hotels, since the quantity changed
                        BookHotelResult.Text = "Book Hotel Result: Hotel booked successfully!";
                        LoadBookedHotels();
                        LoadAvailableHotels();

                        // reset the input fields
                        txtStartDate.Text = "";
                        txtEndDate.Text = "";

                        // details about the balance have changed too 
                        displayBalance();
                        break;

                    // otherwise, report the specific type of error encountered in the result label
                    case 1:
                        BookHotelResult.Text = "Book Hotel Result: Insufficient balance";
                        break;
                    case 2:
                        BookHotelResult.Text = "Book Hotel Result: No rooms available at this hotel";
                        break;
                    case 3:
                        BookHotelResult.Text = "Book Hotel Result: User not found";
                        break;
                    case 4:
                        BookHotelResult.Text = "Book Hotel Result: Hotel not found";
                        break;
                    default:
                        BookHotelResult.Text = "Book Hotel Result: An error occurred";
                        break;
                }

            }
            // otherwise, print out the unexpected error (should've caught most errors, though)
            catch (Exception ex)
            {
                BookHotelResult.Text = "Book Hotel Result: " + ex.Message;
            }
        }
    }
}