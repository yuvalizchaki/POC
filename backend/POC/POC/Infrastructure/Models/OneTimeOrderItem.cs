using System.ComponentModel.DataAnnotations;

namespace POC.Infrastructure.Models;

public class OneTimeOrderItem
{
    [Key]
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; }

    // Foreign key
    public int OrderId { get; set; }
    public Order Order { get; set; }
}