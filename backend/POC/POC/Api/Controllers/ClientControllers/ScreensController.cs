using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.App.Commands.PairScreen;
using POC.App.Commands.RemoveScreen;
using POC.App.Queries.GetAllScreens;
using POC.App.Queries.GetScreen;
using POC.Contracts.Screen;

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
        var command = new PairScreenCommand(pairScreenDto);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveScreen(int id)
    {
        var command = new RemoveScreenCommand(id);
        await _mediator.Send(command);
        return Ok(NoContent());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ScreenDto>> GetScreen(int id)
    {
        var query = new GetScreenQuery(id);
        var result = await _mediator.Send(query);
        return result != null ? Ok(result) : NotFound();
    }
    
    //get all
    [HttpGet]
    public async Task<ActionResult<List<ScreenDto>>> GetAllScreens()
    {
        var query = new GetAllScreensQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}