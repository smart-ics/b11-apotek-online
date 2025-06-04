using AptOnline.Application.Helpers;
using AptOnline.Application.SepContext;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.SepAgg;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using RestSharp;

namespace AptOnline.Infrastructure.BillingContext.SepAgg;


public class SepGetByRegService : ISepGetByRegService
{
    private readonly BillingOptions _opt;

    public SepGetByRegService(IOptions<BillingOptions> opt)
    {
        _opt = opt.Value;
    }

    public MayBe<SepType> Execute(IRegKey req)
    {
        var responseData = Task.Run(() => ExecuteAsync(req.RegId)).GetAwaiter().GetResult();
        return MayBe
            .From(responseData?.data)
            .Map(x => x.ToSepType());
    }

    private async Task<SepGetByRegResponse?> ExecuteAsync(string regId)
    {
        if (regId.Trim().Length == 0)
            return null;
        // BUILD
        var endpoint = $"{_opt.BaseApiUrl}/api/Bpjs/Sep";
        var client = new RestClient(endpoint);
        var req = new RestRequest("{regId}")
            .AddUrlSegment("regId", regId);

        //  EXECUTE
        var response = await client.ExecuteGetAsync<SepGetByRegResponse>(req);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return null;
        //  RETURN
        return response.Data;
    }
}