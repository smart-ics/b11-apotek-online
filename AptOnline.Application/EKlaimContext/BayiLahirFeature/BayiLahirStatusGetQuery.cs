using AptOnline.Domain.EKlaimContext.BayiLahirFeature;
using MediatR;

namespace AptOnline.Application.EKlaimContext.BayiLahirFeature;

public record BayiLahirStatusGetQuery(string BayiLahirStatusId) : IRequest<BayiLahirStatusGetResponse>;

public record BayiLahirStatusGetResponse(string BayiLahirStatusId, string BayiLahirStatusName);

public class BayiLahirStatusGet : IRequestHandler<BayiLahirStatusGetQuery, BayiLahirStatusGetResponse>
{
    private readonly IBayiLahirStatusDal _bayiLahirStatusDal;

    public BayiLahirStatusGet(IBayiLahirStatusDal bayiLahirStatusDal)
    {
        _bayiLahirStatusDal = bayiLahirStatusDal;
    }

    public Task<BayiLahirStatusGetResponse> Handle(BayiLahirStatusGetQuery request, CancellationToken cancellationToken)
    {
        var result = _bayiLahirStatusDal
            .GetData(BayiLahirStatusType.Key(request.BayiLahirStatusId))
            .Map(x => new BayiLahirStatusGetResponse(x.BayiLahirStatusId, x.BayiLahirStatusName))
            .GetValueOrThrow($"Status Bayi Lahir '{request.BayiLahirStatusId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }
}