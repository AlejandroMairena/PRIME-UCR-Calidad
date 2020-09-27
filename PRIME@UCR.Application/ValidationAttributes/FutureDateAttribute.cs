using System;
using System.ComponentModel.DataAnnotations;

namespace PRIME_UCR.Application.ValidationAttributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime) value;
            if (date.CompareTo(DateTime.Now) > 0)
            {
                return ValidationResult.Success;
            }
            
            return new ValidationResult($"Date must be in the future");
        }
    }
}