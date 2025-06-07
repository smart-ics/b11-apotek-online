using System.Globalization;
using AptOnline.Application.AptolCloudContext.ObatBpjsAgg;
using AptOnline.Domain.AptolCloudContext.ResepBpjsAgg;
using AptOnline.Infrastructure.AptolCloudContext.Shared;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AptOnline.Infrastructure.AptolCloudContext.ResepBpjsAgg
{
    public class ResepBpjsListService : IResepBpjsListService
    {
        private readonly BpjsOptions _opt;
        private readonly string _timestamp;
        private readonly string _signature;
        private readonly string _decryptKey;

        public ResepBpjsListService(IOptions<BpjsOptions> opt)
        {
            _opt = opt.Value;
            _timestamp = BpjsHelper.GetTimeStamp().ToString();
            _signature = BpjsHelper.GenHMAC256(_opt.ConsId + "&" + _timestamp, _opt.SecretKey);
            _decryptKey = _opt.ConsId + _opt.SecretKey + _timestamp;
        }
        public IEnumerable<ResepBpjsModel> Execute(ResepBpjsListServiceParam req)
        {
            var tipe = "TGLRSP";
            var tgl1 = DateTime.ParseExact(req.TglAwal, "yyyy-MM-dd", CultureInfo.InvariantCulture).AddSeconds(1);
            var tgl2 = DateTime.ParseExact(req.TglAkhir, "yyyy-MM-dd", CultureInfo.InvariantCulture).AddDays(1).AddSeconds(-1);
            var reqObj = new ResepBpjsListRequest(_opt.PpkId, req.KodeJenisObat, tipe,
                tgl1.ToString("yyyy-MM-dd HH:mm:ss"), tgl2.ToString("yyyy-MM-dd HH:mm:ss"));

            var reqBody = JsonConvert.SerializeObject(reqObj);
            var endpoint = $"{_opt.BaseApiUrl}/daftarresep";

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
            if (response.StatusCode == 0 || response.StatusCode == System.Net.HttpStatusCode.GatewayTimeout)
                throw new Exception("Daftar Resep BPJS\n Gagal terhubung ke Server BPJS");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Daftar Resep BPJS\n {response.ErrorMessage}");
            var jResult = JObject.Parse(response.Content);
            var tmpMeta = jResult.SelectToken("metaData");
            var tmpResp = jResult.SelectToken("response")?.ToString();
            if (!tmpMeta["code"].ToString().Equals("200"))
                throw new KeyNotFoundException($"Daftar Resep BPJS\n[{tmpMeta["code"].ToString()}] " +
                    $"{tmpMeta["message"].ToString()}");
            try
            {
                var decryptedResp = BpjsHelper.Decrypt(_decryptKey, tmpResp);
                jResult["response"] = JArray.Parse(decryptedResp);
                var oResult = jResult.ToObject<ResepBpjsListResponse>();
                var result = oResult.response.Select(x => new ResepBpjsModel(
                    x.NOAPOTIK, x.NOSEP_KUNJUNGAN, x.NOKARTU, x.NAMA, x.FASKESASAL, 
                    x.NORESEP, x.TGLRESEP, x.KDJNSOBAT, x.TGLENTRY));
                return result;
            }
            catch { throw new Exception($"Daftar Resep BPJS\n{response.Content}"); }
            //return null;
        }
    }
    #region Dto
    public class ResepBpjsListRequest
    {
        public ResepBpjsListRequest(string kdppk, string kdJnsObat, string jnsTgl, string tglMulai, string tglAkhir)
        {
            this.kdppk = kdppk;
            KdJnsObat = kdJnsObat;
            JnsTgl = jnsTgl;
            TglMulai = tglMulai;
            TglAkhir = tglAkhir;
        }

        public string kdppk { get; set; }
        public string KdJnsObat { get; set; }
        public string JnsTgl { get; set; }
        public string TglMulai { get; set; }
        public string TglAkhir { get; set; }
    }
    public class ResepBpjsListResponse
    {
        public IEnumerable<ResepBpjsListItemDto> response { get; set; }
        public AptolCloudResponseMeta metaData { get; set; }
    }
    public class ResepBpjsListItemDto
    {
        public string NORESEP { get; set; }
        public string NOAPOTIK { get; set; }
        public string NOSEP_KUNJUNGAN { get; set; }
        public string NOKARTU { get; set; }
        public string NAMA { get; set; }
        public string TGLENTRY { get; set; }
        public string TGLRESEP { get; set; }
        public string TGLPELRSP { get; set; }
        public string BYTAGRSP { get; set; }
        public string BYVERRSP { get; set; }
        public string KDJNSOBAT { get; set; }
        public string FASKESASAL { get; set; }
        public string FLAGITER { get; set; }
    }
    #endregion
}