using AptOnline.Application.EKlaimContext.CaraMasukFeature;
using AptOnline.Application.EKlaimContext.PayorFeature;
using AptOnline.Application.EKlaimContext.TbIndikatorFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.EKlaimContext;

[Route("api/[controller]")]
[ApiController]
public class TbIndikatorController : Controller
{
    private readonly IMediator _mediator;

    public TbIndikatorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetData(string id)
    {
        var query = new TbIndikatorGetQuery(id);
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }

    [HttpGet]
    public async Task<IActionResult> ListData()
    {
        var query = new TbIndikatorListQuery();
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }
}