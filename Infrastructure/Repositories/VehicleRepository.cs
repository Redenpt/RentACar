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

        public async Task<bool> LicensePlateExistsAsync(string licensePlate)
        {
            return await _context.Vehicles
                .AnyAsync(v => 
                            v.IsActive &&
                            v.LicensePlate.ToUpper() == licensePlate.ToUpper());
        }

        //para ignorar o proprio veículo
        public async Task<bool> LicensePlateExistsAsync(string licensePlate, Guid vehicleID)
        {
            return await _context.Vehicles
                .AnyAsync(v => 
                            v.IsActive &&
                            v.LicensePlate.ToUpper() == licensePlate.ToUpper() &&
                            v.ID != vehicleID);
        }
    }
}
