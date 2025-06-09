using AptOnline.Application.AptolCloudContext.ResepBpjsAgg;
using AptOnline.Domain.AptolCloudContext.ResepBpjsAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Infrastructure.AptolCloudContext.Shared;
using AptOnline.Infrastructure.Helpers;
using MassTransit.Internals.GraphValidation;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using static MassTransit.ValidationResultExtensions;

namespace AptOnline.Infrastructure.AptolCloudContext.ResepBpjsAgg
{
    public class ResepBpjsSaveServiceFaker //: IResepBpjsSaveService
    {
        public ResepBpjsModel Execute(ResepMidwareModel req)
        {
            //{"noSep_Kunjungan":"0137R0410125V000002","noKartu":"0002054278642","nama":"SUGIANTO",
            //"faskesAsal":"0137A047","noApotik":"0137A04701250000001","noResep":"56067",
            //"tglResep":"2025-01-28", "kdJnsObat":"2","tglEntry":"2025-01-28"}

            return new ResepBpjsModel("0137A04701250000001",
                req.Sep.SepNo, req.Sep.PesertaBpjs.PesertaBpjsNo,
                req.Registrasi.Pasien.PasienName, req.Ppk.PpkId, req.ResepRsId.Substring(4,5),
                req.CreateTimestamp.ToString("yyyy-MM-dd"), req.JenisObatId,
                req.CreateTimestamp.ToString("yyyy-MM-dd"));
        }
    }
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
            var reqObj = BuildRequest(req);
            var reqBody = JsonConvert.SerializeObject(reqObj);
            var endpoint = $"{_opt.BaseApiUrl}/sjpresep/v3/insert";

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
                throw new Exception("Simpan Resep BPJS\n Gagal terhubung ke Server BPJS");
            if(response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Simpan Resep BPJS\n {response.ErrorMessage}");
            var jResult = JObject.Parse(response.Content);
            var tmpMeta = jResult.SelectToken("metaData");
            var tmpResp = jResult.SelectToken("response")?.ToString();
            if (!tmpMeta["code"].ToString().Equals("200"))
                throw new Exception($"Simpan Resep BPJS\n[{tmpMeta["code"].ToString()}] {tmpMeta["message"].ToString()}");
            try
            {
                var decryptedResp = BpjsHelper.Decrypt(_decryptKey, tmpResp);
                jResult["response"] = JObject.Parse(decryptedResp);
                var oResult = jResult.ToObject<ResepBpjsSaveResponse>();
                return oResult.response.ToModel();
            }
            catch { throw new Exception($"Simpan Resep BPJS\n{response.Content}"); }
        }

        #region Request Builder
        private static ResepBpjsSaveRequest BuildRequest(ResepMidwareModel resep)
        {
            return new ResepBpjsSaveRequest
            {
                TGLSJP = resep.Sep.SepDateTime.ToString("yyyy-MM-dd"),
                REFASALSJP = resep.Sep.SepNo,
                POLIRSP = resep.PoliBpjs.PoliBpjsId,
                KDJNSOBAT = resep.JenisObatId,
                NORESEP = resep.ResepRsId.Substring(4, 5),
                IDUSERSJP = "test-bridging",
                TGLRSP = resep.CreateTimestamp.ToString("yyyy-MM-dd"),
                TGLPELRSP = resep.ConfirmTimeStamp.ToString("yyyy-MM-dd"),
                KdDokter = resep.Sep.Dpjp.DokterId,
                iterasi = resep.Iterasi.ToString()
            };
        }
        #endregion
    }

    #region Dto

    public class ResepBpjsSaveResponse
    {
        public ResepBpjsSaveDto response { get; set; }
        public AptolCloudResponseMeta metaData { get; set; }
    }
    #endregion
}