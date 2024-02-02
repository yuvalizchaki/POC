using POC.Contracts.ScreenProfile;

namespace POC.App.Commands.CreateScreenProfile;

using MediatR;

public class CreateScreenProfileCommand(CreateScreenProfileDto createScreenProfileDto) : IRequest<ScreenProfileDto>
{
    public CreateScreenProfileDto CreateScreenProfileDto { get; } = createScreenProfileDto;
}