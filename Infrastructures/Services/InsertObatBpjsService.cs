
using AptOnline.Api.Models;
using System.Security.Cryptography.X509Certificates;
using AptOnline.Api.Helpers;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using RestSharp;
using JknBridgerService.Helpers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AptOnline.Api.Infrastructures.Services
{
    public interface IInsertObatBpjsService
    {
        InsertObatRespDto ExecuteRacik(InsertObatRacikReqDto req);
        InsertObatRespDto ExecuteNonRacik(InsertObatNonRacikReqDto req);
    }
    public class InsertObatBpjsService : IInsertObatBpjsService
    {
        private readonly BpjsOptions _opt;
        private readonly string _timestamp;
        private readonly string _signature;
        private readonly string _decryptKey;
        public InsertObatBpjsService(IOptions<BpjsOptions> opt)
        {
            _opt = opt.Value;
            _timestamp = BpjsHelper.GetTimeStamp().ToString();
            _signature = BpjsHelper.GenHMAC256(_opt.ConsId + "&" + _timestamp, _opt.SecretKey);
            _decryptKey = _opt.ConsId + _opt.SecretKey + _timestamp;
        }

        public InsertObatRespDto ExecuteNonRacik(InsertObatNonRacikReqDto req)
        {
            var jReq = JObject.FromObject(req);
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
                LogHelper.Log(req.NOSJP, reqBody, response.Content, response.StatusCode.ToString());
                try
                {
                    //{ "response":null,"metaData":{ "code":"201","message":"300 - Obat Beirisan dengan pemakaian obat Tgl Resep 28-01-2025"} }
                    var result = JsonConvert.DeserializeObject<InsertObatRespDto>(response.Content);
                    return result;
                }
                catch
                {
                    throw new Exception(response.Content);
                }
            }
            else
            {
                LogHelper.Log(req.NOSJP, reqBody, response.ErrorMessage, response.StatusCode.ToString());
                throw new Exception(response.ErrorMessage);
            }
        }

        public InsertObatRespDto ExecuteRacik(InsertObatRacikReqDto req)
        {
            var jReq = JObject.FromObject(req);
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
            LogHelper.Log(req.NOSJP, reqBody, response.Content, response.StatusCode.ToString());
            if (response.StatusCode == System.Net.HttpStatusCode.OK)

            {
                try
                {
                    //{"response":null,"metaData":{"code":"200","message":"Obat Berhasil Simpan.."}}
                    var result = JsonConvert.DeserializeObject<InsertObatRespDto>(response.Content);
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

    public class InsertObatNonRacikReqDto
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

    public class InsertObatRacikReqDto
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

    public class InsertObatRespDto
    {
        public object response { get; set; }
        public InsertObatRespMetadata metaData { get; set; }
    }

    public class InsertObatRespMetadata
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    #endregion
}