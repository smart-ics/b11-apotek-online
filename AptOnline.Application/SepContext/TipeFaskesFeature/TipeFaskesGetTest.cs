using AptOnline.Domain.SepContext.TipeFaskesFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.SepContext.TipeFaskesFeature;

public class TipeFaskesGetTest
{
    private readonly TipeFaskesGetHandler _sut;
    private readonly Mock<ITipeFaskesDal> _tipeFaskesDal;

    public TipeFaskesGetTest()
    {
        _tipeFaskesDal = new Mock<ITipeFaskesDal>();
        _sut = new TipeFaskesGetHandler(_tipeFaskesDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new TipeFaskesGetResponse("A", "B");
        _tipeFaskesDal.Setup(x => x.GetData(It.IsAny<ITipeFaskesKey>()))
            .Returns(MayBe.From(new TipeFaskesType("A", "B")));
        var actual = await _sut.Handle(new TipeFaskesGetQuery("A"), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        _tipeFaskesDal.Setup(x => x.GetData(It.IsAny<ITipeFaskesKey>()))
            .Returns(MayBe.From<TipeFaskesType>(null!));
        var actual = () => _sut.Handle(new TipeFaskesGetQuery("A"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}