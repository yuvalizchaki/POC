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
    public async Task<ActionResult<List<OrderDto>>> OrderAdded(OrderDto orderDto)
    {
        var query = new GetAllOrdersQuery();
        var result = await _mediator.Send(query);
        result.Add(orderDto);
        return Ok(result);
    }
}