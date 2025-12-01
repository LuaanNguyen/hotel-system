# Quick Test Data Viewer
# Run this script to view current test credentials and hotel data

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "   HOTEL SYSTEM - TEST DATA VIEWER" -ForegroundColor Cyan
Write-Host "========================================`n" -ForegroundColor Cyan

# Check if XML files exist
$staffXml = "WcfHotelService\App_Data\Staff.xml"
$membersXml = "WcfHotelService\App_Data\Members.xml"
$hotelsXml = "WcfHotelService\App_Data\Hotels.xml"

# Display Staff Users
if (Test-Path $staffXml) {
    Write-Host "?? STAFF USERS:" -ForegroundColor Yellow
    [xml]$staff = Get-Content $staffXml
    $staff.StaffMembers.StaffMember | ForEach-Object {
        Write-Host "  Username: $($_.Username)" -ForegroundColor Green
        Write-Host "  Password: $($_.Password)" -ForegroundColor Gray
        Write-Host ""
    }
} else {
    Write-Host "? Staff.xml not found at $staffXml" -ForegroundColor Red
}

# Display Member Users
if (Test-Path $membersXml) {
    Write-Host "`n?? MEMBER USERS:" -ForegroundColor Yellow
    [xml]$members = Get-Content $membersXml
    $members.Members.Member | Select-Object -First 3 | ForEach-Object {
        Write-Host "  Username: $($_.Username)" -ForegroundColor Green
        Write-Host "  Password: $($_.Password)" -ForegroundColor Gray
        Write-Host "  Balance: $($_.Balance)" -ForegroundColor Cyan
        Write-Host ""
    }
    $totalMembers = $members.Members.Member.Count
    if ($totalMembers -gt 3) {
        Write-Host "  ... and $($totalMembers - 3) more members" -ForegroundColor Gray
    }
} else {
    Write-Host "? Members.xml not found at $membersXml" -ForegroundColor Red
}

# Display Hotels
if (Test-Path $hotelsXml) {
    Write-Host "`n?? AVAILABLE HOTELS:" -ForegroundColor Yellow
    [xml]$hotels = Get-Content $hotelsXml
    $hotels.Hotels.Hotel | Select-Object -First 5 | ForEach-Object {
        Write-Host "  ID: $($_.ID)" -ForegroundColor Green
        Write-Host "  Name: $($_.Name)" -ForegroundColor Cyan
        Write-Host "  Price: `$$($_.Price)" -ForegroundColor Yellow
        Write-Host "  Available Rooms: $($_.BookedRooms)" -ForegroundColor Magenta
        Write-Host "  Location: $($_.Address.City), $($_.Address.State)" -ForegroundColor Gray
        Write-Host ""
    }
    $totalHotels = $hotels.Hotels.Hotel.Count
    if ($totalHotels -gt 5) {
        Write-Host "  ... and $($totalHotels - 5) more hotels" -ForegroundColor Gray
    }
} else {
    Write-Host "? Hotels.xml not found at $hotelsXml" -ForegroundColor Red
}

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "? Ready to test! Press F5 in Visual Studio" -ForegroundColor Green
Write-Host "========================================`n" -ForegroundColor Cyan

# Check build status
Write-Host "?? BUILD STATUS:" -ForegroundColor Yellow
if (Test-Path "HotelProject\bin\HotelProject.dll") {
    $dll = Get-Item "HotelProject\bin\HotelProject.dll"
    Write-Host "  ? HotelProject.dll exists" -ForegroundColor Green
    Write-Host "  Last built: $($dll.LastWriteTime)" -ForegroundColor Gray
} else {
    Write-Host "  ? HotelProject.dll not found - Run build first!" -ForegroundColor Red
}

Write-Host "`n?? NEXT STEPS:" -ForegroundColor Cyan
Write-Host "  1. Open HotelProject.sln in Visual Studio" -ForegroundColor White
Write-Host "  2. Set multiple startup projects (both WcfHotelService and HotelProject)" -ForegroundColor White
Write-Host "  3. Press F5 to run" -ForegroundColor White
Write-Host "  4. Test using LOCAL_TESTING_GUIDE.md checklist`n" -ForegroundColor White
