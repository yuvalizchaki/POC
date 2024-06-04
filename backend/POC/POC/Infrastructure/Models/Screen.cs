using System.ComponentModel.DataAnnotations;

namespace POC.Infrastructure.Models;

public class Screen
{
    [Key]
    public int Id { get; set; }
    
    // public string IpAddress { get; set; }
    
    //foreign key
    public int ScreenProfileId { get; set; }
    public string? HashToken { get; set; }
    public ScreenProfile ScreenProfile { get; set; }
   

}