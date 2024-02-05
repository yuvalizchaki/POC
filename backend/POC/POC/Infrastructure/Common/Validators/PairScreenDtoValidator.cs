using FluentValidation;
using POC.Contracts.Screen;

namespace POC.Infrastructure.Common.Validators;

public class PairScreenDtoValidator : AbstractValidator<PairScreenDto>
{
    public PairScreenDtoValidator()
    {
        RuleFor(x => x.IpAddress).NotEmpty().NotNull();
        RuleFor(x => x.ScreenProfileId).NotEmpty().NotNull();
    }
}