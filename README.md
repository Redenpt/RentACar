# 🚗 Rent A Car

ASP.NET Core application for vehicle rental management.

## 📝 Prerequisites

Before running the project, make sure you have the following installed:

- Git
- .NET 8 SDK
- SQL Server 

---

## 📦 Technologies

- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server

---

## ⚙️ How to Run the Project

### 1️⃣ Clone the repository

```bash
git clone https://github.com/Redenpt/RentACar.git
```

### 2️⃣ Go to the project folder
```bash
cd RentACar/RentACar
```
### 3️⃣ Restore dependencies
```bash
dotnet restore
```
### 4️⃣ Run the application
```bash
dotnet run
```
---

## 💡 Tip: After running, check the console output for a line like: Now listening on: http://localhost:5228
### Copy this URL to your browser to open the application


## 🗄 Database

The database is automatically created on the first run.

- Migrations are applied automatically
- Initial data (users, vehicles, and rental contracts) is populated via Seed

There is no need to run `dotnet ef database update`.

---

## 🔑 Features

- Customer Management
- Vehicle Management
- Rental Contract Management