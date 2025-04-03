using AptOnline.Domain.PharmacyContext.BrgAgg;
using AptOnline.Domain.PharmacyContext.DphoAgg;
using FluentAssertions;
using GuardNet;
using Xunit;

namespace AptOnline.Domain.PharmacyContext.MapDphoAgg;

public class MapDphoModel
{
    public MapDphoModel(BrgType brg, DphoRefference dpho)
    {
        if (brg == BrgType.Default ^ dpho == DphoRefference.Default)
            throw new ArgumentException("MapDpho is invalid state");
        Brg = brg;
        Dpho = dpho;
    }
    public static MapDphoModel Default 
        => new MapDphoModel(BrgType.Default, DphoRefference.Default);

    public BrgType Brg { get; private set; }
    public DphoRefference Dpho { get; private set; }
}

public interface IBrgKey
{
    string BrgId { get; }
}

public class MapDphoModelTest
{
    [Theory]
    [InlineData("A", "B", "C", "D")]
    [InlineData("", "", "", "")]
    public void UT1_GivenAllValidInput_ShouldSuccess(string brgId, string brgName,
        string dphoId, string dphoName)
    {
        var brg = new BrgType(brgId, brgName);
        var dpho = new DphoRefference(dphoId, dphoName);
        var act = () => new MapDphoModel(brg, dpho);
    }
    [Fact]
    public void UT2_GivenSomeEmpty_ShouldReturnException()
    {
        var brg = new BrgType("A", "B");
        var act = () => new MapDphoModel(brg, DphoRefference.Default);
        act.Should().Throw<ArgumentException>();
    }

}