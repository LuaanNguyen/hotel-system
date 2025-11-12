<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DiscountControl.ascx.cs" Inherits="HotelProject.DiscountControl" %>

<div>
    <asp:Label ID="lblOriginal" runat="server" Text="Original Price:" />
    <asp:TextBox ID="txtOriginalPrice" runat="server" />
    <br />

    <asp:Label ID="lblPercent" runat="server" Text="Discount (%):" />
    <asp:TextBox ID="txtDiscount" runat="server" />
    <br />

    <asp:Button ID="btnCalculate" runat="server" Text="Calculate"
                OnClick="btnCalculate_Click" />

    <br />
    <asp:Label ID="lblDiscountedPrice" runat="server" />
</div>
