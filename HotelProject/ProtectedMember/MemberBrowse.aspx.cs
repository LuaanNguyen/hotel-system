using HotelProject.RatingServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace HotelProject
{
    public partial class MemberBrowse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAvailableHotels();
            }
        }

        protected List<HotelListing> Hotels
        {
            get { return Session["Hotels"] as List<HotelListing>; }
            set { Session["Hotels"] = value; }
        }

        protected void LoadAvailableHotels()
        {
            HotelDetailTextBox.Text = "Select a hotel to view further details...";
            try
            {
                Service1Client prxy = new Service1Client();
                HotelListing[] hotelsArray = prxy.BrowseHotels();

                if (hotelsArray == null || hotelsArray.Length == 0)
                {
                    HotelDetailTextBox.Text = "No hotels have been booked + posted by an agent yet.";
                    HotelListView.DataSource = null;
                    Hotels = null;
                }
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

        protected void HotelListView_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            HotelListView.SelectedIndex = e.NewSelectedIndex;
            HotelListView.DataSource = Hotels;
            HotelListView.DataBind();
        }

        protected void lvHotels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HotelListView.SelectedIndex >= 0 && Hotels != null && HotelListView.SelectedIndex < Hotels.Count)
            {
                HotelListing selectedHotel = Hotels[HotelListView.SelectedIndex];
                DisplayHotelDetails(selectedHotel);
            }
        }

        protected void DisplayHotelDetails(HotelListing hotelListing)
        {
            StringBuilder details = new StringBuilder();
            details.AppendLine($"{hotelListing.Name}");
            details.AppendLine("=========================");
            details.AppendLine("Address: ");
            details.AppendLine($"{hotelListing.HotelAddress.Number} {hotelListing.HotelAddress.Street}");
            details.AppendLine($"{hotelListing.HotelAddress.City}, {hotelListing.HotelAddress.State}, {hotelListing.HotelAddress.Zip}");
            details.AppendLine($"Nearest Airport: {hotelListing.NearestAirport}");
            details.AppendLine($"Quantity of Rooms: {hotelListing.BookedRooms}");
            details.AppendLine($"Price per Hotel Room: {hotelListing.Price:F2}");
            HotelDetailTextBox.Text = details.ToString();
        }
    }
}