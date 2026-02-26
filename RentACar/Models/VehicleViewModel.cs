using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace RentACar.Models
{
    public class VehicleViewModel
    {
        public Guid ID { get; set; } 

        [Required(ErrorMessage = "A marca é obrigatória.")]
        [MaxLength(50, ErrorMessage = "A marca não pode ter mais de 50 caracteres.")]
        [Display(Name = "Marca")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O modelo não pode ter mais de 50 caracteres.")]
        [Display(Name = "Modelo")]
        public string Model { get; set; }

        [Required(ErrorMessage = "A matrícula é obrigatória.")]
        [MaxLength(15, ErrorMessage = "A matrícula não pode ter mais de 15 caracteres.")]
        [Display(Name = "Matrícula")]
        public string LicensePlate { get; set; }

        [Required(ErrorMessage = "O ano é obrigatório.")]
        [Display(Name = "Ano")]
        public int Year { get; set; }

        [Required(ErrorMessage = "O tipo de combustível é obrigatório.")]
        [Display(Name = "Combustível")]
        public FuelType FuelType { get; set; }
    }
}
