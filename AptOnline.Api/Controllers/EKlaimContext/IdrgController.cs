using AptOnline.Application.EKlaimContext.IdrgFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.EKlaimContext;

[Route("api/[controller]")]
[ApiController]
public class IdrgController : Controller
{
    private readonly IMediator _mediator;

    public IdrgController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("{id}/{im}")]
    public async Task<IActionResult> GetData(string id, int im)
    {
        var query = new IdrgGetQuery(id, im);
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }
    
    [HttpGet]
    [Route("diagnosa/{keyword}")]
    public async Task<IActionResult> SearchDiagnosa(string keyword)
    {
        var query = new IdrgSearchDiagQuery(keyword);
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }    
    
    [HttpGet]
    [Route("prosedur/{keyword}")]
    public async Task<IActionResult> SearchProsedur(string keyword)
    {
        var query = new IdrgSearchProsedurQuery(keyword);
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }    
}