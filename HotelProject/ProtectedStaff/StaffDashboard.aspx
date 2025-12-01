<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffDashboard.aspx.cs" Inherits="HotelProject.Protected.StaffDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Staff Dashboard</h2>

            <asp:Button ID="DefaultButton" runat="server" Text="Return to Default Page" OnClick="DefaultButton_Click" />
            <asp:Button ID="LogOutButton" runat="server" Text="Log Out" OnClick="LogOut_Click" /><br /><br />

            <asp:Label ID="WelcomeLabel" runat="server" Text="Welcome, "></asp:Label><br /><br />
            
            <asp:Label ID="BookHotelsTitle" runat="server" Text="Book Hotel Rooms for app's members here" Font-Bold="true"></asp:Label><br />
            <asp:GridView ID="gvHotels" runat="server" AutoGenerateColumns="False" 
                OnSelectedIndexChanged="gvHotels_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="HotelID" HeaderText="ID" />
                    <asp:BoundField DataField="Name" HeaderText="Hotel Name" />
                    <asp:BoundField DataField="BookedRooms" HeaderText="Booked Rooms" />
                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                    <asp:CommandField ShowSelectButton="True" SelectText="Select" />
                </Columns>
            </asp:GridView>

            <br />

            <div id="BookingPanel" runat="server" visible="false">
                <h3>Book Rooms for: <asp:Label ID="lblSelectedHotel" runat="server" /></h3>
                <asp:Label ID="InstructionLabel" runat="server" Text="Note: Any discounts set will be applied to previously booked rooms as well (i.e.,
                               if you already booked 1 room at 100$, and book another at a 10% discount, both those rooms will be set to 90$)"></asp:Label><br />

                <asp:Label runat="server" Text="Rooms to Book:" />
                <asp:TextBox ID="txtRooms" runat="server" /><br />

                <asp:Label runat="server" Text="Set Discount (%):" />
                <asp:TextBox ID="txtDiscount" runat="server" /><br />

                <asp:Button ID="btnBook" runat="server" Text="Submit Booking" OnClick="btnBook_Click" />
                <br /><br />

                <asp:Label ID="lblResult" runat="server" ForeColor="Red" />
            </div>

            <div>
                    <asp:Label ID="ChangePasswordTitle" runat="server" Text="Change Password" Font-Bold="true"></asp:Label><br />
                    <asp:Label ID="NewPasswordLabel" runat="server" Text="Enter New Password: "></asp:Label>
                    <asp:TextBox ID="NewPasswordTextBox" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="ConfirmPasswordLabel" runat="server" Text="Confirm Password: "></asp:Label>
                    <asp:TextBox ID="ConfirmPasswordTextBox" runat="server"></asp:TextBox><br />
                    <asp:Button ID="ChangePasswordBtn" runat="server" Text="Change Password" OnClick="ChangePassword_Click"/><br />
                    <asp:Label ID="ResultLabel" runat="server" Text="Result: "></asp:Label>

            </div>
        </div>

    </form>
</body>
</html>
