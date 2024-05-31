using System.ComponentModel.DataAnnotations;

namespace POC.Infrastructure.Models;

public class PeopleOrderItemDto
{
    public int Id { get; set; }
    public int PeopleProfileId { get; set; }
    public int DepartmentId { get; set; }
    public int PeopleId { get; set; }
    public string PeopleProfileName { get; set; }
    public string PeopleProfileDescription { get; set; }
    public bool IsBundle { get; set; }
    public List<string> PeopleProfileImages { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
}