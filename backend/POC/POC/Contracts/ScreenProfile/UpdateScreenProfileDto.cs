using POC.Contracts.Screen;

namespace POC.Contracts.ScreenProfile;

public class UpdateScreenProfileDto
{
    // TODO: Implement CRM adapter and related classes
    public string Name { get; set; } = null!;
    public int CompanyId { get; set; } = 1;
    public ScreenProfileFilteringDto ScreenProfileFiltering { get; set; } = null!;
}