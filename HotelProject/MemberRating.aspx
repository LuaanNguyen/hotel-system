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
                Width="600px"></asp:TextBox><br /><br />

            <asp:Label ID="AddRating" runat="server" Text="TryIt: Rate a hotel, given a username" Font-Bold="true"></asp:Label><br />
            <asp:Label ID="AddRatingDesc" runat="server" Text="Given a username, hotelID, comment, and score, this service will add a rating
                           for that corresponding hotel and username. To see that the hotel was successfully added, please use the above TryIt
                           service." Font-Italic="true"></asp:Label><br />
            
            <asp:Label ID="Label1" runat="server" Text="Valid usernames: SophiaGu, LuanNguyen, MoTopiwala"></asp:Label><br />
            <asp:Label ID="UsernameLabel1" runat="server" Text="Enter Username:"></asp:Label>
            <asp:TextBox ID="UsernameTextbox2" runat="server"></asp:TextBox><br /><br />

            <asp:Label ID="Label2" runat="server" Text="Valid Hotel ID's range from 1 to 10 (inclusive)"></asp:Label><br />
            <asp:Label ID="HotelLabel" runat="server" Text="Enter Hotel ID:"></asp:Label>
            <asp:TextBox ID="HotelIDTextbox" runat="server"></asp:TextBox><br /><br />

            <asp:Label ID="Label3" runat="server" Text="Valid ratings are floats between 0 and 5 (inclusive)"></asp:Label><br />
            <asp:Label ID="RatingLabel" runat="server" Text="Enter Rating: "></asp:Label>
            <asp:TextBox ID="RatingTextbox" runat="server"></asp:TextBox><br /><br />

            <asp:Label ID="CommentLabel" runat="server" Text="Enter Comment: "></asp:Label>
            <asp:TextBox ID="CommentTextbox" runat="server"></asp:TextBox><br /><br />

            <asp:Button ID="AddRatingButton" runat="server" Text="Add Rating" OnClick="AddRating_Click"/><br />
            <asp:Label ID="ResultLabel2" runat="server" Text="Result: "></asp:Label>

        </div>
    </form>
</body>
</html>
