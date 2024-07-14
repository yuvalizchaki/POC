namespace POC.Contracts.CrmDTOs;

public class AdminLoginDto
{
    public string Username { get; set; }
    public int CompanyId { get; set; }
    public string token { get; set; }
}