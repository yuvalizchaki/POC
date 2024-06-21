using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POC.App.Queries.GetAllOrders;
using POC.App.Queries.GetOrder;
using POC.Contracts.CrmDTOs;

namespace POC.Api.Controllers.CrmControllers;
[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Screen,Admin")]
[Authorize(Policy = "CompanyIdIsOne")]
public class OrdersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
    {
        //TODO: (note) admin doesnt have screen profile id, so he cant access this endpoint
        var cid = User.FindFirst("CompanyId");
        var sid = User.FindFirst("ScreenProfileId");
        if (cid == null || sid == null)
        {
            return Unauthorized();
        }
        var query = new GetAllOrdersQuery(int.Parse(sid.Value), int.Parse(cid.Value));
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
