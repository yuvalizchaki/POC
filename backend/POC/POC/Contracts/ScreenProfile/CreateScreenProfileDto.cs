using System.ComponentModel.DataAnnotations;

namespace POC.Contracts.ScreenProfile;

public class CreateScreenProfileDto
{
    // TODO: Implement CRM adapter and related classes

    [Required]
    [StringLength(20, ErrorMessage = "Name length can't be more than 20.")]
    public string Name { get; set; }
    
    [Required]
    public int CompanyId { get; set; }
    
    [Required]
    public ScreenProfileFilteringDto ScreenProfileFiltering { get; set; } 
    
}
