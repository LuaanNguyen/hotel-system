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
            <asp:Label ID="DescLabel" runat="server" Text="This is the Member Login page. 
                Cookies have been implemented, so as soon as you login, you will automatically 
                be redirected to the Member Dashboard for 10 minutes (10 minutes is cookie expiration timeframe).
                Hint: Try logging in with username SophiaGu and password SophiaGu123"
                Font-Italic="true"></asp:Label>
            <uc:Login ID="LoginControl1" runat="server" /><br />
            <asp:Label ID="ResultLabel" runat="server" Text="Result: "></asp:Label><br />

        </div>
    </form>
</body>
</html>
