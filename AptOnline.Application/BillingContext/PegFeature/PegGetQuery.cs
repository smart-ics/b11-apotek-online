using AptOnline.Domain.BillingContext.PegFeature;
using MediatR;

namespace AptOnline.Application.BillingContext.PegFeature;

public record PegGetQuery(string PegId) : IRequest<PegGetResponse>;

public record PegGetResponse(string PegId, string PegName, string Nik);

public class PegGet : IRequestHandler<PegGetQuery, PegGetResponse>
{
    private readonly IPegDal _pegDal;

    public PegGet(IPegDal pegDal)
    {
        _pegDal = pegDal;
    }

    public Task<PegGetResponse> Handle(PegGetQuery request, CancellationToken cancellationToken)
    {
        var result = _pegDal
            .GetData(PegType.Key(request.PegId))
            .Map(x => new PegGetResponse(x.PegId, x.PegName, x.Nik))
            .GetValueOrThrow($"Cara Masuk '{request.PegId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }
}