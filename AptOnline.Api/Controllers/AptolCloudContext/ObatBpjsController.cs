using AptOnline.Application.AptolCloudContext.ObatBpjsAgg;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.AptolCloudContext
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObatBpjsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ObatBpjsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{keyword}")]
        public async Task<IActionResult> ListData(string kodeJenisFaskes, string tglResep, string keyword)
        {
            var query = new ListObatBpjsQuery(kodeJenisFaskes, tglResep, keyword);
            var result = await _mediator.Send(query);
            return Ok(new JSendOk(result));
        }
    }
}
