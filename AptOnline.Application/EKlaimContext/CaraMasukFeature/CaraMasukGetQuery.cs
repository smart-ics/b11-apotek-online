using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using FluentAssertions;
using MediatR;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.CaraMasukFeature;

public record CaraMasukGetQuery(string CaraMasukId) : IRequest<CaraMasukGetResponse>;

public record CaraMasukGetResponse(string CaraMasukId, string CaraMasukName);

public class CaraMasukGetHandler : IRequestHandler<CaraMasukGetQuery, CaraMasukGetResponse>
{
    private readonly ICaraMasukDal _caraMasukDal;

    public CaraMasukGetHandler(ICaraMasukDal caraMasukDal)
    {
        _caraMasukDal = caraMasukDal;
    }

    public Task<CaraMasukGetResponse> Handle(CaraMasukGetQuery request, CancellationToken cancellationToken)
    {
        var result = _caraMasukDal
            .GetData(CaraMasukType.Key(request.CaraMasukId))
            .Map(x => new CaraMasukGetResponse(x.CaraMasukId, x.CaraMasukName))
            .GetValueOrThrow($"Cara Masuk '{request.CaraMasukId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }
}

public class CaraMasukGetHandlerTest
{
    private readonly CaraMasukGetHandler _sut;
    private readonly Mock<ICaraMasukDal> _caraMasukDal;

    public CaraMasukGetHandlerTest()
    {
        _caraMasukDal = new Mock<ICaraMasukDal>();
        _sut = new CaraMasukGetHandler(_caraMasukDal.Object);
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
