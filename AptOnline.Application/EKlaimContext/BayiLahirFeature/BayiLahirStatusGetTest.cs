using AptOnline.Application.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.EKlaimContext.BayiLahirFeature;
using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.BayiLahirFeature;

public class BayiLahirStatusGetTest
{
    private readonly BayiLahirStatusGet _sut;
    private readonly Mock<IBayiLahirStatusDal> _bayiLahirStatusDal;

    public BayiLahirStatusGetTest()
    {
        _bayiLahirStatusDal = new Mock<IBayiLahirStatusDal>();
        _sut = new BayiLahirStatusGet(_bayiLahirStatusDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new  BayiLahirStatusGetResponse("A", "B");
        _bayiLahirStatusDal.Setup(x => x.GetData(It.IsAny<IBayiLahirStatusKey>()))
            .Returns(MayBe.From(new BayiLahirStatusType("A", "B")));
        var actual = await _sut.Handle(new BayiLahirStatusGetQuery("A"), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        _bayiLahirStatusDal.Setup(x => x.GetData(It.IsAny<IBayiLahirStatusKey>()))
            .Returns(MayBe.From<BayiLahirStatusType>(null!));
        var actual = () => _sut.Handle(new BayiLahirStatusGetQuery("A"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}