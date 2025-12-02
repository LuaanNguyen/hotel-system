<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberChangePassword.aspx.cs" Inherits="HotelProject.ProtectedMember.MemberChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Change member password</h2>
            <asp:Label ID="WelcomeLabel" runat="server" Text="Welcome, "></asp:Label><br/><br />

            <asp:Label ID="NewPasswordLabel" runat="server" Text="Enter New Password: "></asp:Label>
            <asp:TextBox ID="NewPasswordTextBox" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="ConfirmPasswordLabel" runat="server" Text="Confirm Password: "></asp:Label>
            <asp:TextBox ID="ConfirmPasswordTextBox" runat="server"></asp:TextBox><br />

            <asp:Button ID="ChangePasswordBtn" runat="server" Text="Change Password" OnClick="ChangePassword_Click"/>
            <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" />
            <asp:Button ID="HomeButton" runat="server" Text="Return to Home" OnClick="HomeButton_Click" />
            <br />

            <asp:Label ID="ResultLabel" runat="server" Text="Result: "></asp:Label>
        </div>
    </form>
</body>
</html>
