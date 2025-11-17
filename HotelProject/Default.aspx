<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HotelProject.Default" %>
<%@ Register Src="~/LoginControl.ascx" TagPrefix="uc" TagName="Login" %>
<%@ Register Src="~/DiscountControl.ascx" TagPrefix="uc" TagName="Discount" %>
<%@ Register Src="~/AgentProfile.ascx" TagPrefix="uc" TagName="AgentProfile" %>
<%@ Register Src="~/MemberProfile.ascx" TagPrefix="uc" TagName="MemberProfile" %>


<!--Default page linking all services made by Sophia-->
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Title" runat="server" Text="Hotel Application" Font-Bold="true" Font-Size="large"></asp:Label>
            <br />
            <asp:Label ID="Names" runat="server" Text="Implemented by: Luan Nguyen, Sophia Gu, Muhammed Topiwala" Font-Italic="true"></asp:Label>
            <br />
            <asp:Label ID="Description" runat="server" Text="This service allows members to sign in, book hotel rooms, and rate those hotels. 
                Staff can use the application to receive notifications about discounted hotel prices, and book a batch of hotel rooms. 
                They will then offer those discounted rooms to the members through the application. New users can create an account
                to become a member."></asp:Label>
            <br /><br />
            <asp:Table ID="ComponentsTable" runat="server" GridLines="Both" BorderWidth="1">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" Font-Bold="true">
                        Application and Components Summary Table
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                        Percentage of overall contribution: Luan Nguyen: 33%, Sophia Gu: 33%, 
                        Muhammed Hunaid Topiwala: 33%
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Provider Name</asp:TableCell>
                    <asp:TableCell>Page and Component type</asp:TableCell>
                    <asp:TableCell>Component Description (What does the component do?)</asp:TableCell>
                    <asp:TableCell> Actual resources and methods used, link to component (where is it used)</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Sophia Gu</asp:TableCell>
                    <asp:TableCell>.aspx page and server controls</asp:TableCell>
                    <asp:TableCell>The public Default page that calls and links other pages</asp:TableCell>
                    <asp:TableCell>GUI Design and C# code <br />
                                   Link: Current Page
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Sophia Gu</asp:TableCell>
                    <asp:TableCell>.ascx page and user controls</asp:TableCell>
                    <asp:TableCell>The Login User control component used for Staff & Member Login page</asp:TableCell>
                    <asp:TableCell>C# Code behind the GUI. Present on the Staff Login Page and Member Login Page<br />
                                   Staff Login Page: <asp:HyperLink ID="StaffLogin" runat="server" NavigateUrl="~/StaffLogin.aspx">Staff Login</asp:HyperLink><br />
                                   Member Login Page: <asp:HyperLink ID="MemberLoginLink" runat="server" NavigateUrl="~/MemberLogin.aspx">Member Login</asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Sophia Gu</asp:TableCell>
                    <asp:TableCell>Cookies</asp:TableCell>
                    <asp:TableCell>Stores username and password</asp:TableCell>
                    <asp:TableCell>GUI design and C# Code behind GUI using HTTP cookies library. Present in Staff & Member Login Page <br />
                                   Staff Login Page: <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/StaffLogin.aspx">Staff Login</asp:HyperLink><br />
                                   Member Login Page: <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/MemberLogin.aspx">Member Login</asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Sophia Gu</asp:TableCell>
                    <asp:TableCell>WSDL Service (fetch rated hotels by username)</asp:TableCell>
                    <asp:TableCell>Operation Name: GetRatedHotels
                        <br />     Input: Username (string)
                        <br />     Output: All hotels that the member rated
                    </asp:TableCell>
                    <asp:TableCell>GUI design, C# code behind the GUI, Members.xml & Hotels.xml file, C# code for WSDL service
                        <br />     Member Rating TryIt Page: <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/MemberRating.aspx">Member Rating Page</asp:HyperLink>
                        <br />     Service Deployed Here: <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="http://webstrar8.fulton.asu.edu/page0/Service1.svc">Access Service</asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Sophia Gu</asp:TableCell>
                    <asp:TableCell>WSDL Service (adds a rating for a hotel)</asp:TableCell>
                    <asp:TableCell>Operation Name: AddHotelRating
                        <br />     Input: Username (string), HotelID (string), rating (float), comment (string)
                        <br />     Output: bool
                    </asp:TableCell>
                    <asp:TableCell>GUI design, C# code behind the GUI, Members.xml & Hotels.xml file, C# code for WSDL service
                        <br />     Member Rating Page TryIt: <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/MemberRating.aspx">Member Rating Page</asp:HyperLink>
                        <br />     Service Deployed Here: <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="http://webstrar8.fulton.asu.edu/page0/Service1.svc">Access Service</asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Sophia Gu</asp:TableCell>
                    <asp:TableCell>WSDL Service (verifies Member Login)</asp:TableCell>
                    <asp:TableCell>Operation Name: LoginMember
                        <br />     Input: username(string), password(string)
                        <br />     Output: bool
                    </asp:TableCell>
                    <asp:TableCell>C# code for the WSDL service, with the XDocument class to verify login information
                        <br />     Member Login Page: <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/MemberLogin.aspx">Member Login</asp:HyperLink>
                        <br />     Service Deployed Here: <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="http://webstrar8.fulton.asu.edu/page0/Service1.svc">Access Service</asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Sophia Gu</asp:TableCell>
                    <asp:TableCell>WSDL Service (verifies Staff Login)</asp:TableCell>
                    <asp:TableCell>Operation Name: LoginStaff
                        <br />     Input: username(string), password(string)
                        <br />     Output: bool
                    </asp:TableCell>
                    <asp:TableCell>C# code for WSDL service, with the XDocument class to verify login information
                        <br />     Staff Login Page: <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/StaffLogin.aspx">Staff Login</asp:HyperLink>
                        <br />     Service Deployed Here: <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="http://webstrar8.fulton.asu.edu/page0/Service1.svc">Access Service</asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>Luan Nguyen</asp:TableCell>
                    <asp:TableCell>DLL (SecurityLib)</asp:TableCell>
                    <asp:TableCell>Contains SHA-256 hashing function for credential security</asp:TableCell>
                    <asp:TableCell>
                        Used in: Test Hash Button<br />
                        Link: This Page
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>Luan Nguyen</asp:TableCell>
                    <asp:TableCell>.ascx User Control</asp:TableCell>
                    <asp:TableCell>Calculates a discounted hotel price</asp:TableCell>
                    <asp:TableCell>
                        Used in: Luan’s Components Demo<br />
                        Control: &lt;uc:Discount /&gt;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>Luan Nguyen</asp:TableCell>
                    <asp:TableCell>.asmx Web Service</asp:TableCell>
                    <asp:TableCell>Provides discount calculation service endpoint</asp:TableCell>
                    <asp:TableCell>
                        TryIt Link:<br />
                        <asp:HyperLink ID="TryDiscountServiceRow" runat="server" NavigateUrl="~/DiscountService.asmx">DiscountService.asmx</asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>     
            
            <br /><br />

            <!-- Luan's services-->

            <div style="margin-top:30px; padding:20px; border:1px solid #aaa; width:90%; max-width:800px;">
                <h2 style="text-align:center; font-family:Arial;">Luan's Components Demo</h2>
    
                <table style="width:100%; margin-top:20px;">
                    <tr>
                        <td style="vertical-align:top; padding:10px; width:33%;">
                            <uc:Discount ID="Discount1" runat="server" />
                        </td>

                        <td style="vertical-align:top; padding:10px; width:33%;">
                            <uc:AgentProfile ID="AgentProfile1" runat="server" />
                        </td>

                        <td style="vertical-align:top; padding:10px; width:33%;">
                            <uc:MemberProfile ID="MemberProfile1" runat="server" />
                        </td>
                    </tr>
                </table>

                <asp:Button ID="btnTryHash" runat="server" Text="Test Hash" OnClick="btnTryHash_Click" />
                <asp:Label ID="lblHashResult" runat="server" />

                <asp:HyperLink ID="TryDiscountService" runat="server" NavigateUrl="~/DiscountService.asmx">Try Discount Service</asp:HyperLink>
            </div>
        </div>
    </form>
</body>
</html>
