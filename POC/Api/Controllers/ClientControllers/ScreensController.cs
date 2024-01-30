using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    // [HttpPost]
    // public async Task<IActionResult> PairScreen([FromBody] PairScreenDto pairScreenDto)
    // {
    //     var command = new PairScreenCommand(pairScreenDto);
    //     var result = await _mediator.Send(command);
    //     return Ok(result);
    // }
    //
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> RemoveScreen(int id)
    // {
    //     var command = new RemoveScreenCommand(id);
    //     var result = await _mediator.Send(command);
    //     return Ok(result);
    // }
    //
    // [HttpGet("{id}")]
    // public async Task<ActionResult<ScreenDto>> GetScreen(int id)
    // {
    //     var query = new GetScreenQuery(id);
    //     var result = await _mediator.Send(query);
    //     return Ok(result);
    // }
}