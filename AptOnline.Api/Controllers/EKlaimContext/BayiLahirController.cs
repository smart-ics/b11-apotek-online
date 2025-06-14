using AptOnline.Application.EKlaimContext.BayiLahirFeature;
using AptOnline.Application.EKlaimContext.CaraMasukFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.EKlaimContext;

[Route("api/[controller]")]
[ApiController]
public class BayiLahirController : Controller
{
    private readonly IMediator _mediator;

    public BayiLahirController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("status/{id}")]
    public async Task<IActionResult> GetData(string id)
    {
        var query = new BayiLahirStatusGetQuery(id);
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }

    [HttpGet("status")]
    public async Task<IActionResult> ListData()
    {
        var query = new BayiLahirStatusListQuery();
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }
}