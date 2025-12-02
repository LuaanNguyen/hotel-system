# Hotel Application

Group Members: Luan Nguyen, Sophia Gu, Muhammed Hunaid Topiwala

Instructor: Yinong Chen

[CSE 445 Team Project - Group 8](https://docs.google.com/document/d/1y6lL__66V247L99GCH2AsyLCN385ElqNvzgojYsYJ5s/edit?usp=sharing)

## Homepage

<img width="1316" alt="Homepage" src="https://github.com/user-attachments/assets/266ecf61-ed50-4a4f-91b0-f661fe25fa30" />

## Test Components

<img width="290" alt="Test Components" src="https://github.com/user-attachments/assets/1818af2a-c6f7-42e0-9c87-9ebb9114349c" />

## Agent Notification Control

<img width="527" alt="Agent Notification" src="https://github.com/user-attachments/assets/33af2be4-1e69-418d-b8bb-fb3ba59d4cab" />

## Member Browsing Control

<img width="799" alt="Member Browsing" src="https://github.com/user-attachments/assets/cef4d8ea-59c4-498d-802a-36b7f5bd7e2a" />

## Application and Components Summary Table

### Sophia Gu's Contributions

<img width="1307" alt="Sophia Gu Contributions" src="https://github.com/user-attachments/assets/fa04a0f5-4a58-4012-a48b-38990971a95f" />

### Luan Nguyen's Contributions

<img width="1299" alt="Luan Nguyen Contributions" src="https://github.com/user-attachments/assets/b6cbbfa5-26c2-4a5b-a0c7-1d48ef6922ff" />

### Muhammed Hunaid Topiwala's Contributions

<img width="1292" alt="Muhammed Topiwala Contributions" src="https://github.com/user-attachments/assets/73a935df-5e16-4a31-893b-b58a618a4226" />

## Live Deployment (WebStrar)

Our application is deployed on ASU's WebStrar server:

| Component | URL |
|-----------|-----|
| **Web Application** | http://webstrar8.fulton.asu.edu/page8/Default.aspx |
| **WCF Service** | http://webstrar8.fulton.asu.edu/page0/Service1.svc |

### Test Credentials

| Role | Username | Password |
|------|----------|----------|
| Member | SophiaGu | SophiaGu123 |
| Member | LuanNguyen | LuanNguyen123 |
| Member | MoTopiwala | MoTopiwala123 |
| Staff | TA | Cse445! |

### Accessing WebStrar (VPN Required for Development)

To deploy or modify files on WebStrar, you need ASU VPN access:

1. **Install Cisco AnyConnect VPN** from [ASU VPN Guide](https://asu.my.salesforce-sites.com/kb/articles/FAQ/How-do-I-Install-Cisco-AnyConnect-SSLVPN)
2. **Connect to VPN**: `sslvpn.asu.edu/2fa`
   - Use your ASURITE ID and password
   - Second password: `push` (triggers DUO verification)
3. **Map Network Drive**: `\\webstrar.fulton.asu.edu\website8`
   - Username: `asuad\<your-asurite>`
   - Drive letter: `Z:`

## Running Locally

### Prerequisites
- Visual Studio 2022
- .NET Framework 4.0+

### Steps

1. Clone or extract the repository
2. Open `HotelProject.sln` in Visual Studio 2022
3. Right-click **WcfHotelService** → Set as Startup Project → Press F5
   - Note the localhost port (e.g., `localhost:51042`)
4. Right-click **HotelProject** → Set as Startup Project → Press F5
5. Browser opens to `Default.aspx`

### If NuGet packages fail:

<img width="1156" height="430" alt="image" src="https://github.com/user-attachments/assets/668aa9c1-798f-40b8-ab32-23d6290bceeb" />

Run in Package Manager Console: `Update-Package -reinstall`
