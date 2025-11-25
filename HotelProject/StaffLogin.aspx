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
            <h3>Staff Login Page</h3>
            <asp:Label ID="DescLabel" runat="server" Text="Staff Login Page with Cookies.
                Hint: Login with TA as the username and Cse445! as the password. You'll 
                be redirected to the dashboard automatically." Font-Italic="true"></asp:Label>
            <uc:Login ID="LoginControl1" runat="server" />        
        </div>
    </form>
</body>
</html>