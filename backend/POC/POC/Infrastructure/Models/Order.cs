using System.ComponentModel.DataAnnotations;
using POC.Infrastructure.Common.Constants;

namespace POC.Infrastructure.Models;

// public class Order
// {
//     [Key]
//     public int Id { get; set; }
//     public int DepartmentId { get; set; }
//     public int CustomerId { get; set; }
//     public string ClientName { get; set; }
//     public string FirstName { get; set; }
//     public string LastName { get; set; }
//     public DateTime StartDate { get; set; }
//     public DateTime EndDate { get; set; }
//     public DateTime? DepartDate { get; set; }
//     public DateTime? ReturnDate { get; set; }
//     public OrderStatus Status { get; set; }
//     public bool IsPickup { get; set; }
//     public DateTime CreatedOn { get; set; }
//     public DateTime? UpdatedOn { get; set; }
//
//     // Navigation properties
//     public List<InventoryItem> InventoryItems { get; set; }
//     public List<ServiceItem> ServiceItems { get; set; }
//     public List<PeopleOrderItem> PeopleOrderItems { get; set; }
//     public List<OneTimeOrderItem> OneTimeOrderItems { get; set; }
// }
public class Order
{
    [Key]
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string? ClientName { get; set; }
}
