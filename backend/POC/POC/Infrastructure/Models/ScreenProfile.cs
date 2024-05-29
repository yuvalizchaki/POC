using System.ComponentModel.DataAnnotations;

namespace POC.Infrastructure.Models;

public class ScreenProfile
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    // public ScreenProfileFiltering ScreenProfileFiltering { get; set; } = null;
    public List<Screen> Screens { get; set; } = [];
}


// public class ScreenProfileFiltering
// {
//     [Key]
//     public int Id { get; set; }

//     public OrderTimeRange OrderTimeRange { get; set; }
//     public List<OrderStatus>? OrderStatusses { get; set; }
//     public bool? IsPickup { get; set; } <-- true/false/don't care
//     public bool? IsSale { get; set; } <-- true/false/don't care
//     public List<int>? EntityIds { get; set; }
// }