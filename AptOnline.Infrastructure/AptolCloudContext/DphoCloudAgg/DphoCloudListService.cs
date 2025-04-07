using System.Security.Cryptography.X509Certificates;
using AptOnline.Application.AptolCloudContext.DphoCloudAgg;
using AptOnline.Domain.PharmacyContext.DphoAgg;
using AptOnline.Infrastructure.AptolCloudContext.DphoAgg;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AptOnline.Infrastructure.AptolCloudContext.DphoCloudAgg;

public class DphoCloudListService : IDphoCloudListService
{
    private readonly BpjsOptions _opt;
    private readonly string _timestamp;
    private readonly string _signature;
    private readonly string _decryptKey;
    public DphoCloudListService(IOptions<BpjsOptions> opt)
    {
        _opt = opt.Value;
        _timestamp = BpjsHelper.GetTimeStamp().ToString();
        _signature = BpjsHelper.GenHMAC256(_opt.ConsId + "&" + _timestamp, _opt.SecretKey);
        _decryptKey = _opt.ConsId + _opt.SecretKey + _timestamp;
    }
    public IEnumerable<DphoType> Execute()
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
            var decryptedResp = BpjsHelper.Decrypt(_decryptKey, tempResp?.ToString() ?? string.Empty);
            jResult["response"] = JObject.Parse(decryptedResp);
        }
        catch
        {
            // ignored
        }

        var listRefDphoBpjs = jResult.ToObject<DphoCloudListResponse>();
        var result = listRefDphoBpjs?.response.list.Select(x => new DphoType(
            x.kodeobat, x.namaobat, x.prb, x.kronis,
            x.kemo, Convert.ToDecimal(x.harga), x.restriksi,
            x.generik, true)) ?? new List<DphoType>();
        return result;
    }
}