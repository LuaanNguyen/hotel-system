using System;

namespace HotelProject
{
    public partial class MemberProfile : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUserName.Text = "Member Name: " + (Context.User != null ? Context.User.Identity.Name : "Guest");
            // TODO: for Assignment 6
        }
    }
}
