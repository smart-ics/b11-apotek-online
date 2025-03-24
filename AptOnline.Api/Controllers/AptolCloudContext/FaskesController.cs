using AptOnline.Application.AptolCloudContext.FaskesAgg;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.AptolCloudContext
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaskesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FaskesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{jenisFaskes}/{keyword}")]
        public async Task<IActionResult> ListData(string jenisFaskes, string keyword)
        {
            var query = new ListFaskesQuery(jenisFaskes, keyword);
            var result = await _mediator.Send(query);
            return Ok(new JSendOk(result));
        }
    }
}
