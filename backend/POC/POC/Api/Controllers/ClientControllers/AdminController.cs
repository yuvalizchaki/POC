using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POC.App.Commands.AdminLogin;
using POC.App.Commands.SendPopupMessage;
using POC.App.Queries.GetAllConnectedScreens;
using POC.Contracts.Admin;
using POC.Contracts.Auth;
using POC.Contracts.CrmDTOs;
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
            var adminLoginDto = await _mediator.Send(command);
            return Ok(adminLoginDto);
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
    
    [Authorize(Roles = "Admin")]
    [HttpPost("message-screens/{profileId}")]
    public async Task<IActionResult> MessageScreens(int profileId, [FromBody] AdminMessageDto messageDto)
    {
        var senderName = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (senderName == null) return BadRequest("Sender name is required.");
        
        var command = new SendPopupMessageCommand(profileId,senderName!, messageDto);
        try
        {
            await _mediator.Send(command);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending message to screen.");
            return BadRequest(e.Message);
        }
    }
    

}