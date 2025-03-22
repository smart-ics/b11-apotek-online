using AptOnline.Application.PharmacyContext.MapDphoAgg;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using RestSharp;

namespace AptOnline.Infrastructure.PharmacyContext.MapDphoAgg;

public class GetMapDphoService : IGetMapDphoService
{
    private readonly FarmasiOptions _opt;

    public GetMapDphoService(IOptions<FarmasiOptions> opt)
    {
        _opt = opt.Value;
    }

    public MapDphoModel Execute(string brgId)
    {
        var mapping = Task.Run(() => GetData(brgId)).GetAwaiter().GetResult();
        if (mapping is null) return null;
        var result = new MapDphoModel
        {
            BrgId = mapping.data.brgId,
            BrgName = mapping.data.brgName,
            DphoId = mapping.data.dphoId,
            DphoName = mapping.data.dphoName
        };
        return result;
    }

    private async Task<GetMapDphoResponse> GetData(string id)
    {
        if (id.Trim().Length == 0)
            return null;

        // BUILD
        var endpoint = $"{_opt.BaseApiUrl}/api/Dpho/Map/{id}";
        var client = new RestClient(endpoint);
        var req = new RestRequest();

        //  EXECUTE
        var response = await client.ExecuteGetAsync<GetMapDphoResponse>(req);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return null;
        //  RETURN
        return response.Data;
    }
}



