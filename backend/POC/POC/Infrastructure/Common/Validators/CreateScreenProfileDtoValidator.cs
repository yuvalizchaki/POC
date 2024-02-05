using FluentValidation;
using POC.Contracts.ScreenProfile;

namespace POC.Infrastructure.Common.Validators;

public class CreateScreenProfileDtoValidator : AbstractValidator<CreateScreenProfileDto>
{
    public CreateScreenProfileDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
    }
}