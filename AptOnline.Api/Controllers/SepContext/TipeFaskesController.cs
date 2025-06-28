using AptOnline.Application.SepContext.TipeFaskesFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.SepContext;

[Route("api/[controller]")]
[ApiController]
public class TipeFaskesController : Controller
{
    private readonly IMediator _mediator;

    public TipeFaskesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetData(string id)
    {
        var query = new TipeFaskesGetQuery(id);
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }

    [HttpGet]
    public async Task<IActionResult> ListData()
    {
        var query = new TipeFaskesListQuery();
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }

}