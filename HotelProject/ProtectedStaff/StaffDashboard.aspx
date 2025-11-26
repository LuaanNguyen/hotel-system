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

                <asp:Label runat="server" Text="Rooms to Book:" />
                <asp:TextBox ID="txtRooms" runat="server" /><br /><br />

                <asp:Label runat="server" Text="Discount (%):" />
                <asp:TextBox ID="txtDiscount" runat="server" /><br /><br />

                <asp:Button ID="btnBook" runat="server" Text="Submit Booking" OnClick="btnBook_Click" />
                <br /><br />

                <asp:Label ID="lblResult" runat="server" ForeColor="Red" />
            </div>
        </div>
    </form>
</body>
</html>
