using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POC.App.Commands.CreateScreenProfile;
using POC.Contracts.Auth;
using POC.Contracts.Response;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Common.Validators;
using POC.Services;

namespace POC.Api.Controllers.ClientControllers;
[ApiController]
[Route("[controller]")]
public class AdminController(IMediator mediator, ILogger<ScreenProfilesController> logger)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<ScreenProfilesController> _logger = logger;
    private const String AdminUsername = "admin";
    private const String AdminPassword = "admin";

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginPostDto loginDto)
    {   
        //TODO: Implement actual authentication
        if (loginDto.Username != AdminUsername || loginDto.Password != AdminPassword)
        {
            return Unauthorized();
        }
        String token = AuthService.GenerateAdminToken(loginDto.Username, loginDto.Password);
        return Ok(token);
    }

}