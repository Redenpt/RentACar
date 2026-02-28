using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Validation
{
    public class LicensePlateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            string licensePlate = value.ToString()!.Trim();
            var regex = @"^(?:[A-Z]{2}-\d{2}-\d{2}|\d{2}-\d{2}-[A-Z]{2}|\d{2}-[A-Z]{2}-\d{2})$";

            if (!Regex.IsMatch(licensePlate, regex))
                return new ValidationResult("Matrícula inválida. Deve contar um dos seguintes formatos: AA-00-00, 00-00-AA ou 00-AA-00.");
            return ValidationResult.Success;
        }
    }

    public class YearRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            int year = (int)value;
            if (year < 1900 || year > DateTime.Now.Year)
                return new ValidationResult($"Ano deve estar entre 1900 e {DateTime.Now.Year}.");
            return ValidationResult.Success;
        }
    }
}
