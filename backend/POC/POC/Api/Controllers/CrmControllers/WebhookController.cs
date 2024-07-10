using System.Windows.Input;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using POC.App.Commands.OrderAdded;
using POC.App.Commands.OrderDeleted;
using POC.App.Commands.OrderUpdated;
using POC.Contracts.CrmDTOs;

namespace POC.Api.Controllers.CrmControllers;

[ApiController]
[Route("[controller]")]
// [Authorize(Roles = "CRM")] //TODO : add authorization
public class WebhookController : ControllerBase
{
    private readonly IMediator _mediator;

    public WebhookController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    
    [HttpPost("order")]
    public async Task<ActionResult<BaseOrderDto>> WebhookOrderCommand([FromBody] OrderCommand orderCommand)
    {
        object command;
        string cmd = orderCommand.cmd;
        var order = orderCommand.order;

        if (cmd == "create")
            command = new OrderAddedCommand((OrderDto)order);
        else if (cmd == "delete")
            command = new OrderDeletedCommand(order.Id);
        else if (cmd == "update")
            command = new OrderUpdatedCommand((OrderDto)order);
        else
            return BadRequest("Invalid status");

        await _mediator.Send(command);
        return Ok(order);
    }
    
}