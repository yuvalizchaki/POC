using System.ComponentModel.DataAnnotations;

namespace POC.Contracts.Screen;

public class PairScreenDto
{
    [Required(ErrorMessage = "Pairing code is required.")]
    [StringLength(6, ErrorMessage = "Pairing code must be 6 characters long.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Pairing code must be a number.")]
    public string PairingCode { get; set; }
    
    public int ScreenProfileId { get; set; }
}