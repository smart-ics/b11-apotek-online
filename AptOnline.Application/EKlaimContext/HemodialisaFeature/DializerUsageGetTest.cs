using AptOnline.Domain.EKlaimContext.HemodialisaFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.HemodialisaFeature;

public class DializerUsageGetTest
{
    private readonly DializerUsageGet _sut;
    private readonly Mock<IDializerUsageDal> _dializerUsageDal;

    public DializerUsageGetTest()
    {
        _dializerUsageDal = new Mock<IDializerUsageDal>();
        _sut = new DializerUsageGet(_dializerUsageDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new  DializerUsageGetResponse("A", "B");
        _dializerUsageDal.Setup(x => x.GetData(It.IsAny<IDializerUsageKey>()))
            .Returns(MayBe.From(new DializerUsageType("A", "B")));
        var actual = await _sut.Handle(new DializerUsageGetQuery("A"), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        _dializerUsageDal.Setup(x => x.GetData(It.IsAny<IDializerUsageKey>()))
            .Returns(MayBe.From<DializerUsageType>(null!));
        var actual = () => _sut.Handle(new DializerUsageGetQuery("A"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}