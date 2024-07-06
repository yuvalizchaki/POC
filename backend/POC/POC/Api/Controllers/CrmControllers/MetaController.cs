using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POC.App.Queries.GetMetadata;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Common.Exceptions;

namespace POC.Api.Controllers.CrmControllers;


[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Admin")]
public class MetaController : ControllerBase 
{
    private readonly IMediator _mediator;
    private readonly ILogger<MetaController> _logger;
    
    public MetaController(IMediator mediator, ILogger<MetaController> logger)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ScreenProfileDto>> GetMetaData(int id)
    {
        var query = new GetMetadataQuery(id);

        try
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (ScreenProfileNotFoundException e)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting meta data, should not happen for now, but just in case.");
            return BadRequest();
        }
    }
}
