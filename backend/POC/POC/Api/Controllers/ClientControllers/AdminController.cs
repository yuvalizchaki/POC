using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POC.App.Commands.AdminLogin;
using POC.App.Commands.CreateScreenProfile;
using POC.Contracts.Auth;
using POC.Contracts.Response;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Common.Exceptions;
using POC.Infrastructure.Common.Validators;
using POC.Services;

namespace POC.Api.Controllers.ClientControllers;
[ApiController]
[Route("[controller]")]
public class AdminController
    : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ScreenProfilesController> _logger;
    
    public AdminController(IMediator mediator, ILogger<ScreenProfilesController> logger)
    {
        _logger = logger;
        _mediator = mediator;
    }
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginPostDto loginDto)
    {   
        var command = new AdminLoginCommand(loginDto);

        try
        {
            var token = await _mediator.Send(command);
            return Ok(token);
        }
        catch (AdminLoginException e)
        {
            return Unauthorized();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while logging in");
            return BadRequest();
        }
    }

}