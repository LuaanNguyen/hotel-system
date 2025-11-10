<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginControl.ascx.cs" Inherits="HotelProject.LoginControl" %>

<div>

    <h3>Login Page</h3>
    <asp:Label ID="UsernameLabel" runat="server" Text="Username:"></asp:Label>
    <asp:TextBox ID="UsernameTextBox" runat="server"></asp:TextBox>
    <br />

    <asp:Label ID="PasswordLabel" runat="server" Text="Password:"></asp:Label>
    <asp:TextBox ID="PasswordTextBox" runat="server"></asp:TextBox>
    <br />

    <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="buttonLogin_Click" />
    <br />
    <asp:Label ID="Result" runat="server" ForeColor="Red">Result: </asp:Label>
    <asp:Label ID="CookieUsername" runat="server">Logged in username: </asp:Label>
    <asp:Label ID="CookiePassword" runat="server">Logged in password: </asp:Label>
</div>