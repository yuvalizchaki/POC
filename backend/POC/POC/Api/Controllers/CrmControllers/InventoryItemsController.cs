using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POC.App.Queries.GetAllInventoryItems;
using POC.Infrastructure.Models;

namespace POC.Api.Controllers.CrmControllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Screen,Admin")]
[Authorize(Policy = "CompanyIdIsOne")]
public class InventoryItemsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<InventoryItemDto>>> GetAllInventoryItems()
    {
        var cid = User.FindFirst("CompanyId");
        var sid = User.FindFirst("ScreenProfileId");
        if (cid == null || sid == null)
        {
            return Unauthorized();
        }
        var query = new GetAllInventoryItemsQuery(int.Parse(sid.Value), int.Parse(cid.Value));
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