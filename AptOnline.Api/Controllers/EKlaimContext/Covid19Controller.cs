using AptOnline.Application.EKlaimContext.CaraMasukFeature;
using AptOnline.Application.EKlaimContext.Covid19Feature;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.EKlaimContext;

[Route("api/[controller]")]
[ApiController]
public class Covid19Controller : Controller
{
    private readonly IMediator _mediator;

    public Covid19Controller(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("tipeNoKartu/{id}")]
    public async Task<IActionResult> GetTipeNoKartu(string id)
    {
        var query = new TipeNoKartuGetQuery(id);
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }

    [HttpGet("tipeNoKartu")]
    public async Task<IActionResult> ListTipeNoKartu()
    {
        var query = new TipeNoKartuListQuery();
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }
    
    [HttpGet("status/{id}")]
    public async Task<IActionResult> GetStatus(string id)
    {
        var query = new Covid19StatusGetQuery(id);
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }

    [HttpGet("status")]
    public async Task<IActionResult> ListCovid19Status()
    {
        var query = new Covid19StatusListQuery();
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }    
}