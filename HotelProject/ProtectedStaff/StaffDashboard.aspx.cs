using HotelProject.RatingServiceReference;
using System;
using System.Net.NetworkInformation;
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

        // when a hotel is selected from the GUI
        protected void gvHotels_SelectedIndexChanged(object sender, EventArgs e)
        {
            string hotelId = gvHotels.SelectedRow.Cells[0].Text;
            string hotelName = gvHotels.SelectedRow.Cells[1].Text;

            ViewState["HotelID"] = hotelId;
            lblSelectedHotel.Text = hotelName;

            BookingPanel.Visible = true;
        }

        // booking method
        protected void btnBook_Click(object sender, EventArgs e)
        {
            string hotelId = ViewState["HotelID"].ToString();
            int rooms = int.Parse(txtRooms.Text);
            float discount = float.Parse(txtDiscount.Text);

            Service1Client proxy = new Service1Client();
            bool success = proxy.BookHotelRooms(hotelId, rooms, discount);

            lblResult.Text = success ? "Booking successful!" : "Booking failed.";

            // Refresh
            LoadHotels();
        }

        // changing password
        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            // get the username from the forms authentication ticket
            string username = User.Identity.Name;

            // check the fields are all filled
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

            // otherwise create the service prxy and proceed
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
    }
}
