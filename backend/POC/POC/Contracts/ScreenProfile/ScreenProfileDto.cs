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

public class ScreenProfileFilteringDto
{
    public OrderTimeRangeDto OrderTimeRange { get; set; } = null!;
    public List<OrderStatus>? OrderStatusses { get; set; }
    public TriState IsPickup { get; set; }
    public TriState IsSale { get; set; } 
    public List<int>? EntityIds { get; set; }
}

public class OrderTimeRangeDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}