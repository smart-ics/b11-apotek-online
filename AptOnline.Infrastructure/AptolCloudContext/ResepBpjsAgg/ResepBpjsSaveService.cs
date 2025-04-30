using System.Security.Cryptography.X509Certificates;
using AptOnline.Application.AptolCloudContext.ResepBpjsAgg;
using AptOnline.Domain.AptolCloudContext.ResepBpjsAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Infrastructure.AptolCloudContext.Shared;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nuna.Lib.CleanArchHelper;
using RestSharp;

namespace AptOnline.Infrastructure.AptolCloudContext.ResepBpjsAgg
{
    //public interface IResepBpjsSaveService
    //{
    //    ResepBpjsSaveDto Execute(ResepBpjsSaveRequest req);
    //}
    public class ResepBpjsSaveService : IResepBpjsSaveService
    {
        private readonly BpjsOptions _opt;
        private readonly string _timestamp;
        private readonly string _signature;
        private readonly string _decryptKey;
        public ResepBpjsSaveService(IOptions<BpjsOptions> opt)
        {
            _opt = opt.Value;
            _timestamp = BpjsHelper.GetTimeStamp().ToString();
            _signature = BpjsHelper.GenHMAC256(_opt.ConsId + "&" + _timestamp, _opt.SecretKey);
            _decryptKey = _opt.ConsId + _opt.SecretKey + _timestamp;
        }
        public ResepBpjsModel Execute(ResepMidwareModel req)
        {
            var jReq = JObject.FromObject(req);
            var reqBody = JsonConvert.SerializeObject(jReq);
            var endpoint = $"{_opt.BaseApiUrl}/sjpresep/v3/insert";
            var client = new RestClient(endpoint)
            {
                ClientCertificates = new X509CertificateCollection()
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-cons-id", _opt.ConsId);
            request.AddHeader("X-timestamp", _timestamp);
            request.AddHeader("X-signature", _signature);
            request.AddHeader("user_key", _opt.UserKey);
            request.AddHeader("Content-Type", "Application/x-www-form-urlencoded");

            request.AddParameter("application/x-www-form-urlencoded", reqBody, ParameterType.RequestBody);
            var response = client.Execute(request);
            
            JObject jResult;
            try { jResult = JObject.Parse(response.Content); }
            catch 
            {
                LogHelper.Log(req.ResepRsId, reqBody, response.ErrorMessage, response.StatusCode.ToString());
                throw new Exception(response.StatusDescription); 
            }

            if (!jResult["metaData"]["code"].ToString().Equals("200"))
            {
                LogHelper.Log(req.ResepRsId, reqBody, response.Content, response.StatusCode.ToString());
                //throw new Exception(response.Content);
            }
            var tempResp = jResult.SelectToken("response");
            try
            {
                string decryptedResp = BpjsHelper.Decrypt(_decryptKey, tempResp.ToString());
                jResult["response"] = JObject.Parse(decryptedResp);
                LogHelper.Log(req.ResepRsId, reqBody, jResult.ToString(), response.StatusCode.ToString());
            }
            catch { }
            var tmpResult = jResult.ToObject<ResepBpjsSaveResponse>();
            var result = tmpResult.response.ToModel();
            return result;
        }
    }
    #region Dto
    public class ResepBpjsSaveResponse
    {
        public ResepBpjsSaveDto response { get; set; }
        public AptolCloudResponseMeta metaData { get; set; }
    }
    #endregion
}