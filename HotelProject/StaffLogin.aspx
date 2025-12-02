﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffLogin.aspx.cs" Inherits="HotelProject.StaffLogin" %>
<%@ Register Src="~/LoginControl.ascx" TagPrefix="uc" TagName="Login" %>
<!--Staff Login Page created by Sophia Gu-->
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Staff Login Page</h2>
            <asp:Button ID="HomeButton" runat="server" Text="Return to Home" OnClick="HomeButton_Click" /><br />
            <asp:Label ID="TryTwiceLabel" runat="server" Text="Try twice to get the login details" Font-Italic="true"></asp:Label><br /><br />
            <asp:Label ID="DescLabel" runat="server" Visible="false" Text="Hint: Login with TA as the username and Cse445! as the password." Font-Italic="true"></asp:Label>
            <uc:Login ID="LoginControl1" runat="server" />
            <asp:Label ID="ResultLabel" runat="server" Text="Result: "></asp:Label>
        </div>
    </form>
</body>
</html>