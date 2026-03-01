using Domain.Entities;

namespace RentACar.Models
{
    public class DashboardViewModel
    {
        public int TotalCustomers { get; set; }
        public int TotalVehicles { get; set; }
        public int ActiveContracts { get; set; }
        public int ScheduledContracts { get; set; }

        public int AvailableVehicles { get; set; }
        public int RentedVehicles { get; set; }

        public Dictionary<string, int> ContractsPerMonth { get; set; } = new();
        public List<RentalContract> ContractsEndingInNextDays { get; set; } = new();
        public List<(string Name, int Count)> TopCustomers { get; set; } = new();
        public List<(string Name, int Count)> TopVehicles { get; set; } = new();
    }
}
