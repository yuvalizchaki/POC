using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POC.App.Commands.OrderAdded;
using POC.App.Commands.OrderDeleted;
using POC.App.Commands.OrderUpdated;
using POC.Contracts.CrmDTOs;

namespace POC.Api.Controllers.CrmControllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Screen,Admin")]
public class WebhookController : ControllerBase
{
    private readonly IMediator _mediator;

    public WebhookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("orders")]
    public async Task<ActionResult<OrderDto>> WebhookOrderAdded([FromBody] OrderDto orderDto)
    {
        var command = new OrderAddedCommand(orderDto);
        await _mediator.Send(command);
        return Ok(orderDto);
    }

    [HttpPut("orders/{id}")]
    public async Task<ActionResult<OrderDto>> WebhookOrderUpdated(int id, [FromBody] OrderDto orderDto)
    {
        var command = new OrderUpdatedCommand(orderDto);
        await _mediator.Send(command);
        return Ok(orderDto);
    }

    [HttpDelete("orders/{id}")]
    public async Task<ActionResult> WebhookOrderDeleted(int id)
    {
        var command = new OrderDeletedCommand(id);
        await _mediator.Send(command);
        return Ok();
    }
}