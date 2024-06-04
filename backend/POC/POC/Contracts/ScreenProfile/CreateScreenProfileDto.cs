namespace POC.Contracts.ScreenProfile;

public class CreateScreenProfileDto
{
    // TODO: Implement CRM adapter and related classes

    public string Name { get; set; }
    public int CompanyId { get; set; } = 1;
    public ScreenProfileFilteringDto ScreenProfileFiltering { get; set; }
    
}
