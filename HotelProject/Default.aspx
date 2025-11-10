<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HotelProject.Default" %>
<%@ Register Src="~/LoginControl.ascx" TagPrefix="uc" TagName="Login" %>
<!--Default page linking all services made by Sophia-->
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Title" runat="server" Text="Hotel Application" Font-Bold="true" Font-Size="large"></asp:Label>
            <br />
            <asp:Label ID="Names" runat="server" Text="Implemented by: Luan Nguyen, Sophia Gu, Muhammed Topiwala" Font-Italic="true"></asp:Label>
            <br />
            <asp:Label ID="Description" runat="server" Text="This service allows members to sign in, book hotel rooms, and rate those hotels. 
                Staff can use the application to receive notifications about discounted hotel prices, and book a batch of hotel rooms. 
                They will then offer those discounted rooms to the members through the application. New users can create an account
                to become a member."></asp:Label>
            <br />
            <asp:Table ID="ComponentsTable" runat="server" GridLines="Both" BorderWidth="1">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                        Application and Components Summary Table
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                        Percentage of overall contribution: Luan Nguyen: 33%, Sophia Gu: 33%, 
                        Muhammed Hunaid Topiwala: 33%
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Provider Name</asp:TableCell>
                    <asp:TableCell>Page and Component type</asp:TableCell>
                    <asp:TableCell>Component Description (What does the component do?)</asp:TableCell>
                    <asp:TableCell> Actual resources and methods used, link to component (where is it used></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Sophia Gu</asp:TableCell>
                    <asp:TableCell>.aspx page and server controls</asp:TableCell>
                    <asp:TableCell>The public Default page that calls and links other pages</asp:TableCell>
                    <asp:TableCell>GUI Design and C# code <br />
                                   Link: Current Page
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Sophia Gu</asp:TableCell>
                    <asp:TableCell>.ascx page and user controls</asp:TableCell>
                    <asp:TableCell>The Login User control component used for Staff & Member Login page</asp:TableCell>
                    <asp:TableCell>C# Code behind the GUI. Present on the Staff Login Page and Member Login Page<br />
                                   Staff Login Page: <asp:HyperLink ID="StaffLogin" runat="server" NavigateUrl="~/StaffLogin.aspx">Staff Login</asp:HyperLink><br />
                                   Member Login Page: <asp:HyperLink ID="MemberLoginLink" runat="server" NavigateUrl="~/MemberLogin.aspx">Member Login</asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Sophia Gu</asp:TableCell>
                    <asp:TableCell>Cookies</asp:TableCell>
                    <asp:TableCell>Stores username and password</asp:TableCell>
                    <asp:TableCell>GUI design and C# Code behind GUI using HTTP cookies library. Present in Staff & Member Login Page <br />
                                   Staff Login Page: <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/StaffLogin.aspx">Staff Login</asp:HyperLink><br />
                                   Member Login Page: <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/MemberLogin.aspx">Member Login</asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>           
        </div>
    </form>
</body>
</html>
