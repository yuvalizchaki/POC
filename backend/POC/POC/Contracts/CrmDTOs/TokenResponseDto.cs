namespace POC.Contracts.CrmDTOs
{
    // TODO: Implement CRM adapter and related classes

    public class TokenResponseDto
    {
        public required string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public required string TokenType { get; set; }
        public required string Scope { get; set; }
    }
}