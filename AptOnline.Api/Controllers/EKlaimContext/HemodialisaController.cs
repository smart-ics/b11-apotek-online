using AptOnline.Application.EKlaimContext.Covid19Feature;
using AptOnline.Application.EKlaimContext.HemodialisaFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.EKlaimContext;

[Route("api/[controller]")]
[ApiController]
public class HemodialisaController : Controller
{
    private readonly IMediator _mediator;

    public HemodialisaController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("dializerUsage/{id}")]
    public async Task<IActionResult> GetDializerUsage(string id)
    {
        var query = new DializerUsageGetQuery(id);
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }

    [HttpGet("dializerUsage")]
    public async Task<IActionResult> ListDializerUsage()
    {
        var query = new DializerUsageListQuery();
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }
    
}