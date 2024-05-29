using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using POC.App.Queries.GetAllOrders;
using POC.App.Queries.GetOrder;
using POC.Contracts.CrmDTOs;

namespace POC.Api.Controllers.CrmControllers;
[ApiController]
[Route("[controller]")]
public class OrdersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
    {
        var query = new GetAllOrdersQuery();
        try
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
        catch (HttpRequestException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            // Log the exception
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
