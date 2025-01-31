using AptOnline.Api.Models;
using AptOnline.Helpers;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;

namespace AptOnline.Api.Services;

public interface IGetDokterBillingService
{
    DokterDto? Execute(string id);
}
public class GetDokterBillingService : IGetDokterBillingService
{
    private readonly BillingOptions _opt;
    private readonly ITokenService _token;

    public GetDokterBillingService(IOptions<BillingOptions> opt, ITokenService token)
    {
        _opt = opt.Value;
        _token = token;
    }

    public DokterDto? Execute(string id)
    {
        var dokter = Task.Run(() => ExecuteAsync(id)).GetAwaiter().GetResult();
        if (dokter is null) return null;
        return dokter;
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
