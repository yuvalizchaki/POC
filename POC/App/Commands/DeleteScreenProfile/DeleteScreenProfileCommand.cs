using MediatR;
using POC.Contracts.ScreenProfile;

namespace POC.App.Commands.DeleteScreenProfile;

public class DeleteScreenProfileCommand: IRequest
{
    public int Id { get; }
    public DeleteScreenProfileCommand(int id)
    {
        Id = id;
    }
}