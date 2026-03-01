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
        Task<bool> EmailExistsAsync(string email, Guid? userId = null);
        Task<bool> DriverLicenseExistsAsync(string driverLicense, Guid? customerID = null);
        Task<bool> HasActiveRentalsAsync(Guid customerID);
        Task<int> GetTotalActiveCustomersAsync();
        Task<List<(string CustomerName, int Count)>> GetTopCustomersAsync(int top = 5);
    }
}

