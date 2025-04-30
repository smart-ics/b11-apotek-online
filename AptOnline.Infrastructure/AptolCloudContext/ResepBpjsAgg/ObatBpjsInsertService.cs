using System.Security.Cryptography.X509Certificates;
using AptOnline.Application.AptolCloudContext.ResepBpjsAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Infrastructure.AptolCloudContext.Shared;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AptOnline.Infrastructure.AptolCloudContext.ResepBpjsAgg
{
    public class ObatBpjsInsertService : IObatBpjsInsertService
    {
        private readonly BpjsOptions _opt;
        private readonly string _timestamp;
        private readonly string _signature;
        private readonly string _decryptKey;
        public ObatBpjsInsertService(IOptions<BpjsOptions> opt)
        {
            _opt = opt.Value;
            _timestamp = BpjsHelper.GetTimeStamp().ToString();
            _signature = BpjsHelper.GenHMAC256(_opt.ConsId + "&" + _timestamp, _opt.SecretKey);
            _decryptKey = _opt.ConsId + _opt.SecretKey + _timestamp;
        }

        public object Execute(ObatBpjsInsertParam req)
        {
            throw new NotImplementedException();
        }

        public object ExecuteNonRacik(ObatBpjsInsertParam reqParam)
        {
            var jReq = JObject.FromObject(reqParam.Obat);
            jReq.Property("PenjualanId").Remove();
            jReq.Property("BarangId").Remove();
            var reqBody = JsonConvert.SerializeObject(jReq);
            var endpoint = $"{_opt.BaseApiUrl}/obatnonracikan/v3/insert";
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

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                LogHelper.Log(reqParam.NoSep, reqBody, response.Content, response.StatusCode.ToString());
                try
                {
                    //{ "response":null,"metaData":{ "code":"201","message":"300 - Obat Beirisan dengan pemakaian obat Tgl Resep 28-01-2025"} }
                    var result = JsonConvert.DeserializeObject<ObatBpjsInsertResponse>(response.Content);
                    return result;
                }
                catch
                {
                    throw new Exception(response.Content);
                }
            }
            else
            {
                LogHelper.Log(reqParam.NoSep, reqBody, response.ErrorMessage, response.StatusCode.ToString());
                throw new Exception(response.ErrorMessage);
            }
        }

        public object ExecuteRacik(ObatBpjsInsertParam reqParam)
        {
            var jReq = JObject.FromObject(reqParam.Obat);
            jReq.Property("PenjualanId").Remove();
            jReq.Property("BarangId").Remove();
            var reqBody = JsonConvert.SerializeObject(jReq);
            var endpoint = $"{_opt.BaseApiUrl}/obatnonracikan/v3/insert";
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
            LogHelper.Log(reqParam.NoSep, reqBody, response.Content, response.StatusCode.ToString());
            if (response.StatusCode == System.Net.HttpStatusCode.OK)

            {
                try
                {
                    //{"response":null,"metaData":{"code":"200","message":"Obat Berhasil Simpan.."}}
                    var result = JsonConvert.DeserializeObject<ObatBpjsInsertResponse>(response.Content);
                    return result;
                }
                catch
                {
                    throw new Exception(response.Content);
                }
            }
            else
                throw new Exception(response.ErrorMessage);
        }
    }
    #region Dto

    public class ObatBpjsInsertNonRacikRequestDto
    {
        public string BarangId { get; set; }
        public string NOSJP { get; set; } //no apotik (response insert resep)
        public string NORESEP { get; set; } //kp
        public string KDOBT { get; set; } // mapping dpho
        public string NMOBAT { get; set; } // mapping dpho
        public int SIGNA1OBT { get; set; } //resep
        public int SIGNA2OBT { get; set; } //resep
        public int JMLOBT { get; set; } //resep
        public int JHO { get; set; } //resep
        public string CatKhsObt { get; set; } //resep
    }

    public class ObatBpjsInsertRacikRequestDto
    {
        public string BarangId {  get; set; }    
        public string NOSJP { get; set; }
        public string NORESEP { get; set; }
        public string JNSROBT { get; set; }
        public string KDOBT { get; set; }
        public string NMOBAT { get; set; }
        public int SIGNA1OBT { get; set; }
        public int SIGNA2OBT { get; set; }
        public int PERMINTAAN { get; set; }
        public int JMLOBT { get; set; }
        public int JHO { get; set; }
        public string CatKhsObt { get; set; }
    }
    public class ObatBpjsInsertResponse
    {
        public object response { get; set; }
        public AptolCloudResponseMeta metaData { get; set; }
    }
    #endregion
}