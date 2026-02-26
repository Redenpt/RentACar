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
    public class VehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            return _vehicleRepository.GetAllAsync();
        }

        public async Task<Vehicle> GetVehicleByIdAsync(Guid ID)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(ID);
            if (vehicle == null || !vehicle.IsActive)
                throw new EntityNotFoundException("Veículo");

            return vehicle;
        }

        public async Task<Vehicle> GetVehicleByLicensePlateAsync(string licensePlate)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
                throw new LicensePlateRequiredException();

            var vehicle = await _vehicleRepository.GetByLicensePlateAsync(licensePlate);

            if (vehicle == null || !vehicle.IsActive)
                throw new EntityNotFoundException("Veículo");

            return vehicle;
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            if (await _vehicleRepository.LicensePlateExistsAsync(vehicle.LicensePlate))
                throw new EntityAlreadyExistsException("Veículo", vehicle.LicensePlate);

            vehicle.LicensePlate = vehicle.LicensePlate.Trim().ToUpper(); //passar tudo para maiusculas (ex AA-00-BB)
            await _vehicleRepository.AddAsync(vehicle);
        }

        public Task UpdateVehicleAsync(Vehicle vehicle)
        {
            if (!vehicle.IsActive)
                throw new EntityInactiveException("Veículo");

            vehicle.LastUpdate();
            return _vehicleRepository.UpdateAsync(vehicle);
        }

        //  (soft delete )
        public async Task DeleteVehicleAsync(Guid ID)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(ID);
            if (vehicle == null)
                throw new EntityNotFoundException("Veículo");

            if (!vehicle.IsActive)
                throw new EntityInactiveException("Veículo");

            vehicle.SoftDelete();
            await _vehicleRepository.DeleteAsync(vehicle.ID);
        }
    }
}
