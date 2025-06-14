using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using MediatR;

namespace AptOnline.Application.EKlaimContext.CaraMasukFeature;

public record CaraMasukGetQuery(string CaraMasukId) : IRequest<CaraMasukGetResponse>;

public record CaraMasukGetResponse(string CaraMasukId, string CaraMasukName);

public class CaraMasukGet : IRequestHandler<CaraMasukGetQuery, CaraMasukGetResponse>
{
    private readonly ICaraMasukDal _caraMasukDal;

    public CaraMasukGet(ICaraMasukDal caraMasukDal)
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