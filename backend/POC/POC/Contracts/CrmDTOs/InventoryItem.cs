using System.ComponentModel.DataAnnotations;

namespace POC.Infrastructure.Models;

public class InventoryItemDto
{

    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public bool IsBundle { get; set; }
    public List<string> ProductImages { get; set; }
    
}