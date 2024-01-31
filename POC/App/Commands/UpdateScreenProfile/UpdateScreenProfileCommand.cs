using MediatR;
using POC.Contracts.ScreenProfile;

namespace POC.App.Commands.UpdateScreenProfile;

public class UpdateScreenProfileCommand : IRequest<ScreenProfileDto>
{
    public int Id { get; }
    public UpdateScreenProfileDto ScreenProfile { get; }
    public UpdateScreenProfileCommand(int id, UpdateScreenProfileDto screenProfile)
    {
        Id = id;
        ScreenProfile = screenProfile;
    }
}