using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities;
using Domain.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RentACar.Models
{
    public class RentalContractViewModel
    {
        public Guid ID { get; set; } 

        [Required(ErrorMessage = "O cliente é obrigatório.")]
        [Display(Name = "Cliente")]
        public Guid? CustomerID { get; set; }

        [Required(ErrorMessage = "O veículo é obrigatório.")]
        [Display(Name = "Veículo")]
        public Guid? VehicleID { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        [NotPastDate(ErrorMessage = "A data de início não pode ser no passado.")]
        [Display(Name = "Data de Início")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "A data de fim é obrigatória.")]
        [DateGreaterThan(nameof(StartDate), ErrorMessage = "A data de fim deve ser posterior à data de início.")]
        [Display(Name = "Data de Fim")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "A quilometragem inicial é obrigatória.")]
        [Display(Name = "Quilometragem Inicial")]
        [Range(0, int.MaxValue, ErrorMessage = "A quilometragem inicial não pode ser negativa.")]
        public int? InitialMileage { get; set; }

        public RentalStatus Status { get; set; }

        [BindNever]
        public CustomerViewModel? Customer { get; set; }
        [BindNever]
        public VehicleViewModel? Vehicle { get; set; }
    }
}
