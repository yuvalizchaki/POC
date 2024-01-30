namespace POC.Infrastructure.Models;

public class Screen
{
    public int Id { get; set; }
    public string IpAddress { get; set; }
    
    public int ScreenProfileId { get; set; }
    public ScreenProfile ScreenProfile { get; set; }
}