using AptOnline.Application.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using RestSharp;

namespace AptOnline.Infrastructure.BillingContext.LayananAgg;

public class LayananGetService : ILayananGetService
{
    private readonly BillingOptions _opt;

    public LayananGetService(IOptions<BillingOptions> opt)
    {
        _opt = opt.Value;
    }

    public LayananModel Execute(ILayananKey req)
    {
        var responseData = Task.Run(() => ExecuteAsync(req.LayananId)).GetAwaiter().GetResult();
        var result = responseData?.data;
        return result;
    }

    private async Task<LayananGetResponse?> ExecuteAsync(string id)
    {
        if (id.Trim().Length == 0)
            return null;
        
        // BUILD
        var endpoint = $"{_opt.BaseApiUrl}/api/Layanan";
        var client = new RestClient(endpoint);
        var req = new RestRequest("{id}")
            .AddUrlSegment("id", id);

        //  EXECUTE
        var response = await client.ExecuteGetAsync<LayananGetResponse>(req);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return null;
        
        //  RETURN
        return response.Data;
    }

}
