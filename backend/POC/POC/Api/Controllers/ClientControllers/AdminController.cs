using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POC.App.Commands.AdminLogin;
using POC.App.Queries.GetAllConnectedScreens;
using POC.Contracts.Auth;
using POC.Infrastructure.Common.Exceptions;

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
    [AllowAnonymous]
    [HttpPost]
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
    [Authorize(Roles = "Admin")]
    [HttpGet("connected-screens")]
    public async Task<IActionResult> GetConnectedScreens()
    {
        var query = new GetAllConnectedScreensQuery();
        try
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all connected screens.");
            return BadRequest(e.Message);
        }
    }
    

}