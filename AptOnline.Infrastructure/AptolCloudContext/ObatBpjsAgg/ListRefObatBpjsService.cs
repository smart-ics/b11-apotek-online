using System.Security.Cryptography.X509Certificates;
using AptOnline.Api.Helpers;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using RestSharp;
using JknBridgerService.Helpers;
using Newtonsoft.Json.Linq;
using AptOnline.Infrastructure.AptolCloudContext.ObatBpjsAgg;

namespace AptOnline.Api.Infrastructures.Services
{
    public class ListRefObatBpjsService : IListRefObatBpjsService
    {
        private readonly BpjsOptions _opt;
        private readonly string _timestamp;
        private readonly string _signature;
        private readonly string _decryptKey;
        public ListRefObatBpjsService(IOptions<BpjsOptions> opt)
        {
            _opt = opt.Value;
            _timestamp = BpjsHelper.GetTimeStamp().ToString();
            _signature = BpjsHelper.GenHMAC256(_opt.ConsId + "&" + _timestamp, _opt.SecretKey);
            _decryptKey = _opt.ConsId + _opt.SecretKey + _timestamp;
        }
        public ListObatBpjsResponse Execute(string kodeJnsObat, string tglResep, string keyword)
        {
            var endpoint = $"{_opt.BaseApiUrl}/referensi/obat/{kodeJnsObat}/{tglResep}/{keyword}";
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

            if (!jResult["metaData"]["code"].ToString().Equals("200"))
            {
                throw new Exception(response.Content);
            }
            var tempResp = jResult.SelectToken("response");
            try
            {
                string decryptedResp = BpjsHelper.Decrypt(_decryptKey, tempResp.ToString());
                jResult["response"] = JObject.Parse(decryptedResp);
            }
            catch { }
            var result = jResult.ToObject<ListObatBpjsResponse>();
            return result;
        }
    }
}