using AptOnline.Domain.EKlaimContext.Covid19Feature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.Covid19Feature;

public class Covid19StatusGetTest
{
    private readonly Covid19StatusGet _sut;
    private readonly Mock<ICovid19StatusDal> _covid19StatusDal;

    public Covid19StatusGetTest()
    {
        _covid19StatusDal = new Mock<ICovid19StatusDal>();
        _sut = new Covid19StatusGet(_covid19StatusDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new  Covid19StatusGetResponse("A", "B");
        _covid19StatusDal.Setup(x => x.GetData(It.IsAny<ICovid19StatusKey>()))
            .Returns(MayBe.From(new Covid19StatusType("A", "B")));
        var actual = await _sut.Handle(new Covid19StatusGetQuery("A"), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        _covid19StatusDal.Setup(x => x.GetData(It.IsAny<ICovid19StatusKey>()))
            .Returns(MayBe.From<Covid19StatusType>(null!));
        var actual = () => _sut.Handle(new Covid19StatusGetQuery("A"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}