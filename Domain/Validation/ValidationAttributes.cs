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
            var regex = @"^(?:[A-Z]{2}-\d{2}-\d{2}|\d{2}-\d{2}-[A-Z]{2}|\d{2}-[A-Z]{2}-\d{2}|[A-Z]{2}-\d{2}-[A-Z]{2})$";

            if (!Regex.IsMatch(licensePlate, regex))
                return new ValidationResult("Matrícula inválida. Deve contar um dos seguintes formatos: AA-00-00, 00-00-AA, AA-00-AA ou 00-AA-00.");
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

    public class NotPastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success; 

            if (value is not DateTime date)
                return new ValidationResult("Tipo de data inválido.");

            if (date.Date < DateTime.Today)
            {
                return new ValidationResult("A data não pode ser inferior ao dia atual");
            }

            return ValidationResult.Success;
        }
    }

    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var currentValue = (DateTime)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                return new ValidationResult($"Propriedade desconhecida: {_comparisonProperty}");

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);

            if (comparisonValue == null)
                return ValidationResult.Success;

            var comparisonDate = (DateTime)comparisonValue;

            if (currentValue < comparisonDate)
            {
                return new ValidationResult(ErrorMessage ?? "A data de fim de aluguer deve ser superior á data de inicio de aluguer.");
            }

            return ValidationResult.Success;
        }
    }
}
