using System.Security.Cryptography.X509Certificates;
using AptOnline.Application.AptolCloudContext.ResepBpjsAgg;
using AptOnline.Infrastructure.AptolCloudContext.Shared;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AptOnline.Infrastructure.AptolCloudContext.ResepBpjsAgg
{
 
    public class ResepBpjsDeleteService : IResepBpjsDeleteService
    {
        private readonly BpjsOptions _opt;
        private readonly string _timestamp;
        private readonly string _signature;
        //private readonly string _decryptKey;
        public ResepBpjsDeleteService(IOptions<BpjsOptions> opt)
        {
            _opt = opt.Value;
            _timestamp = BpjsHelper.GetTimeStamp().ToString();
            _signature = BpjsHelper.GenHMAC256(_opt.ConsId + "&" + _timestamp, _opt.SecretKey);
            //_decryptKey = _opt.ConsId + _opt.SecretKey + _timestamp;
        }
        public ResepBpjsDeleteResponseDto Execute(ResepBpjsDeleteParam param)
        {
            var reqObj = new ResepBpjsDeleteRequest(param.NoApotik, param.NoSep, param.NoResep);
            var reqBody = JsonConvert.SerializeObject(reqObj);
            var endpoint = $"{_opt.BaseApiUrl}/sjpresep/v3/hapusresep";
            var client = new RestClient(endpoint);
            var request = new RestRequest(Method.DELETE)
                .AddHeaders(new Dictionary<string, string>
                {
            {"X-cons-id", _opt.ConsId},
            {"X-timestamp", _timestamp},
            {"X-signature", _signature},
            {"user_key", _opt.UserKey},
            {"Content-Type", "application/x-www-form-urlencoded"}
                })
                .AddParameter("application/x-www-form-urlencoded", reqBody, ParameterType.RequestBody);

            var response = client.Execute<ResepBpjsDeleteResponse>(request);
            if (response.StatusCode == 0 || response.StatusCode == System.Net.HttpStatusCode.GatewayTimeout)
                throw new Exception("Hapus Resep BPJS\n Gagal terhubung ke Server BPJS");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Hapus Resep BPJS\n {response.ErrorMessage}");
            //{
            //    "metaData": {
            //        "code": "200",
            //    "message": "OK"
            //        },
            //    "response": null
            //}
            var result = new ResepBpjsDeleteResponseDto(param.NoResep, param.NoApotik);
            result.SetResp(response.Data.metaData.code, response.Data.metaData.message);
            return result;
        }
    }

    #region Dto
    public class ResepBpjsDeleteRequest
    {
        public ResepBpjsDeleteRequest(string noApotik, string noSep, string noResep)
        {
            nosjp = noApotik;
            refasalsjp = noSep;
            noresep = noResep;
        }

        public string nosjp { get; set; }
        public string refasalsjp { get; set; }
        public string noresep { get; set; }
        
    }

    public class ResepBpjsDeleteResponse
    {
        public AptolCloudResponseMeta metaData { get; set; }
        public object response { get; set; }
    }

    #endregion
}