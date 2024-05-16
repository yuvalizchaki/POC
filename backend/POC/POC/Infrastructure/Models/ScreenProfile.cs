using System.ComponentModel.DataAnnotations;

namespace POC.Infrastructure.Models;

public class ScreenProfile
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Screen> Screens { get; set; } = [];
}