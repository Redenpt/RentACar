using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RentACar.Helpers;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> ManageCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            var operationResult = TempData.Get<OperationResult>("OperationResult");
            if (operationResult != null)
            {
                ViewBag.OperationResult = operationResult;
            }
            
            return View(customers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCustomer(CustomerViewModel model)
        {
            var errors = new List<string>();
            if (!ModelState.IsValid)
            {
                errors = ModelStateExtensions.GetModelStateErrorList(ModelState);
                var operationResult = new OperationResult(false, model, errors, OperationType.Create, true);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageCustomers");
            }

            try
            {
                var customer = new Customer
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Phone = model.Phone,
                    DrivingLicense = model.DrivingLicense,
                };

                await _customerService.AddCustomerAsync(customer);
                var operationResult = new OperationResult(true, "Utilizador", OperationType.Create);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageCustomers");
            }
            catch (EntityAlreadyExistsException ex)
            {
                errors.Add(ex.Message);
                var operationResult = new OperationResult(false, model, errors, OperationType.Create, true);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageCustomers");
            }
            catch (Exception ex)
            {
                var operationResult = new OperationResult(false, "Utilizador", OperationType.Create, $"Ocorreu um erro inesperado: {ex.Message}");
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageCustomers");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCustomer(CustomerViewModel model)
        {
            var errors = new List<string>();
            if (!ModelState.IsValid)
            {
                errors = ModelStateExtensions.GetModelStateErrorList(ModelState);
                var operationResult = new OperationResult(false, model, errors, OperationType.Update, true);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageCustomers");
            }

            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(model.ID);

                customer.FullName = model.FullName;
                customer.Email = model.Email.Trim().ToLower();
                customer.Phone = model.Phone;
                customer.DrivingLicense = model.DrivingLicense;

                await _customerService.UpdateCustomerAsync(customer);
                var operationResult = new OperationResult(true, "Utilizador", OperationType.Update);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageCustomers");
            }
            catch (EntityNotFoundException ex)
            {
                errors.Add(ex.Message);
                var operationResult = new OperationResult(false, model, errors, OperationType.Update, true);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageCustomers");
            }
            catch (EntityAlreadyExistsException ex)
            {
                errors.Add(ex.Message);
                var operationResult = new OperationResult(false, model, errors, OperationType.Update, true);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageCustomers");
            }

            catch (Exception ex)
            {
                var operationResult = new OperationResult(false, "Utilizador", OperationType.Update, $"Ocorreu um erro inesperado: {ex.Message}");
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageCustomers");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCustomer(Guid customerID)
        {
            try
            {
                await _customerService.DeleteCustomerAsync(customerID);
                var operationResult = new OperationResult(true, "Utilizador", OperationType.Delete);
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageCustomers");
            }
            catch (EntityNotFoundException ex)
            {
                var operationResult = new OperationResult(false, "Utilizador", OperationType.Delete, $"Impossível eliminar utilizador. {ex.Message}");
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageCustomers");
            }
            catch (Exception ex)
            {
                var operationResult = new OperationResult(false, "Utilizador", OperationType.Delete, $"Ocorreu um erro inesperado: {ex.Message}");
                TempData.Put("OperationResult", operationResult);
                return RedirectToAction("ManageCustomers");
            }
        }
    }
}
