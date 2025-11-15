<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberRating.aspx.cs" Inherits="HotelProject.MemberRating" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>Member Rating Page</h3><br />

            <asp:Label ID="Display" runat="server" Text="TryIt: Fetch Rated Hotels by Username" Font-Bold="true"></asp:Label><br />
            <asp:Label ID="DisplayDesc" runat="server" Text="Given a username, service displays all the hotels 
                           member has rated and corresponding details. Hint: valid usernames are SophiaGu, LuanNguyen, and MoTopiwala" Font-Italic="true"></asp:Label><br />

            <asp:Label ID="UsernameLabel" runat="server" Text="Enter Username:"></asp:Label>
            <asp:TextBox ID="UsernameTextbox" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Get Ratings" OnClick="GetRatings_Click"/><br />
            <asp:Label ID="ResultLabel1" runat="server" Text="Result: "></asp:Label><br />
            <asp:TextBox ID="RatingDisplay" runat="server"
                TextMode="MultiLine"
                ReadOnly="true"
                Rows="25"
                Width="600px"></asp:TextBox>
            
        </div>
    </form>
</body>
</html>
