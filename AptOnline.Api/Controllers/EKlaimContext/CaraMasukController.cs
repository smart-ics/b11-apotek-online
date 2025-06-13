using AptOnline.Application.EKlaimContext.CaraMasukFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.EKlaimContext;

[Route("api/[controller]")]
[ApiController]
public class CaraMasukController : Controller
{
    private readonly IMediator _mediator;

    public CaraMasukController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetData(string id)
    {
        var query = new CaraMasukGetQuery(id);
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }

    [HttpGet]
    public async Task<IActionResult> ListData()
    {
        var query = new CaraMasukListQuery();
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }
}