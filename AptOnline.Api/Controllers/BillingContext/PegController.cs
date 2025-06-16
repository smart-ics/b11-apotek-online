using AptOnline.Application.BillingContext.PegFeature;
using AptOnline.Application.EKlaimContext.CaraMasukFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.BillingContext;

[Route("api/[controller]")]
[ApiController]
public class PegController : Controller
{
    private readonly IMediator _mediator;

    public PegController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetData(string id)
    {
        var query = new PegGetQuery(id);
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }

    [HttpGet]
    public async Task<IActionResult> ListData()
    {
        var query = new PegListQuery();
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
        
    }

    [HttpPost]
    public async Task<IActionResult> Save(PegSaveCommand cmd)
    {
        var result = await _mediator.Send(cmd);
        return Ok(new JSendOk(result));
    }
}