﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberBrowse.aspx.cs" Inherits="HotelProject.MemberBrowse" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Browse Hotels</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Browse And Book Hotels</h2>

            <asp:Label ID="WelcomeLabel" runat="server" Text="Welcome, "></asp:Label><br /><br />

            <asp:Button ID="DefaultButton" runat="server" Text="Default Page" OnClick="DefaultButton_Click" />
            <asp:Button ID="RateButton" runat="server" Text="Rate Hotels" OnClick="RateButton_Click" />
            <asp:Button ID="LogOut" runat="server" Text="Log Out" OnClick="LogOut_Click" />
            <asp:Button ID="ChangePasswordButton" runat="server" Text="Change Password" OnClick="ChangePassword_Click" />
            <br /><br />

            <asp:Label ID="AddMoneyTitle" runat="server" Text="View balance/add funds" Font-Bold="true"></asp:Label><br />
            <asp:Label ID="CurrBalanceLabel" runat="server" Text="Current Balance: "></asp:Label><br />
            <asp:Label ID="AddMoneyLabel" runat="server" Text="Add Money: "></asp:Label>
            <asp:TextBox ID="AddMoneyTextBox" runat="server"></asp:TextBox>
            <asp:Button ID="AddMoneyButton" runat="server" Text="Add" OnClick="AddMoney_Click" /><br />
            <asp:Label ID="ErrorLabel" runat="server" Text="Error: "></asp:Label>
            <br /><br />

            <asp:Label ID="BrowseHotelTitle" runat="server" Text="Browse Hotels" Font-Bold="true"></asp:Label><br />
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
                    <div>The agent has not posted any hotels. Check back again later.</div>
                </EmptyDataTemplate>
            </asp:ListView>

            <asp:Label ID="HotelDetailLabel" runat="server" Text="Hotel Details"></asp:Label>
            <br />
            <asp:TextBox ID="HotelDetailTextBox" runat="server"
                TextMode="MultiLine"
                ReadOnly="true"
                Rows="20"
                Width="600px"></asp:TextBox>

            <br />
            <asp:Label ID="BookHotelsInstruction" runat="server" Text="(Hint: Select a hotel from the list view above, select the dates, and then click on the Book Hotel button)"></asp:Label><br />
            <div class="form-group">
                <label>Start Date:</label>
                <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label>End Date:</label>
                <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" CssClass="form-control" />
            </div>
            <asp:Button ID="BookHotelButton" runat="server" Text="Book Selected Hotel" OnClick="BookHotel_Click" /><br />
            <asp:Label ID="BookHotelResult" runat="server" Text="Book Hotel Result: "></asp:Label><br /><br />

            <asp:Label ID="BookedHotelsLabel" runat="server" Text="Booked Hotels" Font-Bold="true"></asp:Label><br />
            <asp:TextBox ID="BookedHotelsTextBox" runat="server"
                TextMode="MultiLine"
                ReadOnly="true"
                Rows="20"
                Width="600px"></asp:TextBox>
        </div>
    </form>
</body>
</html>