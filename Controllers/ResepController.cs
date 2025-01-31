using Microsoft.AspNetCore.Mvc;
using MediatR;
using Nuna.Lib.ActionResultHelper;
using AptOnline.Api.Usecases;

namespace AptOnline.Controllers
{
    public class ResepController : Controller
    {
        private readonly IMediator _mediator;

        public ResepController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("ref/dpho")]
        public async Task<IActionResult> Create(ListRefDphoBpjsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(new JSendOk(result));
        }
        [HttpPost]
        [Route("sendToAptol")]
        public async Task<IActionResult> Send([FromBody]SendResepToAptolCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(new JSendOk(result));
        }

    }
}
