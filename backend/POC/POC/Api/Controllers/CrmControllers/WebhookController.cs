using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.App.Commands.OrderAdded;
using POC.App.Commands.OrderDeleted;
using POC.App.Commands.OrderUpdated;
using POC.Contracts.CrmDTOs;

namespace POC.Api.Controllers.CrmControllers;
[ApiController]
[Route("[controller]")]
public class WebhookController : ControllerBase
{
    private readonly IMediator _mediator;

    public WebhookController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    // when a new order is added, the webhook url is this post method
    // we will add a caching mechanism here after POC
    [HttpPost("order-added")]
    public async Task<ActionResult<OrderDto>> WebhookOrderAdded(OrderDto orderDto)
    {
        var command = new OrderAddedCommand(orderDto);
        await _mediator.Send(command);
        return Ok(orderDto);
    }
    // when a new order is updated, the webhook url is this post method
    // we will add a caching mechanism here after POC
    [HttpPost("order-updated")]
    public async Task<ActionResult<OrderDto>> WebhookOrderUpdated(OrderDto orderDto)
    {
        var command = new OrderUpdatedCommand(orderDto);
        await _mediator.Send(command);
        return Ok(orderDto);
    }
    [HttpPost("order-deleted")]
    public async Task<ActionResult<OrderDto>> WebhookOrderDeleted(OrderDto orderDto)
    {
        var command = new OrderDeletedCommand(orderDto);
        await _mediator.Send(command);
        return Ok(orderDto);
    }
}