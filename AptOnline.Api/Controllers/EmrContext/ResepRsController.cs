using AptOnline.Application.EmrContext.ResepRsAgg;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.EmrContext
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResepRsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResepRsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetData(string id)
        {
            var query = new ResepRsGetDataQuery(id);
            var result = await _mediator.Send(query);
            return Ok(new JSendOk(result));
        }
    }
}
