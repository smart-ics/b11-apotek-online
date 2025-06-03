using AptOnline.Domain.Helpers;
using FluentAssertions;
using Xunit;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.PharmacyContext.BrgAgg;

public record BrgType : IBrgKey
{
    public BrgType(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        
        BrgId = id;
        BrgName = name;
    }
    public string BrgId { get; }
    public string BrgName { get; }
    public static BrgType Default 
        => new BrgType(AppConst.DASH, AppConst.DASH);
}

public static class BrgTypeTest
{
    [Theory]
    [InlineData("-","-")]
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