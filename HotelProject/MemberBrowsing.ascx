<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberBrowsing.ascx.cs" Inherits="HotelProject.MemberBrowsing" %>

<div style="border: 2px solid #2196F3; padding: 20px; border-radius: 8px; background-color: #f9f9f9; max-width: 800px;">
    <h3 style="color: #2196F3; text-align: center; margin-top: 0;">Browse Available Hotels</h3>
    
    <div style="margin-bottom: 20px; padding: 15px; background-color: #e3f2fd; border-radius: 5px;">
        <div style="margin-bottom: 10px;">
            <asp:Label ID="lblSearch" runat="server" Text="Search Hotels:" Font-Bold="true" style="display: inline-block; width: 120px;"></asp:Label>
            <asp:TextBox ID="txtSearch" runat="server" Width="250px" placeholder="Enter hotel name or location"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" 
                        BackColor="#2196F3" ForeColor="White" BorderStyle="None" 
                        Padding="8px 15px" Font-Bold="true" style="cursor: pointer; border-radius: 4px; margin-left: 10px;" />
        </div>
        
        <div style="margin-bottom: 10px;">
            <asp:Label ID="lblFilter" runat="server" Text="Filter by Price:" Font-Bold="true" style="display: inline-block; width: 120px;"></asp:Label>
            <asp:DropDownList ID="ddlPriceRange" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlPriceRange_SelectedIndexChanged">
                <asp:ListItem Value="all" Selected="True">All Prices</asp:ListItem>
                <asp:ListItem Value="budget">Budget ($0-$100)</asp:ListItem>
                <asp:ListItem Value="moderate">Moderate ($100-$200)</asp:ListItem>
                <asp:ListItem Value="luxury">Luxury ($200+)</asp:ListItem>
            </asp:DropDownList>
            
            <asp:Label ID="lblSortBy" runat="server" Text="Sort by:" Font-Bold="true" style="display: inline-block; width: 80px; margin-left: 20px;"></asp:Label>
            <asp:DropDownList ID="ddlSortBy" runat="server" Width="120px" AutoPostBack="true" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged">
                <asp:ListItem Value="name">Name</asp:ListItem>
                <asp:ListItem Value="price">Price</asp:ListItem>
                <asp:ListItem Value="rating" Selected="True">Rating</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    
    <asp:GridView ID="gvHotels" runat="server" AutoGenerateColumns="False" 
                  CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%"
                  OnRowCommand="gvHotels_RowCommand">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="HotelId" HeaderText="ID" Visible="False" />
            <asp:BoundField DataField="Name" HeaderText="Hotel Name" />
            <asp:BoundField DataField="Location" HeaderText="Location" />
            <asp:BoundField DataField="PricePerNight" HeaderText="Price/Night" DataFormatString="${0:F2}" />
            <asp:BoundField DataField="Rating" HeaderText="Rating" DataFormatString="{0}" />
            <asp:BoundField DataField="AvailableRooms" HeaderText="Available Rooms" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button ID="btnViewDetails" runat="server" Text="View Details" 
                                CommandName="ViewDetails" CommandArgument='<%# Eval("HotelId") %>'
                                BackColor="#2196F3" ForeColor="White" BorderStyle="None" 
                                Padding="5px 10px" Font-Size="Small" style="cursor: pointer; border-radius: 3px;" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#2196F3" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>
    
    <div style="margin-top: 20px; padding: 15px; background-color: #fff3cd; border-left: 4px solid #ffc107; border-radius: 4px; display: none;" id="divDetails" runat="server">
        <h4 style="color: #856404; margin-top: 0;">Hotel Details</h4>
        <asp:Label ID="lblHotelDetails" runat="server" Text=""></asp:Label>
    </div>
    
    <div style="margin-top: 15px; padding: 10px; background-color: #e3f2fd; border-left: 4px solid #2196F3; border-radius: 4px;">
        <asp:Label ID="lblStatus" runat="server" Text="Showing all available hotels." ForeColor="#1565c0"></asp:Label>
    </div>
</div>
