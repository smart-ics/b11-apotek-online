
using System.Security.Cryptography.X509Certificates;
using AptOnline.Helpers;
using Microsoft.Extensions.Options;
using RestSharp;
using JknBridgerService.Helpers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AptOnline.Api.Services
{
    public interface IInsertResepBpjsService
    {
        InsertResepBpjsRespDto Execute(InsertResepBpjsReqDto req);
    }
    public class InsertResepBpjsService : IInsertResepBpjsService
    {
        private readonly BpjsOptions _opt;
        private readonly string _timestamp;
        private readonly string _signature;
        private readonly string _decryptKey;
        public InsertResepBpjsService(IOptions<BpjsOptions> opt)
        {
            _opt = opt.Value;
            _timestamp = BpjsHelper.GetTimeStamp().ToString();
            _signature = BpjsHelper.GenHMAC256(_opt.ConsId + "&" + _timestamp, _opt.SecretKey);
            _decryptKey = _opt.ConsId + _opt.SecretKey + _timestamp;
        }
        public InsertResepBpjsRespDto Execute(InsertResepBpjsReqDto req)
        {
            var endpoint = $"{_opt.BaseApiUrl}/sjpresep/v3/insert";
            var reqBody = JsonConvert.SerializeObject(req);
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
            var result = jResult.ToObject<InsertResepBpjsRespDto>();
            return result;
        }
    }
    #region Dto

    public class InsertResepBpjsReqDto
    {
        public string TGLSJP { get; set; } //tgl sep
        public string REFASALSJP { get; set; } //nosep (api sep)
        public string POLIRSP { get; set; } //poli asal resep
        public string KDJNSOBAT { get; set; } //item obat -> mapping dhpo + sep prb
        public string NORESEP { get; set; } //kp
        public string IDUSERSJP { get; set; } //user sep/resp
        public string TGLRSP { get; set; } //
        public string TGLPELRSP { get; set; }
        public string KdDokter { get; set; } //dokter resep (mapping)
        public string iterasi { get; set; } //resep
    }

    public class InsertResepBpjsRespDto
    {
        public InsertResepBpjsRespResponse response { get; set; }
        public InsertResepBpjsRespMetadata metaData { get; set; }
    }

    public class InsertResepBpjsRespResponse
    {
        public string noSep_Kunjungan { get; set; }
        public string noKartu { get; set; }
        public string nama { get; set; }
        public string faskesAsal { get; set; }
        public string noApotik { get; set; }
        public string noResep { get; set; }
        public string tglResep { get; set; }
        public string kdJnsObat { get; set; }
        public string byTagRsp { get; set; }
        public string byVerRsp { get; set; }
        public string tglEntry { get; set; }
    }

    public class InsertResepBpjsRespMetadata
    {
        public string code { get; set; }
        public string message { get; set; }
    }
    #endregion
}