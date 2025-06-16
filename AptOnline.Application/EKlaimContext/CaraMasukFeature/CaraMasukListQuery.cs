using MediatR;

namespace AptOnline.Application.EKlaimContext.CaraMasukFeature;

public record CaraMasukListQuery : IRequest<List<CaraMasukListResponse>>;

public record CaraMasukListResponse(string CaraMasukId, string CaraMasukName);

public class CaraMasukListHandler : IRequestHandler<CaraMasukListQuery, List<CaraMasukListResponse>>
{
    private readonly ICaraMasukDal _caraMasukDal;

    public CaraMasukListHandler(ICaraMasukDal caraMasukDal)
    {
        _caraMasukDal = caraMasukDal;
    }

    public Task<List<CaraMasukListResponse>> Handle(CaraMasukListQuery request, CancellationToken cancellationToken)
    {
        var result = _caraMasukDal
            .ListData()
            .Map(x => x.Select(y => new CaraMasukListResponse(y.CaraMasukId, y.CaraMasukName)).ToList())
            .GetValueOrThrow("Cara Masuk tidak ditemukan");

        return Task.FromResult(result);
    }
}