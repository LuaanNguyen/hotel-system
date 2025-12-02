<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentBookingPage.aspx.cs" Inherits="HotelProject.AgentBookingPage" %>
<%@ Register Src="~/AgentBooking.ascx" TagPrefix="uc" TagName="AgentBooking" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agent Booking - Hotel Management System</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 20px;
        }
        .container {
            max-width: 800px;
            margin: 0 auto;
            background-color: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0,0,0,0.1);
        }
        .header {
            text-align: center;
            margin-bottom: 30px;
            color: #4CAF50;
        }
        .navigation {
            text-align: center;
            margin-top: 20px;
            padding-top: 20px;
            border-top: 2px solid #4CAF50;
        }
        .nav-link {
            margin: 0 10px;
            color: #4CAF50;
            text-decoration: none;
            font-weight: bold;
        }
        .nav-link:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h1>Agent Batch Booking System</h1>
                <p>Book multiple hotel rooms with exclusive agent discounts</p>
            </div>
            
            <uc:AgentBooking ID="AgentBooking1" runat="server" />
            
            <div class="navigation">
                <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Default.aspx" CssClass="nav-link">
                    ? Back to Home
                </asp:HyperLink>
                <asp:HyperLink ID="lnkStaffLogin" runat="server" NavigateUrl="~/StaffLogin.aspx" CssClass="nav-link">
                    Staff Login
                </asp:HyperLink>
            </div>
        </div>
    </form>
</body>
</html>
