using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using FluentAssertions;
using Xunit;

namespace AptOnline.Domain.AptolCloudContext.FaskesAgg;

public class FaskesModel : IFaskesKey
{
    public FaskesModel(string id, string name)
    {
        if (id == string.Empty ^ name == string.Empty)
            throw new ArgumentException("FaskesModel is invalid");
        
        FaskesId = id;
        FaskesName = name;
    }

    public static FaskesModel Default()
        => new FaskesModel(string.Empty, string.Empty);
    public string FaskesId { get; private set; }
    public string FaskesName { get; private set; }
}

public class FaskesModelTest
{
    [Fact]
    public void UT1_GivenAllPropertiesNotEmpty_ShouldReturnValidFaskesModel()
    {
        var act = () => new FaskesModel("A", "B");
    }
    [Fact]
    public void UT2_GivenAllPropertiesEmpty_ShouldReturnValidFaskesModel()
    {
        var act = () => new FaskesModel("", "");
    }
    [Fact]
    public void UT3_GivenSomePropertyEmpty_ShouldThrowArgumentException()
    {
        var act = () => new FaskesModel("A", "");
        act.Should().Throw<ArgumentException>();
    }}