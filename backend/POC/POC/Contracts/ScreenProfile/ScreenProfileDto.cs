using Microsoft.VisualBasic;
using POC.Contracts.Screen;
using POC.Infrastructure.Common.Constants;

namespace POC.Contracts.ScreenProfile;

public class ScreenProfileDto
{
    // TODO: Implement CRM adapter and related classes

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int CompanyId { get; set; } = 1;

    public List<ScreenDto> Screens { get; set; } = [];
    
    public ScreenProfileFilteringDto ScreenProfileFiltering { get; set; } = null!;

}
