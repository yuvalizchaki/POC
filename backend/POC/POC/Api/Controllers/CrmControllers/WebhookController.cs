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
public class WebhookController(IMediator mediator) : ControllerBase
{
    [HttpPost("order")]
    public async Task<ActionResult<BaseOrderDto>> WebhookOrderCommand([FromBody] OrderCommand orderCommand)
    {
        object command;
        var cmd = orderCommand.cmd;
        var order = orderCommand.order;

        switch (cmd)
        {
            case "create":
                command = new OrderAddedCommand((CrmOrder)order);
                break;
            case "delete":
                command = new OrderDeletedCommand(order.Id);
                break;
            case "update":
                command = new OrderUpdatedCommand((CrmOrder)order);
                break;
            default:
                return BadRequest("Invalid status");
        }

        await mediator.Send(command);
        return Ok(order);
    }
    
}