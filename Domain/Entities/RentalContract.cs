using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class RentalContract : BaseEntity
    {
        [Required]
        public Guid CustomerID { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public Guid VehicleID { get; set; }
        public Vehicle Vehicle { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int InitialMileage { get; set; }
    }
}
