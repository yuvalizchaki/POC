using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POC.App.Commands.CreateScreenProfile;
using POC.Contracts.Response;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Common.Validators;
using POC.Services;

namespace POC.Api.Controllers.ClientControllers;
[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase 
    {
    private readonly IMediator _mediator;
    private readonly ILogger<ScreenProfilesController> _logger;
    private const String AdminUsername = "admin";
    private const String AdminPassword = "admin";
    
    public AdminController(IMediator mediator, ILogger<ScreenProfilesController> logger)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(String username, String password)
    {   
        //TODO: Implement actual authentication
        if (username != AdminUsername || password != AdminPassword)
        {
            return Unauthorized();
        }
        String token = AuthService.GenerateAdminToken(username, password);
        return Ok(token);
    }
}