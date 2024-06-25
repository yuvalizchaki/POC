using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using POC.Api.Helpers;
using POC.Infrastructure.Models;

namespace POC.Services;

public class AuthService
{
    public const int TokenExpirationDays = 60;
    public static string GenerateScreenToken(Screen screen, int companyId)
    {
        return GenerateToken(GenerateScreenClaims(screen, companyId));

    }
    public static string GenerateAdminToken(String username, String password, int companyId)
    {
        return GenerateToken(GenerateAdminClaims(username, password, companyId));
    }
    private static string GenerateToken(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthSettings.PrivateKey));
        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256Signature);
        
        var token = new JwtSecurityToken(AuthSettings.Issuer, 
            AuthSettings.Audience, 
            claims, 
            expires: DateTime.Now.AddDays(TokenExpirationDays), 
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private static Claim[] GenerateScreenClaims(Screen screen, int companyId)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Role, "Screen"),
            new Claim("ScreenId", screen.Id.ToString()),
            new Claim("CompanyId", companyId.ToString()), 
            new Claim("ScreenProfileId", screen.ScreenProfileId.ToString())
        };
        return claims;
    }
    private static Claim[] GenerateAdminClaims(String username, String password, int companyId)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim("CompanyId", companyId.ToString())
          //  new Claim("Password", password) 
        };
        return claims;
    }
}