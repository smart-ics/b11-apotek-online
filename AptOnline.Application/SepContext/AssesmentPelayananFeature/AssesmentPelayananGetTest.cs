using AptOnline.Domain.SepContext.AssesmentPelayananFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.SepContext.AssesmentPelayananFeature;

public class AssesmentPelayananGetTest
{
    private readonly AssesmentPelayananGetHandler _sut;
    private readonly Mock<IAssesmentPelayananDal> _assesmentPelayananDal;

    public AssesmentPelayananGetTest()
    {
        _assesmentPelayananDal = new Mock<IAssesmentPelayananDal>();
        _sut = new AssesmentPelayananGetHandler(_assesmentPelayananDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new AssesmentPelayananGetResponse("A", "B");
        _assesmentPelayananDal.Setup(x => x.GetData(It.IsAny<IAssesmentPelayananKey>()))
            .Returns(MayBe.From(new AssesmentPelayananType("A", "B")));
        var actual = await _sut.Handle(new AssesmentPelayananGetQuery("A"), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        _assesmentPelayananDal.Setup(x => x.GetData(It.IsAny<IAssesmentPelayananKey>()))
            .Returns(MayBe.From<AssesmentPelayananType>(null!));
        var actual = () => _sut.Handle(new AssesmentPelayananGetQuery("A"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}