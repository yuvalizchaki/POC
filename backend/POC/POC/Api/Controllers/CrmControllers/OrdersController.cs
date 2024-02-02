using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.App.Queries.GetAllOrders;
using POC.App.Queries.GetOrder;
using POC.Contracts.CrmDTOs;

namespace POC.Api.Controllers.CrmControllers;
[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
    {
        var query = new GetAllOrdersQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(int id)
    {
        var query = new GetOrderByIdQuery(id);
        var result = await _mediator.Send(query);
        return result != null ? Ok(result) : NotFound();
    }
    [HttpPost]
    // when a new order is added, the webhook url is this post method
    // we will add a caching mechanism here after POC
    //for now we will just return the new order added
    //probably wont be needed- depends on MapPost
    public async Task<ActionResult<OrderDto>> WebhookOrderAdded(OrderDto orderDto)
    {
        //TODO: send to hub 
        return Ok(orderDto);
    }
}