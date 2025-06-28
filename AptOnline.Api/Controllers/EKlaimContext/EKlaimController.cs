using AptOnline.Application.EKlaimContext;
using AptOnline.Application.EKlaimContext.EKlaimFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.EKlaimContext;

[Route("api/[controller]")]
[ApiController]
public class EKlaimController : Controller
{
    private readonly IMediator _mediator;

    public EKlaimController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<IActionResult> Create(EKlaimCreateCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new JSendOk(result));
    }
    
    [HttpPut]
    public async Task<IActionResult> GenClaimData(EKlaimSetClaimDataCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new JSendOk(result));
    }

    [HttpGet]
    [Route("{regId}")]
    public async Task<IActionResult> GetEKlaimByReg(string regId)
    {
        var query = new EKlaimGetQuery(regId);
        var result = await _mediator.Send(query);
        return Ok(new JSendOk(result));
    }
}