using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RentalContractRepository : IRentalContractRepository
    {
        private readonly ApplicationDbContext _context;

        public RentalContractRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RentalContract>> GetAllAsync(bool includeInactive = false)
        {
            var query = _context.RentalContracts
                                .AsQueryable();

            if (!includeInactive)
                query = query.Where(rc => rc.IsActive);

            query = query.Include(rc => rc.Customer)
                         .Include(rc => rc.Vehicle);

            return await query.ToListAsync();
        }

        public async Task<RentalContract?> GetByIdAsync(Guid ID)
        {
            return await _context.RentalContracts
                                 .FirstOrDefaultAsync(rc => rc.ID == ID && rc.IsActive);
        }

        public async Task<List<RentalContract>> GetAllActiveContractsAsync()
        {
            var today = DateTime.Today;

            return await _context.RentalContracts
                                 .Include(rc => rc.Vehicle)
                                 .Include(rc => rc.Customer)
                                 .Where(rc => rc.StartDate <= today && rc.EndDate >= today)
                                 .ToListAsync();
        }

        public async Task AddAsync(RentalContract rentalContract)
        {
            _context.RentalContracts.Add(rentalContract);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RentalContract rentalContract)
        {
            _context.RentalContracts.Update(rentalContract);
            await _context.SaveChangesAsync();
        }

        // Soft delete
        public async Task DeleteAsync(Guid ID)
        {
            var rentalContract = await _context.RentalContracts.FindAsync(ID);
            if (rentalContract != null)
            {
                _context.RentalContracts.Update(rentalContract);
                await _context.SaveChangesAsync();
            }
        }

        //pode ignorar o proprio contrato
        public async Task<bool> IsVehicleAvailableAsync(Guid vehicleId, DateTime startDate, DateTime endDate, Guid? currentContractId = null)
        {
            return !await _context.RentalContracts
                                  .AnyAsync(rc => rc.VehicleID == vehicleId
                                                 && rc.IsActive
                                                 && rc.StartDate <= endDate
                                                 && rc.EndDate >= startDate
                                                 && (currentContractId == null || rc.ID != currentContractId)
                                  );
        }

        public async Task<int> GetActiveContractsAsync()
        {
            var now = DateTime.Now;

            return await _context.RentalContracts
                .Where(rc => rc.IsActive &&
                             rc.StartDate <= now &&
                             rc.EndDate >= now)
                .CountAsync();
        }


        public async Task<int> GetScheduledContractsAsync()
        {
            var now = DateTime.Now;

            return await _context.RentalContracts
                .Where(rc => rc.IsActive &&
                             rc.StartDate > now)
                .CountAsync();
        }

        public async Task<List<RentalContract>> GetRentalContractsEndingNextDaysAsync(int days = 7)
        {
            var today = DateTime.Today;
            var endDateLimit = today.AddDays(days);

            return await _context.RentalContracts
                .Where(rc => rc.IsActive
                          && rc.EndDate >= today
                          && rc.EndDate <= endDateLimit)
                .OrderBy(rc => rc.EndDate)
                .Include(rc => rc.Vehicle)
                .Include(rc => rc.Customer)
                .ToListAsync();
        }
    }
}
