using AptOnline.Api.Models;
using AptOnline.Helpers;
using Microsoft.Extensions.Options;
using RestSharp;

namespace AptOnline.Api.Services;

public interface IGetLayananBillingService
{
    LayananDto? Execute(string regId);
}
public class GetLayananBillingService : IGetLayananBillingService
{
    private readonly BillingOptions _opt;

    public GetLayananBillingService(IOptions<BillingOptions> opt)
    {
        _opt = opt.Value;
    }

    public LayananDto? Execute(string id)
    {

        var layanan = Task.Run(() => ExecuteAsync(id)).GetAwaiter().GetResult();
        if (layanan is null) return null;
        return layanan;
    }

    private async Task<LayananDto?> ExecuteAsync(string id)
    {
        if (id.Trim().Length == 0)
            return null;
        // BUILD
        var endpoint = $"{_opt.BaseApiUrl}/api/Layanan";
        var client = new RestClient(endpoint);
        var req = new RestRequest("{id}")
            .AddUrlSegment("id", id);

        //  EXECUTE
        var response = await client.ExecuteGetAsync<LayananDto>(req);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return null;
        //  RETURN
        return response.Data;
    }
}
