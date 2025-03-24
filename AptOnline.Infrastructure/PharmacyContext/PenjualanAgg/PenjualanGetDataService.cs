using AptOnline.Application.PharmacyContext.PenjualanAgg;
using AptOnline.Domain.PharmacyContext.TrsDuAgg;
using AptOnline.Infrastructure.EmrContext.ResepRsAgg;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using RestSharp;

namespace AptOnline.Infrastructure.PharmacyContext.PenjualanAgg
{
    public class PenjualanGetDataService : IPenjualanGetDataService
    {
        private readonly FarmasiOptions _opt;
        public PenjualanGetDataService(IOptions<FarmasiOptions> opt)
        {
            _opt = opt.Value;
        }

        public PenjualanModel Execute(IPenjualanKey req)
        {
            var penjualanDto = Task.Run(() => ExecuteAsync(req.PenjualanId)).GetAwaiter().GetResult();
            return penjualanDto?.data;
        }
        private async Task<PenjualanDto> ExecuteAsync(string id)
        {
            if (id.Trim().Length == 0)
                return null;

            // BUILD
            var endpoint = $"{_opt.BaseApiUrl}/api/Penjualan";
            var client = new RestClient(endpoint);
            var req = new RestRequest("{penjualanId}")
                .AddUrlSegment("penjualanId", id);

            //  EXECUTE
            var response = await client.ExecuteGetAsync<PenjualanDto>(req);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;
            //  RETURN
            return response.Data;
        }
    }
}
