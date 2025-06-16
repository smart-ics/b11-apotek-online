using AptOnline.Application.EKlaimContext.CaraMasukFeature;
using AptOnline.Application.EKlaimContext.PayorFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.EKlaimContext;

[Route("api/[controller]")]
[ApiController]
public class PayorController : Controller
{
    private readonly IMediator _mediator;

    public PayorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetData(string id)
    {
        var query = new PayorGetQuery(id);
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }

    [HttpGet]
    public async Task<IActionResult> ListData()
    {
        var query = new PayorListQuery();
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }
}