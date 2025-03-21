using AptOnline.Api.Models;
using AptOnline.Api.Helpers;
using Microsoft.Extensions.Options;
using RestSharp;

namespace AptOnline.Api.Infrastructures.Services;

public interface IGetSepBillingService
{
    SepDto? Execute(string regId);
}
public class GetSepBillingService : IGetSepBillingService
{
    private readonly BillingOptions _opt;

    public GetSepBillingService(IOptions<BillingOptions> opt)
    {
        _opt = opt.Value;
    }

    public SepDto? Execute(string id)
    {

        var sep = Task.Run(() => ExecuteAsync(id)).GetAwaiter().GetResult();
        if (sep is null) return null;
        return sep;
    }

    private async Task<SepDto?> ExecuteAsync(string id)
    {
        if (id.Trim().Length == 0)
            return null;
        // BUILD
        var endpoint = $"{_opt.BaseApiUrl}/api/Bpjs/Sep";
        var client = new RestClient(endpoint);
        var req = new RestRequest("{regId}")
            .AddUrlSegment("regId", id);

        //  EXECUTE
        var response = await client.ExecuteGetAsync<SepDto>(req);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return null;
        //  RETURN
        return response.Data;
    }
}
