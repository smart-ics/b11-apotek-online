using AptOnline.Application.PharmacyContext.MapDphoAgg;
using AptOnline.Domain.PharmacyContext.BrgAgg;
using AptOnline.Domain.PharmacyContext.DphoAgg;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using RestSharp;

namespace AptOnline.Infrastructure.PharmacyContext.MapDphoAgg;

public class MapDphoGetService : IMapDphoGetService
{
    private readonly FarmasiOptions _opt;

    public MapDphoGetService(IOptions<FarmasiOptions> opt)
    {
        _opt = opt.Value;
    }

    public MapDphoModel Execute(IBrgKey brgKey)
    {
        var response = Task.Run(() => GetData(brgKey.BrgId)).GetAwaiter().GetResult();
        var brg = new BrgType(
            response?.data?.brgId ?? string.Empty, 
            response?.data?.brgName ?? string.Empty);
        var dpho = new DphoRefference(
            response?.data?.dphoId ?? string.Empty,
            response?.data?.dphoName ?? string.Empty);
        var result = new MapDphoModel(brg, dpho);
        return result;
    }

    private async Task<MapDphoGetResponse> GetData(string id)
    {
        if (id.Trim().Length == 0)
            return null;

        // BUILD
        var endpoint = $"{_opt.BaseApiUrl}/api/Dpho/Map/{id}";
        var client = new RestClient(endpoint);
        var req = new RestRequest();

        //  EXECUTE
        var response = await client.ExecuteGetAsync<MapDphoGetResponse>(req);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return null;

        //  RETURN
        return response.Data;
    }
}



