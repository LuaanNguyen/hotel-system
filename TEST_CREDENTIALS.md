# ?? Test Credentials

## Default Users (Check XML files for current data)

### Staff Users
Located in: `WcfHotelService\App_Data\Staff.xml`

**Example Staff Login:**
- Username: `staff1`
- Password: Check Staff.xml file
- Role: Staff/Agent

### Member Users  
Located in: `WcfHotelService\App_Data\Members.xml`

**Example Member Login:**
- Username: `member1`
- Password: Check Members.xml file
- Role: Member
- Balance: Check Members.xml file

### Test Member (Create via Registration)
- Username: `testuser`
- Password: `Test123!`
- Initial Balance: `1000.00`

---

## ?? Test Hotels
Located in: `WcfHotelService\App_Data\Hotels.xml`

Hotels available for testing:
- Check Hotels.xml for current listings
- Note Hotel IDs for booking tests
- Check available room counts

---

## ?? Quick Test Scenarios

### Scenario 1: New Member Journey
1. Register as `testuser`
2. Login with credentials
3. Browse available hotels
4. Book a room (balance will be deducted)
5. Rate a hotel
6. Change password

### Scenario 2: Staff Operations
1. Login as staff
2. View Staff Dashboard
3. Post discount notifications
4. Book batch of rooms for members
5. Update hotel pricing

### Scenario 3: Component Verification
1. Visit Default.aspx
2. Verify component summary table
3. Test AgentBooking control
4. Test MemberBrowsing control  
5. Check Global.asax tracking (dev tools console)

---

## ?? Important URLs (Local)

```
Main Page:        http://localhost:[port]/Default.aspx
Staff Login:      http://localhost:[port]/StaffLogin.aspx
Member Login:     http://localhost:[port]/MemberLogin.aspx
Registration:     http://localhost:[port]/MemberRegister.aspx
Staff Dashboard:  http://localhost:[port]/ProtectedStaff/StaffDashboard.aspx
Member Browse:    http://localhost:[port]/ProtectedMember/MemberBrowse.aspx
Member Rating:    http://localhost:[port]/ProtectedMember/MemberRating.aspx
Change Password:  http://localhost:[port]/ProtectedMember/MemberChangePassword.aspx
Agent Booking:    http://localhost:[port]/AgentBookingPage.aspx

WCF Service:      http://localhost:[servicePort]/Service1.svc
```

Replace [port] with your actual port numbers shown when running the applications.

---

## ?? Troubleshooting Quick Reference

**Problem**: WCF Service not found
**Solution**: Check service is running in separate browser window

**Problem**: Authentication not working
**Solution**: Verify Forms Authentication in Web.config

**Problem**: Protected pages not accessible
**Solution**: Login first, check Web.config in protected folders

**Problem**: Balance not updating
**Solution**: Check Members.xml has write permissions

**Problem**: Component table not showing
**Solution**: Check Default.aspx for ComponentsTable control

---

*Generated for Assignment 5 Testing*
*Branch: muhammed/assignment5*
