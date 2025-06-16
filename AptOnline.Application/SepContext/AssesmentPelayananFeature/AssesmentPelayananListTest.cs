using AptOnline.Domain.SepContext.AssesmentPelayananFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.SepContext.AssesmentPelayananFeature;

public class AssesmentPelayananListTest
{
    private readonly AssesmentPelayananListHandler _sut;
    private readonly Mock<IAssesmentPelayananDal> _assesmentPelayananDal;

    public AssesmentPelayananListTest()
    {
        _assesmentPelayananDal = new Mock<IAssesmentPelayananDal>();
        _sut = new AssesmentPelayananListHandler(_assesmentPelayananDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new List<AssesmentPelayananGetResponse> { new("A", "B") };
        var result = new List<AssesmentPelayananType> { new("A", "B") };
        _assesmentPelayananDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<AssesmentPelayananType>>(result));
        var actual = await _sut.Handle(new AssesmentPelayananListQuery(), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenExistedId_ThenReturnExpected()
    {
        _assesmentPelayananDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<AssesmentPelayananType>>(null!));
        var actual = () => _sut.Handle(new AssesmentPelayananListQuery(), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}