using AptOnline.Application.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.SepContext.KelasRawatFeature;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Infrastructure.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using RestSharp;
using Xunit;

namespace AptOnline.Infrastructure.BillingContext.RegAgg;

public class RegGetService : IRegGetService
{   
    private readonly BillingOptions _opt;
    public RegGetService(IOptions<BillingOptions> opt)
    {
        _opt = opt.Value;
    }
    public RegType Execute(IRegKey req)
    {
        var response = Task.Run(() => ExecuteAsync(req.RegId)).GetAwaiter().GetResult();
        var dto = response?.data;
        if (dto is null)
            return RegType.Default;

        var result = dto.ToModel();
        return result;
    }

    private async Task<RegGetResponse?> ExecuteAsync(string id)
    {
        if (id.Trim().Length == 0)
            return null;

        // BUILD
        var endpoint = $"{_opt.BaseApiUrl}/api/reg";
        var client = new RestClient(endpoint);
        var req = new RestRequest("{id}")
            .AddUrlSegment("id", id);

        //  EXECUTE
        var response = await client.ExecuteGetAsync<RegGetResponse>(req);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return null;

        //  RETURN
        return response.Data;
    }
}

public class RegGetServiceTest
{
    private readonly IRegGetService _sut;
    public RegGetServiceTest()
    {
        var opt = FakeAppSetting.GetBillingOptions();
        _sut = new RegGetService(opt);
    }

    [Fact]
    public void UT1_GivenValidRegID_WhenExecute_ThenReturnAsExpected()
    {
        var expected = RegType.Load(
            "RG01376274", DateTime.Parse("2024-06-05"), DateTime.Parse("2024-06-05"),
            PasienType.Load("337502200259454", "HAYDAR RAFA SATYA PUTRA,SDR", new DateTime(3000, 1, 1), GenderType.Default),
            JenisRegEnum.RawatJalan, KelasRawatType.Default);
        var regId = "RG01376274";
        var req = RegType.Key(regId);

        var actual = _sut.Execute(req);

        actual.Should().BeEquivalentTo(expected);
    }
}
