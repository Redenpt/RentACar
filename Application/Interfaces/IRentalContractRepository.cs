using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRentalContractRepository
    {
        Task<List<RentalContract>> GetAllAsync(bool includeInactive = false);            
        Task<RentalContract> GetByIdAsync(Guid id);
        Task<List<RentalContract>> GetAllActiveContractsAsync();
        Task AddAsync(RentalContract vehicle);              
        Task UpdateAsync(RentalContract vehicle);           
        Task DeleteAsync(Guid id);
        Task<bool> IsVehicleAvailableAsync(Guid vehicleId, DateTime startDate, DateTime endDate, Guid? currentContractId = null);
        Task<int> GetActiveContractsAsync();
        Task<int> GetScheduledContractsAsync();
        Task<List<RentalContract>> GetRentalContractsEndingNextDaysAsync(int days = 7);
    }
}
