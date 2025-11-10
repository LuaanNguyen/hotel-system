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
            <h3>Member Login Page</h3>
            <uc:Login ID="LoginControl1" runat="server" /><br />

            <asp:Label ID="CookieTitle" runat="server" Text="TryIt: Cookie" Font-Bold="true"></asp:Label><br />
            <asp:Label ID="Description" runat="server" Text="Log in with the 
                given credentials, exit the browser, and re-enter. 
                Your username and password information will be saved below"></asp:Label><br />
            <asp:Label ID="CookieUsername" runat="server" Text="Entered Username: "></asp:Label><br />
            <asp:Label ID="CookiePassword" runat="server" Text="Entered Password: "></asp:Label><br />
        </div>
    </form>
</body>
</html>
