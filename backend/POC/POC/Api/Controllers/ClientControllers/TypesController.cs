using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POC.App.Queries.GetCompanyTypes;
using POC.App.Queries.GetTagsTypes;

namespace POC.Api.Controllers.ClientControllers;


[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Screen, Admin")]
public class TypesController(ILogger<TypesController> logger, IMediator mediator) : ControllerBase
{
    //one for tags types and one for company types
    
    
    [HttpGet("tags")]
    public async Task<ActionResult<String>> GetTagTypes()
    {
        try
        {
            var result = await mediator.Send(new GetTagsTypesQuery());
            return Ok(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while fetching tag types");
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
    [HttpGet("company")]
    public async Task<ActionResult<String>> GetCompanyTypes()
    {
        try
        {
            var result = await mediator.Send(new GetCompanyTypesQuery());
            return Ok(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while fetching company types");
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
}