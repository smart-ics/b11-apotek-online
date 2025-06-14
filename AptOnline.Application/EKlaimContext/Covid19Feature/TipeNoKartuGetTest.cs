using AptOnline.Domain.EKlaimContext.Covid19Feature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.Covid19Feature;

public class TipeNoKartuGetTest
{
    private readonly TipeNoKartuGet _sut;
    private readonly Mock<ITipeNoKartuDal> _tipeNoKartuDal;

    public TipeNoKartuGetTest()
    {
        _tipeNoKartuDal = new Mock<ITipeNoKartuDal>();
        _sut = new TipeNoKartuGet(_tipeNoKartuDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new  TipeNoKartuGetResponse("A", "B");
        _tipeNoKartuDal.Setup(x => x.GetData(It.IsAny<ITipeNoKartuKey>()))
            .Returns(MayBe.From(new TipeNoKartuType("A", "B")));
        var actual = await _sut.Handle(new TipeNoKartuGetQuery("A"), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        _tipeNoKartuDal.Setup(x => x.GetData(It.IsAny<ITipeNoKartuKey>()))
            .Returns(MayBe.From<TipeNoKartuType>(null!));
        var actual = () => _sut.Handle(new TipeNoKartuGetQuery("A"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}