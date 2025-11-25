using HotelProject.RatingServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelProject
{
    public partial class MemberRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            // Check for nonempty fields first
            if(!string.IsNullOrEmpty(UsernameTextbox.Text) ||
               !string.IsNullOrEmpty(PasswordTextbox.Text) ||
               !string.IsNullOrEmpty(PasswordConfirmTextbox.Text) ||
               !string.IsNullOrEmpty(BalanceTextbox.Text))
            {
                ResultLabel.Text = "Result: Please fill out all fields";
                return;
            }

            if(PasswordTextbox.Text != PasswordConfirmTextbox.Text)
            {
                ResultLabel.Text = "Result: Ensure your passwords are matching";
                return;
            }


            try
            {
                Service1Client prxy = new Service1Client();

                string username = UsernameTextbox.Text;
                string password = PasswordTextbox.Text;
                float balance = float.Parse(BalanceTextbox.Text);

                if(prxy.RegisterMember(username, password, balance))
                {
                    ResultLabel.Text = $"Result: User with username {username} created successfully.";
                }
                else
                {
                    ResultLabel.Text = "Result: Failed to create user";
                }

            }
            catch (Exception ex)
            {
                ResultLabel.Text += ex.Message;
            }
        }
    }
}