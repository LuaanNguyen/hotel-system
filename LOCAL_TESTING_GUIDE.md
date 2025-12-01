# ?? Local Testing Guide for Hotel Management System

## Overview
This guide will help you run and test the merged application locally before deploying to Webstrar.

---

## ? Pre-Testing Checklist

- [x] Build Status: **SUCCESSFUL**
- [x] Merge Status: **COMPLETE** (main ? muhammed/assignment5)
- [x] Forms Authentication: **CONFIGURED**
- [x] Assignment 5 Components: **PRESERVED**

---

## ?? Running the Application

### Method 1: Visual Studio (Recommended)

#### Step 1: Configure Multiple Startup Projects

1. In Visual Studio, right-click on the **solution** in Solution Explorer
2. Select **"Set Startup Projects..."**
3. Choose **"Multiple startup projects"**
4. Set the following:
   - **WcfHotelService**: Action = **Start**
   - **HotelProject**: Action = **Start**
   - **SecurityLib**: Action = **None**
5. Click **OK**

#### Step 2: Run the Application

1. Press **F5** (or click Start Debugging)
2. Both projects will start:
   - **WCF Service** will open in a browser showing the service page
   - **Web Application** will open to Default.aspx
3. Note the WCF Service URL (e.g., `http://localhost:64549/Service1.svc`)

---

### Method 2: Run Projects Separately

If Method 1 doesn't work:

#### A. Start WCF Service First:
1. Right-click **WcfHotelService** project
2. Select **"Set as Startup Project"**
3. Press **F5**
4. Browser opens showing WCF test page
5. **Note the URL** (keep this window open)

#### B. Then Start Web Application:
1. Right-click **HotelProject** project
2. Select **"Set as Startup Project"**  
3. Press **F5**
4. Browser opens to Default.aspx
5. Application should connect to WCF service

---

## ?? Testing Checklist

### Test 1: Default Page ?

**URL**: `http://localhost:xxxxx/Default.aspx`

- [ ] Page loads without errors
- [ ] Modern UI styling is visible (blue buttons, card layout)
- [ ] Navigation buttons are present:
  - Staff Login
  - Staff Dashboard
  - Login Member
  - Register Member
  - Rate Hotels
  - Browse / Book Hotels
  - Change Password
  - Test Hash
- [ ] **Component Summary Table** is displayed (YOUR CONTRIBUTION)
- [ ] Table shows all team members' components
- [ ] Test Components section shows LoginControl and DiscountControl

**Expected Output**: Clean page with professional styling and your complete component table

---

### Test 2: Authentication System ?

#### Staff Login
**URL**: Click "Staff Login" button

- [ ] Redirects to `/StaffLogin.aspx`
- [ ] Login form displays
- [ ] Try credentials:
  - Username: `staff1` (or check WcfHotelService/App_Data/Staff.xml)
  - Password: (from Staff.xml)
- [ ] Successful login redirects to Staff Dashboard
- [ ] Forms Authentication cookie is created

#### Member Login
**URL**: Click "Login Member" button

- [ ] Redirects to `/MemberLogin.aspx`
- [ ] Login form displays with cookie checkbox
- [ ] Try credentials:
  - Username: `member1` (or check WcfHotelService/App_Data/Members.xml)
  - Password: (from Members.xml)
- [ ] Successful login grants access to protected pages

**Expected Output**: Authentication redirects work, cookies are set, protected pages accessible

---

### Test 3: Member Registration ?

**URL**: Click "Register Member" button

- [ ] Redirects to `/MemberRegister.aspx`
- [ ] Registration form displays
- [ ] Fill in:
  - Username: `testuser`
  - Password: `Test123!`
  - Balance: `1000`
- [ ] Click Register
- [ ] Success message appears
- [ ] New member added to Members.xml

**Expected Output**: New member created successfully

---

### Test 4: Assignment 5 Components ?

#### Global.asax Session Tracking (YOUR COMPONENT)

**Test Method**: Check Application State
- [ ] Application starts (tracked in Global.asax)
- [ ] Session begins when you first visit
- [ ] Open browser developer tools ? Console
- [ ] Check for debug output showing visitors

**Expected Output**: Application tracks total visitors and active sessions

#### AgentNotification Component (YOUR COMPONENT)

**URL**: Navigate to a page that uses `AgentNotification.ascx`

- [ ] Component loads without errors
- [ ] Displays hotel discount notifications
- [ ] Shows hotel details and discount percentages

**Expected Output**: Discount notifications display correctly

#### AgentBooking Component (YOUR COMPONENT)

**URL**: `/AgentBookingPage.aspx`

