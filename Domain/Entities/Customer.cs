using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class Customer : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        [RegularExpression(@"^\d{9,15}$")]
        public string Phone { get; set; }

        [Required]
        [MaxLength(20)]
        public string DrivingLicense { get; set; }

        public ICollection<RentalContract> RentalContracts { get; set; }
    }
}
