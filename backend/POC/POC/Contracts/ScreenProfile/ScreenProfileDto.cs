using POC.Contracts.Screen;

namespace POC.Contracts.ScreenProfile;

public class ScreenProfileDto
{
    // TODO: Implement CRM adapter and related classes

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<ScreenDto> Screens { get; set; } = [];

}
