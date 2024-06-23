using System.Security.Cryptography;
using System.Text;
using MediatR;
using POC.Infrastructure.Common.Exceptions;
using POC.Infrastructure.Models;
using POC.Infrastructure.Repositories;
using POC.Services;

namespace POC.App.Commands.AdminLogin;

public class AdminLoginCommandHandler(AdminRepository repository)
    : IRequestHandler<AdminLoginCommand, String>
{
    public async Task<String> Handle(AdminLoginCommand request, CancellationToken cancellationToken)
    {
        // Hash the password
        byte[] result = SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(request.LoginPostDto.Password));
        String hashedPassword = Encoding.Default.GetString(result);
        
        // Get the admin by username and password
        var admin = await repository.GetByUserNameAndPassAsync(request.LoginPostDto.Username, hashedPassword);
        if (admin == null)
        {
            throw new AdminLoginException("Invalid email or password");
        }
        
        String token = AuthService.GenerateAdminToken(request.LoginPostDto.Username, request.LoginPostDto.Password, int.Parse(admin.CompanyId));
        return token;
    }
    
}