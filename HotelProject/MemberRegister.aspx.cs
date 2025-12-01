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

        // event listener for clicking the register button
        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            // Check for nonempty fields first
            if(!string.IsNullOrEmpty(UsernameTextbox.Text) &&
               !string.IsNullOrEmpty(PasswordTextbox.Text) &&
               !string.IsNullOrEmpty(PasswordConfirmTextbox.Text) &&
               !string.IsNullOrEmpty(BalanceTextbox.Text))
            {

                // next, check if the two password fields match
                if (PasswordTextbox.Text != PasswordConfirmTextbox.Text)
                {
                    ResultLabel.Text = "Result: Ensure your passwords are matching";
                    return;
                }

                // check if the balance entered is a float and is valid
                float balance;
                if(!float.TryParse(BalanceTextbox.Text, out balance))
                {
                    ResultLabel.Text = "Result: Enter in a decimal for the initial balance";
                    return;
                }
                if(balance < 0)
                {
                    ResultLabel.Text = "Result: Enter in a positive number for the initial balance";
                    return;

                }


                    // if all tests passed, use a proxy to call the service
                    try
                    {
                        Service1Client prxy = new Service1Client();

                        string username = UsernameTextbox.Text;
                        string password = PasswordTextbox.Text;

                        if (prxy.RegisterMember(username, password, balance))
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
            else
            {
                ResultLabel.Text = "Result: Please fill out all fields.";
                return;
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MemberLogin.aspx");
        }

        protected void DefaultButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}