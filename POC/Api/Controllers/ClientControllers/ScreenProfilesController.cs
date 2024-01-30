using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.App.Commands.CreateScreenProfile;
using POC.Contracts.ScreenProfile;

namespace POC.Api.Controllers.ClientControllers;

[ApiController]
[Route("[controller]")]
public class ScreenProfilesController : ControllerBase 
{
    private readonly IMediator _mediator;
    private readonly ILogger<ScreenProfilesController> _logger;
    
    public ScreenProfilesController(IMediator mediator, ILogger<ScreenProfilesController> logger)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateScreenProfile([FromBody] CreateScreenProfileDto createScreenProfile)
    {
        var command = new CreateScreenProfileCommand(createScreenProfile);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ScreenProfileDto>> GetScreenProfile(int id)
    {
        // var query = new GetScreenProfileQuery(id);
        // var result = await _mediator.Send(query);
        // return Ok(result);
        throw new NotImplementedException();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateScreenProfile(int id, [FromBody] UpdateScreenProfileDto screenProfile)
    {
        // var command = new UpdateScreenProfileCommand(id, screenProfile);
        // var result = await _mediator.Send(command);
        // return Ok(result);
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteScreenProfile(int id)
    {
        // var command = new DeleteScreenProfileCommand(id);
        // var result = await _mediator.Send(command);
        // return Ok(result);
        throw new NotImplementedException();
    }
    
}