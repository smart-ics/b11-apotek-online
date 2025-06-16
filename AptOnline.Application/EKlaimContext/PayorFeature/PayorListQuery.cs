using AptOnline.Application.EKlaimContext.CaraMasukFeature;
using MediatR;

namespace AptOnline.Application.EKlaimContext.PayorFeature;

public record PayorListQuery() : IRequest<List<PayorListResponse>>;

public record PayorListResponse(string PayorId, string PayorName, string Code);

public class PayorListHandler : IRequestHandler<PayorListQuery, List<PayorListResponse>>
{
    private readonly IPayorDal _payorDal;

    public PayorListHandler(IPayorDal payorDal)
    {
        _payorDal = payorDal;
    }

    public Task<List<PayorListResponse>> Handle(PayorListQuery request, CancellationToken cancellationToken)
    {
        var result = _payorDal
            .ListData()
            .Map(x => x.Select(y => new PayorListResponse(y.PayorId, y.PayorName, y.Code)).ToList())
            .GetValueOrThrow("Payor tidak ditemukan");

        return Task.FromResult(result);
    }
}