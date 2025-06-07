using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using AptOnline.Application.AptolCloudContext.ObatBpjsAgg;
using AptOnline.Application.AptolCloudContext.PoliBpjsAgg;
using AptOnline.Application.AptolCloudContext.ResepBpjsAgg;
using AptOnline.Domain.AptolCloudContext.ObatBpjsAgg;
using AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;
using AptOnline.Domain.AptolCloudContext.ResepBpjsAgg;
using AptOnline.Infrastructure.AptolCloudContext.PoliBpjsAgg;
using AptOnline.Infrastructure.AptolCloudContext.Shared;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit.Sdk;

namespace AptOnline.Infrastructure.AptolCloudContext.ResepBpjsAgg
{
    public class ObatBpjsPerSepListService : IObatBpjsPerSepListService
    {
        private readonly BpjsOptions _opt;
        private readonly string _timestamp;
        private readonly string _signature;
        private readonly string _decryptKey;

        public ObatBpjsPerSepListService(IOptions<BpjsOptions> opt)
        {
            _opt = opt.Value;
            _timestamp = BpjsHelper.GetTimeStamp().ToString();
            _signature = BpjsHelper.GenHMAC256(_opt.ConsId + "&" + _timestamp, _opt.SecretKey);
            _decryptKey = _opt.ConsId + _opt.SecretKey + _timestamp;
        }

        public IEnumerable<ResepBpjsItemModel> Execute(ObatBpjsPerSepListServiceParam req)
        {
            var endpoint = $"{_opt.BaseApiUrl}/pelayanan/obat/daftar/{req.NoSep}";

            var client = new RestClient(endpoint);
            var request = new RestRequest(Method.GET)
                .AddHeaders(new Dictionary<string, string>
                {
            {"X-cons-id", _opt.ConsId},
            {"X-timestamp", _timestamp},
            {"X-signature", _signature},
            {"user_key", _opt.UserKey},
            {"Content-Type", "application/json"}
                });
            var response = client.Execute(request);
            if (response.StatusCode == 0 || response.StatusCode == System.Net.HttpStatusCode.GatewayTimeout)
                throw new Exception("Daftar Pelayanan Obat BPJS\n Gagal terhubung ke Server BPJS");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Daftar Pelayanan Obat BPJS\n {response.Content}");
            var jResult = JObject.Parse(response.Content);
            var tmpMeta = jResult.SelectToken("metaData");
            var tmpResp = jResult.SelectToken("response")?.ToString();
            if (!tmpMeta["code"].ToString().Equals("200"))
                throw new KeyNotFoundException($"Daftar Pelayanan Obat BPJS\n[{tmpMeta["code"].ToString()}] " +
                    $"{tmpMeta["message"].ToString()}");
            try
            {
                var decryptedResp = BpjsHelper.Decrypt(_decryptKey, tmpResp);
                jResult["response"] = JObject.Parse(decryptedResp);
                var oResult = jResult.ToObject<ObatBpjsPerSepListResponse>();
                var result = oResult.response.listobat.Select(x => x.ToModel());
                return result;
            }
            catch { throw new Exception($"Daftar Pelayanan Obat BPJS\n{response.Content}"); }
        }
    }
    #region Dto
    public class ObatBpjsPerSepListResponse
    {
        public ObatBpjsPerSepListDto response { get; set; }
        public AptolCloudResponseMeta metaData { get; set; }
    }
    public class ObatBpjsPerSepListDto
    {
        public string noSepApotek { get; set; }
        public string noSepAsal { get; set; }
        public string noresep { get; set; }
        public string nokartu { get; set; }
        public string nmpst { get; set; }
        public string kdjnsobat { get; set; }
        public string nmjnsobat { get; set; }
        public string tglpelayanan { get; set; }
        public IEnumerable<ListobatItem> listobat { get; set; }
    }

    public class ListobatItem
    {
        public string kodeobat { get; set; }
        public string namaobat { get; set; }
        public string tipeobat { get; set; }
        public string signa1 { get; set; }
        public string signa2 { get; set; }
        public string hari { get; set; }
        public object permintaan { get; set; }
        public string jumlah { get; set; }
        public string harga { get; set; }
        public ResepBpjsItemModel ToModel()
        {
            return new ResepBpjsItemModel
                (
                kodeobat, namaobat, tipeobat.Equals("R"), 
                Convert.ToDecimal(signa1), Convert.ToDecimal(signa2),
                Convert.ToDecimal(hari), Convert.ToDecimal(permintaan),
                Convert.ToDecimal(jumlah), Convert.ToDecimal(harga)
                );
        }
    }
    #endregion
}