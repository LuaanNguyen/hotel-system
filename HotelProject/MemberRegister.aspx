<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberRegister.aspx.cs" Inherits="HotelProject.MemberRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Registration/Sign-up page for new members</h2>

            <asp:Label ID="UsernameLabel" runat="server" Text="Enter Preferred Username: "></asp:Label>
            <asp:TextBox ID="UsernameTextbox" runat="server"></asp:TextBox><br />

            <asp:Label ID="PasswordLabel" runat="server" Text="Enter Password: "></asp:Label>
            <asp:TextBox ID="PasswordTextbox" runat="server"></asp:TextBox><br />

            <asp:Label ID="PasswordConfirmLabel" runat="server" Text="Confirm Password: "></asp:Label>
            <asp:TextBox ID="PasswordConfirmTextbox" runat="server"></asp:TextBox><br />

            <asp:Label ID="BalanceLabel" runat="server" Text="Enter in initial balance: "></asp:Label>
            <asp:TextBox ID="BalanceTextbox" runat="server"></asp:TextBox><br />
            <asp:Button ID="RegisterButton" runat="server" Text="Register" OnClick="RegisterButton_Click" /><br />


            <asp:Label ID="ResultLabel" runat="server" Text="Result: "></asp:Label>


        </div>
    </form>
</body>
</html>
