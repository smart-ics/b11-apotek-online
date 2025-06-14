using AptOnline.Application.EKlaimContext.CaraMasukFeature;
using MediatR;

namespace AptOnline.Application.EKlaimContext.BayiLahirFeature;

public record BayiLahirStatusListQuery() : IRequest<List<BayiLahirStatusGetResponse>>;

public record BayiLahirStatusListResponse(string BayiLahirStatusId, string BayiLahirStatusName);

public class BayiLahirStatusListHandler : IRequestHandler<BayiLahirStatusListQuery, List<BayiLahirStatusGetResponse>>
{
    private readonly IBayiLahirStatusDal _bayiLahirStatusDal;

    public BayiLahirStatusListHandler(IBayiLahirStatusDal bayiLahirStatusDal)
    {
        _bayiLahirStatusDal = bayiLahirStatusDal;
    }

    public Task<List<BayiLahirStatusGetResponse>> Handle(BayiLahirStatusListQuery request, CancellationToken cancellationToken)
    {
        var result = _bayiLahirStatusDal
            .ListData()
            .Map(x => x.Select(y => new BayiLahirStatusGetResponse(y.BayiLahirStatusId, y.BayiLahirStatusName)).ToList())
            .GetValueOrThrow("Status Bayi Lahir tidak ditemukan");

        return Task.FromResult(result);
    }
}