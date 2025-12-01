using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace HotelProject
{
    /// <summary>
    /// Member Browsing User Control
    /// Allows members to browse and search available hotels
    /// Author: Muhammed Hunaid Topiwala
    /// </summary>
    public partial class MemberBrowsing : System.Web.UI.UserControl
    {
        private static List<Hotel> allHotels = new List<Hotel>();

        /// <summary>
        /// Page load event handler
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeHotels();
                LoadHotels();
            }
        }

        /// <summary>
        /// Initializes sample hotel data
        /// </summary>
        private void InitializeHotels()
        {
            if (allHotels.Count == 0)
            {
                allHotels = new List<Hotel>
                {
                    new Hotel { HotelId = "H001", Name = "Westin Hotel", Location = "Downtown Phoenix", PricePerNight = 189.99, Rating = 4.5, AvailableRooms = 25 },
                    new Hotel { HotelId = "H002", Name = "Hampton Inn Phoenix", Location = "Airport North", PricePerNight = 129.99, Rating = 4.2, AvailableRooms = 30 },
                    new Hotel { HotelId = "H003", Name = "La Quinta Inn", Location = "Scottsdale", PricePerNight = 99.99, Rating = 4.0, AvailableRooms = 20 },
                    new Hotel { HotelId = "H004", Name = "Best Western Plus Chandler", Location = "Chandler", PricePerNight = 119.99, Rating = 4.3, AvailableRooms = 15 },
                    new Hotel { HotelId = "H005", Name = "Hilton Garden Inn Phoenix", Location = "Airport", PricePerNight = 159.99, Rating = 4.4, AvailableRooms = 28 },
                    new Hotel { HotelId = "H006", Name = "Marriott Phoenix Resort", Location = "Tempe", PricePerNight = 229.99, Rating = 4.7, AvailableRooms = 12 },
                    new Hotel { HotelId = "H007", Name = "Comfort Inn", Location = "Mesa", PricePerNight = 89.99, Rating = 3.8, AvailableRooms = 35 },
                    new Hotel { HotelId = "H008", Name = "Embassy Suites", Location = "Downtown", PricePerNight = 199.99, Rating = 4.6, AvailableRooms = 18 }
                };
            }
        }

        /// <summary>
        /// Loads hotels into the GridView
        /// </summary>
        private void LoadHotels(List<Hotel> hotelsToDisplay = null)
        {
            if (hotelsToDisplay == null)
            {
                hotelsToDisplay = allHotels;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("HotelId", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Location", typeof(string));
            dt.Columns.Add("PricePerNight", typeof(double));
            dt.Columns.Add("Rating", typeof(double));
            dt.Columns.Add("AvailableRooms", typeof(int));

            foreach (var hotel in hotelsToDisplay)
            {
                dt.Rows.Add(hotel.HotelId, hotel.Name, hotel.Location, 
                           hotel.PricePerNight, hotel.Rating, hotel.AvailableRooms);
            }

            gvHotels.DataSource = dt;
            gvHotels.DataBind();

            lblStatus.Text = string.Format("Showing {0} hotel(s).", hotelsToDisplay.Count);
        }

        /// <summary>
        /// Search button click event handler
        /// </summary>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim().ToLower();
            
            if (string.IsNullOrEmpty(searchTerm))
            {
                LoadHotels();
                lblStatus.Text = "Showing all available hotels.";
                return;
            }

            var filteredHotels = allHotels.Where(h => 
                h.Name.ToLower().Contains(searchTerm) || 
                h.Location.ToLower().Contains(searchTerm)).ToList();

            LoadHotels(filteredHotels);
            
            if (filteredHotels.Count == 0)
            {
                lblStatus.Text = "No hotels found matching your search.";
            }
            else
            {
                lblStatus.Text = string.Format("Found {0} hotel(s) matching '{1}'.", filteredHotels.Count, searchTerm);
            }
        }

        /// <summary>
        /// Price range filter change event handler
        /// </summary>
        protected void ddlPriceRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Hotel> filteredHotels = allHotels;

            switch (ddlPriceRange.SelectedValue)
            {
                case "budget":
                    filteredHotels = allHotels.Where(h => h.PricePerNight < 100).ToList();
                    break;
                case "moderate":
                    filteredHotels = allHotels.Where(h => h.PricePerNight >= 100 && h.PricePerNight < 200).ToList();
                    break;
                case "luxury":
                    filteredHotels = allHotels.Where(h => h.PricePerNight >= 200).ToList();
                    break;
            }

            ApplySorting(ref filteredHotels);
            LoadHotels(filteredHotels);
        }

        /// <summary>
        /// Sort by dropdown change event handler
        /// </summary>
        protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            var filteredHotels = GetCurrentFilteredHotels();
            ApplySorting(ref filteredHotels);
            LoadHotels(filteredHotels);
        }

        /// <summary>
        /// Applies sorting to hotel list
        /// </summary>
        private void ApplySorting(ref List<Hotel> hotels)
        {
            switch (ddlSortBy.SelectedValue)
            {
                case "name":
                    hotels = hotels.OrderBy(h => h.Name).ToList();
                    break;
                case "price":
                    hotels = hotels.OrderBy(h => h.PricePerNight).ToList();
                    break;
                case "rating":
                    hotels = hotels.OrderByDescending(h => h.Rating).ToList();
                    break;
            }
        }

        /// <summary>
        /// Gets currently filtered hotels based on price range
        /// </summary>
        private List<Hotel> GetCurrentFilteredHotels()
        {
            switch (ddlPriceRange.SelectedValue)
            {
                case "budget":
                    return allHotels.Where(h => h.PricePerNight < 100).ToList();
                case "moderate":
                    return allHotels.Where(h => h.PricePerNight >= 100 && h.PricePerNight < 200).ToList();
                case "luxury":
                    return allHotels.Where(h => h.PricePerNight >= 200).ToList();
                default:
                    return allHotels;
            }
        }

        /// <summary>
        /// GridView row command event handler
        /// </summary>
        protected void gvHotels_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                string hotelId = e.CommandArgument.ToString();
                var hotel = allHotels.FirstOrDefault(h => h.HotelId == hotelId);

                if (hotel != null)
                {
                    divDetails.Style["display"] = "block";
                    lblHotelDetails.Text = string.Format(
                        "<strong>{0}</strong><br/>" +
                        "Location: {1}<br/>" +
                        "Price per Night: ${2:F2}<br/>" +
                        "Rating: {3}/5<br/>" +
                        "Available Rooms: {4}<br/>" +
                        "<br/><em>To book this hotel, please contact our booking department or use the member portal.</em>",
                        hotel.Name, hotel.Location, hotel.PricePerNight, hotel.Rating, hotel.AvailableRooms
                    );
                }
            }
        }
    }

    /// <summary>
    /// Hotel data model
    /// </summary>
    public class Hotel
    {
        public string HotelId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public double PricePerNight { get; set; }
        public double Rating { get; set; }
        public int AvailableRooms { get; set; }
    }
}
