using Application.Services;
using Domain.Common;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Helpers;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class RentalContractsController : Controller
    {
        private readonly RentalContractService _rentalContractService;
        private readonly CustomerService _customerService;
        private readonly VehicleService _vehicleService;

        public RentalContractsController(RentalContractService rentalContractService, CustomerService customerService, VehicleService vehicleService)
        {
            _rentalContractService = rentalContractService;
            _customerService = customerService;
            _vehicleService = vehicleService;
        }

        public async Task<IActionResult> ManageRentalContracts()
        {
            var today = DateTime.Today;
            var rentalContracts = await _rentalContractService.GetAllRentalContractsAsync();
            rentalContracts = rentalContracts.OrderByDescending(rc => rc.StartDate).ToList();
            var contracts = rentalContracts.Select(
                                rc => new RentalContractViewModel
                                {
                                    ID = rc.ID,
                                    CustomerID = rc.CustomerID,
                                    VehicleID = rc.VehicleID,
                                    StartDate = rc.StartDate,
                                    EndDate = rc.EndDate,
                                    InitialMileage = rc.InitialMileage,
                                    Status = rc.StartDate > today ? RentalStatus.Pending
                                           : rc.StartDate <= today && rc.EndDate >= today ? RentalStatus.Active
                                           : RentalStatus.Completed,

                                    Customer = new CustomerViewModel
                                    {
                                        ID = rc.Customer.ID,
                                        FullName = rc.Customer.FullName,
                                        Email = rc.Customer.Email,
                                        Phone = rc.Customer.Phone,
                                        DrivingLicense = rc.Customer.DrivingLicense
                                    },

                                    Vehicle = new VehicleViewModel
                                    {
                                        ID = rc.Vehicle.ID,
                                        Brand = rc.Vehicle.Brand,
                                        VehicleModel = rc.Vehicle.VehicleModel,
                                        LicensePlate = rc.Vehicle.LicensePlate,
                                        Year = rc.Vehicle.Year,
                                        FuelType = rc.Vehicle.FuelType
                                    }
                                })
                                .ToList();

            var activeCustomers = await _customerService.GetAllCustomersAsync();
            var customerViewModels = activeCustomers.Select(
                                        c => new CustomerViewModel
                                        {
                                            ID = c.ID,
                                            FullName = c.FullName,
                                            Email = c.Email,
                                            Phone = c.Phone,
                                            DrivingLicense = c.DrivingLicense
                                        })
                                        .ToList();

            var activeVehicles = await _vehicleService.GetAllVehiclesAsync();
            var vehicleViewModels = activeVehicles.Select(
                                        v => new VehicleViewModel
                                        {
                                            ID = v.ID,
                                            Brand = v.Brand,
                                            VehicleModel = v.VehicleModel,
                                            LicensePlate = v.LicensePlate,
                                            Year = v.Year,
                                            FuelType = v.FuelType,
                                        })
                                        .ToList();
            ViewBag.Customers = customerViewModels;
            ViewBag.Vehicles = vehicleViewModels;

            var operationResult = TempData.Get<OperationResult>("OperationResult");

            if (operationResult != null)
            {
                ViewBag.OperationResult = operationResult;
            }
            
            return View(contracts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRentalContract(RentalContractViewModel model)
        {
            var errors = new List<string>();
            if (!ModelState.IsValid)
            {
                errors = ModelStateExtensions.GetModelStateErrorList(ModelState);
                var operationResult = new OperationResult(false, model, errors, OperationType.Create, true);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageRentalContracts");
            }

            try
            {
                var rentalContract = new RentalContract
                {
                    CustomerID = model.CustomerID.Value,
                    VehicleID = model.VehicleID.Value,
                    StartDate = model.StartDate.Value,
                    EndDate = model.EndDate.Value,
                    InitialMileage = model.InitialMileage.Value,
                };

                await _rentalContractService.AddRentalContractAsync(rentalContract);
                var operationResult = new OperationResult(true, "Contrato de Aluguer", OperationType.Create);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageRentalContracts");
            }
            catch (EntityAlreadyExistsException ex)
            {
                errors.Add(ex.Message);
                var operationResult = new OperationResult(false, model, errors, OperationType.Create, true);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageRentalContracts");
            }
            catch (VehicleAlreadyRentedException ex)
            {
                errors.Add(ex.Message);
                var operationResult = new OperationResult(false, model, errors, OperationType.Create, true);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageRentalContracts");
            }
            catch (Exception ex)
            {
                var operationResult = new OperationResult(false, "Contrato de Aluguer", OperationType.Create, $"Ocorreu um erro inesperado: {ex.Message}");
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageRentalContracts");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRentalContract(Guid rentalContractID)
        {
            try
            {
                await _rentalContractService.DeleteRentalContractAsync(rentalContractID);
                var operationResult = new OperationResult(true, "Contrato de Aluguer", OperationType.Delete);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageRentalContracts");
            }
            catch (EntityNotFoundException ex)
            {
                var operationResult = new OperationResult(false, "Contrato de Aluguer", OperationType.Delete, $"Impossível eliminar Contrato de Aluguer. {ex.Message}");
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageRentalContracts");
            }
            catch (Exception ex)
            {
                var operationResult = new OperationResult(false, "Contrato de Aluguer", OperationType.Delete, $"Ocorreu um erro inesperado: {ex.Message}");
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageRentalContracts");
            }
        }
    }
}
