using System.Security.Cryptography.X509Certificates;
using AptOnline.Api.Helpers;
using Microsoft.Extensions.Options;
using JknBridgerService.Helpers;
using AptOnline.Application.AptolCloudContext.DphoAgg;
using AptOnline.Domain.AptolCloudContext.DphoAgg;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace AptOnline.Infrastructure.AptolCloudContext.DphoAgg;

public class ListDphoService : IListDphoService
{
    private readonly BpjsOptions _opt;
    private readonly string _timestamp;
    private readonly string _signature;
    private readonly string _decryptKey;
    public ListDphoService(IOptions<BpjsOptions> opt)
    {
        _opt = opt.Value;
        _timestamp = BpjsHelper.GetTimeStamp().ToString();
        _signature = BpjsHelper.GenHMAC256(_opt.ConsId + "&" + _timestamp, _opt.SecretKey);
        _decryptKey = _opt.ConsId + _opt.SecretKey + _timestamp;
    }
    public IEnumerable<DphoModel> Execute()
    {
        var endpoint = $"{_opt.BaseApiUrl}/referensi/dpho";
        var client = new RestClient(endpoint)
        {
            ClientCertificates = new X509CertificateCollection()
        };
        var request = new RestRequest(Method.GET);
        request.AddHeader("X-cons-id", _opt.ConsId);
        request.AddHeader("X-timestamp", _timestamp);
        request.AddHeader("X-signature", _signature);
        request.AddHeader("user_key", _opt.UserKey);
        var response = client.Execute(request);

        JObject jResult;
        try { jResult = JObject.Parse(response.Content); }
        catch { throw new Exception(response.StatusDescription); }

        if (!jResult["metaData"]["code"].ToString().Equals("200", StringComparison.Ordinal))
        {
            throw new Exception(response.Content);
        }
        var tempResp = jResult.SelectToken("response");
        try
        {
            string decryptedResp = BpjsHelper.Decrypt(_decryptKey, tempResp?.ToString() ?? string.Empty);
            jResult["response"] = JObject.Parse(decryptedResp);
        }
        catch { }
        var listRefDphoBpjs = jResult.ToObject<ListDphoResponse>();
        var result = listRefDphoBpjs?.response.list.Select(x => new DphoModel
        {
            DphoId = x.kodeobat,
            DphoName = x.namaobat,
            Prb = x.prb,
            Kronis = x.kronis,
            Kemo = x.kemo,
            Harga = Convert.ToDecimal(x.harga),
            Restriksi = x.restriksi,
            Generik = x.generik,
            IsAktif = true//(bool)x.aktif
        }) ?? new List<DphoModel>();
        return result;
    }
}