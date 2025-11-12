<%@ Control Language="C#" AutoEventWireup="true"
    CodeBehind="AgentProfile.ascx.cs"
    Inherits="HotelProject.AgentProfile" %>

<div style="padding:15px; border:1px solid #ccc; width:300px;">
    <h3>Agent Profile</h3>

    <asp:Label ID="lblAgentName" runat="server" Text="Agent Name: " />
    <br /><br />

    <asp:Label ID="lblTotalRooms" runat="server" Text="Total Rooms Booked: " />
    <br /><br />

    <asp:Label ID="lblLastDiscount" runat="server" Text="Last Discount Applied: " />
</div>
