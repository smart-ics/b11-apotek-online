using AptOnline.Domain.EKlaimContext.Covid19Feature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.Covid19Feature;

public class Covid19StatusListTest
{
    private readonly Covid19StatusListHandler _sut;
    private readonly Mock<ICovid19StatusDal> _covid19StatusDal;

    public Covid19StatusListTest()
    {
        _covid19StatusDal = new Mock<ICovid19StatusDal>();
        _sut = new Covid19StatusListHandler(_covid19StatusDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new List<Covid19StatusGetResponse> { new("A", "B") };
        var result = new List<Covid19StatusType> { new("A", "B") };
        _covid19StatusDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<Covid19StatusType>>(result));
        var actual = await _sut.Handle(new Covid19StatusListQuery(), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenExistedId_ThenReturnExpected()
    {
        _covid19StatusDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<Covid19StatusType>>(null!));
        var actual = () => _sut.Handle(new Covid19StatusListQuery(), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}