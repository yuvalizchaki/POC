using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using POC.Infrastructure.Common.Constants;

namespace POC.Infrastructure.Models;

public class ScreenProfile
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CompanyId { get; set; } = 1;
    public ScreenProfileFiltering ScreenProfileFiltering { get; set; } = null;
    public List<Screen> Screens { get; set; } = [];
}


public class ScreenProfileFiltering
{
    public OrderTimeRange? OrderTimeRange { get; set; }
    public List<OrderStatus>? OrderStatusses { get; set; }
    public TriState IsPickup { get; set; }
    public TriState IsSale { get; set; } 
    public List<int>? EntityIds { get; set; }
}

public class OrderTimeRange
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}