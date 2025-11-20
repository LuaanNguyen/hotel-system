<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberDashboard.aspx.cs" Inherits="HotelProject.MemberDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>Member Dashboard</h3><br />
            <asp:Label ID="WelcomeLabel" runat="server" Text=""></asp:Label><br /><br />

            <asp:Label ID="CookieTitle" runat="server" Text="TryIt: Cookie" Font-Bold="true"></asp:Label><br />
            <asp:Label ID="DescLabel" runat="server" Text="Username and Password saved with cookies, and pulled for display here.
                To test further, you can redirect to the default page with the link, and reenter the Member Login Page
                from the Default.aspx. You'll skip the login screen, and come back to this screen." Font-Italic="true"></asp:Label><br />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Default</asp:HyperLink><br />
            <asp:Label ID="CookieUsername" runat="server" Text="Entered Username: "></asp:Label><br />
            <asp:Label ID="CookiePassword" runat="server" Text="Entered Password: "></asp:Label><br />
        </div>
    </form>
</body>
</html>
