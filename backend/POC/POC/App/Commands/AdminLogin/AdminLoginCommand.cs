using MediatR;
using POC.Contracts.Auth;
using POC.Contracts.CrmDTOs;

namespace POC.App.Commands.AdminLogin;


public class AdminLoginCommand(LoginPostDto loginPostDto) : IRequest<AdminLoginDto>
{
    public LoginPostDto LoginPostDto { get; } = loginPostDto;
}