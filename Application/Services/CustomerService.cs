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
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<List<Customer>> GetAllCustomersAsync()
        {
            return _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid ID)
        {
            var customer = await _customerRepository.GetByIdAsync(ID);
            if (customer == null || !customer.IsActive)
                throw new EntityNotFoundException("Utilizador");

            return customer;
        }

        public async Task<Customer> GetByEmailAsync(string email)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);
            if (customer == null || !customer.IsActive)
                throw new EntityNotFoundException("Utilizador");

            return customer;
        }


        public async Task AddCustomerAsync(Customer customer)
        {
            if (await _customerRepository.EmailExistsAsync(customer.Email))
                throw new EntityAlreadyExistsException("Utilizador", customer.Email);

            if (await _customerRepository.DriverLicenseExistsAsync(customer.DrivingLicense))
                throw new EntityAlreadyExistsException("Utilizador", customer.DrivingLicense);

            customer.Email = customer.Email.Trim().ToLower(); //passar para minusculas
            await _customerRepository.AddAsync(customer);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            if (!customer.IsActive)
                throw new EntityInactiveException("Utilizador");

            if (await _customerRepository.EmailExistsAsync(customer.Email, customer.ID))
                throw new EntityAlreadyExistsException("Utilizador", customer.Email);

            if (await _customerRepository.DriverLicenseExistsAsync(customer.DrivingLicense, customer.ID))
                throw new EntityAlreadyExistsException("Utilizador", customer.DrivingLicense);

            customer.LastUpdate();
            await _customerRepository.UpdateAsync(customer);
        }

        //  (soft delete )
        public async Task DeleteCustomerAsync(Guid ID)
        {
            var customer = await _customerRepository.GetByIdAsync(ID);
            if (customer == null)
                throw new EntityNotFoundException("Utilizador");

            if (!customer.IsActive)
                throw new EntityInactiveException("Utilizador");

            customer.SoftDelete();
            await _customerRepository.DeleteAsync(customer.ID);
        }
    }
}
