using AptOnline.Domain.Helpers;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using FluentAssertions;
using GuardNet;
using Xunit;

namespace AptOnline.Domain.PharmacyContext.BrgAgg;

public record BrgType : IBrgKey
{
    public BrgType(string id, string name)
    {
        Guard.NotNullOrWhitespace(id, nameof(id));
        Guard.NotNullOrWhitespace(name, nameof(name));
        
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