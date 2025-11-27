<%--- @ Control Language="C#" AutoEventWireup="true" CodeBehind="AgentNotification.ascx.cs" Inherits="HotelProject.AgentNotification" %> ---%>

<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="AgentNotification.ascx.cs" 
    Inherits="HotelProject.AgentNotification" %>

<div style="border: 2px solid #2196F3; padding: 20px; border-radius: 8px; background-color: #f5f5f5; max-width: 500px;">
    <h3 style="color: #2196F3; margin-top: 0;">Agent Notification System</h3>
    
    <p style="font-size: 14px; color: #666;">
        <strong>Purpose:</strong> Displays real-time notifications for hotel staff agents about 
        discount opportunities and booking alerts.
    </p>

    <div style="background-color: white; padding: 15px; border-radius: 5px; margin: 15px 0;">
        <h4 style="margin-top: 0; color: #333;">Active Notifications:</h4>
        
        <!-- Notification List Display Area -->
        <asp:Panel ID="pnlNotifications" runat="server" 
                   style="max-height: 300px; overflow-y: auto; border: 1px solid #ddd; padding: 10px; border-radius: 4px;">
            <asp:Label ID="lblNotifications" runat="server" />
        </asp:Panel>
    </div>

    <!-- Refresh Button -->
    <asp:Button ID="btnRefresh" runat="server" 
                Text="🔄 Refresh Notifications" 
                OnClick="btnRefresh_Click"
                style="background-color: #2196F3; color: white; padding: 10px 20px; 
                       border: none; border-radius: 5px; cursor: pointer; font-size: 14px;" />
    
    <!-- Status Label -->
    <div style="margin-top: 15px;">
        <asp:Label ID="lblStatus" runat="server" 
                   style="font-size: 12px; color: #666; font-style: italic;" />
    </div>

    <!-- Information Box -->
    <div style="margin-top: 20px; padding: 10px; background-color: #E3F2FD; border-left: 4px solid #2196F3; border-radius: 4px;">
        <strong>📋 TryIt Instructions:</strong>
        <ul style="margin: 5px 0; padding-left: 20px; font-size: 13px;">
            <li>Click "Refresh Notifications" to load the latest alerts</li>
            <li>Notifications show discount opportunities for hotels</li>
            <li>Staff agents can use this to identify booking opportunities</li>
        </ul>
    </div>
</div>