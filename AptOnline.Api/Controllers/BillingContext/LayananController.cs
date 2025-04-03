using AptOnline.Application.BillingContext.LayananAgg;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.BillingContext;

[Route("api/[controller]")]
[ApiController]
public class LayananController : Controller
{
    private readonly IMediator _mediator;

    public LayananController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    [HttpGet("{id}")]
    public async Task<IActionResult> GetData(string id)
    {
        var query = new LayananGetQuery(id);
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }

}