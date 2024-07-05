using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace POC.Infrastructure.Common.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidateEnumAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is not Enum)
        {
            return new ValidationResult("Invalid enum type");
        }

        var enumType = value.GetType();
        if (!Enum.IsDefined(enumType, value))
        {
            return new ValidationResult("Invalid enum value");
        }

        return ValidationResult.Success;
    }
}

//list of enum
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidateUnitEnumListAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is not IEnumerable)
        {
            return new ValidationResult("Invalid type");
        }

        var enumType = value.GetType().GetGenericArguments()[0];
        foreach (var item in (IEnumerable)value)
        {
            if (!Enum.IsDefined(enumType, item))
            {
                return new ValidationResult("Invalid enum value in the list");
            }
        }

        return ValidationResult.Success;
    }
}