using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<RentalContract> RentalContracts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Relacionamento RentalContract -> Customer
            builder.Entity<RentalContract>()
                .HasOne(rc => rc.Customer)
                .WithMany(c => c.RentalContracts)
                .HasForeignKey(rc => rc.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento RentalContract -> Vehicle
            builder.Entity<RentalContract>()
                .HasOne(rc => rc.Vehicle)
                .WithMany(v => v.RentalContracts)
                .HasForeignKey(rc => rc.VehicleID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
