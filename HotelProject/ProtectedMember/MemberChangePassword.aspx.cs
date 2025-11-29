using HotelProject.RatingServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelProject.ProtectedMember
{
    public partial class MemberChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = User.Identity.Name;
            WelcomeLabel.Text = "Welcome, ";
            WelcomeLabel.Text += username;
        }

        // Change password
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

            if (prxy.ChangePassword(username, NewPasswordTextBox.Text, 1))
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