using System;
using System.ComponentModel.DataAnnotations;

namespace PAService.Helpers.Validators
{
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
            ErrorMessage = "Дата завершения должна быть позже даты активации";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = value as DateTime?;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property is null)
                return new ValidationResult($"Свойство с именем {_comparisonProperty} не найдено");

            var comparisonValue = property.GetValue(validationContext.ObjectInstance) as DateTime?;

            if (currentValue.HasValue && comparisonValue.HasValue)
            {
                if (currentValue <= comparisonValue)
                    return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
