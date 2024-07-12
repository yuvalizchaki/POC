using MediatR;

namespace POC.App.Commands.NotifyScreenDisonnected;

public class NotifyScreenDisconnectedCommand(int screenId) : IRequest
{
    public int ScreenId { get; } = screenId;
}
