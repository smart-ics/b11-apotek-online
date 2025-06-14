using AptOnline.Application.EKlaimContext.CaraMasukFeature;
using AptOnline.Application.EKlaimContext.DischargeStatusFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.EKlaimContext;

[Route("api/[controller]")]
[ApiController]
public class DischargeStatusController : Controller
{
    private readonly IMediator _mediator;

    public DischargeStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetData(string id)
    {
        var query = new DischargeStatusGetQuery(id);
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }

    [HttpGet]
    public async Task<IActionResult> ListData()
    {
        var query = new DischargeStatusListQuery();
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }
}