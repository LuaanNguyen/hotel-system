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
            <asp:Label ID="HintLabel" runat="server" Text="Try entering SophiaGu as username, with password SophiaGu123. 
                Other valid credentials are LuanNguyen (username), LuanNguyen123 (password) AND MoTopiwala (username), MoTopiwala123 (password)"></asp:Label>
            <uc:Login ID="LoginControl1" runat="server" /><br />
            <asp:Label ID="ResultLabel" runat="server" Text="Result: "></asp:Label>
        </div>
    </form>
</body>
</html>
