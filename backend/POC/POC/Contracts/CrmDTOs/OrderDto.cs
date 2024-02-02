namespace POC.Contracts.CrmDTOs;

public class OrderDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string? ClientName { get; set; }
}