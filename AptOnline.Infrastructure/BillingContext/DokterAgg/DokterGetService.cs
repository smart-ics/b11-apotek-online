using AptOnline.Application.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;

namespace AptOnline.Infrastructure.BillingContext.DokterAgg;

public class DokterGetService : IDokterGetService
{
    private readonly BillingOptions _opt;
    private readonly ITokenService _token;

    public DokterGetService(IOptions<BillingOptions> opt, ITokenService token)
    {
        _opt = opt.Value;
        _token = token;
    }

    public DokterModel Execute(IDokterKey req)
    {
        var dokter = Task.Run(() => ExecuteAsync(req.DokterId)).GetAwaiter().GetResult();
        var result = dokter is null ? 
            null : new DokterModel(dokter.data.dokterId, dokter.data.dokterName);
        return result!;
    }

    private async Task<DokterDto?> ExecuteAsync(string id)
    {
        if (id.Trim().Length == 0)
            return null;
        // BUILD
        var token = await _token.Get("Aptol_Billing") ??
            throw new ArgumentException($"Get Token {_opt.BaseApiUrl} failed");
        var endpoint = $"{_opt.BaseApiUrl}/api/Dokter";
        var client = new RestClient(endpoint)
        {
            Authenticator = new JwtAuthenticator(token)
        };
        var req = new RestRequest("{id}")
            .AddUrlSegment("id", id);

        //  EXECUTE
        var response = await client.ExecuteGetAsync<DokterDto>(req);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return null;
        //  RETURN
        return response.Data;
    }
}
