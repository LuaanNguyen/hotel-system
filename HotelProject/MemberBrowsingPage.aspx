<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberBrowsingPage.aspx.cs" Inherits="HotelProject.MemberBrowsingPage" %>
<%@ Register Src="~/MemberBrowsing.ascx" TagPrefix="uc" TagName="MemberBrowsing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Browse Hotels - Hotel Management System</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 20px;
        }
        .container {
            max-width: 900px;
            margin: 0 auto;
            background-color: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0,0,0,0.1);
        }
        .header {
            text-align: center;
            margin-bottom: 30px;
            color: #2196F3;
        }
        .navigation {
            text-align: center;
            margin-top: 20px;
            padding-top: 20px;
            border-top: 2px solid #2196F3;
        }
        .nav-link {
            margin: 0 10px;
            color: #2196F3;
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
                <h1>Browse Hotels</h1>
                <p>Find and explore available hotels for your next stay</p>
            </div>
            
            <uc:MemberBrowsing ID="MemberBrowsing1" runat="server" />
            
            <div class="navigation">
                <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Default.aspx" CssClass="nav-link">
                    ? Back to Home
                </asp:HyperLink>
                <asp:HyperLink ID="lnkMemberLogin" runat="server" NavigateUrl="~/MemberLogin.aspx" CssClass="nav-link">
                    Member Login
                </asp:HyperLink>
                <asp:HyperLink ID="lnkMemberRating" runat="server" NavigateUrl="~/MemberRating.aspx" CssClass="nav-link">
                    Rate Hotels
                </asp:HyperLink>
            </div>
        </div>
    </form>
</body>
</html>
