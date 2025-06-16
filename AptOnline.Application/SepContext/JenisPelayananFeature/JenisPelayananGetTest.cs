using AptOnline.Application.SepContext.KelasRawatFeature;
using AptOnline.Domain.SepContext.JenisPelayananFeature;
using AptOnline.Domain.SepContext.KelasRawatFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.SepContext.JenisPelayananFeature;

public class JenisPelayananGetTest
{
    private readonly JenisPelayananGetHandler _sut;
    private readonly Mock<IJenisPelayananDal> _jenisPelayananDal;

    public JenisPelayananGetTest()
    {
        _jenisPelayananDal = new Mock<IJenisPelayananDal>();
        _sut = new JenisPelayananGetHandler(_jenisPelayananDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new JenisPelayananGetResponse("A", "B");
        _jenisPelayananDal.Setup(x => x.GetData(It.IsAny<IJenisPelayananKey>()))
            .Returns(MayBe.From(new JenisPelayananType("A", "B")));
        var actual = await _sut.Handle(new JenisPelayananGetQuery("A"), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        _jenisPelayananDal.Setup(x => x.GetData(It.IsAny<IJenisPelayananKey>()))
            .Returns(MayBe.From<JenisPelayananType>(null!));
        var actual = () => _sut.Handle(new JenisPelayananGetQuery("A"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}