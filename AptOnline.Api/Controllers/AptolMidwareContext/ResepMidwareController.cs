using AptOnline.Application.AptolCloudContext.ResepBpjsAgg;
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
        if (!result.BridgeState.Equals("CREATED"))
        {
            var saveCommand = new ResepBpjsSaveCommand(command.ResepMidwareId);
            var result1 = await _mediator.Send(saveCommand);
            return Ok(new JSendOk(result1));
        } 
        return Ok(new JSendOk(result));
    }
    [HttpPut]
    [Route("send")]
    public async Task<IActionResult> Send(ResepBpjsSaveCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new JSendOk(result));
    }
    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete(ResepBpjsDeleteCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new JSendOk(result));
    }
    [HttpDelete]
    [Route("obat/delete")]
    public async Task<IActionResult> DeleteObat(ObatBpjsDeleteCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new JSendOk(result));
    }
}