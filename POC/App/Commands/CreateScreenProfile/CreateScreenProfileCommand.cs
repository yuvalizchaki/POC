using POC.Contracts.ScreenProfile;

namespace POC.App.Commands.CreateScreenProfile;

using MediatR;

public class CreateScreenProfileCommand : IRequest<ScreenProfileDto>
{
    public CreateScreenProfileDto CreateScreenProfileDto { get; }

    public CreateScreenProfileCommand(CreateScreenProfileDto createScreenProfileDto)
    {
        CreateScreenProfileDto = createScreenProfileDto;
    }
}