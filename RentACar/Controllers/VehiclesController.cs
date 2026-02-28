using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RentACar.Helpers;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly VehicleService _vehicleService;

        public VehiclesController(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<IActionResult> ManageVehicles()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            var operationResult = TempData.Get<OperationResult>("OperationResult");
            if (operationResult != null)
            {
                ViewBag.OperationResult = operationResult;
            }
            
            return View(vehicles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVehicle(VehicleViewModel model)
        {
            var errors = new List<string>();
            if (!ModelState.IsValid)
            {
                errors = ModelStateExtensions.GetModelStateErrorList(ModelState);
                var operationResult = new OperationResult(false, model, errors, OperationType.Create, true);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageVehicles");
            }

            try
            {
                var vehicle = new Vehicle
                {
                    Brand = model.Brand,
                    Model = model.VehicleModel,
                    LicensePlate = model.LicensePlate,
                    Year = model.Year,
                    FuelType = model.FuelType,
                };

                await _vehicleService.AddVehicleAsync(vehicle);
                var operationResult = new OperationResult(true, "Veículo", OperationType.Create);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageVehicles");
            }
            catch (EntityAlreadyExistsException ex)
            {
                errors.Add(ex.Message);
                var operationResult = new OperationResult(false, model, errors, OperationType.Create, true);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageVehicles");
            }
            catch (Exception ex)
            {
                var operationResult = new OperationResult(false, "Veículo", OperationType.Create, $"Ocorreu um erro inesperado: {ex.Message}");
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageVehicles");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVehicle(VehicleViewModel model)
        {
            var errors = new List<string>();
            if (!ModelState.IsValid)
            {
                errors = ModelStateExtensions.GetModelStateErrorList(ModelState);
                var operationResult = new OperationResult(false, model, errors, OperationType.Update, true);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageVehicles");
            }

            try
            {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(model.ID);

                vehicle.Brand = model.Brand;
                vehicle.Model = model.VehicleModel;
                vehicle.LicensePlate = model.LicensePlate;
                vehicle.Year = model.Year;
                vehicle.FuelType = model.FuelType;

                await _vehicleService.UpdateVehicleAsync(vehicle);
                var operationResult = new OperationResult(true, "Veículo", OperationType.Update);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageVehicles");
            }
            catch (EntityNotFoundException ex)
            {
                errors.Add(ex.Message);
                var operationResult = new OperationResult(false, model, errors, OperationType.Update, true);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageVehicles");
            }
            catch (EntityAlreadyExistsException ex)
            {
                errors.Add(ex.Message);
                var operationResult = new OperationResult(false, model, errors, OperationType.Update, true);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageVehicles");
            }

            catch (Exception ex)
            {
                var operationResult = new OperationResult(false, "Veículo", OperationType.Update, $"Ocorreu um erro inesperado: {ex.Message}");
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageVehicles");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVehicle(Guid vehicleID)
        {
            try
            {
                await _vehicleService.DeleteVehicleAsync(vehicleID);
                var operationResult = new OperationResult(true, "Veículo", OperationType.Delete);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageVehicles");
            }
            catch (EntityNotFoundException ex)
            {
                var operationResult = new OperationResult(false, "Veículo", OperationType.Delete, $"Impossível eliminar Veículo. {ex.Message}");
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageVehicles");
            }
            catch (Exception ex)
            {
                var operationResult = new OperationResult(false, "Veículo", OperationType.Delete, $"Ocorreu um erro inesperado: {ex.Message}");
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageVehicles");
            }
        }
    }
}
