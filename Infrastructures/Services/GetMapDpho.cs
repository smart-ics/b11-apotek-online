using AptOnline.Helpers;
using Microsoft.Extensions.Options;
using Nuna.Lib.ActionResultHelper;
using RestSharp;

namespace AptOnline.Api.Infrastructures.Services;

public interface IGetMapDpho
{
    RespMapDpho Execute(string brgId);
}

public class GetMapDpho : IGetMapDpho
{
    private readonly FarmasiOptions _opt;

    public GetMapDpho(IOptions<FarmasiOptions> opt)
    {
        _opt = opt.Value;
    }

    public RespMapDpho Execute(string brgId)
    {
        var mapping = Task.Run(() => GetData(brgId)).GetAwaiter().GetResult();
        if (mapping is null) return null;
        var result = new RespMapDpho
        {
            BrgId = mapping.data.brgId,
            BrgName = mapping.data.brgName,
            DphoId = mapping.data.dphoId,
            DphoName = mapping.data.dphoName
        };
        return result;
    }

    private async Task<RespMapDphoDto> GetData(string id)
    {
        if (id.Trim().Length == 0)
            return null;
        // BUILD
        var endpoint = $"{_opt.BaseApiUrl}/api/Dpho/Map/{id}";
        var client = new RestClient(endpoint);
        var req = new RestRequest();

        //  EXECUTE
        var response = await client.ExecuteGetAsync<RespMapDphoDto>(req);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return null;
        //  RETURN
        return response.Data;
    }
}


public class RespMapDphoDto
{
    public string status { get; set; }
    public string code { get; set; }
    public RespMapDphoDtoData data { get; set; }
}

public class RespMapDphoDtoData
{
    public string brgId { get; set; }
    public string brgName { get; set; }
    public string dphoId { get; set; }
    public string dphoName { get; set; }
}

public class RespMapDpho
{
    public string BrgId { get; set; }
    public string BrgName { get; set; }
    public string DphoId { get; set; }
    public string DphoName { get; set; }
}
