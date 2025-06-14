using MediatR;

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