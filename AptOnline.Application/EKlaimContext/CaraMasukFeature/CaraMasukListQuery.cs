using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using FluentAssertions;
using MediatR;
using Moq;
using Nuna.Lib.PatternHelper;
using Xunit;

namespace AptOnline.Application.EKlaimContext.CaraMasukFeature;

public record CaraMasukListQuery() : IRequest<List<CaraMasukGetResponse>>;

public record CaraMasukListResponse(string CaraMasukId, string CaraMasukName);

public class CaraMasukListHandler : IRequestHandler<CaraMasukListQuery, List<CaraMasukGetResponse>>
{
    private readonly ICaraMasukDal _caraMasukDal;

    public CaraMasukListHandler(ICaraMasukDal caraMasukDal)
    {
        _caraMasukDal = caraMasukDal;
    }

    public Task<List<CaraMasukGetResponse>> Handle(CaraMasukListQuery request, CancellationToken cancellationToken)
    {
        var result = _caraMasukDal
            .ListData()
            .Map(x => x.Select(y => new CaraMasukGetResponse(y.CaraMasukId, y.CaraMasukName)).ToList())
            .GetValueOrThrow("Cara Masuk tidak ditemukan");

        return Task.FromResult(result);
    }
}

public class CaraMasukListHandlerTest
{
    private readonly CaraMasukListHandler _sut;
    private readonly Mock<ICaraMasukDal> _caraMasukDal;

    public CaraMasukListHandlerTest()
    {
        _caraMasukDal = new Mock<ICaraMasukDal>();
        _sut = new CaraMasukListHandler(_caraMasukDal.Object);
    }

    [Fact]
    public async Task UT1_GivenExistedId_ThenReturnExpected()
    {
        var expected = new List<CaraMasukGetResponse> { new("A", "B") };
        var result = new List<CaraMasukType> { new("A", "B") };
        _caraMasukDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<CaraMasukType>>(result));
        var actual = await _sut.Handle(new CaraMasukListQuery(), CancellationToken.None);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UT2_GivenExistedId_ThenReturnExpected()
    {
        _caraMasukDal.Setup(x => x.ListData())
            .Returns(MayBe.From<IEnumerable<CaraMasukType>>(null!));
        var actual = () => _sut.Handle(new CaraMasukListQuery(), CancellationToken.None);
        await actual.Should().ThrowAsync<KeyNotFoundException>();
    }
}
