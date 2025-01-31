using AptOnline.Api.Models;
using AptOnline.Helpers;
using Microsoft.Extensions.Options;
using RestSharp;

namespace AptOnline.Api.Services;

public interface IGetDuFarmasiService
{
    PenjualanDto? Execute(string duId);
}
public class GetDuFarmasiService : IGetDuFarmasiService
{
    private readonly FarmasiOptions _opt;

    public GetDuFarmasiService(IOptions<FarmasiOptions> opt)
    {
        _opt = opt.Value;
    }

    public PenjualanDto? Execute(string id)
    {

        var du = Task.Run(() => ExecuteAsync(id)).GetAwaiter().GetResult();
        if (du is null) return null;
        return du;
    }

    private async Task<PenjualanDto?> ExecuteAsync(string id)
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
