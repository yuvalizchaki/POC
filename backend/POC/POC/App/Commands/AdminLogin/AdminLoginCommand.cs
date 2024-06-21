using MediatR;
using POC.Contracts.Auth;

namespace POC.App.Commands.AdminLogin;


public class AdminLoginCommand(LoginPostDto loginPostDto) : IRequest<String>
{
    public LoginPostDto LoginPostDto { get; } = loginPostDto;
}