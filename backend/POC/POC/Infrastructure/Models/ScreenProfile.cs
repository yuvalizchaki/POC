namespace POC.Infrastructure.Models;

public class ScreenProfile
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Screen> Screens { get; set; } = [];
}