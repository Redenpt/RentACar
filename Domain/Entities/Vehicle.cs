using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Microsoft.VisualBasic.FileIO;

namespace Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [Required]
        [MaxLength(15)]
        public string LicensePlate { get; set; }

        [Required]
        [Range(1900, 2100)]
        public int Year { get; set; }

        [Required]
        public FuelType FuelType { get; set; }

        public bool IsAvailable { get; set; } = true;

        public ICollection<RentalContract> RentalContracts { get; set; }
    }
}
