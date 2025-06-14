using AptOnline.Domain.SepContext.KelasRawatFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.SepContext.KelasRawatFeature;

public class KelasRawatGetTest
{
    private readonly KelasRawatGetHandler _sut;
    private readonly Mock<IKelasRawatDal> _kelasRawatDal;

    public KelasRawatGetTest()
    {
        _kelasRawatDal = new Mock<IKelasRawatDal>();
        _sut = new KelasRawatGetHandler(_kelasRawatDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new KelasRawatGetResponse("A", "B");
        _kelasRawatDal.Setup(x => x.GetData(It.IsAny<IKelasRawatKey>()))
            .Returns(MayBe.From(new KelasRawatType("A", "B")));
        var actual = await _sut.Handle(new KelasRawatGetQuery("A"), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        _kelasRawatDal.Setup(x => x.GetData(It.IsAny<IKelasRawatKey>()))
            .Returns(MayBe.From<KelasRawatType>(null!));
        var actual = () => _sut.Handle(new KelasRawatGetQuery("A"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}