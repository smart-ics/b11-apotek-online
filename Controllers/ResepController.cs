using Microsoft.AspNetCore.Mvc;
using MediatR;
using Nuna.Lib.ActionResultHelper;
using AptOnline.Api.Usecases;

namespace AptOnline.Controllers
{
    [Route("api/resep")]
    public class ResepController : Controller
    {
        private readonly IMediator _mediator;
        
        public ResepController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> Send([FromBody]SendResepToAptolCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(new JSendOk(result));
        }
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] SendResepToAptolCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(new JSendOk(result));
        }
        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> ListData([FromBody] SendResepToAptolCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(new JSendOk(result));
        }
    }
}
