using POC.Contracts.Screen;

namespace POC.Contracts.ScreenProfile;

public class UpdateScreenProfileDto
{
    public string Name { get; set; } = null!;

    public List<ScreenDto> Screens { get; set; } = new List<ScreenDto>();
}