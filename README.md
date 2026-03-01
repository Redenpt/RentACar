# 🚗 Rent A Car

ASP.NET Core application for vehicle rental management.

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
cd RentACar
```

### 2️⃣ Restore dependencies

dotnet restore

### 3️⃣ Run the application

dotnet run

---

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