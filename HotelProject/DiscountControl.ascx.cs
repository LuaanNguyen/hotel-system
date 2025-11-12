using System;

namespace HotelProject
{
    public partial class DiscountControl : System.Web.UI.UserControl
    {
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtOriginalPrice.Text, out decimal original) &&
                decimal.TryParse(txtDiscount.Text, out decimal percent))
            {
                decimal amount = original * (percent / 100M);
                decimal final = original - amount;

                lblDiscountedPrice.Text = "Discounted Price: " + final.ToString("C");
            }
            else
            {
                lblDiscountedPrice.Text = "Invalid input.";
            }
        }
    }
}
