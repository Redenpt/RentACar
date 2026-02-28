using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync(bool includeInactive = false);            
        Task<Customer> GetByIdAsync(Guid id);
        Task<Customer> GetByEmailAsync(string email);
        Task AddAsync(Customer customer);              
        Task UpdateAsync(Customer customer);           
        Task DeleteAsync(Guid id);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> EmailExistsAsync(string email, Guid userId);
        Task<bool> DriverLicenseExistsAsync(string driverLicense);
        Task<bool> DriverLicenseExistsAsync(string driverLicense, Guid customerID);
    }
}
