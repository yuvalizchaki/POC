using System.ComponentModel.DataAnnotations;

namespace POC.Infrastructure.Models;

public class ServiceItem
{
    [Key]
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public int Quantity { get; set; }
    public int ServiceProfileId { get; set; }
    public int ServiceId { get; set; }
    public string ServiceProfileName { get; set; }
    public string ServiceProfileDescription { get; set; }
    public bool IsBundle { get; set; }
    public List<string> ServiceProfileImages { get; set; }
    public int? Icon { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }

    // Foreign key
    public int OrderId { get; set; }
    public Order Order { get; set; }
}