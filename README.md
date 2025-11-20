# SIS – Student Information System

A lightweight Student Information System built with **.NET** designed to manage students, courses, enrollment, and academic records. This project demonstrates clean architecture, modular design, and practical use of modern .NET features.

## 🚀 Features
- Student registration & management
- Course creation & assignments
- Enrollment module
- Grades & attendance tracking
- Role-based access (Admin, Staff, Student)
- SQL database integration

## 🛠️ Tech Stack
- .NET / ASP.NET Core
- Entity Framework Core
- SQL Server
- Bootstrap / Tailwind (if UI included)
- Swagger / OpenAPI

## 📦 Installation
1. Clone the repository:
   git clone https://github.com/yourusername/sis.git
   cd sis

2. Update the database connection string in appsettings.json.

3. Apply migrations:
   dotnet ef database update

4. Run the project:
   dotnet run

## 📁 Project Structure
/SIS
 ├── Controllers
 ├── Models
 ├── Services
 ├── Data
 ├── wwwroot
 └── appsettings.json

## 🤝 Contributing
Pull requests are welcome! For major changes, open an issue to discuss changes first.

## 📄 License
This project is licensed under the MIT License.
