# 🚗 Rent A Car

Aplicação ASP.NET Core para gestão de aluguer de veículos.

## 📦 Tecnologias

- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server

---

## ⚙️ Como executar o projeto

### 1️⃣ Clonar o repositório

git clone <url-do-repo>
cd RentACar

### 2️⃣ Restaurar dependências

dotnet restore

### 3️⃣ Executar

dotnet run

---

## 🗄 Base de Dados

A base de dados é criada automaticamente na primeira execução.

- As migrations são aplicadas automaticamente
- Os dados iniciais (utilizadores, veículos e contratos) são populados via Seed

Não é necessário executar `dotnet ef database update`.

---

## 🔑 Funcionalidades

- Gestão de Clientes
- Gestão de Veículos
- Gestão de Contratos de Aluguer