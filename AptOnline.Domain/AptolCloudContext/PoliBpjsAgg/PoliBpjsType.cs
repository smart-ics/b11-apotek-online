using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.Helpers;
using AptOnline.Domain.PharmacyContext.BrgAgg;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;

public record PoliBpjsType : IPoliBpjsKey
{
    public PoliBpjsType(string id, string name)
    {
        if (id == string.Empty ^ name == string.Empty)
            throw new ArgumentException("Poli BPJS is invalid state");

        PoliBpjsId = id;
        PoliBpjsName = name;
    }
    public string PoliBpjsId { get; }
    public string PoliBpjsName { get; }
    public static PoliBpjsType Default => new(AppConst.DASH, AppConst.DASH);
}

public static class PoliBpjsTest
{
    [Theory]
    [InlineData("","")]
    [InlineData("A","B")]
    public static void UT1_GivenValidInput_ThenSuccess(string id, string name)
    {
        var act = new PoliBpjsType(id, name);
    }

    [Fact]
    public static void UT2_GivenSomeEmpty_ThenThrowArgumentException()
    {
        var act = () => new PoliBpjsType("A", "");
        act.Should().Throw<ArgumentException>();
    }    
}