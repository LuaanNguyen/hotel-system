using HotelProject.RatingServiceReference;
using System;
using System.Web.UI;

namespace HotelProject.Protected
{
    public partial class StaffDashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadHotels();
            }
        }

        private void LoadHotels()
        {
            Service1Client proxy = new Service1Client();
            gvHotels.DataSource = proxy.GetAllHotels();
            gvHotels.DataBind();
        }

        protected void gvHotels_SelectedIndexChanged(object sender, EventArgs e)
        {
            string hotelId = gvHotels.SelectedRow.Cells[0].Text;
            string hotelName = gvHotels.SelectedRow.Cells[1].Text;

            ViewState["HotelID"] = hotelId;
            lblSelectedHotel.Text = hotelName;

            BookingPanel.Visible = true;
        }

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
    }
}
