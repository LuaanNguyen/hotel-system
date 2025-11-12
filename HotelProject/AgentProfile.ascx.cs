
using System;

namespace HotelProject
{
    public partial class AgentProfile : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            lblAgentName.Text = "Agent Name: Luan Nguyen";
            lblTotalRooms.Text = "Total Rooms Booked: _____";
            lblLastDiscount.Text = "Last Discount Applied: ______";
        }
    }
}
