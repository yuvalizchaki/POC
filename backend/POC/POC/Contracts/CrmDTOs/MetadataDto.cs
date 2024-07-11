using POC.Infrastructure.Models;

namespace POC.Contracts.CrmDTOs;

public class MetadataDto
{
    public DisplayConfig DisplayConfig { get; set; }
    public bool IsInventory { get; set; }
    
    public string Name { get; set; }
    
    public int ScreenId { get; set; }
    
}