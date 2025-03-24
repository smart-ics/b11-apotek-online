using AptOnline.Infrastructure.AptolCloudContext.PkpAgg;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nuna.Lib.ActionResultHelper;

namespace AptOnline.Api.Controllers
{
    [Route("api/ref")]
    public class ReferensiController : Controller
    {
        //private readonly IMediator _mediator;

        //public ReferensiController(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}

        //[HttpGet]
        //[Route("dpho")]
        //public async Task<IActionResult> ListDpho()
        //{
        //    // var query = new ListRefDphoBpjsQuery();
        //    // var result = await _mediator.Send(query);
        //    // return Ok(new JSendOk(result));
        //    throw new NotImplementedException();
        //}

        //[HttpGet]
        //[Route("poli")]
        //public async Task<IActionResult> ListPoli(string keyword)
        //{
        //    // var query = new ListRefPoliBpjsQuery(keyword);
        //    // var result = await _mediator.Send(query);
        //    // return Ok(new JSendOk(result));
        //    throw new NotImplementedException();
        //}

        //[HttpGet]
        //[Route("faskes")]
        //public async Task<IActionResult> ListFaskes(string jenisFaskes, string keyword)
        //{
        //    // var query = new ListRefFaskesBpjsQuery(jenisFaskes, keyword);
        //    // var result = await _mediator.Send(query);
        //    // return Ok(new JSendOk(result));
        //    throw new NotImplementedException();
        //}

        //[HttpGet]
        //[Route("ppkSetting")]
        //public async Task<IActionResult> GetSettingPpk()
        //{
        //    var query = new GetSettingPpkBpjsQuery();
        //    var result = await _mediator.Send(query);
        //    return Ok(new JSendOk(result));
        //}

        //[HttpGet]
        //[Route("obat")]
        //public async Task<IActionResult> ListObat(string kodeJenisObat, string tglResep, string keyword)
        //{
        //    // var query = new ListRefObatBpjsQuery(kodeJenisObat, tglResep, keyword);
        //    // var result = await _mediator.Send(query);
        //    // return Ok(new JSendOk(result));
        //    throw new NotImplementedException();
        //}
    }
}
