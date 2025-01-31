
using AptOnline.Api.Models;
using System.Security.Cryptography.X509Certificates;
using AptOnline.Helpers;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using RestSharp;
using JknBridgerService.Helpers;
using Newtonsoft.Json.Linq;

namespace AptOnline.Api.Services
{
    public interface IDeleteResepBpjsService
    {
        DeleteResepBpjsRespDto Execute(DeleteResepBpjsReqDto req);
    }
    public class DeleteResepBpjsService : IDeleteResepBpjsService
    {
        private readonly BpjsOptions _opt;
        private readonly string _timestamp;
        private readonly string _signature;
        private readonly string _decryptKey;
        public DeleteResepBpjsService(IOptions<BpjsOptions> opt)
        {
            _opt = opt.Value;
            _timestamp = BpjsHelper.GetTimeStamp().ToString();
            _signature = BpjsHelper.GenHMAC256(_opt.ConsId + "&" + _timestamp, _opt.SecretKey);
            _decryptKey = _opt.ConsId + _opt.SecretKey + _timestamp;
        }
        public DeleteResepBpjsRespDto Execute(DeleteResepBpjsReqDto req)
        {
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
            request.AddJsonBody(req);
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
            var result = jResult.ToObject<DeleteResepBpjsRespDto>();
            return result;
        }
    }
    #region Dto


    public class DeleteResepBpjsReqDto
    {
        public string nosjp { get; set; }
        public string refasalsjp { get; set; }
        public string noresep { get; set; }
    }


    public class DeleteResepBpjsRespDto
    {
        public DeleteResepBpjsRespMetadata metaData { get; set; }
        public object response { get; set; }
    }

    public class DeleteResepBpjsRespMetadata
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    #endregion
}