using MediatR;
using POC.Contracts.ScreenProfile;

namespace POC.App.Commands.DeleteScreenProfile;

public class DeleteScreenProfileCommand(int id) : IRequest
{
    public int Id { get; } = id;
}