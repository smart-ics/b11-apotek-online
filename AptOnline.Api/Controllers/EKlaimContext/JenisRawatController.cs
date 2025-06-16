using AptOnline.Application.EKlaimContext.JenisRawatFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.EKlaimContext;

[Route("api/[controller]")]
[ApiController]
public class JenisRawatController : Controller
{
    private readonly IMediator _mediator;

    public JenisRawatController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetData(string id)
    {
        var query = new JenisRawatGetQuery(id);
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }

    [HttpGet]
    public async Task<IActionResult> ListData()
    {
        var query = new JenisRawatListQuery();
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }

}