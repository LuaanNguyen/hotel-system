<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffDashboard.aspx.cs" Inherits="HotelProject.Protected.StaffDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Staff Dashboard</h2><br/>
            <asp:Label ID="DescLabel" runat="server" Text="This is a protected page for authenticated Staff members only."></asp:Label>
        </div>
    </form>
</body>
</html>
