using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Validation;
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
        public string VehicleModel { get; set; }

        [Required]
        [MaxLength(15)]
        [LicensePlate]
        public string LicensePlate { get; set; }

        [Required]
        [YearRange]
        public int? Year { get; set; }

        [Required]
        public FuelType? FuelType { get; set; }

        public ICollection<RentalContract> RentalContracts { get; set; }
    }
}
