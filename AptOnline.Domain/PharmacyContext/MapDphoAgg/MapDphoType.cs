using AptOnline.Domain.PharmacyContext.BrgAgg;
using AptOnline.Domain.PharmacyContext.DphoAgg;
using FluentAssertions;
using GuardNet;
using Xunit;

namespace AptOnline.Domain.PharmacyContext.MapDphoAgg;

public class MapDphoType
{
    public MapDphoType(BrgType brg, DphoRefference dpho)
    {
        if (brg == BrgType.Default ^ dpho == DphoRefference.Default)
            throw new ArgumentException("MapDpho is invalid state");
        Brg = brg;
        Dpho = dpho;
    }
    public static MapDphoType Default 
        => new MapDphoType(BrgType.Default, DphoRefference.Default);

    public BrgType Brg { get; private set; }
    public DphoRefference Dpho { get; private set; }
}

public class MapDphoModelTest
{
    [Theory]
    [InlineData("A", "B", "C", "D")]
    public void UT1_GivenAllValidInput_ShouldSuccess(string brgId, string brgName,
        string dphoId, string dphoName)
    {
        var brg = new BrgType(brgId, brgName);
        var dpho = new DphoRefference(dphoId, dphoName);
        var act = () => new MapDphoType(brg, dpho);
        act.Should().NotThrow<ArgumentException>();
    }
    [Fact]
    public void UT2_GivenSomeEmpty_ShouldReturnException()
    {
        var brg = new BrgType("A", "B");
        var act = () => new MapDphoType(brg, DphoRefference.Default);
        act.Should().Throw<ArgumentException>();
    }

}