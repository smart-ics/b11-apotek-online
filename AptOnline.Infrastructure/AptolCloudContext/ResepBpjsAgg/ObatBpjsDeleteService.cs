using System.Net;
using AptOnline.Application.AptolCloudContext.ResepBpjsAgg;
using AptOnline.Infrastructure.AptolCloudContext.Shared;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace AptOnline.Infrastructure.AptolCloudContext.ResepBpjsAgg
{
    public class ObatBpjsDeleteService : IObatBpjsDeleteService
    {
        private readonly BpjsOptions _opt;
        private readonly string _timestamp;
        private readonly string _signature;
        public ObatBpjsDeleteService(IOptions<BpjsOptions> opt)
        {
            _opt = opt.Value;
            _timestamp = BpjsHelper.GetTimeStamp().ToString();
            _signature = BpjsHelper.GenHMAC256(_opt.ConsId + "&" + _timestamp, _opt.SecretKey);
        }

        public ObatBpjsDeleteResponseDto Execute(ObatBpjsDeleteParam reqParam)
        {
            var tipeObat = reqParam.Obat.IsRacik ? "R" : "N";
            var reqObj = new ObatBpjsDeleteRequest(reqParam.NoApotik, reqParam.NoResep, reqParam.Obat.Dpho.DphoId, tipeObat);
            var reqBody = JsonConvert.SerializeObject(reqObj);
            var dto = new ObatBpjsDeleteResponseDto(reqParam.Obat.Brg.BrgId, reqParam.Obat.Brg.BrgName);
            var endpoint = $"{_opt.BaseApiUrl}/pelayanan/obat/hapus/";

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

            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Hapus Pelayanan Obat BPJS\n {response.ErrorMessage}");

            try
            {
                var resp = JsonConvert.DeserializeObject<ObatBpjsDeleteResponse>(response.Content);
                dto.SetResp(resp.metaData.code, resp.metaData.message);
            }
            catch
            {
                throw new Exception($"Hapus Pelayanan Obat BPJS\n {response.Content}");
            }

            return dto;
        }
    }
    #region Dto

    public class ObatBpjsDeleteRequest
    {
        public ObatBpjsDeleteRequest(string noApotik, string noResep, string kodeObat, string tipeObat)
        {
            nosepapotek = noApotik;
            noresep = noResep;
            kodeobat = kodeObat;
            tipeobat = tipeObat;
        }

        public string nosepapotek { get; set; }
        public string noresep { get; set; }
        public string kodeobat { get; set; }
        public string tipeobat { get; set; }
    }
    public class ObatBpjsDeleteResponse
    {
        public object response { get; set; }
        public AptolCloudResponseMeta metaData { get; set; }
    }
    #endregion
}