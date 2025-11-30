
﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberRating.aspx.cs" Inherits="HotelProject.MemberRating" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Member Rating Page</h2>

            <asp:Label ID="WelcomeLabel" runat="server" Text="Welcome, "></asp:Label><br />
            
            <asp:Label ID="PastRatingSection" runat="server" Text="Past Ratings" Font-Bold="true"></asp:Label><br />
            <asp:TextBox ID="RatingTextBox" runat="server"
                TextMode="MultiLine"
                ReadOnly="true"
                Rows="25"
                Width="600px"></asp:TextBox><br /><br />

             <asp:Label ID="RateHotelTitle" runat="server" Text="Select and Rate a Hotel" Font-Bold="true"></asp:Label><br />
            <asp:Label ID="RateInstructions" runat="server" Text="Click a hotel below, fill out the fields, and click on the Rate Hotel button to rate a hotel" Font-Italic="true"></asp:Label><br />
            <asp:HiddenField ID="hiddenHotelId" runat="server" />
            <asp:ListView ID="HotelListView" runat="server" 
                OnSelectedIndexChanging="HotelListView_SelectedIndexChanging"
                OnSelectedIndexChanged="lvHotels_SelectedIndexChanged">
                <LayoutTemplate>
                    <table>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lblSelectHotel" runat="server"
                                CommandName="Select" Text='<%# Eval("Name") %>'></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <SelectedItemTemplate>
                    <tr style="background-color: cornflowerblue; color: white">
                        <td>
                            <asp:LinkButton ID="lblSelectHotel" runat="server"
                                CommandName="Select"
                                Text='<%# Eval("Name") %>'></asp:LinkButton>
                        </td>
                    </tr>
                </SelectedItemTemplate>
                <EmptyDataTemplate>
                    <div>The service has no hotels. Check back again later.</div>
                </EmptyDataTemplate>
            </asp:ListView>
            <br />

            <asp:Label ID="RatingLabel" runat="server" Text="Rating (decimal between 1 and 5):"></asp:Label>
            <asp:TextBox ID="RatingEnterTextBox" runat="server"></asp:TextBox><br />
            <asp:Label ID="CommentLabel" runat="server" Text="Comment: "></asp:Label>
            <asp:TextBox ID="CommentTextBox" runat="server"></asp:TextBox><br />
            <asp:Button ID="RateButton" runat="server" Text="Rate Hotel" OnClick="RateButton_Click"/><br />
            <asp:Label ID="ResultLabel" runat="server" Text="Result: "></asp:Label>

        </div>
    </form>
</body>
</html>
