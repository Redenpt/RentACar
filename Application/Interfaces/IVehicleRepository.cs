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
        Task<bool> LicensePlateExistsAsync(string licensePlate);
        Task<bool> LicensePlateExistsAsync(string licensePlate, Guid vehicleID);
    }
}
