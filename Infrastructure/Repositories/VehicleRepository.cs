using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vehicle>> GetAllAsync(bool includeInactive = false)
        {
            if (includeInactive)
                return await _context.Vehicles.ToListAsync();

            return await _context.Vehicles
                                 .Where(v => v.IsActive)
                                 .ToListAsync();
        }

        public async Task<Vehicle?> GetByIdAsync(Guid ID)
        {
            return await _context.Vehicles
                                 .FirstOrDefaultAsync(v => v.ID == ID && v.IsActive);
        }

        public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate)
        {
            licensePlate = licensePlate.Trim().ToUpper();

            return await _context.Vehicles
                                 .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate && v.IsActive);
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }

        // Soft delete
        public async Task DeleteAsync(Guid ID)
        {
            var vehicle = await _context.Vehicles.FindAsync(ID);
            if (vehicle != null)
            {
                _context.Vehicles.Update(vehicle);
                await _context.SaveChangesAsync();
            }
        }

        //pode ignorar o proprio veículo
        public async Task<bool> LicensePlateExistsAsync(string licensePlate, Guid? vehicleID = null)
        {
            return await _context.Vehicles
                .AnyAsync(v => v.IsActive
                            && v.LicensePlate.ToUpper() == licensePlate.ToUpper()
                            && (vehicleID == null || v.ID != vehicleID));
        }

        public async Task<bool> HasActiveRentalsAsync(Guid vehicleId)
        {
            var today = DateTime.Today;
            return await _context.RentalContracts
                .AnyAsync(rc => rc.VehicleID == vehicleId
                             && rc.EndDate >= today
                             && rc.IsActive);
        }

        public async Task<bool> HasCurrentlyActiveRentalAsync(Guid vehicleId)
        {
            var today = DateTime.Today;
            return await _context.RentalContracts
                .AnyAsync(rc => rc.VehicleID == vehicleId
                             && rc.StartDate <= today   
                             && rc.EndDate >= today     
                             && rc.IsActive);           
        }

        public async Task<int> GetTotalActiveVehiclesAsync()
        {
            return await _context.Vehicles
                                 .Where(v => v.IsActive)
                                 .CountAsync();
        }

        public async Task<int> GetCurrentlyRentedVehiclesCountAsync()
        {
            var today = DateTime.Today;

            return await _context.RentalContracts
                .Where(rc => rc.IsActive
                          && rc.StartDate <= today
                          && rc.EndDate >= today)
                .Select(rc => rc.VehicleID)
                .Distinct()
                .CountAsync();
        }

        public async Task<List<(string VehicleLicense, int Count)>> GetTopVehiclesAsync(int top = 5)
        {
            return await _context.RentalContracts
                .Where(rc => rc.IsActive)
                .GroupBy(rc => rc.Vehicle.LicensePlate)
                .Select(g => new { VehicleLicense = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(top)
                .AsNoTracking()
                .ToListAsync()
                .ContinueWith(t => t.Result.Select(x => (x.VehicleLicense, x.Count)).ToList());
        }
    }
}
