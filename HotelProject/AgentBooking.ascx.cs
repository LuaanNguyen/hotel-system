using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace HotelProject
{
    /// <summary>
    /// Agent Booking User Control
    /// Allows hotel staff agents to book multiple rooms with discounts
    /// Author: Muhammed Hunaid Topiwala
    /// </summary>
    public partial class AgentBooking : System.Web.UI.UserControl
    {
        private static List<Booking> bookings = new List<Booking>();
        private static Dictionary<string, double> hotelDiscounts = new Dictionary<string, double>
        {
            { "Westin", 25.0 },
            { "Hampton", 15.0 },
            { "LaQuinta", 30.0 },
            { "BestWestern", 20.0 },
            { "Hilton", 18.0 }
        };

        /// <summary>
        /// Page load event handler
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCheckIn.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                txtCheckOut.Text = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
                LoadBookingsGrid();
            }
        }

        /// <summary>
        /// Hotel selection changed event handler
        /// </summary>
        protected void ddlHotels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlHotels.SelectedValue))
            {
                double discount = hotelDiscounts.ContainsKey(ddlHotels.SelectedValue) 
                    ? hotelDiscounts[ddlHotels.SelectedValue] 
                    : 0;
                lblDiscountValue.Text = discount.ToString() + "%";
            }
            else
            {
                lblDiscountValue.Text = "0%";
            }
        }

        /// <summary>
        /// Book rooms button click event handler
        /// </summary>
        protected void btnBookRooms_Click(object sender, EventArgs e)
        {
            if (ValidateBooking())
            {
                string bookingId = "BK" + DateTime.Now.Ticks.ToString().Substring(10);
                string hotelName = ddlHotels.SelectedItem.Text;
                int roomCount = int.Parse(txtRoomCount.Text);
                DateTime checkIn = DateTime.Parse(txtCheckIn.Text);
                double discount = hotelDiscounts.ContainsKey(ddlHotels.SelectedValue) 
                    ? hotelDiscounts[ddlHotels.SelectedValue] 
                    : 0;

                Booking booking = new Booking
                {
                    BookingId = bookingId,
                    AgentId = txtAgentId.Text,
                    HotelName = hotelName,
                    RoomCount = roomCount,
                    CheckInDate = checkIn,
                    CheckOutDate = DateTime.Parse(txtCheckOut.Text),
                    Discount = discount,
                    BookingDate = DateTime.Now
                };

                bookings.Add(booking);
                LoadBookingsGrid();

                lblStatus.Text = string.Format("Success! Booking {0}: {1} room(s) at {2} with {3}% discount.", 
                    bookingId, roomCount, hotelName, discount);
            }
        }

        /// <summary>
        /// Clear button click event handler
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtAgentId.Text = string.Empty;
            ddlHotels.SelectedIndex = 0;
            txtRoomCount.Text = "1";
            txtCheckIn.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            txtCheckOut.Text = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
            lblDiscountValue.Text = "0%";
            lblStatus.Text = string.Empty;
        }

        /// <summary>
        /// Validates booking input fields
        /// </summary>
        private bool ValidateBooking()
        {
            if (string.IsNullOrWhiteSpace(txtAgentId.Text))
            {
                lblStatus.Text = "Error: Agent ID is required.";
                return false;
            }

            if (string.IsNullOrEmpty(ddlHotels.SelectedValue))
            {
                lblStatus.Text = "Error: Please select a hotel.";
                return false;
            }

            if (!int.TryParse(txtRoomCount.Text, out int roomCount) || roomCount < 1)
            {
                lblStatus.Text = "Error: Please enter a valid number of rooms (minimum 1).";
                return false;
            }

            if (!DateTime.TryParse(txtCheckIn.Text, out DateTime checkIn))
            {
                lblStatus.Text = "Error: Invalid check-in date.";
                return false;
            }

            if (!DateTime.TryParse(txtCheckOut.Text, out DateTime checkOut))
            {
                lblStatus.Text = "Error: Invalid check-out date.";
                return false;
            }

            if (checkOut <= checkIn)
            {
                lblStatus.Text = "Error: Check-out date must be after check-in date.";
                return false;
            }

            if (checkIn < DateTime.Now.Date)
            {
                lblStatus.Text = "Error: Check-in date cannot be in the past.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Loads bookings data into the GridView
        /// </summary>
        private void LoadBookingsGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("BookingId", typeof(string));
            dt.Columns.Add("HotelName", typeof(string));
            dt.Columns.Add("RoomCount", typeof(int));
            dt.Columns.Add("CheckInDate", typeof(DateTime));
            dt.Columns.Add("Discount", typeof(double));

            foreach (var booking in bookings)
            {
                if (bookings.IndexOf(booking) < 5)
                {
                    dt.Rows.Add(booking.BookingId, booking.HotelName, booking.RoomCount, 
                               booking.CheckInDate, booking.Discount);
                }
            }

            gvBookings.DataSource = dt;
            gvBookings.DataBind();
        }
    }

    /// <summary>
    /// Booking data model
    /// </summary>
    public class Booking
    {
        public string BookingId { get; set; }
        public string AgentId { get; set; }
        public string HotelName { get; set; }
        public int RoomCount { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public double Discount { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
