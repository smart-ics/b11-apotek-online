using AptOnline.Domain.EKlaimContext.JenisRawatFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.JenisRawatFeature;

public class JenisRawatGetTest
{
    private readonly JenisRawatGetHandler _sut;
    private readonly Mock<IJenisRawatDal> _jenisRawatDal;

    public JenisRawatGetTest()
    {
        _jenisRawatDal = new Mock<IJenisRawatDal>();
        _sut = new JenisRawatGetHandler(_jenisRawatDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new JenisRawatGetResponse("A", "B");
        _jenisRawatDal.Setup(x => x.GetData(It.IsAny<IJenisRawatKey>()))
            .Returns(MayBe.From(new JenisRawatType("A", "B")));
        var actual = await _sut.Handle(new JenisRawatGetQuery("A"), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        _jenisRawatDal.Setup(x => x.GetData(It.IsAny<IJenisRawatKey>()))
            .Returns(MayBe.From<JenisRawatType>(null!));
        var actual = () => _sut.Handle(new JenisRawatGetQuery("A"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}