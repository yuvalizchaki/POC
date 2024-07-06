using System.ComponentModel.DataAnnotations;

namespace POC.Contracts.ScreenProfile;

public class CreateScreenProfileDto
{
    // TODO: Implement CRM adapter and related classes

    [Required]
    public string Name { get; set; }
    
    [Required]
    public int CompanyId { get; set; } = 1;
    
    [Required]
    public ScreenProfileFilteringDto ScreenProfileFiltering { get; set; } 
    
}
