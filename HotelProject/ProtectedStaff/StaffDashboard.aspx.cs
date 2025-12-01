using HotelProject.RatingServiceReference;
using System;
using System.Net.NetworkInformation;
using System.Web.Security;
using System.Web.UI;

namespace HotelProject.Protected
{
    public partial class StaffDashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string username = User.Identity.Name;
                WelcomeLabel.Text = "Welcome, ";
                WelcomeLabel.Text += username;
                LoadHotels();
            }
        }

        // initially displays hotels
        private void LoadHotels()
        {
            Service1Client proxy = new Service1Client();
            gvHotels.DataSource = proxy.GetAllHotels();
            gvHotels.DataBind();
        }

        // Event listener for when a hotel is selected
        protected void gvHotels_SelectedIndexChanged(object sender, EventArgs e)
        {
            string hotelId = gvHotels.SelectedRow.Cells[0].Text;
            string hotelName = gvHotels.SelectedRow.Cells[1].Text;

            ViewState["HotelID"] = hotelId;
            lblSelectedHotel.Text = hotelName;

            BookingPanel.Visible = true;
        }

        // Staff-booking method
        protected void btnBook_Click(object sender, EventArgs e)
        {
            string hotelId = ViewState["HotelID"].ToString();
            int rooms;
            float discount;

            // check if the inputted fields are valid first
            if (!int.TryParse(txtRooms.Text, out rooms)) {
                lblResult.Text = "Enter in a valid integer for the number of rooms.";
                return;
            }
            if (rooms <= 0)
            {
                lblResult.Text = "You must enter in a postive number for the number of rooms.";
                return;
            }

            if(!float.TryParse(txtDiscount.Text, out discount))
            {
                lblResult.Text = "Enter in a valid decimal for the discount field.";
                return;
            }
            if((discount < 0 || discount > 100))
            {
                lblResult.Text = "Enter in a discount between 0 and 100 (inclusive)";
                return;
            }
            
            // If all error checks pass, we can proceed with calling the service
            Service1Client proxy = new Service1Client();
            bool success = proxy.BookHotelRooms(hotelId, rooms, discount);

            lblResult.Text = success ? "Booking successful!" : "Booking failed.";

            // Refresh the hotels, since more rooms have been booked
            LoadHotels();
        }

        // changing password
        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            // get the username from the forms authentication ticket
            string username = User.Identity.Name;

            // check that the fields are all filled
            if (string.IsNullOrEmpty(NewPasswordTextBox.Text) || string.IsNullOrEmpty(ConfirmPasswordTextBox.Text))
            {
                ResultLabel.Text = "Result: Enter in all fields";
                return;
            }

            // now check that both entered passwords actually match
            if (NewPasswordTextBox.Text != ConfirmPasswordTextBox.Text)
            {
                ResultLabel.Text = "Result: Ensure all fields are matching";
                return;
            }

            // if all error checks passed, create the service prxy and proceed
            Service1Client prxy = new Service1Client();

            if (prxy.ChangePassword(username, NewPasswordTextBox.Text, 0))
            {
                ResultLabel.Text = "Result: Successfully changed password!";
            }
            else
            {
                ResultLabel.Text = "Result: An error occured while trying to change the password.";
            }

        }

        // event listener for the log out button
        protected void LogOut_Click(object sender, EventArgs e)
        {
            // clear authentication
            FormsAuthentication.SignOut();

            // clear session
            Session.Clear();
            Session.Abandon();

            // go back to login page
            Response.Redirect("~/StaffLogin.aspx");
        }

        protected void DefaultButton_Click(object sender, EventArgs e)
        {
            // go back to the default page
            Response.Redirect("~/Default.aspx");
        }
    }
}
