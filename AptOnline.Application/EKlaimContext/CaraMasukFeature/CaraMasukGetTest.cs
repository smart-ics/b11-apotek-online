using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using FluentAssertions;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.CaraMasukFeature;

public class CaraMasukGetTest
{
    private readonly CaraMasukGet _sut;
    private readonly Mock<ICaraMasukDal> _caraMasukDal;

    public CaraMasukGetTest()
    {
        _caraMasukDal = new Mock<ICaraMasukDal>();
        _sut = new CaraMasukGet(_caraMasukDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new  CaraMasukGetResponse("A", "B");
        _caraMasukDal.Setup(x => x.GetData(It.IsAny<ICaraMasukKey>()))
            .Returns(MayBe.From(new CaraMasukType("A", "B")));
        var actual = await _sut.Handle(new CaraMasukGetQuery("A"), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenInvalidId_ThenThrowEx()
    {
        _caraMasukDal.Setup(x => x.GetData(It.IsAny<ICaraMasukKey>()))
            .Returns(MayBe.From<CaraMasukType>(null!));
        var actual = () => _sut.Handle(new CaraMasukGetQuery("A"), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}