using System.Diagnostics;
using Application.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class HomeController : Controller
    {
        private readonly RentalContractService _rentalContractService;
        private readonly CustomerService _customerService;
        private readonly VehicleService _vehicleService;

        public HomeController(RentalContractService rentalContractService, CustomerService customerService, VehicleService vehicleService)
        {
            _rentalContractService = rentalContractService;
            _customerService = customerService;
            _vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index()
        {
            var now = DateTime.Now;
            var lastYearMonth = now.AddMonths(-11); // últimos 12 meses

            var totalCustomers = await _customerService.GetTotalActiveCustomersAsync();
            var topCustomers = await _customerService.GetTopCustomersAsync();

            var totalVehicles = await _vehicleService.GetTotalActiveVehiclesAsync();
            var topVehicles = await _vehicleService.GetTopVehiclesAsync();
            var rentedVehicles = await _vehicleService.GetCurrentlyRentedVehiclesCountAsync();
            var availableVehicles = totalVehicles - rentedVehicles;

            var activeContracts = await _rentalContractService.GetActiveContractsAsync();
            var scheduledContracts = await _rentalContractService.GetScheduledContractsAsync();

            var contracts = await _rentalContractService.GetAllRentalContractsAsync();
            var contractsPerMonth = contracts
                .Where(c => c.StartDate >= new DateTime(lastYearMonth.Year, lastYearMonth.Month, 1))
                .GroupBy(c => new DateTime(c.StartDate.Year, c.StartDate.Month, 1))
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key.ToString("MMM yyyy"), g => g.Count());

            var contractsEndingNextDays = await _rentalContractService.GetRentalContractsEndingNextDaysAsync();
            var model = new DashboardViewModel
            {
                TotalCustomers = totalCustomers,
                TotalVehicles = totalVehicles,
                ActiveContracts = activeContracts,
                ScheduledContracts = scheduledContracts,
                AvailableVehicles = availableVehicles,
                RentedVehicles = rentedVehicles,
                ContractsPerMonth = contractsPerMonth,
                ContractsEndingInNextDays = contractsEndingNextDays,
                TopCustomers = topCustomers,
                TopVehicles = topVehicles
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
