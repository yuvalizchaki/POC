using MediatR;
using POC.Api.Hubs;

namespace POC.App.Commands.NotifyScreenConnected;

public class NotifyScreenConnectedHandler(AdminHub adminHub): IRequestHandler<NotifyScreenConnectedCommand>
{
    public async Task Handle(NotifyScreenConnectedCommand request, CancellationToken cancellationToken)
    {
        await adminHub.NotifyScreenConnected(request.ScreenId);
    }
}