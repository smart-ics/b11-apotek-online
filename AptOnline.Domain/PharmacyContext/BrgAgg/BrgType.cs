using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.PharmacyContext.BrgAgg;

public record BrgType
{
    public BrgType(string id, string name)
    {
        if (id == string.Empty ^ name == string.Empty)
            throw new ArgumentException("BrgType is invalid state");
        BrgId = id;
        BrgName = name;
    }
    public string BrgId { get; }
    public string BrgName { get; }
    public static BrgType Default => new BrgType(string.Empty, string.Empty);
}

public static class BrgTypeTest
{
    [Theory]
    [InlineData("","")]
    [InlineData("A","B")]
    public static void UT1_GivenValidInput_ThenSuccess(string id, string name)
    {
        var act = new BrgType(id, name);
    }

    [Fact]
    public static void UT2_GivenSomeEmpty_ThenThrowArgumentException()
    {
        var act = () => new BrgType("A", "");
        act.Should().Throw<ArgumentException>();
    }
}