- [ ] Page loads with AgentBooking control
- [ ] Form displays for bulk hotel booking
- [ ] Agent can enter:
  - Hotel ID
  - Number of rooms to book
  - Discount percentage
- [ ] Submit functionality works

**Expected Output**: Agent can book multiple rooms with discounts

#### MemberBrowsing Component (YOUR COMPONENT)

**URL**: Use a page that includes `MemberBrowsing.ascx`

- [ ] Component displays hotel listings
- [ ] Shows available hotels
- [ ] Filtering/sorting works (if implemented)
- [ ] Hotel details are visible

**Expected Output**: Member can browse hotels effectively

---

### Test 5: Main Branch Features ?

#### Browse/Book Hotels
**URL**: Click "Browse / Book Hotels" button

- [ ] Redirects to `/ProtectedMember/MemberBrowse.aspx`
- [ ] If not logged in, redirects to login (Forms Auth working!)
- [ ] Shows available hotels
- [ ] Member can book a room
- [ ] Balance is deducted
- [ ] Booking saved to Members.xml

#### Rate Hotels
**URL**: Click "Rate Hotels" button

- [ ] Redirects to `/ProtectedMember/MemberRating.aspx`
- [ ] Protected by Forms Authentication
- [ ] Member can rate hotels
- [ ] Rating saved to system

#### Change Password
**URL**: Click "Change Password" button

- [ ] Redirects to `/ProtectedMember/MemberChangePassword.aspx`
- [ ] Form displays
- [ ] Member can update password
- [ ] Password updated in Members.xml

#### Staff Dashboard
**URL**: Click "Staff Dashboard" button

- [ ] Redirects to `/ProtectedStaff/StaffDashboard.aspx`
- [ ] Requires staff login
- [ ] Shows staff-specific functions
- [ ] Can manage hotels/bookings

**Expected Output**: All main branch features work correctly

---

## ?? Debugging Tips

### If WCF Service Isn't Found:

1. Check the service is running (separate browser window)
2. Verify the endpoint in `HotelProject\Web.config`:
   ```xml
   <endpoint address="http://localhost:64549/Service1.svc" ...
   ```
3. Update port number if different

### If Authentication Doesn't Work:

1. Check `HotelProject\Web.config` has Forms Authentication:
   ```xml
   <authentication mode="Forms">
     <forms name="name" loginUrl="~/Login.aspx" ...
   ```
2. Check protected folders have Web.config files

### If Component Table Doesn't Show:

1. Open `HotelProject\Default.aspx`
2. Verify `ComponentsTable` control exists
3. Check for ASP.NET runtime errors

### If Assignment 5 Components Missing:

1. Verify files exist:
   - `HotelProject\AgentBooking.ascx`
   - `HotelProject\AgentNotification.ascx`
   - `HotelProject\MemberBrowsing.ascx`
2. Check they're registered in Default.aspx:
   ```asp
   <%@ Register Src="~/AgentBooking.ascx" TagPrefix="uc" TagName="AgentBooking" %>
   ```

---

## ?? What to Look For

### ? Success Indicators:

- Pages load without HTTP 500 errors
- Navigation works smoothly
- Authentication redirects properly
- WCF service calls return data
- Your component table is visible and complete
- All team features are functional

### ? Common Issues:

- **Port conflicts**: WCF service using different port
- **Service not running**: Start WcfHotelService first
- **Authentication loops**: Check Web.config settings
- **Missing files**: Run build again

---

## ?? Testing Results Template

After testing, document results:

```
? Default.aspx: PASS/FAIL - Notes:
? Staff Login: PASS/FAIL - Notes:
? Member Login: PASS/FAIL - Notes:
? Registration: PASS/FAIL - Notes:
? Global.asax Tracking: PASS/FAIL - Notes:
? AgentNotification: PASS/FAIL - Notes:
? AgentBooking: PASS/FAIL - Notes:
? MemberBrowsing: PASS/FAIL - Notes:
? Browse/Book: PASS/FAIL - Notes:
? Rate Hotels: PASS/FAIL - Notes:
? Change Password: PASS/FAIL - Notes:
? Staff Dashboard: PASS/FAIL - Notes:
```

---

## ?? Ready for Webstrar?

Once all tests pass locally:

1. ? Update service endpoint in Web.config
2. ? Deploy WcfHotelService first
3. ? Deploy HotelProject second
4. ? Test on Webstrar

---

## ?? Need Help?

**Common Issues Resolved:**
- ? Build errors fixed
- ? Merge conflicts resolved
- ? Stale references removed
- ? Global.asax syntax corrected

**Current Branch**: `muhammed/assignment5`
**Last Commit**: Fix build errors and syntax issues
**Build Status**: ? SUCCESSFUL

---

**Good luck with testing! ??**
