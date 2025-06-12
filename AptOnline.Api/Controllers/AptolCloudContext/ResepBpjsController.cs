using AptOnline.Application.AptolCloudContext.ResepBpjsAgg;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers.AptolCloudContext
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResepBpjsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResepBpjsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListData(string tglAwalYmd, string tglAkhirYmd)
        {
           var query = new ResepBpjsListQuery("2", tglAwalYmd, tglAkhirYmd);
            var result = await _mediator.Send(query);
            return Ok(new JSendOk(result));
        }

        [HttpGet("obat/sep/{noSep}")]
        public async Task<IActionResult> ListDataObat(string noSep)
        {
            var query = new ObatBpjsPerSepListQuery(noSep);
            var result = await _mediator.Send(query);
            return Ok(new JSendOk(result));
        }

        [HttpGet("obat/peserta/{noPeserta}")]
        public async Task<IActionResult> ListRiwayatObat(string noPeserta, string tglAwalYmd, string tglAkhirYmd)
        {
            var query = new ObatBpjsPerPesertaListQuery(noPeserta, tglAwalYmd, tglAkhirYmd);
            var result = await _mediator.Send(query);
            return Ok(new JSendOk(result));
        }
    }
}
