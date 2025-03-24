using AptOnline.Application.AptolCloudContext.DphoAgg;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.AptolCloudContext
{
    [Route("api/[controller]")]
    [ApiController]
    public class DphoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DphoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListData()
        {
            var query = new ListDphoQuery();
            var result = await _mediator.Send(query);
            return Ok(new JSendOk(result));
        }
    }
}
