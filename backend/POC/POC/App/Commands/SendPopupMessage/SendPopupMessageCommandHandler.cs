using MediatR;
using POC.Api.Hubs;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.SendPopupMessage;

public class SendPopupMessageCommandHandler(
    ScreenProfileRepository repository,
    ScreenHub screenHub
    ) : IRequestHandler<SendPopupMessageCommand>
{
    public async Task Handle(SendPopupMessageCommand request, CancellationToken cancellationToken)
    {
        var relevantProfile = await repository.GetByIdAsync(request.ProfileId);
        
        if (relevantProfile == null)
        {
            throw new Exception("Profile not found for this admin");
        }
        
        var screenIds = relevantProfile.Screens.Select(s => s.Id).ToArray();
        
        var popupMessage = request.PopupMessage.ToPopupMessageDto(request.SenderName);

        await screenHub.SendPopupMessage(screenIds, popupMessage);
    }
}