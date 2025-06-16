using AptOnline.Domain.SepContext.TipeFaskesFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.SepContext.TipeFaskesFeature;

public class TipeFaskesListTest
{
    private readonly TipeFaskesListHandler _sut;
    private readonly Mock<ITipeFaskesDal> _tipeFaskesDal;

    public TipeFaskesListTest()
    {
        _tipeFaskesDal = new Mock<ITipeFaskesDal>();
        _sut = new TipeFaskesListHandler(_tipeFaskesDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new List<TipeFaskesGetResponse> { new("A", "B") };
        var result = new List<TipeFaskesType> { new("A", "B") };
        _tipeFaskesDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<TipeFaskesType>>(result));
        var actual = await _sut.Handle(new TipeFaskesListQuery(), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenExistedId_ThenReturnExpected()
    {
        _tipeFaskesDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<TipeFaskesType>>(null!));
        var actual = () => _sut.Handle(new TipeFaskesListQuery(), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}