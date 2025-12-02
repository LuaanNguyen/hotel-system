# ?? Webstrar Deployment Guide

## Prerequisites

? Local testing completed successfully  
? All features working locally  
? Build successful  
? Branch: muhammed/assignment5

---

## ?? Deployment Steps

### Phase 1: Deploy WCF Service

#### Step 1: Publish WcfHotelService

1. **In Visual Studio:**
   - Right-click **WcfHotelService** project
   - Select **Publish**
   - Choose **Folder** as publish target
   - Select a folder (e.g., `C:\Publish\WcfService`)
   - Click **Publish**

2. **Upload to Webstrar:**
   - Connect to Webstrar via FTP or file manager
   - Navigate to your assigned page folder (e.g., `page99`)
   - Create a subfolder: `WcfService` (or similar)
   - Upload all files from the publish folder

3. **Test the Service:**
   - Open browser to: `http://webstrar99.fulton.asu.edu/page99/WcfService/Service1.svc`
   - You should see the WCF service page
   - **Copy this URL** - you'll need it!

#### Step 2: Update Service Endpoint

**Before deploying the web application**, update the service reference:

1. Open `HotelProject\Web.config`

2. Find the endpoint configuration (around line 20-30):
   ```xml
   <endpoint address="http://localhost:64549/Service1.svc"
             binding="basicHttpBinding"
             bindingConfiguration="BasicHttpBinding_IService1"
             contract="RatingServiceReference.IService1"
             name="BasicHttpBinding_IService1" />
   ```

3. Replace `http://localhost:64549/Service1.svc` with your Webstrar URL:
   ```xml
   <endpoint address="http://webstrar99.fulton.asu.edu/page99/WcfService/Service1.svc"
             binding="basicHttpBinding"
             bindingConfiguration="BasicHttpBinding_IService1"
             contract="RatingServiceReference.IService1"
             name="BasicHttpBinding_IService1" />
   ```

4. Save the file

---

### Phase 2: Deploy Web Application

#### Step 1: Build for Release

1. In Visual Studio:
   - Change from **Debug** to **Release** mode (top toolbar)
   - Right-click **HotelProject**
   - Select **Clean**
   - Select **Rebuild**

#### Step 2: Publish HotelProject

1. **In Visual Studio:**
   - Right-click **HotelProject**
   - Select **Publish**
   - Choose **Folder** as publish target
   - Select a folder (e.g., `C:\Publish\HotelProject`)
   - Click **Publish**

2. **Upload to Webstrar:**
   - Connect to Webstrar via FTP or file manager
   - Navigate to your assigned page folder (e.g., `page99`)
   - Upload all files from the publish folder to the root of your page folder
   - Ensure these files/folders are uploaded:
     - `Default.aspx`
     - `Web.config`
     - `Global.asax`
     - `bin/` folder (contains DLLs)
     - `ProtectedMember/` folder
     - `ProtectedStaff/` folder
     - All `.aspx`, `.ascx`, and `.asmx` files

#### Step 3: Set Permissions (if needed)

On Webstrar, ensure write permissions for:
- `App_Data` folder (for XML file updates)
- Any folders that need to store user data

---

### Phase 3: Test on Webstrar

#### Test 1: Service Connectivity

1. Open: `http://webstrar99.fulton.asu.edu/page99/Default.aspx`
2. If you see errors about service not found:
   - Double-check the endpoint URL in Web.config
   - Verify WCF service is accessible
   - Check firewall/permissions on Webstrar

#### Test 2: Authentication

1. Click **"Login Member"**
2. Try credentials: **SophiaGu** / **SophiaGu123**
3. Verify login succeeds
4. Check protected pages are accessible

#### Test 3: Your Components

1. **Component Summary Table:**
   - Visit Default.aspx
   - Scroll down to find your table
   - Verify all entries are visible

2. **Global.asax Tracking:**
   - Check if session tracking works
   - May need server logs to verify

3. **AgentBooking:**
   - Navigate to AgentBookingPage.aspx
   - Test booking functionality

4. **MemberBrowsing:**
   - Test hotel browsing features
   - Verify data loads from service

#### Test 4: Main Features

