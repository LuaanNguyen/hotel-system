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
                    new Hotel { HotelId = "1", Name = "Westin", Location = "Tempe", PricePerNight = 41.04, Rating = 4.2, AvailableRooms = 35 },
                    new Hotel { HotelId = "2", Name = "La Quinta Inn", Location = "Tempe", PricePerNight = 29.53, Rating = 4.0, AvailableRooms = 38 },
                    new Hotel { HotelId = "3", Name = "Hampton Inn Phoenix-Airport North", Location = "Phoenix", PricePerNight = 33.00, Rating = 4.1, AvailableRooms = 95 },
                    new Hotel { HotelId = "4", Name = "Hampton Inn and Suites Tempe", Location = "Tempe", PricePerNight = 75.00, Rating = 4.4, AvailableRooms = 81 },
                    new Hotel { HotelId = "5", Name = "Days Inn By Wyndham Phoenix North", Location = "Phoenix", PricePerNight = 77.00, Rating = 3.2, AvailableRooms = 75 },
                    new Hotel { HotelId = "6", Name = "Best Western Plus Chandler", Location = "Chandler", PricePerNight = 77.00, Rating = 4.3, AvailableRooms = 90 },
                    new Hotel { HotelId = "7", Name = "Hilton Garden Inn Phoenix Airport", Location = "Phoenix", PricePerNight = 83.00, Rating = 4.4, AvailableRooms = 77 },
                    new Hotel { HotelId = "8", Name = "SureStay By Best Western Phoenix Airport", Location = "Phoenix", PricePerNight = 99.00, Rating = 3.5, AvailableRooms = 10 }
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
