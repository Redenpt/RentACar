using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class CustomerViewModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [MaxLength(150, ErrorMessage = "O nome completo não pode ter mais de 150 caracteres.")]
        [Display(Name = "Nome Completo")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        [MaxLength(150, ErrorMessage = "O email não pode ter mais de 150 caracteres.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [MaxLength(20, ErrorMessage = "O telefone não pode ter mais de 20 caracteres.")]
        [RegularExpression(@"^\+?\d{9,15}$", ErrorMessage = "Telefone inválido.")]
        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "A carta de condução é obrigatória.")]
        [MaxLength(20, ErrorMessage = "A carta de condução não pode ter mais de 20 caracteres.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "A carta de condução só pode conter números.")]
        [Display(Name = "Carta de Condução")]
        public string DrivingLicense { get; set; }
    }
}