- [ ] Member Registration
- [ ] Hotel Booking
- [ ] Rate Hotels
- [ ] Change Password
- [ ] Staff Dashboard

---

## ?? Common Webstrar Issues & Solutions

### Issue 1: HTTP 500 Error
**Cause**: Configuration error or missing dependencies  
**Solution**:
- Check Web.config is valid XML
- Verify all DLLs are in bin folder
- Check Webstrar error logs
- Ensure .NET Framework 4.7.2 is available

### Issue 2: Service Not Found
**Cause**: Incorrect endpoint URL or service not deployed  
**Solution**:
- Verify WCF service URL is correct
- Test service URL directly in browser
- Check Web.config endpoint address
- Ensure no typos in URL

### Issue 3: Authentication Not Working
**Cause**: Forms Authentication may not be configured on server  
**Solution**:
- Check Web.config has Forms Authentication
- Verify protected folders have Web.config files
- Contact instructor if server doesn't support Forms Auth

### Issue 4: XML Files Not Updating
**Cause**: Permission denied  
**Solution**:
- Check folder permissions on Webstrar
- May need to request write permissions
- Consider using database instead of XML (if allowed)

### Issue 5: Assembly Load Errors
**Cause**: Missing DLLs or wrong .NET version  
**Solution**:
- Copy entire bin folder
- Ensure SecurityLib.dll is included
- Verify all dependent assemblies are present

---

## ?? Deployment Checklist

### Before Deployment:
- [ ] Local testing completed and successful
- [ ] Build in Release mode successful
- [ ] Service endpoint updated in Web.config
- [ ] All team features tested locally
- [ ] Your Assignment 5 components verified

### WCF Service Deployment:
- [ ] Service published to folder
- [ ] Files uploaded to Webstrar
- [ ] Service URL tested in browser
- [ ] Service URL copied for Web.config

### Web Application Deployment:
- [ ] Web.config updated with Webstrar service URL
- [ ] Application published to folder
- [ ] All files uploaded to Webstrar
- [ ] Folder structure preserved
- [ ] bin folder uploaded completely

### Post-Deployment Testing:
- [ ] Default.aspx loads
- [ ] Service connectivity confirmed
- [ ] Member login works
- [ ] Component table displays
- [ ] Assignment 5 features work
- [ ] Main branch features work
- [ ] Authentication functions properly

---

## ?? If You Need Help

**Check These First:**
1. Webstrar error logs (if accessible)
2. Browser developer tools (F12) ? Console tab
3. Network tab for failed requests
4. Service URL directly in browser

**Common Questions:**
- **Q**: Service URL is different locally vs Webstrar?  
  **A**: Yes, update Web.config endpoint address to Webstrar URL

- **Q**: Can I test without deploying service first?  
  **A**: No, deploy WCF service first, then web app

- **Q**: Do I need to update service reference?  
  **A**: Just update the endpoint address in Web.config, no need to regenerate

---

## ?? Success Criteria

Your deployment is successful when:

? Default.aspx loads on Webstrar  
? Component summary table is visible  
? Member login works  
? Hotels can be browsed and booked  
? Your Assignment 5 components are accessible  
? No HTTP 500 errors  
? WCF service calls succeed  

---

## ?? Deployment URLs Template

After deployment, document your URLs:

```
WCF Service: http://webstrar99.fulton.asu.edu/page99/WcfService/Service1.svc
Main Page: http://webstrar99.fulton.asu.edu/page99/Default.aspx
Staff Login: http://webstrar99.fulton.asu.edu/page99/StaffLogin.aspx
Member Login: http://webstrar99.fulton.asu.edu/page99/MemberLogin.aspx
Agent Booking: http://webstrar99.fulton.asu.edu/page99/AgentBookingPage.aspx
```

*(Replace page99 with your actual assigned page number)*

---

## ?? Ready to Deploy!

**Current Status:**
- ? Code merged and tested
- ? Build successful
- ? Local testing ready
- ? Awaiting Webstrar deployment

**Good luck with your deployment! ??**

---

*Generated: November 30, 2025*  
*For Assignment 5 Webstrar Deployment*
