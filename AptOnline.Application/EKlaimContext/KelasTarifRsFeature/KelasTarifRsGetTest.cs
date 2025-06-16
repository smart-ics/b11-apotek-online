using AptOnline.Domain.EKlaimContext.KelasTarifRsFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.KelasTarifRsFeature;

public class KelasTarifRsGetTest
{
    private readonly KelasTarifRsGet _sut;
    private readonly Mock<IKelasTarifRsDal> _kelasTarifRsDal;

    public KelasTarifRsGetTest()
    {
        _kelasTarifRsDal = new Mock<IKelasTarifRsDal>();
        _sut = new KelasTarifRsGet(_kelasTarifRsDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new  KelasTarifRsGetResponse("A", "B");
        _kelasTarifRsDal.Setup(x => x.GetData(It.IsAny<IKelasTarifRsKey>()))
            .Returns(MayBe.From(new KelasTarifRsType("A", "B")));
        var actual = await _sut.Handle(new KelasTarifRsGetQuery("A"), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        _kelasTarifRsDal.Setup(x => x.GetData(It.IsAny<IKelasTarifRsKey>()))
            .Returns(MayBe.From<KelasTarifRsType>(null!));
        var actual = () => _sut.Handle(new KelasTarifRsGetQuery("A"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}