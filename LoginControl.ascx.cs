using System;
using System.Web.UI;

namespace HotelProject
{
    public partial class LoginControl: UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            if (username == "test" && password == "test")
            {
                Result.Text += "Login Successful";
            }
            else
            {
                Result.Text += "Login Not Successful";
            }
    }
}