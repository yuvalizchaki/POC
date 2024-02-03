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
        try
        {
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();;
        }catch(ApplicationException e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(int id)
    {
        var query = new GetOrderByIdQuery(id);
        try
        {
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }catch(ApplicationException e)
        {
            return BadRequest(e.Message);
        }
    }
    
}