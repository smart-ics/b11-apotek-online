using System.Net;
using AptOnline.Application.AptolCloudContext.ResepBpjsAgg;
using AptOnline.Infrastructure.AptolCloudContext.Shared;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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

        public ObatBpjsInsertResponseDto Execute(ObatBpjsInsertParam req)
        {
            if (req.Obat.IsRacik)
                return ExecuteRacik(req);
            else
                return ExecuteNonRacik(req);
        }

        //public ObatBpjsInsertResponseDto ExecuteNonRacik(ObatBpjsInsertParam reqParam)
        //{
        //    var reqObj = BuildRequestNonRacik(reqParam);
        //    var reqBody = JObject.FromObject(reqObj);
        //    var dto = new ObatBpjsInsertResponseDto(reqParam.Obat.Brg.BrgId, reqParam.Obat.Brg.BrgName);
        //    var endpoint = $"{_opt.BaseApiUrl}/obatnonracikan/v3/insert";
        //    var client = new RestClient(endpoint)
        //    {
        //        ClientCertificates = new X509CertificateCollection()
        //    };
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("X-cons-id", _opt.ConsId);
        //    request.AddHeader("X-timestamp", _timestamp);
        //    request.AddHeader("X-signature", _signature);
        //    request.AddHeader("user_key", _opt.UserKey);
        //    request.AddHeader("Content-Type", "Application/x-www-form-urlencoded");

        //    request.AddParameter("application/x-www-form-urlencoded", reqBody, ParameterType.RequestBody);

        //    var response = client.Execute(request);

        //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        try
        //        {
        //            //{"response":null,"metaData":{"code":"200","message":"Obat Berhasil Simpan.."}}
        //            //{ "response":null,"metaData":{ "code":"201","message":"300 - Obat Beirisan dengan pemakaian obat Tgl Resep 28-01-2025"} }
        //            var resp = JsonConvert.DeserializeObject<ObatBpjsInsertResponse>(response.Content);
        //            dto.SetResp(resp.metaData.code, resp.metaData.message);
        //        }
        //        catch
        //        {
        //            throw new Exception(response.Content);
        //        }
        //    }
        //    else
        //        throw new Exception(response.ErrorMessage);
        //    return dto;
        //}
        public ObatBpjsInsertResponseDto ExecuteNonRacik(ObatBpjsInsertParam reqParam)
        {
            var reqObj = BuildRequestNonRacik(reqParam);
            var reqBody = JsonConvert.SerializeObject(reqObj);
            var dto = new ObatBpjsInsertResponseDto(reqParam.Obat.Brg.BrgId, reqParam.Obat.Brg.BrgName);
            var endpoint = $"{_opt.BaseApiUrl}/obatnonracikan/v3/insert";

            var client = new RestClient(endpoint);
            var request = new RestRequest(Method.POST)
                .AddHeaders(new Dictionary<string, string>
                {
            {"X-cons-id", _opt.ConsId},
            {"X-timestamp", _timestamp},
            {"X-signature", _signature},
            {"user_key", _opt.UserKey},
            {"Content-Type", "application/x-www-form-urlencoded"}
                })
                .AddParameter("application/x-www-form-urlencoded", reqBody, ParameterType.RequestBody);

            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Insert Obat Non Racik\n {response.ErrorMessage}");

            try
            {
                var resp = JsonConvert.DeserializeObject<ObatBpjsInsertResponse>(response.Content);
                dto.SetResp(resp.metaData.code, resp.metaData.message);
            }
            catch
            {
                throw new Exception($"Insert Obat Non Racik\n {response.Content}");
            }

            return dto;
        }

        public ObatBpjsInsertResponseDto ExecuteRacik(ObatBpjsInsertParam reqParam)
        {
            var reqObj = BuildRequestRacik(reqParam);
            var reqBody = JsonConvert.SerializeObject(reqObj);
            var dto = new ObatBpjsInsertResponseDto(reqParam.Obat.Brg.BrgId, reqParam.Obat.Brg.BrgName);
            var endpoint = $"{_opt.BaseApiUrl}/obatracikan/v3/insert";

            var client = new RestClient(endpoint);
            var request = new RestRequest(Method.POST)
                .AddHeaders(new Dictionary<string, string>
                {
            {"X-cons-id", _opt.ConsId},
            {"X-timestamp", _timestamp},
            {"X-signature", _signature},
            {"user_key", _opt.UserKey},
            {"Content-Type", "application/x-www-form-urlencoded"}
                })
                .AddParameter("application/x-www-form-urlencoded", reqBody, ParameterType.RequestBody);

            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.ErrorMessage);

            try
            {
                var resp = JsonConvert.DeserializeObject<ObatBpjsInsertResponse>(response.Content);
                dto.SetResp(resp.metaData.code, resp.metaData.message);
            }
            catch
            {
                throw new Exception(response.Content);
            }

            return dto;
        }
        //public ObatBpjsInsertResponseDto ExecuteRacik(ObatBpjsInsertParam reqParam)
        //{
        //    var reqObj = BuildRequestRacik(reqParam);
        //    //var jReq = JObject.FromObject(reqParam.Obat);
        //    var reqBody = JsonConvert.SerializeObject(reqObj);
        //    var dto = new ObatBpjsInsertResponseDto(reqParam.Obat.Brg.BrgId, reqParam.Obat.Brg.BrgName);
        //    var endpoint = $"{_opt.BaseApiUrl}/obatnonracikan/v3/insert";
        //    var client = new RestClient(endpoint)
        //    {
        //        ClientCertificates = new X509CertificateCollection()
        //    };
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("X-cons-id", _opt.ConsId);
        //    request.AddHeader("X-timestamp", _timestamp);
        //    request.AddHeader("X-signature", _signature);
        //    request.AddHeader("user_key", _opt.UserKey);
        //    request.AddHeader("Content-Type", "Application/x-www-form-urlencoded");

        //    request.AddParameter("application/x-www-form-urlencoded", reqBody, ParameterType.RequestBody);
        //    var response = client.Execute(request);
        //    if (response.StatusCode == System.Net.HttpStatusCode.OK)

        //    {
        //        try
        //        {
        //            //{"response":null,"metaData":{"code":"200","message":"Obat Berhasil Simpan.."}}
        //            //{ "response":null,"metaData":{ "code":"201","message":"300 - Obat Beirisan dengan pemakaian obat Tgl Resep 28-01-2025"} }
        //            var resp = JsonConvert.DeserializeObject<ObatBpjsInsertResponse>(response.Content);
        //            dto.SetResp(resp.metaData.code, resp.metaData.message);
        //        }
        //        catch
        //        {
        //            throw new Exception(response.Content);
        //        }
        //    }
        //    else
        //        throw new Exception(response.ErrorMessage);
        //    return dto;
        //}
        #region Request Builder
        private static ObatBpjsInsertRacikRequest BuildRequestRacik(ObatBpjsInsertParam param)
        {
            var item = param.Obat;
            return new ObatBpjsInsertRacikRequest
            {
                NOSJP = param.NoApotik,
                NORESEP = param.NoResep,
                JNSROBT = item.RacikId,
                KDOBT = item.Dpho.DphoId,
                NMOBAT = item.Dpho.DphoName,
                SIGNA1OBT = item.Signa1,
                SIGNA2OBT = Convert.ToInt16(item.Signa2),
                PERMINTAAN = item.Permintaan,
                JMLOBT = item.Jumlah,
                JHO = item.Jho,
                CatKhsObt = ""
            };
        }
        private static ObatBpjsInsertNonRacikRequest BuildRequestNonRacik(ObatBpjsInsertParam param)
        {
            var item = param.Obat;
            return new ObatBpjsInsertNonRacikRequest
            {
                NOSJP = param.NoApotik,
                NORESEP = param.NoResep,
                KDOBT = item.Dpho.DphoId,
                NMOBAT = item.Dpho.DphoName,
                SIGNA1OBT = item.Signa1,
                SIGNA2OBT = Convert.ToInt16(item.Signa2),
                JMLOBT = item.Jumlah,
                JHO = item.Jho,
                CatKhsObt = ""
            };
        }
        #endregion
    }
    #region Dto

    public class ObatBpjsInsertNonRacikRequest
    {
       // public string BarangId { get; set; }
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
    public class ObatBpjsInsertRacikRequest
    {
        //public string BarangId {  get; set; }    
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