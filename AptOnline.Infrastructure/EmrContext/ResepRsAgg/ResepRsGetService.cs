using AptOnline.Application.EmrContext.ResepRsAgg;
using AptOnline.Domain.EmrContext.ResepRsAgg;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using RestSharp;

namespace AptOnline.Infrastructure.EmrContext.ResepRsAgg;

public class ResepRsGetService : IResepRsGetService
{
    private readonly FarmasiOptions _opt;

    public ResepRsGetService(IOptions<FarmasiOptions> opt)
    {
        _opt = opt.Value;
    }
    public ResepRsModel Execute(IResepRsKey req)
    {
        var resepRsDto = Task.Run(() => ExecuteAsync(req.ResepId)).GetAwaiter().GetResult();
        return resepRsDto?.data.ToModel();
    }

    private async Task<ResepRsDto> ExecuteAsync(string id)
    {
        if (id.Trim().Length == 0)
            return null;

        // BUILD
        var endpoint = $"{_opt.BaseApiUrl}/api/Resep";
        var client = new RestClient(endpoint);
        var req = new RestRequest("{resepId}")
            .AddUrlSegment("resepId", id);

        //  EXECUTE
        var response = await client.ExecuteGetAsync<ResepRsDto>(req);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return null;
        //  RETURN
        return response.Data;
    }
}
