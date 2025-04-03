using AptOnline.Domain.PharmacyContext.DphoAgg;
using FluentAssertions;
using GuardNet;
using Xunit;

namespace AptOnline.Domain.PharmacyContext.MapDphoAgg;

public class MapDphoModel : IBrgKey, IDphoKey
{
    public MapDphoModel(string brgId, string brgName, string dphoId, string dphoName)
    {
        var emptyCount = new[] { brgId, brgName, dphoId, dphoName }
            .Count(string.IsNullOrEmpty);

        var isInvalid = emptyCount is > 0 and < 4;
        if (isInvalid)
            throw new ArgumentException("MapDphoModel is invalid");        
        
        BrgId = brgId;
        BrgName = brgName;
        DphoId = dphoId;
        DphoName = dphoName;
    }
    public static MapDphoModel Default 
        => new MapDphoModel(string.Empty, string.Empty, string.Empty, string.Empty);
    public string BrgId { get; private set; }
    public string BrgName { get; private set; }
    public string DphoId { get; private set; }
    public string DphoName { get; private set; }
}

public interface IBrgKey
{
    string BrgId { get; }
}

public class MapDphoModelTest
{
    [Fact]
    public void UT1_GivenAllPropertiesNotEmpty_ShouldReturnValidMapDphoModel()
    {
        var act = () => new MapDphoModel("A", "B", "C", "D");
    }
    [Fact]
    public void UT2_GivenAllPropertiesEmpty_ShouldReturnValidMapDphoModel()
    {
        var act = () => new MapDphoModel("", "", "", "");
    }
    [Fact]
    public void UT3_GivenSomePropertyEmpty_ShouldThrowArgumentException()
    {
        var act = () => new MapDphoModel("A", "", "C", "D");
        act.Should().Throw<ArgumentException>();
    }
}