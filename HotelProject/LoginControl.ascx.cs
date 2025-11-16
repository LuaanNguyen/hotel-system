using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelProject
{

    // create an event handler for login for the separate login pages to handle
    public delegate void LoginAttemptEventHandler(object sender, LoginEventArgs e);

    public partial class LoginControl : System.Web.UI.UserControl
    {
        
        public event LoginAttemptEventHandler LoginAttempt;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // once the login button is clicked, notify the event
        protected void btn_LoginClick(object sender, EventArgs e)
        {
            if (LoginAttempt != null)
            {
                LoginAttempt(this, new LoginEventArgs
                {
                    Username = UsernameTextbox.Text,
                    Password = PasswordTextbox.Text
                });
            }
        }
    }

    public class LoginEventArgs : EventArgs
    {
        public string Username { get; set; }
        public string Password { get; set; }


    }
}