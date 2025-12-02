<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgentBooking.ascx.cs" Inherits="HotelProject.AgentBooking" %>

<div style="border: 2px solid #4CAF50; padding: 20px; border-radius: 8px; background-color: #f9f9f9; max-width: 600px;">
    <h3 style="color: #4CAF50; text-align: center; margin-top: 0;">Agent Batch Booking System</h3>
    
    <div style="margin-bottom: 15px;">
        <asp:Label ID="lblAgentId" runat="server" Text="Agent ID:" Font-Bold="true" style="display: inline-block; width: 150px;"></asp:Label>
        <asp:TextBox ID="txtAgentId" runat="server" Width="200px" placeholder="Enter Agent ID"></asp:TextBox>
    </div>
    
    <div style="margin-bottom: 15px;">
        <asp:Label ID="lblHotel" runat="server" Text="Select Hotel:" Font-Bold="true" style="display: inline-block; width: 150px;"></asp:Label>
        <asp:DropDownList ID="ddlHotels" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlHotels_SelectedIndexChanged">
            <asp:ListItem Value="">-- Select Hotel --</asp:ListItem>
            <asp:ListItem Value="Westin">Westin Hotel</asp:ListItem>
            <asp:ListItem Value="Hampton">Hampton Inn Phoenix</asp:ListItem>
            <asp:ListItem Value="LaQuinta">La Quinta Inn</asp:ListItem>
            <asp:ListItem Value="BestWestern">Best Western Plus Chandler</asp:ListItem>
            <asp:ListItem Value="Hilton">Hilton Garden Inn Phoenix</asp:ListItem>
        </asp:DropDownList>
    </div>
    
    <div style="margin-bottom: 15px;">
        <asp:Label ID="lblRoomCount" runat="server" Text="Number of Rooms:" Font-Bold="true" style="display: inline-block; width: 150px;"></asp:Label>
        <asp:TextBox ID="txtRoomCount" runat="server" Width="200px" TextMode="Number" Text="1"></asp:TextBox>
    </div>
    
    <div style="margin-bottom: 15px;">
        <asp:Label ID="lblCheckIn" runat="server" Text="Check-in Date:" Font-Bold="true" style="display: inline-block; width: 150px;"></asp:Label>
        <asp:TextBox ID="txtCheckIn" runat="server" Width="200px" TextMode="Date"></asp:TextBox>
    </div>
    
    <div style="margin-bottom: 15px;">
        <asp:Label ID="lblCheckOut" runat="server" Text="Check-out Date:" Font-Bold="true" style="display: inline-block; width: 150px;"></asp:Label>
        <asp:TextBox ID="txtCheckOut" runat="server" Width="200px" TextMode="Date"></asp:TextBox>
    </div>
    
    <div style="margin-bottom: 15px;">
        <asp:Label ID="lblDiscount" runat="server" Text="Discount (%):" Font-Bold="true" style="display: inline-block; width: 150px;"></asp:Label>
        <asp:Label ID="lblDiscountValue" runat="server" Text="0%" ForeColor="#4CAF50" Font-Bold="true"></asp:Label>
    </div>
    
    <div style="text-align: center; margin-top: 20px;">
        <asp:Button ID="btnBookRooms" runat="server" Text="Book Rooms" 
                    OnClick="btnBookRooms_Click" 
                    BackColor="#4CAF50" ForeColor="White" 
                    BorderStyle="None" Padding="10px 20px" 
                    Font-Bold="true" style="cursor: pointer; border-radius: 4px;" />
        
        <asp:Button ID="btnClear" runat="server" Text="Clear" 
                    OnClick="btnClear_Click" 
                    BackColor="#f44336" ForeColor="White" 
                    BorderStyle="None" Padding="10px 20px" 
                    Font-Bold="true" style="cursor: pointer; border-radius: 4px; margin-left: 10px;" />
    </div>
    
    <div style="margin-top: 20px; padding: 10px; background-color: #e8f5e9; border-left: 4px solid #4CAF50; border-radius: 4px;">
        <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="#2e7d32"></asp:Label>
    </div>
    
    <div style="margin-top: 20px;">
        <h4 style="color: #4CAF50;">Recent Bookings:</h4>
        <asp:GridView ID="gvBookings" runat="server" AutoGenerateColumns="False" 
                      CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="BookingId" HeaderText="Booking ID" />
                <asp:BoundField DataField="HotelName" HeaderText="Hotel" />
                <asp:BoundField DataField="RoomCount" HeaderText="Rooms" />
                <asp:BoundField DataField="CheckInDate" HeaderText="Check-in" DataFormatString="{0:MM/dd/yyyy}" />
                <asp:BoundField DataField="Discount" HeaderText="Discount" DataFormatString="{0}%" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#4CAF50" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </div>
</div>
