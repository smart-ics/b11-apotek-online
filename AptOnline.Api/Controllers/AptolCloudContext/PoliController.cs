using AptOnline.Application.AptolCloudContext.PoliBpjsAgg;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.AptolCloudContext
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PoliController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{keyword}")]
        public async Task<IActionResult> ListData(string keyword)
        {
            var query = new ListPoliBpjsQuery(keyword);
            var result = await _mediator.Send(query);
            return Ok(new JSendOk(result));
        }
    }
}
