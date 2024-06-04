using POC.Infrastructure.Common.Constants;
using POC.Infrastructure.Models;

namespace POC.Contracts.CrmDTOs;

public class OrderDto
{
    // TODO: Implement CRM adapter and related classes

    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public int CustomerId { get; set; }
    public string ClientName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime? DepartDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public OrderStatus Status { get; set; }
    public bool IsPickup { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    
    // Navigation properties
    public List<InventoryItemDto> InventoryItems { get; set; }
    public List<ServiceItemDto> ServiceItems { get; set; }
    public List<PeopleOrderItemDto> PeopleOrderItems { get; set; }
    public List<OneTimeOrderItemDto> OneTimeOrderItems { get; set; }
}