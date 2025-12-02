<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberLogin.aspx.cs" Inherits="HotelProject.MemberLogin" %>
<%@ Register Src="~/LoginControl.ascx" TagPrefix="uc" TagName="Login" %>
<!--Member Login page to demonstrate the Login User control created by Sophia Gu-->
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Member Login Page</h2>
            <asp:Button ID="HomeButton" runat="server" Text="Return to Home" OnClick="HomeButton_Click" /><br />
            <asp:Label ID="TryTwiceLabel" runat="server" Text="Try twice to get the login details" Font-Italic="true"></asp:Label><br /><br />
            <asp:Label ID="HintLabel" runat="server" Visible="false" Text="Hint: Valid credentials are SophiaGu/SophiaGu123, LuanNguyen/LuanNguyen123, MoTopiwala/MoTopiwala123"></asp:Label>
            <uc:Login ID="LoginControl1" runat="server" /><br />
            <asp:Label ID="ResultLabel" runat="server" Text="Result: "></asp:Label>
        </div>
    </form>
</body>
</html>
