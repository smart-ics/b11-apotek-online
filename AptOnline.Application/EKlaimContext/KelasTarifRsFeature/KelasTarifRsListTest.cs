using AptOnline.Application.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.EKlaimContext.KelasTarifRsFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.KelasTarifRsFeature;

public class KelasTarifRsListTest
{
    private readonly KelasTarifRsListHandler _sut;
    private readonly Mock<IKelasTarifRsDal> _kelasTarifRsDal;

    public KelasTarifRsListTest()
    {
        _kelasTarifRsDal = new Mock<IKelasTarifRsDal>();
        _sut = new KelasTarifRsListHandler(_kelasTarifRsDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new List<KelasTarifRsGetResponse> { new("A", "B") };
        var result = new List<KelasTarifRsType> { new("A", "B") };
        _kelasTarifRsDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<KelasTarifRsType>>(result));
        var actual = await _sut.Handle(new KelasTarifRsListQuery(), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenExistedId_ThenReturnExpected()
    {
        _kelasTarifRsDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<KelasTarifRsType>>(null!));
        var actual = () => _sut.Handle(new KelasTarifRsListQuery(), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}