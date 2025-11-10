<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginControl.ascx.cs" Inherits="HotelProject.LoginControl" %>


<div>
    <asp:Label ID="LoginTitle" runat="server" Text="Login User Control" Font-Bold="true"></asp:Label>
    <br />

    <asp:Label ID="UsernameLabel" runat="server" Text="Username:"></asp:Label>
    <asp:TextBox ID="UsernameTextbox" runat="server"></asp:TextBox>
    <br />

    <asp:Label ID="PasswordLabel" runat="server" Text="Password:"></asp:Label>
    <asp:TextBox ID="PasswordTextbox" runat="server"></asp:TextBox>
    <br />

    <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="btn_LoginClick" />
    <asp:Label ID="Result" runat="server" Text="Result:"></asp:Label>
    
</div>