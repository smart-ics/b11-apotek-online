using AptOnline.Application.BillingContext.LayananAgg;
using AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;
using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Infrastructure.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using RestSharp;
using Xunit;

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
        var response = Task.Run(() => ExecuteAsync(req.LayananId)).GetAwaiter().GetResult();
        var responseData = response?.data;
        if (responseData is null)
            return LayananModel.Default;

        var result = new LayananModel(
            responseData.LayananId,
            responseData.LayananName, 
            responseData.IsActive);
        result.SetPoliBpjs(new PoliBpjsType(
            responseData.LayananBpjsId,
            responseData.LayananBpjsName));
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

public class LayananGetResponseTest
{
    private readonly LayananGetService _sut;

    public LayananGetResponseTest()
    {
        var opt = FakeAppSetting.GetBillingOptions();
        _sut = new LayananGetService(opt);
    }

    [Fact]
    public void UT1_GivenValidLayananID_WhenExecute_ThenReturnAsExpected()
    {
        var expected = new LayananModel("2RJ05", "POLI ANAK", true);
        expected.SetPoliBpjs(new PoliBpjsType("ANA", "ANAK"));
        var actual = _sut.Execute(expected);
        actual.Should().BeEquivalentTo(expected);
    }
}
