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

    public MapDphoType Execute(IBrgKey brgKey)
    {
        var response = Task.Run(() => GetData(brgKey.BrgId)).GetAwaiter().GetResult();
        var brg = new BrgType(
            response?.data?.BrgId ?? string.Empty, 
            response?.data?.BrgName ?? string.Empty);
        var dpho = new DphoRefference(
            response?.data?.DphoId ?? string.Empty,
            response?.data?.DphoName ?? string.Empty);
        var result = new MapDphoType(brg, dpho);
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
        //  TODO: Mapping Result ke MapDphoDto masih gagal
        var response = await client.ExecuteGetAsync<MapDphoGetResponse>(req);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return null;

        //  RETURN
        return response.Data;
    }
}



