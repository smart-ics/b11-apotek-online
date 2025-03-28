using AptOnline.Application.PharmacyContext.PenjualanAgg;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.PharmacyContext
{
    [Route("api/[controller]")]
    [ApiController]
    public class PenjualanController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PenjualanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetData(string id)
        {
            var query = new PenjualanGetDataQuery(id);
            var result = await _mediator.Send(query);
            return Ok(new JSendOk(result));
        }
    }
}
