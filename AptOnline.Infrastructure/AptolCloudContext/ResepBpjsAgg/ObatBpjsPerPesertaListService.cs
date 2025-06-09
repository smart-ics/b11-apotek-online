using AptOnline.Application.AptolCloudContext.ResepBpjsAgg;
using AptOnline.Domain.AptolCloudContext.ResepBpjsAgg;
using AptOnline.Infrastructure.AptolCloudContext.Shared;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AptOnline.Infrastructure.AptolCloudContext.ResepBpjsAgg
{
    public class ObatBpjsPerPesertaListService : IObatBpjsPerPesertaListService
    {
        private readonly BpjsOptions _opt;
        private readonly string _timestamp;
        private readonly string _signature;
        private readonly string _decryptKey;

        public ObatBpjsPerPesertaListService(IOptions<BpjsOptions> opt)
        {
            _opt = opt.Value;
            _timestamp = BpjsHelper.GetTimeStamp().ToString();
            _signature = BpjsHelper.GenHMAC256(_opt.ConsId + "&" + _timestamp, _opt.SecretKey);
            _decryptKey = _opt.ConsId + _opt.SecretKey + _timestamp;
        }

        public RiwayatAptolBpjsModel Execute(ObatBpjsPerPesertaListServiceParam req)
        {
            var endpoint = $"{_opt.BaseApiUrl}/riwayatobat/{req.TglAwal}/{req.TglAkhir}/{req.NoPeserta}";

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
            if (response.StatusCode == 0 || response.StatusCode == System.Net.HttpStatusCode.BadGateway)
                throw new Exception("Riwayat Pelayanan Obat BPJS\n Gagal terhubung ke Server BPJS");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Riwayat Pelayanan Obat BPJS\n {response.Content}");
            var jResult = JObject.Parse(response.Content);
            var tmpMeta = jResult.SelectToken("metaData");
            var tmpResp = jResult.SelectToken("response")?.ToString();
            if (!tmpMeta["code"].ToString().Equals("200"))
                throw new KeyNotFoundException($"Riwayat Pelayanan Obat BPJS\n[{tmpMeta["code"].ToString()}] " +
                    $"{tmpMeta["message"].ToString()}");
            try
            {
                var decryptedResp = BpjsHelper.Decrypt(_decryptKey, tmpResp);
                jResult["response"] = JObject.Parse(decryptedResp);
                var oResult = jResult.ToObject<ObatBpjsPerPesertaListResponse>();
                var result = oResult.response.list.ToModel();
                return result;
            }
            catch { throw new Exception($"Riwayat Pelayanan Obat BPJS\n{response.Content}"); }
        }
    }
    #region Dto
    public class ObatBpjsPerPesertaListResponse
    {
        public ObatBpjsPerPesertaListDto response { get; set; }
        public AptolCloudResponseMeta metaData { get; set; }
    }

    public class ObatBpjsPerPesertaListDto
    {
        public ListItem list { get; set; }
    }

    public class ListItem
    {
        public string nokartu { get; set; }
        public string namapeserta { get; set; }
        public string tgllhr { get; set; }
        public IEnumerable<ItemHistory> history { get; set; }
        public RiwayatAptolBpjsModel ToModel()
        {
            var his = history.Select(x => x.ToModel());
            return new RiwayatAptolBpjsModel(nokartu, namapeserta, tgllhr, his);
        }
    }

    public class ItemHistory
    {
        public string nosjp { get; set; }
        public string tglpelayanan { get; set; }
        public string noresep { get; set; }
        public string kodeobat { get; set; }
        public string namaobat { get; set; }
        public string jmlobat { get; set; }
        public RiwayatAptolBpjsItemModel ToModel()
        {
            return new RiwayatAptolBpjsItemModel(nosjp, tglpelayanan, noresep, 
                kodeobat, namaobat, jmlobat);
        }

    }

    #endregion
}