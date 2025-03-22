using System.Security.Cryptography.X509Certificates;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AptOnline.Infrastructure.AptolCloudContext.PkpAgg
{
    public interface IGetSettingPpkBpjsService
    {
        GetSettingPpkBpjsRespDto Execute();
    }
    public class GetSettingPpkBpjsService : IGetSettingPpkBpjsService
    {
        private readonly BpjsOptions _opt;
        private readonly string _timestamp;
        private readonly string _signature;
        private readonly string _decryptKey;
        public GetSettingPpkBpjsService(IOptions<BpjsOptions> opt)
        {
            _opt = opt.Value;
            _timestamp = BpjsHelper.GetTimeStamp().ToString();
            _signature = BpjsHelper.GenHMAC256(_opt.ConsId + "&" + _timestamp, _opt.SecretKey);
            _decryptKey = _opt.ConsId + _opt.SecretKey + _timestamp;
        }
        public GetSettingPpkBpjsRespDto Execute()
        {
            var endpoint = $"{_opt.BaseApiUrl}/referensi/settingppk/read/{_opt.ProviderId}";
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
            var result = jResult.ToObject<GetSettingPpkBpjsRespDto>();
            return result;
        }
    }
}