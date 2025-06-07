using AptOnline.Application.AptolCloudContext.PoliBpjsAgg;
using AptOnline.Application.AptolCloudContext.PpkAgg;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.AptolCloudContext
{
    [Route("api/[controller]")]
    [ApiController]
    public class PpkController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PpkController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getSetting")]
        public async Task<IActionResult> GetData()
        {
            var query = new GetSettingPpkBpjsQuery();
            var result = await _mediator.Send(query);
            return Ok(new JSendOk(result));
        }
    }
}
