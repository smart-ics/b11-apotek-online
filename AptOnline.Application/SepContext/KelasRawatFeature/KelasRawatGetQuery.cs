using AptOnline.Domain.SepContext.KelasRawatFeature;
using MediatR;

namespace AptOnline.Application.SepContext.KelasRawatFeature;

public record KelasRawatGetQuery(string KelasRawatId) : IRequest<KelasRawatGetResponse>;

public record KelasRawatGetResponse(string KelasRawatId, string KelasRawatName);

public class KelasRawatGetHandler : IRequestHandler<KelasRawatGetQuery, KelasRawatGetResponse>
{
    private readonly IKelasRawatDal _kelasRawatDal;

    public KelasRawatGetHandler(IKelasRawatDal kelasRawatDal)
    {
        _kelasRawatDal = kelasRawatDal;
    }

    public Task<KelasRawatGetResponse> Handle(KelasRawatGetQuery request, CancellationToken cancellationToken)
    {
        var result = _kelasRawatDal
            .GetData(KelasRawatType.Key(request.KelasRawatId))
            .Map(x => new KelasRawatGetResponse(x.KelasRawatId, x.KelasRawatName))
            .GetValueOrThrow($"Kelas Rawat '{request.KelasRawatId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }
}