using AptOnline.Api.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using RestSharp;

namespace AptOnline.Api.Infrastructures.Services;

public interface ITokenService
{
    Task<string?> Get(string provider);
}

public class TokenService : ITokenService
{
    private readonly BillingOptions _opt;
    private readonly IMemoryCache _cache;

    public TokenService(IOptions<BillingOptions> opt, IMemoryCache cache)
    {
        _opt = opt.Value;
        _cache = cache;
    }

    private class LoginReq
    {
        public LoginReq(string email, string pass)
        {
            Email = email;
            Pass = pass;
        }
        public string Email { get; set; }
        public string Pass { get; set; }
    }

    public async Task<string?> Get(string provider)
    {
        var result = _cache.Get<string>($"Token{provider}");
        if (result is not null)
            return result;

        var endpoint = $"{_opt.BaseApiUrl}/api/Token";
        var client = new RestClient(endpoint);
        var req = new RestRequest()
            .AddJsonBody(new LoginReq(_opt.ConsId, _opt.SecretKey));
        var response = await client.ExecutePostAsync<string>(req);

        var cacheOption = new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(1) };
        _cache.Set($"Token{provider}", response.Data, cacheOption);

        return response.Data;
    }
}
