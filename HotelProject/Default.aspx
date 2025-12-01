<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HotelProject.Default" %>
<%@ Register Src="~/LoginControl.ascx" TagPrefix="uc" TagName="Login" %>
<%@ Register Src="~/DiscountControl.ascx" TagPrefix="uc" TagName="Discount" %>
<%@ Register Src="~/AgentNotification.ascx" TagPrefix="uc" TagName="AgentNotification" %>
<%@ Register Src="~/AgentBooking.ascx" TagPrefix="uc" TagName="AgentBooking" %>
<%@ Register Src="~/MemberBrowsing.ascx" TagPrefix="uc" TagName="MemberBrowsing" %>

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
        table { width:100%; border-collapse: collapse; }
        table td, table th { padding: 8px; border: 1px solid #ddd; }
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

    <div class="card" style="text-align:center;">
        <h3>Navigation</h3>
        <asp:Button ID="StaffLoginButton" CssClass="navBtn" runat="server" Text="Staff Login" OnClick="StaffLogin_Click"/>
        <asp:Button ID="StaffPageButton" CssClass="navBtn" runat="server" Text="Staff Dashboard" OnClick="StaffDashboard_Click"/>
        <asp:Button ID="MemberLoginButton" CssClass="navBtn" runat="server" Text="Login Member" OnClick="MemberLogin_Click" />
        <asp:Button ID="MemberRegisterButton" CssClass="navBtn" runat="server" Text="Register Member" OnClick="MemberRegister_Click"/>
        <asp:Button ID="MemberRatingButton" CssClass="navBtn" runat="server" Text="Rate Hotels" OnClick="MemberRating_Click" />
        <asp:Button ID="MemberBrowseButton" CssClass="navBtn" runat="server" Text="Browse / Book Hotels" OnClick="MemberBrowse_Click"/>
        <asp:Button ID="MemberPasswordButton" CssClass="navBtn" runat="server" Text="Change Password" OnClick="MemberPassword_Click"/>
            <asp:Button ID="btnTryHash" CssClass="navBtn" runat="server" Text="Test Hash" OnClick="btnTryHash_Click" /><br /><br />
    </div>

    <div class="card">
        <h3>Application and Components Summary Table</h3>
        <asp:Table ID="ComponentsTable" runat="server" GridLines="Both" BorderWidth="1">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                    <asp:Label ID="TableTitle" runat="server" Text="Application and Components Summary Table" Font-Bold="true"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                    <asp:Label ID="TableSubtitle" runat="server" 
                        Text="CSE 445 - Distributed Software Development - Professor Yinong Chen" Font-Italic="true"></asp:Label>
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
                    <asp:HyperLink ID="DefaultLink" runat="server" NavigateUrl="~/Default.aspx">Default.aspx</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Sophia Gu</asp:TableCell>
                <asp:TableCell>.ascx page and user controls</asp:TableCell>
                <asp:TableCell>The Login User control component used for Staff & Member Login page</asp:TableCell>
                <asp:TableCell>C# Code behind the GUI. Present on the Staff Login Page and Member Login Page<br />
                    <asp:HyperLink ID="MemberLoginLink" runat="server" NavigateUrl="~/MemberLogin.aspx">MemberLogin.aspx</asp:HyperLink><br />
                    <asp:HyperLink ID="StaffLoginLink" runat="server" NavigateUrl="~/StaffLogin.aspx">StaffLogin.aspx</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Sophia Gu</asp:TableCell>
                <asp:TableCell>Cookies</asp:TableCell>
                <asp:TableCell>Stores username and password</asp:TableCell>
                <asp:TableCell>GUI design and C# Code behind GUI using HTTP cookies library. Present in Staff & Member Login Page <br />
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/MemberLogin.aspx">MemberLogin.aspx</asp:HyperLink><br />
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/StaffLogin.aspx">StaffLogin.aspx</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Sophia Gu</asp:TableCell>
                <asp:TableCell>WSDL Service (fetch rated hotels by username)</asp:TableCell>
                <asp:TableCell>Input: Username<br />
                    Output: list of hotels rated by the user</asp:TableCell>
                <asp:TableCell>GUI design, C# code behind the GUI, Members.xml & Hotels.xml file, C# code for WSDL service<br />
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/TryItRating.aspx">TryItRating.aspx</asp:HyperLink></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Sophia Gu</asp:TableCell>
                <asp:TableCell>WSDL Service (user adds a rating for a hotel)</asp:TableCell>
                <asp:TableCell>Input: Username<br />
                    Output: the rating is added to the system</asp:TableCell>
                <asp:TableCell>GUI design, C# code behind the GUI, Members.xml & Hotels.xml file, C# code for WSDL service<br />
                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/TryItRating.aspx">TryItRating.aspx</asp:HyperLink></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Luan Nguyen</asp:TableCell>
                <asp:TableCell>.ascx page and user controls</asp:TableCell>
                <asp:TableCell>The Discount User control component used for checking discount</asp:TableCell>
                <asp:TableCell>C# code behind the GUI. Present on the Staff Login Page<br />
                    <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/StaffLogin.aspx">StaffLogin.aspx</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Luan Nguyen</asp:TableCell>
                <asp:TableCell>WSDL Service (verifies staff credentials)</asp:TableCell>
                <asp:TableCell>Input: Username & Password<br />
                    Output: verified credentials</asp:TableCell>
                <asp:TableCell>GUI design, C# code behind the GUI, Staff.xml file, C# code for WSDL service<br />
                    <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/StaffLogin.aspx">StaffLogin.aspx</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Luan Nguyen</asp:TableCell>
                <asp:TableCell>WSDL Service (verifies member credentials)</asp:TableCell>
                <asp:TableCell>Input: Username & Password<br />
                    Output: verified credentials</asp:TableCell>
                <asp:TableCell>GUI design, C# code behind the GUI, Members.xml file, C# code for WSDL service<br />
                    <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/MemberLogin.aspx">MemberLogin.aspx</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Luan Nguyen</asp:TableCell>
                <asp:TableCell>WSDL Service (fetch available hotels)</asp:TableCell>
                <asp:TableCell>Input: None<br />
                    Output: list of available hotels</asp:TableCell>
                <asp:TableCell>GUI design, C# code behind the GUI, Hotels.xml file, C# code for WSDL service<br />
                    <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/ProtectedMember/MemberBrowse.aspx">MemberBrowse.aspx</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Luan Nguyen</asp:TableCell>
                <asp:TableCell>WSDL Service (registers new members)</asp:TableCell>
                <asp:TableCell>Input: Username, Password, Email<br />
                    Output: new member added to system</asp:TableCell>
                <asp:TableCell>GUI design, C# code behind the GUI, Members.xml file, C# code for WSDL service<br />
                    <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/MemberRegister.aspx">MemberRegister.aspx</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Luan Nguyen</asp:TableCell>
                <asp:TableCell>WSDL Service (changes member password)</asp:TableCell>
                <asp:TableCell>Input: Username, Old Password, New Password<br />
                    Output: password updated in system</asp:TableCell>
                <asp:TableCell>GUI design, C# code behind the GUI, Members.xml file, C# code for WSDL service<br />
                    <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/ProtectedMember/MemberChangePassword.aspx">MemberChangePassword.aspx</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Luan Nguyen</asp:TableCell>
                <asp:TableCell>WSDL Service (books hotel rooms)</asp:TableCell>
                <asp:TableCell>Input: Hotel ID, Member Username, Number of Rooms<br />
                    Output: booking confirmation</asp:TableCell>
                <asp:TableCell>GUI design, C# code behind the GUI, Hotels.xml & Members.xml files, C# code for WSDL service<br />
                    <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/ProtectedMember/MemberBrowse.aspx">MemberBrowse.aspx</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Muhammed Topiwala</asp:TableCell>
                <asp:TableCell>Global.asax and Application State</asp:TableCell>
                <asp:TableCell>Tracks application-wide statistics including total visitors, active sessions, and application lifecycle events</asp:TableCell>
                <asp:TableCell>Global.asax.cs file with Application and Session event handlers. Used throughout the application for session management<br />
                    Implements: Application_Start, Session_Start, Session_End, Application_Error, Application_End, Application_AuthenticateRequest
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Muhammed Topiwala</asp:TableCell>
                <asp:TableCell>.ascx user control (AgentNotification)</asp:TableCell>
                <asp:TableCell>Displays hotel discount notifications for agents/staff. Shows available discount opportunities with hotel details</asp:TableCell>
                <asp:TableCell>C# code behind GUI, consumes discount service<br />
                    <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/AgentNotification.ascx">AgentNotification.ascx</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Muhammed Topiwala</asp:TableCell>
                <asp:TableCell>.ascx user control (AgentBooking)</asp:TableCell>
                <asp:TableCell>Allows agents to book multiple hotel rooms in batch. Provides interface for bulk booking operations</asp:TableCell>
                <asp:TableCell>C# code behind GUI, integrates with hotel booking service<br />
                    <asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="~/AgentBookingPage.aspx">AgentBookingPage.aspx</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Muhammed Topiwala</asp:TableCell>
                <asp:TableCell>.ascx user control (MemberBrowsing)</asp:TableCell>
                <asp:TableCell>Enhanced hotel browsing interface for members. Displays hotel listings with filtering and sorting capabilities</asp:TableCell>
                <asp:TableCell>C# code behind GUI, consumes hotel listing service<br />
                    <asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="~/MemberBrowsing.ascx">MemberBrowsing.ascx</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Muhammed Topiwala</asp:TableCell>
                <asp:TableCell>.aspx page (AgentBookingPage)</asp:TableCell>
                <asp:TableCell>Host page for AgentBooking user control. Provides dedicated interface for agent booking operations</asp:TableCell>
                <asp:TableCell>GUI design and integration of AgentBooking control<br />
                    <asp:HyperLink ID="HyperLink15" runat="server" NavigateUrl="~/AgentBookingPage.aspx">AgentBookingPage.aspx</asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>

    <div class="card">
        <h3>Test Components</h3>
        <h4>Login Control</h4>
        <uc:Login ID="LoginControl1" runat="server" />
        <br />
        <h4>Discount Control</h4>
        <uc:Discount ID="DiscountControl1" runat="server" />
    </div>

</div>
</form>
</body>
</html>
