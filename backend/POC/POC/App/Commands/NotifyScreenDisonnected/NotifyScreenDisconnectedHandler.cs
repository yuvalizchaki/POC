using MediatR;
using POC.Api.Hubs;

namespace POC.App.Commands.NotifyScreenDisonnected;

public class NotifyScreenDisconnectedHandler(AdminHub adminHub): IRequestHandler<NotifyScreenDisconnectedCommand>
{
    public async Task Handle(NotifyScreenDisconnectedCommand request, CancellationToken cancellationToken)
    {
        await adminHub.NotifyScreenDisonnected(request.ScreenId);
    }
}