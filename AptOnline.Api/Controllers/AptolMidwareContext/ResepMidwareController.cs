using AptOnline.Application.AptolCloudContext.PpkAgg;
using AptOnline.Application.AptolMidwareContext.ResepMidwareAgg.ResepRsConfirmUseCase;
using AptOnline.Application.AptolMidwareContext.ResepMidwareAgg.ResepRsValidateUseCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.AptolMidwareContext;

[Route("api/[controller]")]
[ApiController]
public class ResepMidwareController : Controller
{
    private readonly IMediator _mediator;

    public ResepMidwareController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<IActionResult> Create(ResepRsValidateCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new JSendOk(result));
    }
    [HttpPut]
    [Route("confirm")]
    public async Task<IActionResult> Confirm(ResepRsConfirmCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new JSendOk(result));
    }

}