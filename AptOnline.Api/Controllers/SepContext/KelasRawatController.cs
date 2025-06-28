using AptOnline.Application.SepContext.KelasJknFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.SepContext;

[Route("api/[controller]")]
[ApiController]
public class KelasRawatController : Controller
{
    private readonly IMediator _mediator;

    public KelasRawatController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> ListData()
    {
        var query = new KelasJknListQuery();
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }
}