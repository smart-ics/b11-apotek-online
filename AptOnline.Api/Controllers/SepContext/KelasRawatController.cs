using AptOnline.Application.SepContext.KelasRawatFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.EKlaimContext;

[Route("api/[controller]")]
[ApiController]
public class KelasRawatController : Controller
{
    private readonly IMediator _mediator;

    public KelasRawatController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetData(string id)
    {
        var query = new KelasRawatGetQuery(id);
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }

    [HttpGet]
    public async Task<IActionResult> ListData()
    {
        var query = new KelasRawatListQuery();
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }

}