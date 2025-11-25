﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberBrowse.aspx.cs" Inherits="HotelProject.MemberBrowse" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Browse Hotels</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>Browse Hotels</h3><br />
            
            <asp:Label ID="ListLabel" runat="server" Text="Select a hotel to browse:"></asp:Label><br />
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

            <br />
            <asp:Label ID="HotelDetailLabel" runat="server" Text="Hotel Details"></asp:Label>
            <br />
            <asp:TextBox ID="HotelDetailTextBox" runat="server"
                TextMode="MultiLine"
                ReadOnly="true"
                Rows="20"
                Width="600px"></asp:TextBox>
        </div>
    </form>
</body>
</html>