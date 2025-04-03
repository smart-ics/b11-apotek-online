using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.AptolCloudContext.FaskesAgg;

public record FaskesType : IFaskesKey
{
    public FaskesType(string id, string name)
    {
        if (id == string.Empty ^ name == string.Empty)
            throw new ArgumentException("FaskesModel is invalid");
        
        FaskesId = id;
        FaskesName = name;
    }

    public string FaskesId { get; private set; }
    public string FaskesName { get; private set; }
    public static FaskesType Default 
        => new FaskesType(string.Empty, string.Empty);
}

public class FaskesTypeTest
{
    [Fact]
    public void UT1_GivenAllPropertiesNotEmpty_ShouldReturnValidFaskesModel()
    {
        var act = () => new FaskesType("A", "B");
    }
    [Fact]
    public void UT2_GivenAllPropertiesEmpty_ShouldReturnValidFaskesModel()
    {
        var act = () => new FaskesType("", "");
    }
    [Fact]
    public void UT3_GivenSomePropertyEmpty_ShouldThrowArgumentException()
    {
        var act = () => new FaskesType("A", "");
        act.Should().Throw<ArgumentException>();
    }
    [Fact]
    public void UT4_GivenEmptyProps_WhenCompareToDefault_ShouldBeTrue()
    {
        var emptyFaskes = new FaskesType("", "");
        var act = emptyFaskes == FaskesType.Default;
        act.Should().Be(true);
    }
}