using System.ComponentModel.DataAnnotations;

namespace POC.Infrastructure.Models;

public class OneTimeOrderItemDto
{

    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; }
}