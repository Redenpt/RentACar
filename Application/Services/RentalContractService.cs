using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    public class RentalContractService
    {
        private readonly IRentalContractRepository _rentalContractRepository;

        public RentalContractService(IRentalContractRepository rentalContractRepository)
        {
            _rentalContractRepository = rentalContractRepository;
        }

        public Task<List<RentalContract>> GetAllRentalContractsAsync()
        {
            return _rentalContractRepository.GetAllAsync();
        }

        public async Task<RentalContract> GetRentalContractByIdAsync(Guid ID)
        {
            var rentalContract = await _rentalContractRepository.GetByIdAsync(ID);
            if (rentalContract == null || !rentalContract.IsActive)
                throw new EntityNotFoundException("Contrato de Aluguer");

            return rentalContract;
        }

        public async Task AddRentalContractAsync(RentalContract rentalContract)
        {
            if (!await _rentalContractRepository.IsVehicleAvailableAsync(rentalContract.VehicleID, rentalContract.StartDate, rentalContract.EndDate))
                throw new VehicleAlreadyRentedException();

            await _rentalContractRepository.AddAsync(rentalContract);
        }

        public async Task UpdateRentalContractAsync(RentalContract rentalContract)
        {
            if (!rentalContract.IsActive)
                throw new EntityInactiveException("Contrato de Aluguer");

            if (!await _rentalContractRepository.IsVehicleAvailableAsync(rentalContract.VehicleID, rentalContract.StartDate, rentalContract.EndDate, rentalContract.ID))
                throw new VehicleAlreadyRentedException();

            rentalContract.LastUpdate();
            await _rentalContractRepository.UpdateAsync(rentalContract);
        }

        //  (soft delete )
        public async Task DeleteRentalContractAsync(Guid ID)
        {
            var rentalContract = await _rentalContractRepository.GetByIdAsync(ID);
            if (rentalContract == null)
                throw new EntityNotFoundException("Contrato de Aluguer");

            if (!rentalContract.IsActive)
                throw new EntityInactiveException("Contrato de Aluguer");

            rentalContract.SoftDelete();
            await _rentalContractRepository.DeleteAsync(rentalContract.ID);
        }

        public async Task<int> GetActiveContractsAsync()
        {
            return await _rentalContractRepository.GetActiveContractsAsync();
        }

        public async Task<int> GetScheduledContractsAsync()
        {
            return await _rentalContractRepository.GetScheduledContractsAsync();
        }

        public async Task<List<RentalContract>> GetRentalContractsEndingNextDaysAsync()
        {
            return await _rentalContractRepository.GetRentalContractsEndingNextDaysAsync();
        }
    }
}
