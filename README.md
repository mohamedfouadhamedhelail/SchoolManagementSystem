# School Management System

A web-based School Management System built with **ASP.NET Core MVC**, **Entity Framework Core**, and **SQL Server**. The application provides an interface for managing students and departments, with support for searching, filtering, and pagination.

## 🚀 Features

### Student Management

* View all students in a structured table.
* Add new students.
* Edit existing student information.
* Delete student records.
* Search for students.
* Filter students by department.
* Navigate through student records using pagination.

### Department Management

* View all departments.
* Add new departments.
* Edit department information.
* Delete departments.
* Assign students to departments.

### Additional Features

* Server-side data handling using MVC architecture.
* Database operations using Entity Framework Core.
* Data validation.
* Relational data management between students and departments.
* Responsive user interface.

## 🛠️ Technologies Used

* **C#**
* **ASP.NET Core MVC (.NET 8)**
* **Entity Framework Core 8**
* **SQL Server / SQL Server LocalDB**
* **Razor Views**
* **HTML5**
* **CSS3**
* **Bootstrap**
* **LINQ**

## 📁 Project Structure

```text
SchoolManagementSystem/
│
├── Controllers/
│   ├── HomeController.cs
│   ├── StudentController.cs
│   └── DepartmentController.cs
│
├── Data/
│   └── SchoolDbContext.cs
│
├── Models/
│   ├── Student.cs
│   └── Department.cs
│
├── ViewModels/
│   └── StudentIndexViewModel.cs
│
├── Migrations/
│
├── Views/
│   ├── Home/
│   ├── Student/
│   ├── Department/
│   └── Shared/
│
├── wwwroot/
│   └── css/
│
├── appsettings.json
├── Program.cs
└── README.md
```

## ⚙️ Prerequisites

Before running the project, make sure you have:

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* Visual Studio 2022 or a compatible IDE.
* SQL Server LocalDB or SQL Server.
* Entity Framework Core tools.

## 📥 Installation and Setup

### 1. Clone the Repository

```bash
git clone YOUR_REPOSITORY_URL
```

Navigate to the project directory:

```bash
cd SchoolManagementSystem
```

### 2. Configure the Database

Open `appsettings.json` and configure the connection string according to your SQL Server setup.

Example using SQL Server LocalDB:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=SchoolManagementSystemDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

> Make sure the connection string name matches the one configured in `Program.cs`.

### 3. Restore Dependencies

```bash
dotnet restore
```

### 4. Apply Database Migrations

If the project already contains valid EF Core migrations, run:

```bash
dotnet ef database update
```

If the EF Core command-line tool is not installed, install it using:

```bash
dotnet tool install --global dotnet-ef
```

If it is already installed, update it when necessary:

```bash
dotnet tool update --global dotnet-ef
```

### 5. Run the Application

```bash
dotnet run
```

Open the local URL displayed in the terminal to access the application.

## 🗄️ Database Design

The application uses two primary entities:

### Student

Stores student information, including:

* `Id`
* `Name`
* `Email`
* `Age`
* `DepartmentId`

### Department

Stores department information, including:

* `Id`
* `Name`

### Relationships

* A department can have multiple students.
* A student can belong to a department.
* A student's department can be optional.

## 📚 Key Concepts Demonstrated

This project demonstrates practical implementation of:

* Model-View-Controller (MVC) architecture.
* CRUD operations.
* Entity Framework Core database integration.
* Code-First migrations.
* One-to-many relationships.
* ViewModels for presenting data to views.
* Server-side search and filtering.
* Pagination.
* Razor syntax and tag helpers.
* Form validation and model binding.

## 🔮 Future Improvements

Potential improvements include:

* Authentication and authorization.
* Role-based access control for administrators and teachers.
* Student enrollment and course management.
* Dashboard with statistics.
* Advanced validation and error handling.
* Automated unit and integration tests.
* Deployment to a cloud hosting platform.

## 👨‍💻 Author

**Mohamed Fouad Hamed Helail**

* GitHub: [mohamedfouadhamedhelail](https://github.com/mohamedfouadhamedhelail)
* LinkedIn: [Mohamed Fouad](https://www.linkedin.com/in/mohamed-fouad-95a1ba383/)

---

*Built as a practical project to apply ASP.NET Core MVC, Entity Framework Core, and relational database development.*
