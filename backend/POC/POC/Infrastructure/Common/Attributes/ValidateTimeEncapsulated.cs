using System.ComponentModel.DataAnnotations;
using POC.Contracts.ScreenProfile;

namespace POC.Infrastructure.Common.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidateTimeEncapsulatedDto : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not TimeEncapsulatedDto timeEncapsulated)
        {
            return new ValidationResult("Invalid TimeEncapsulated type");
        }

        if (timeEncapsulated.From == null && timeEncapsulated.To == null)
        {
            return new ValidationResult("From or To is required");
        }

        return ValidationResult.Success;
    }
}