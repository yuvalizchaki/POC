using POC.Contracts.Screen;

namespace POC.Contracts.ScreenProfile;

public class ScreenProfileDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public List<ScreenDto> Screens { get; set; }
    
}
