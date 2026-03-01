using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetAllAsync(bool includeInactive = false);            
        Task<Vehicle> GetByIdAsync(Guid id);
        Task<Vehicle> GetByLicensePlateAsync(string licensePlate);
        Task AddAsync(Vehicle vehicle);              
        Task UpdateAsync(Vehicle vehicle);           
        Task DeleteAsync(Guid id);
        Task<bool> LicensePlateExistsAsync(string licensePlate, Guid? vehicleID = null);
        Task<bool> HasActiveRentalsAsync(Guid vehicleID);
        Task<bool> HasCurrentlyActiveRentalAsync(Guid vehicleID);
        Task<int> GetTotalActiveVehiclesAsync();
        Task<int> GetCurrentlyRentedVehiclesCountAsync();
        Task<List<(string VehicleLicense, int Count)>> GetTopVehiclesAsync(int top = 5);
    }
}

