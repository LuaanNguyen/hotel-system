# Hotel Application 

Group Members: Luan Nguyen, Sophia Gu, Muhammed Hunaid Topiwala

Instructor: Yinong Chen

[CSE 445 Team Project - Group 8](https://docs.google.com/document/d/1y6lL__66V247L99GCH2AsyLCN385ElqNvzgojYsYJ5s/edit?usp=sharing)

## How to Run the Application

1. Unzip/extract the zip file. You should endup with a folder name `hotel-system-main` that contains all the source code from every members in our grou.
2. Locate and open `HotelProject.sln` in Visual Studio 2022.
3. Right‑click the HotelProject project and choose Set as Startup Project.
4. Then press F5 (or click the green arrow) to run the site using IIS Express.
5. Your browser will open to the `Default.aspx` page where the application and components summary table resides.

<img width="1717" height="933" alt="Screenshot 2025-11-16 at 9 23 07 PM" src="https://github.com/user-attachments/assets/69e648e0-1a87-4903-ab83-d6bf8a981bf3" />

### If you encounter this error: 

<img width="1156" height="430" alt="image" src="https://github.com/user-attachments/assets/668aa9c1-798f-40b8-ab32-23d6290bceeb" />

You need to restore the NuGet packages. Go to Tools > NuGet Package Manager > Package Manager Console and type in command `Update-Package -reinstall`
