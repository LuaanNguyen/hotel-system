<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HotelProject.Default" %>
<%@ Register Src="~/LoginControl.ascx" TagPrefix="uc" TagName="Login" %>
<%@ Register Src="~/DiscountControl.ascx" TagPrefix="uc" TagName="Discount" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hotel Application</title>

    <style>
        body {background: #f5f5f5; margin:0; padding:0; }
        .container { width: 95%; max-width: 1200px; margin:auto; padding:20px; }
        .card { background:white; padding:20px; margin-top:20px; box-shadow:0 0 10px #ddd; }
        .navBtn {
            margin:4px; padding:8px 14px; background:#007bff;
            border:none; color:white !important; cursor:pointer;
        }
        .navBtn:hover { background:#0056b3; }
        table { width:100%; }
    </style>
</head>

<body>
<form id="form1" runat="server">
<div class="container">

    <div class="card" style="text-align:center;">
        <h1>CSE445: Hotel Application</h1>
        <h4 style="color:#555;">Luan Nguyen, Sophia Gu, Muhammed Topiwala</h4>

        <p style="max-width:800px; margin:auto; color:#444;">
            This ASP.NET web application allows members to register, log in, browse hotels, book rooms, 
            and rate their stays. Staff can log in, post discounted room batches, update hotel pricing, 
            and manage room availability for members.
        </p>
    </div>

    <div class="card">
        <h2>Hotel Navigation </h2>

        <h3>Staff</h3>
        <asp:Button ID="StaffLoginButton" CssClass="navBtn" runat="server" Text="Staff Login" OnClick="StaffLogin_Click"/>
        <asp:Button ID="StaffPageButton" CssClass="navBtn" runat="server" Text="Staff Dashboard" OnClick="StaffDashboard_Click"/>

        <h3>Members</h3>
        <asp:Button ID="MemberLoginButton" CssClass="navBtn" runat="server" Text="Login Member" OnClick="MemberLogin_Click" />
        <asp:Button ID="MemberRegisterButton" CssClass="navBtn" runat="server" Text="Register Member" OnClick="MemberRegister_Click"/>
        <asp:Button ID="MemberRatingButton" CssClass="navBtn" runat="server" Text="Rate Hotels" OnClick="MemberRating_Click" />
        <asp:Button ID="MemberBrowseButton" CssClass="navBtn" runat="server" Text="Browse / Book Hotels" OnClick="MemberBrowse_Click"/>
    </div>

    <div class="card">
        <h2>Application and Components Summary Table (Assignment 5 & 6)</h2>

        <asp:Table ID="ComponentsTable" runat="server" GridLines="Both" BorderWidth="1" CellPadding="8">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" Font-Bold="true">
                    Application and Components Summary Table
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                    Percentage of overall contribution: Luan Nguyen (33%), Sophia Gu (33%), 
                    Muhammed Hunaid Topiwala (33%)
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow Font-Bold="true">
                <asp:TableCell>Provider Name</asp:TableCell>
                <asp:TableCell>Page / Component Type</asp:TableCell>
                <asp:TableCell>Description</asp:TableCell>
                <asp:TableCell>Resources, Methods, & Link</asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Sophia Gu</asp:TableCell>
                <asp:TableCell>.aspx page & server controls</asp:TableCell>
                <asp:TableCell>Public Default page linking all pages</asp:TableCell>
                <asp:TableCell>GUI design + C# code<br />Link: Current Page</asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Sophia Gu</asp:TableCell>
                <asp:TableCell>User Control (.ascx)</asp:TableCell>
                <asp:TableCell>Login Window for Staff & Member Login</asp:TableCell>
                <asp:TableCell>
                    Staff Login: <asp:HyperLink runat="server" NavigateUrl="~/StaffLogin.aspx">Staff Login</asp:HyperLink><br />
                    Member Login: <asp:HyperLink runat="server" NavigateUrl="~/MemberLogin.aspx">Member Login</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Sophia Gu</asp:TableCell>
                <asp:TableCell>Cookies</asp:TableCell>
                <asp:TableCell>Stores username/password for login</asp:TableCell>
                <asp:TableCell>
                    Staff Login: <asp:HyperLink runat="server" NavigateUrl="~/StaffLogin.aspx">Staff Login</asp:HyperLink><br />
                    Member Login: <asp:HyperLink runat="server" NavigateUrl="~/MemberLogin.aspx">Member Login</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Sophia Gu</asp:TableCell>
                <asp:TableCell>WSDL Service</asp:TableCell>
                <asp:TableCell>Fetch rated hotels by username</asp:TableCell>
                <asp:TableCell>
                    TryIt: <asp:HyperLink runat="server" NavigateUrl="~/MemberRating.aspx">Member Rating</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Sophia Gu</asp:TableCell>
                <asp:TableCell>WSDL Service</asp:TableCell>
                <asp:TableCell>User adds a hotel rating</asp:TableCell>
                <asp:TableCell>
                    TryIt: <asp:HyperLink runat="server" NavigateUrl="~/MemberRating.aspx">Member Rating</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Sophia Gu</asp:TableCell>
                <asp:TableCell>WSDL Service</asp:TableCell>
                <asp:TableCell>Members browse staff-posted hotels</asp:TableCell>
                <asp:TableCell>
                    TryIt: <asp:HyperLink runat="server" NavigateUrl="~/MemberBrowse.aspx">Member Browse</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Sophia Gu</asp:TableCell>
                <asp:TableCell>Member Registration</asp:TableCell>
                <asp:TableCell>Create new member with hashed password + CAPTCHA</asp:TableCell>
                <asp:TableCell>
                    <asp:HyperLink runat="server" NavigateUrl="~/MemberRegister.aspx">Member Register</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Sophia Gu</asp:TableCell>
                <asp:TableCell>Forms Authentication</asp:TableCell>
                <asp:TableCell>Protects Member pages & Staff dashboard</asp:TableCell>
                <asp:TableCell>
                    Web.config modifications<br />
                    StaffLogin + MemberLogin pages
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Luan Nguyen</asp:TableCell>
                <asp:TableCell>DLL (SecurityLib)</asp:TableCell>
                <asp:TableCell>SHA-256 hashing function</asp:TableCell>
                <asp:TableCell>Used in Test Hash button shown in "Luan's Components Demo" section</asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Luan Nguyen</asp:TableCell>
                <asp:TableCell>User Control (.ascx)</asp:TableCell>
                <asp:TableCell>Discount calculator UI</asp:TableCell>
                <asp:TableCell>Shown in "Luan's Components Demo" section</asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Luan Nguyen</asp:TableCell>
                <asp:TableCell>.asmx Web Service</asp:TableCell>
                <asp:TableCell>Discount calculation endpoint</asp:TableCell>
                <asp:TableCell>
                    <asp:HyperLink ID="TryDiscountServiceRow" runat="server" NavigateUrl="~/DiscountService.asmx">
                        DiscountService.asmx
                    </asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Luan Nguyen</asp:TableCell>
                <asp:TableCell>Staff Dashboard (.aspx Page)</asp:TableCell>
                <asp:TableCell>Allows staff to book rooms, apply discounts, and update Hotels.xml</asp:TableCell>
                <asp:TableCell>
                    <asp:HyperLink runat="server" NavigateUrl="~/ProtectedStaff/StaffDashboard.aspx">
                        Staff Dashboard
                    </asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>

        </asp:Table>
    </div>

    <div class="card">
        <h2 style="text-align:center;">Luan's Components Demo</h2>

        <table style="width:100%; text-align:center;">
            <tr>
                <td style="padding:10px; width:33%;"><uc:Discount ID="Discount1" runat="server" /></td>
            </tr>
        </table>

        <div style="text-align:center; margin-top:20px;">
            <asp:Button ID="btnTryHash" CssClass="navBtn" runat="server" Text="Test Hash" OnClick="btnTryHash_Click" /><br /><br />
            <asp:Label ID="lblHashResult" runat="server" /><br /><br />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/DiscountService.asmx">
                Try Discount Web Service
            </asp:HyperLink>
        </div>
    </div>

</div> 
</form>
</body>
</html>
