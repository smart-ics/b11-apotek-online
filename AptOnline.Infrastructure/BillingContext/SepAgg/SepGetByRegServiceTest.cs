using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.SepAgg;
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
        var reg = new RegType("RG01376985",
            new DateTime(2024, 6, 6, 23, 26, 31), 
            "337502200146916", "KEISHA KHALIQA RIZQI");
        var dokter = new DokterType("226634", "dr. Dwi Riyanto, Sp.A");
        var expected = new SepType("JP00580127", 
            new DateTime(2024, 6, 6, 23, 26, 31), 
            "1104R0040624V002150",
            "0002278745223", 
            reg, dokter, true, string.Empty);
        
        var act = _sut.Execute(RegType.Key("RG01376985"));
        act.Should().BeEquivalentTo(expected);
    }
    
}