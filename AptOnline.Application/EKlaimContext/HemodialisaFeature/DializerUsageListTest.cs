using AptOnline.Domain.EKlaimContext.HemodialisaFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.HemodialisaFeature;

public class DializerUsageListTest
{
    private readonly DializerUsageListHandler _sut;
    private readonly Mock<IDializerUsageDal> _dializerUsageDal;

    public DializerUsageListTest()
    {
        _dializerUsageDal = new Mock<IDializerUsageDal>();
        _sut = new DializerUsageListHandler(_dializerUsageDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new List<DializerUsageGetResponse> { new("A", "B") };
        var result = new List<DializerUsageType> { new("A", "B") };
        _dializerUsageDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<DializerUsageType>>(result));
        var actual = await _sut.Handle(new DializerUsageListQuery(), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenExistedId_ThenReturnExpected()
    {
        _dializerUsageDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<DializerUsageType>>(null!));
        var actual = () => _sut.Handle(new DializerUsageListQuery(), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}