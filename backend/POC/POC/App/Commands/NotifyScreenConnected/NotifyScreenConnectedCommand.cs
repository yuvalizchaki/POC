using MediatR;

namespace POC.App.Commands.NotifyScreenConnected;

public class NotifyScreenConnectedCommand(int screenId) : IRequest
{
    public int ScreenId { get; } = screenId;
}