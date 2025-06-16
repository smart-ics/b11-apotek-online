using AptOnline.Domain.EKlaimContext.PayorFeature;
using MediatR;

namespace AptOnline.Application.EKlaimContext.PayorFeature;

public record PayorGetQuery(string PayorId) : IRequest<PayorGetResponse>;

public record PayorGetResponse(string PayorId, string PayorName, string Code);

public class PayorGet : IRequestHandler<PayorGetQuery, PayorGetResponse>
{
    private readonly IPayorDal _payorDal;

    public PayorGet(IPayorDal payorDal)
    {
        _payorDal = payorDal;
    }

    public Task<PayorGetResponse> Handle(PayorGetQuery request, CancellationToken cancellationToken)
    {
        var result = _payorDal
            .GetData(PayorType.Key(request.PayorId))
            .Map(x => new PayorGetResponse(x.PayorId, x.PayorName, x.Code))
            .GetValueOrThrow($"Payor (Pay Plan) '{request.PayorId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }
}