using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.App.Commands.PairScreen;
using POC.App.Commands.RemoveScreen;
using POC.App.Queries.GetAllScreens;
using POC.App.Queries.GetScreen;
using POC.Contracts.Response;
using POC.Contracts.Screen;
using POC.Infrastructure.Common.Exceptions;
using POC.Infrastructure.Common.Validators;
using POC.Infrastructure.Extensions;

namespace POC.Api.Controllers.ClientControllers;

[ApiController]
[Route("[controller]")]
public class ScreensController : ControllerBase 
{
    private readonly IMediator _mediator;

    public ScreensController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Example endpoints, adjust based on your actual commands/queries

    [HttpPost]
    public async Task<IActionResult> PairScreen([FromBody] PairScreenDto pairScreenDto)
    {
        var validator = new PairScreenDtoValidator();
        var validationResult = await validator.ValidateAsync(pairScreenDto);

        if (!validationResult.IsValid)
        {
            var errorResponse = new ErrorResponse(validationResult.Errors);
            return BadRequest(errorResponse);
        }
        
        var command = new PairScreenCommand(pairScreenDto);
        
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception e)
        {
            var errorResponse = e.MapToErrorResponse();
            return BadRequest(errorResponse);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveScreen(int id)
    {
        var command = new RemoveScreenCommand(id);

        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (ScreenNotFoundException e)
        {
            return NotFound();
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ScreenDto>> GetScreen(int id)
    {
        var query = new GetScreenQuery(id);

        try
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (ScreenNotFoundException e)
        {
            return NotFound();
        }
    }
    
    //get all
    [HttpGet]
    public async Task<ActionResult<List<ScreenDto>>> GetAllScreens()
    {
        var query = new GetAllScreensQuery();

        try
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception e)
        {
            //shouldnt happen but just in case for now
            return BadRequest(e.Message);
        }
    }
}