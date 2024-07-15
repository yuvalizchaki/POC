namespace POC.Contracts.Admin;

public class PopupMessageDto
{
    public string Message { get; set; }
    public DateTime Time { get; set; }
    public string SenderName { get; set; }
    public int DisplayTime { get; set; }
}