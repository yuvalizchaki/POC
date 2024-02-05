using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.App.Commands.CreateScreenProfile;
using POC.App.Commands.DeleteScreenProfile;
using POC.App.Commands.UpdateScreenProfile;
using POC.App.Queries.GetScreenProfile;
using POC.App.Queries.GetAllScreenProfiles;
using POC.Contracts.Response;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Common.Exceptions;
using POC.Infrastructure.Common.Validators;

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
        var validator = new CreateScreenProfileDtoValidator();
        var validationResult = await validator.ValidateAsync(createScreenProfile);
        
        if (!validationResult.IsValid)
        {
            var errorResponse = new ErrorResponse(validationResult.Errors);
            return BadRequest(errorResponse);
        }
        
        var command = new CreateScreenProfileCommand(createScreenProfile);

        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating screen profile, should not happen for now, but just in case.");
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ScreenProfileDto>> GetScreenProfile(int id)
    {
        var query = new GetScreenProfileQuery(id);

        try
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (ScreenProfileNotFoundException e)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting screen profile, should not happen for now, but just in case.");
            return BadRequest();
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateScreenProfile(int id, [FromBody] UpdateScreenProfileDto screenProfile)
    {
        var validator = new UpdateScreenProfileDtoValidator();
        var validationResult = await validator.ValidateAsync(screenProfile);
        
        if (!validationResult.IsValid)
        {
            var errorResponse = new ErrorResponse(validationResult.Errors);
            return BadRequest(errorResponse);
        }
        
        var command = new UpdateScreenProfileCommand(id, screenProfile);

        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (ScreenProfileNotFoundException e)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating screen profile, should not happen for now, but just in case.");
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteScreenProfile(int id)
    {
        var command = new DeleteScreenProfileCommand(id);

        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (ScreenProfileNotFoundException e)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting screen profile, should not happen for now, but just in case.");
            return BadRequest();
        }
    }
    
    //get all
    [HttpGet]
    public async Task<ActionResult<List<ScreenProfileDto>>> GetAllScreenProfiles()
    {
        var query = new GetAllScreenProfilesQuery();
        try
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all screen profiles, should not happen for now, but just in case.");
            return BadRequest(e.Message);
        }
    }
    
}