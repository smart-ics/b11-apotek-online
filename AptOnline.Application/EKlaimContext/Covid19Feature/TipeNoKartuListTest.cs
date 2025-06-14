using AptOnline.Domain.EKlaimContext.Covid19Feature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.Covid19Feature;

public class TipeNoKartuListTest
{
    private readonly TipeNoKartuListHandler _sut;
    private readonly Mock<ITipeNoKartuDal> _tipeNoKartuDal;

    public TipeNoKartuListTest()
    {
        _tipeNoKartuDal = new Mock<ITipeNoKartuDal>();
        _sut = new TipeNoKartuListHandler(_tipeNoKartuDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new List<TipeNoKartuGetResponse> { new("A", "B") };
        var result = new List<TipeNoKartuType> { new("A", "B") };
        _tipeNoKartuDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<TipeNoKartuType>>(result));
        var actual = await _sut.Handle(new TipeNoKartuListQuery(), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenExistedId_ThenReturnExpected()
    {
        _tipeNoKartuDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<TipeNoKartuType>>(null!));
        var actual = () => _sut.Handle(new TipeNoKartuListQuery(), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}