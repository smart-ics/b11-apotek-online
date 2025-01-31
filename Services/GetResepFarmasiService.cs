using AptOnline.Api.Models;
using AptOnline.Helpers;
using Microsoft.Extensions.Options;
using RestSharp;

namespace AptOnline.Api.Services;

public interface IGetResepFarmasiService
{
    ResepDto Execute(string resepId);
}
public class GetResepFarmasiService : IGetResepFarmasiService
{
    private readonly FarmasiOptions _opt;

    public GetResepFarmasiService(IOptions<FarmasiOptions> opt)
    {
        _opt = opt.Value;
    }

    public ResepDto? Execute(string id)
    {

        var du = Task.Run(() => ExecuteAsync(id)).GetAwaiter().GetResult();
        if (du is null) return null;
        return du;
    }

    private async Task<ResepDto> ExecuteAsync(string id)
    {
        if (id.Trim().Length == 0)
            return null;
        // BUILD
        var endpoint = $"{_opt.BaseApiUrl}/api/Resep";
        var client = new RestClient(endpoint);
        var req = new RestRequest("{resepId}")
            .AddUrlSegment("resepId", id);

        //  EXECUTE
        var response = await client.ExecuteGetAsync<ResepDto>(req);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return null;
        //  RETURN
        return response.Data;
    }
}
