using AptOnline.Application.BillingContext.SepAgg;
using AptOnline.Domain.BillingContext.SepAgg;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using RestSharp;

namespace AptOnline.Infrastructure.BillingContext.SepAgg;


public class SepGetService : ISepGetService
{
    private readonly BillingOptions _opt;

    public SepGetService(IOptions<BillingOptions> opt)
    {
        _opt = opt.Value;
    }

    public SepType Execute(ISepKey req)
    {
        var responseData = Task.Run(() => ExecuteAsync(req.SepId)).GetAwaiter().GetResult();
        var result = responseData?.data;
        return result;
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
