using AptOnline.Domain.SepContext.JenisPelayananFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.SepContext.JenisPelayananFeature;

public class JenisPelayananListTest
{
    private readonly JenisPelayananListHandler _sut;
    private readonly Mock<IJenisPelayananDal> _jenisPelayananDal;

    public JenisPelayananListTest()
    {
        _jenisPelayananDal = new Mock<IJenisPelayananDal>();
        _sut = new JenisPelayananListHandler(_jenisPelayananDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new List<JenisPelayananGetResponse> { new("A", "B") };
        var result = new List<JenisPelayananType> { new("A", "B") };
        _jenisPelayananDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<JenisPelayananType>>(result));
        var actual = await _sut.Handle(new JenisPelayananListQuery(), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenExistedId_ThenReturnExpected()
    {
        _jenisPelayananDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<JenisPelayananType>>(null!));
        var actual = () => _sut.Handle(new JenisPelayananListQuery(), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}