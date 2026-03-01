using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;

namespace Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Customers.Any())
            {
                context.Customers.AddRange(
                    new Customer
                    {
                        ID = new Guid("661db3c1-f757-4a40-b374-1418001e2262"),
                        FullName = "João Miguel Silva",
                        Email = "joao.silva@emailteste.pt",
                        Phone = "912345678",
                        DrivingLicense = "14578963",
                        CreatedAt = new DateTime(2026, 03, 01, 10, 01, 12),
                        UpdatedAt = new DateTime(2026, 03, 01, 10, 01, 17),
                        DeletedAt = null,
                        IsActive = true
                    },
                    new Customer
                    {
                        ID = new Guid("d8fbc734-a8f0-4e50-be90-677e49a04eac"),
                        FullName = "Ricardo Alves Martins",
                        Email = "ricardo.martins@emailteste.pt",
                        Phone = "961234875",
                        DrivingLicense = "36985214",
                        CreatedAt = new DateTime(2026, 03, 01, 10, 02, 28),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    },
                    new Customer
                    {
                        ID = new Guid("02471cce-57df-4046-a328-71efafb45e81"),
                        FullName = "Ana Catarina Ferreira",
                        Email = "ana.ferreira@emailteste.pt",
                        Phone = "934567821",
                        DrivingLicense = "27894561",
                        CreatedAt = new DateTime(2026, 03, 01, 10, 02, 01),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    },
                    new Customer
                    {
                        ID = new Guid("cc105113-d77f-4a14-bb0f-72947a6250a0"),
                        FullName = "Tiago Nuno Rodrigues",
                        Email = "tiago.rodrigues@emailteste.pt",
                        Phone = "+351939876542",
                        DrivingLicense = "59632147",
                        CreatedAt = new DateTime(2026, 03, 01, 10, 03, 16),
                        UpdatedAt = new DateTime(2026, 03, 01, 10, 04, 34),
                        DeletedAt = null,
                        IsActive = true
                    },
                    new Customer
                    {
                        ID = new Guid("08adc0fa-f2d2-41fc-8b0b-d6a0d6c35603"),
                        FullName = "Mariana Sofia Costa",
                        Email = "mariana.costa@emailteste.pt",
                        Phone = "925478963",
                        DrivingLicense = "48751236",
                        CreatedAt = new DateTime(2026, 03, 01, 10, 02, 53),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    }
                );

                await context.SaveChangesAsync();
            }

            if (!context.Vehicles.Any())
            {
                context.Vehicles.AddRange(
                    new Vehicle
                    {
                        ID = new Guid("d4411670-bf74-47f1-80b9-0ab9f2cc6c8b"),
                        Brand = "Peugeot",
                        Model = "208",
                        LicensePlate = "MN-89-OP",
                        Year = 2020,
                        FuelType = FuelType.Gasoline,
                        CreatedAt = new DateTime(2026, 03, 01, 10, 10, 49),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    },
                    new Vehicle
                    {
                        ID = new Guid("0a778536-6d4f-4e00-9086-1c08dd8da646"),
                        Brand = "Renault",
                        Model = "Clio",
                        LicensePlate = "EF-45-GH",
                        Year = 2019,
                        FuelType = FuelType.Diesel,
                        CreatedAt = new DateTime(2026, 03, 01, 10, 09, 59),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    },
                    new Vehicle
                    {
                        ID = new Guid("3dd0fa91-8390-44f0-b268-3463fd29703d"),
                        Brand = "Tesla",
                        Model = "Model 3",
                        LicensePlate = "QR-12-ST",
                        Year = 2023,
                        FuelType = FuelType.Electric,
                        CreatedAt = new DateTime(2026, 03, 01, 10, 11, 12),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    },
                    new Vehicle
                    {
                        ID = new Guid("f1c8118a-c0a9-41ba-9bef-8ed3654ab9d1"),
                        Brand = "Toyota",
                        Model = "Corolla",
                        LicensePlate = "AB-23-CD",
                        Year = 2021,
                        FuelType = FuelType.Gasoline,
                        CreatedAt = new DateTime(2026, 03, 01, 10, 09, 13),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    },
                    new Vehicle
                    {
                        ID = new Guid("74831c70-2b8b-4ebc-8e18-ecc7d29fc510"),
                        Brand = "BMW",
                        Model = "320d",
                        LicensePlate = "IJ-67-KL",
                        Year = 2022,
                        FuelType = FuelType.Diesel,
                        CreatedAt = new DateTime(2026, 03, 01, 10, 10, 25),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    }
                );
            }

            if (!context.RentalContracts.Any())
            {
                context.RentalContracts.AddRange(
                    new RentalContract
                    {
                        ID = new Guid("2b22c14b-4c6c-4474-85a6-1a50ae282229"),
                        CustomerID = new Guid("08adc0fa-f2d2-41fc-8b0b-d6a0d6c35603"),
                        VehicleID = new Guid("f1c8118a-c0a9-41ba-9bef-8ed3654ab9d1"),
                        StartDate = new DateTime(2026, 04, 16),
                        EndDate = new DateTime(2026, 04, 19),
                        InitialMileage = 84693,
                        CreatedAt = new DateTime(2026, 03, 01, 10, 16, 08),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    },
                    new RentalContract
                    {
                        ID = new Guid("4de3e02b-d878-4f2a-8163-2785209b12fe"),
                        CustomerID = new Guid("d8fbc734-a8f0-4e50-be90-677e49a04eac"),
                        VehicleID = new Guid("f1c8118a-c0a9-41ba-9bef-8ed3654ab9d1"),
                        StartDate = new DateTime(2026, 01, 12),
                        EndDate = new DateTime(2026, 01, 21),
                        InitialMileage = 777,
                        CreatedAt = new DateTime(2026, 03, 01, 12, 09, 11),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    },
                    new RentalContract
                    {
                        ID = new Guid("6f0dd553-9c5f-4da4-a0a1-28d2559221e1"),
                        CustomerID = new Guid("661db3c1-f757-4a40-b374-1418001e2262"),
                        VehicleID = new Guid("d4411670-bf74-47f1-80b9-0ab9f2cc6c8b"),
                        StartDate = new DateTime(2026, 03, 02),
                        EndDate = new DateTime(2026, 03, 08),
                        InitialMileage = 1204,
                        CreatedAt = new DateTime(2026, 03, 01, 10, 12, 24),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    },
                    new RentalContract
                    {
                        ID = new Guid("e9ee5883-1f7f-4bc4-a866-62f03a8f6d1e"),
                        CustomerID = new Guid("661db3c1-f757-4a40-b374-1418001e2262"),
                        VehicleID = new Guid("0a778536-6d4f-4e00-9086-1c08dd8da646"),
                        StartDate = new DateTime(2026, 03, 01),
                        EndDate = new DateTime(2026, 03, 07),
                        InitialMileage = 7751,
                        CreatedAt = new DateTime(2026, 03, 01, 10, 14, 48),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    },
                    new RentalContract
                    {
                        ID = new Guid("b3179637-e09a-4e1c-8743-7203b6da9526"),
                        CustomerID = new Guid("d8fbc734-a8f0-4e50-be90-677e49a04eac"),
                        VehicleID = new Guid("d4411670-bf74-47f1-80b9-0ab9f2cc6c8b"),
                        StartDate = new DateTime(2026, 02, 13),
                        EndDate = new DateTime(2026, 02, 15),
                        InitialMileage = 4512,
                        CreatedAt = new DateTime(2026, 03, 01, 12, 10, 32),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    },
                    new RentalContract
                    {
                        ID = new Guid("7d8c81ca-18e2-4361-907d-9e20d888bd98"),
                        CustomerID = new Guid("661db3c1-f757-4a40-b374-1418001e2262"),
                        VehicleID = new Guid("0a778536-6d4f-4e00-9086-1c08dd8da646"),
                        StartDate = new DateTime(2026, 04, 25),
                        EndDate = new DateTime(2026, 04, 30),
                        InitialMileage = 5555,
                        CreatedAt = new DateTime(2026, 03, 01, 12, 08, 28),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    },
                    new RentalContract
                    {
                        ID = new Guid("f5453385-96bf-47d2-a4ac-bffbdebb4db0"),
                        CustomerID = new Guid("cc105113-d77f-4a14-bb0f-72947a6250a0"),
                        VehicleID = new Guid("74831c70-2b8b-4ebc-8e18-ecc7d29fc510"),
                        StartDate = new DateTime(2026, 03, 02),
                        EndDate = new DateTime(2026, 03, 31),
                        InitialMileage = 85555,
                        CreatedAt = new DateTime(2026, 03, 01, 10, 13, 48),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    },
                    new RentalContract
                    {
                        ID = new Guid("829695ee-52b2-4be4-9720-ddb47759ba7c"),
                        CustomerID = new Guid("661db3c1-f757-4a40-b374-1418001e2262"),
                        VehicleID = new Guid("f1c8118a-c0a9-41ba-9bef-8ed3654ab9d1"),
                        StartDate = new DateTime(2026, 04, 01),
                        EndDate = new DateTime(2026, 04, 04),
                        InitialMileage = 45123,
                        CreatedAt = new DateTime(2026, 03, 01, 10, 15, 25),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    },
                    new RentalContract
                    {
                        ID = new Guid("b7473601-c7f8-44de-b510-e5cf03cc9564"),
                        CustomerID = new Guid("d8fbc734-a8f0-4e50-be90-677e49a04eac"),
                        VehicleID = new Guid("3dd0fa91-8390-44f0-b268-3463fd29703d"),
                        StartDate = new DateTime(2026, 03, 01),
                        EndDate = new DateTime(2026, 03, 20),
                        InitialMileage = 785,
                        CreatedAt = new DateTime(2026, 03, 01, 10, 12, 54),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    },
                    new RentalContract
                    {
                        ID = new Guid("7a64b583-aa26-4244-a580-e6561daad775"),
                        CustomerID = new Guid("02471cce-57df-4046-a328-71efafb45e81"),
                        VehicleID = new Guid("74831c70-2b8b-4ebc-8e18-ecc7d29fc510"),
                        StartDate = new DateTime(2026, 01, 13),
                        EndDate = new DateTime(2026, 01, 19),
                        InitialMileage = 1235,
                        CreatedAt = new DateTime(2026, 03, 01, 12, 09, 58),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    },
                    new RentalContract
                    {
                        ID = new Guid("d86d1801-0da0-4b62-a83a-f1602200681e"),
                        CustomerID = new Guid("661db3c1-f757-4a40-b374-1418001e2262"),
                        VehicleID = new Guid("3dd0fa91-8390-44f0-b268-3463fd29703d"),
                        StartDate = new DateTime(2026, 04, 17),
                        EndDate = new DateTime(2026, 04, 17),
                        InitialMileage = 78549,
                        CreatedAt = new DateTime(2026, 03, 01, 10, 16, 57),
                        UpdatedAt = null,
                        DeletedAt = null,
                        IsActive = true
                    }
                );
            }

            await context.SaveChangesAsync();
        }
    }
}
