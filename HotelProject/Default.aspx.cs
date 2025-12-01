using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelProject
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void btnTryHash_Click(object sender, EventArgs e)
        {
            // Test hash functionality
            string testHash = SecurityLib.Security.HashPassword("Test123");
            // Display in debug or log
            System.Diagnostics.Debug.WriteLine("Hash test: " + testHash);
        }

        protected void MemberLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MemberLogin.aspx");
        }

        protected void MemberRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MemberRegister.aspx");
        }

        protected void MemberRating_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ProtectedMember/MemberRating.aspx");
        }

        protected void MemberBrowse_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ProtectedMember/MemberBrowse.aspx");
        }

        protected void MemberPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ProtectedMember/MemberChangePassword.aspx");
        }

        protected void StaffLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StaffLogin.aspx");
        }

        protected void StaffDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ProtectedStaff/StaffDashboard.aspx");
        }
    }
}