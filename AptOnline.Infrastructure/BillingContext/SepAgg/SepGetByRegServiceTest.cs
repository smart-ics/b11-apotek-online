using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.SepContext.KelasRawatFeature;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.SepFeature;
using AptOnline.Infrastructure.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Xunit;

namespace AptOnline.Infrastructure.BillingContext.SepAgg;

public class SepGetByRegServiceTest
{
    private SepGetByRegService _sut;
    private readonly IOptions<BillingOptions> _opt;

    public SepGetByRegServiceTest()
    {
        _opt = FakeAppSetting.GetBillingOptions();
        _sut = new SepGetByRegService(_opt);
    }
    
    [Fact]
    public void ExecuteTest()
    {
        var reg = RegType.Load("RG01376985",
            new DateTime(2024, 6, 6, 23, 26, 31), 
            new DateTime(3000,1,1, 0, 0, 0),
            PasienType.Load("337502200146916", "KEISHA KHALIQA RIZQI", new DateTime(3000,1,1), GenderType.Default),
            JenisRegEnum.Unknown, KelasRawatType.Default);
        var dokter = new DokterType("226634", "dr. Dwi Riyanto, Sp.A");
        var pesertaBpjs = PesertaBpjsType.Default with { PesertaBpjsId = "0002278745223" };
        var expected = new SepType("JP00580127",
            new DateTime(2024, 6, 6, 23, 26, 31),
            "1104R0040624V002150",
            pesertaBpjs.ToRefference(),
            reg, dokter, true, string.Empty, "1");
        
        var act = _sut.Execute(RegType.Key("RG01376985"));
        act.Should().BeEquivalentTo(expected);
    }
    
}