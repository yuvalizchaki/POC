using System.ComponentModel.DataAnnotations;
using POC.Contracts.Screen;

namespace POC.Contracts.ScreenProfile;

public class UpdateScreenProfileDto
{
    // TODO: Implement CRM adapter and related classes
    [Required]
    public string Name { get; set; }

    [Required]
    public int CompanyId { get; set; } = 1;
    
    [Required]
    public ScreenProfileFilteringDto ScreenProfileFiltering { get; set; } = null!;
}