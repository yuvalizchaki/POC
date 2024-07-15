using MediatR;
using POC.Contracts.Admin;

namespace POC.App.Commands.SendPopupMessage;

public class SendPopupMessageCommand(
    int profileId,
    string senderName,
    AdminMessageDto adminMessageDto
    ) : IRequest
{
    public int ProfileId { get; set; } = profileId;
    public string SenderName { get; set; } = senderName;
    public AdminMessageDto PopupMessage { get; set; } = adminMessageDto;
    
}
