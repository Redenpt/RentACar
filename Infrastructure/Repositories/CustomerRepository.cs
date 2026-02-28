using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllAsync(bool includeInactive = false)
        {
            if (includeInactive)
                return await _context.Customers.ToListAsync();

            return await _context.Customers
                                 .Where(c => c.IsActive)
                                 .ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid ID)
        {
            return await _context.Customers
                                 .FirstOrDefaultAsync(c => c.ID == ID && c.IsActive);
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            email = email.Trim().ToLower();

            return await _context.Customers
                                 .FirstOrDefaultAsync(c => c.Email == email && c.IsActive);
        }

        public async Task AddAsync(Customer Customer)
        {
            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer Customer)
        {
            _context.Customers.Update(Customer);
            await _context.SaveChangesAsync();
        }

        // Soft delete
        public async Task DeleteAsync(Guid ID)
        {
            var Customer = await _context.Customers.FindAsync(ID);
            if (Customer != null)
            {
                _context.Customers.Update(Customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Customers
                .AnyAsync(c => c.IsActive && c.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> DriverLicenseExistsAsync(string driverLicense)
        {
            return await _context.Customers
                .AnyAsync(c => c.IsActive && c.DrivingLicense.ToLower() == driverLicense.ToLower());
        }

        //para ignorar o proprio utilizador
        public async Task<bool> EmailExistsAsync(string email, Guid customerID)
        {
            return await _context.Customers
                .AnyAsync(c => c.IsActive
                            && c.Email.ToLower() == email.ToLower()
                            && c.ID != customerID); 
        }

        //para ignorar o proprio utilizador
        public async Task<bool> DriverLicenseExistsAsync(string driverLicense, Guid customerID)
        {
            return await _context.Customers
                .AnyAsync(c => c.IsActive
                            && c.DrivingLicense.ToLower() == driverLicense.ToLower()
                            && c.ID != customerID); 
        }
    }
}
