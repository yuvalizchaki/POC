using FluentValidation;
using POC.Contracts.ScreenProfile;

namespace POC.Infrastructure.Common.Validators;

public class UpdateScreenProfileDtoValidator : AbstractValidator<UpdateScreenProfileDto>
{
    public UpdateScreenProfileDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
    }
}