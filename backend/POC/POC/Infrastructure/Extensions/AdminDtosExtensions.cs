namespace POC.Infrastructure.Extensions;

//for AdminMessageDto
using POC.Contracts.Admin;

public static class AdminMessageDtoExtensions
{
    public static PopupMessageDto ToPopupMessageDto(this AdminMessageDto adminMessage, string senderName)
    {
        return new PopupMessageDto
        {
            Message = adminMessage.Message,
            SenderName = senderName,
            DisplayTime = adminMessage.DisplayTime,
            Time = DateTime.Now
        };
    }
}
