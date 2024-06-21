using System.ComponentModel.DataAnnotations;

namespace POC.Infrastructure.Models;

public class Admin
{
    [Key]
    public string Username { get; set; }
    
    public string HashedPassword { get; set; }
    public string CompanyId { get; set; }
}