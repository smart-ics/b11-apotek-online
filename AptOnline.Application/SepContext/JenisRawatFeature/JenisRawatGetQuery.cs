using AptOnline.Domain.SepContext.JenisRawatFeature;
using AptOnline.Domain.SepContext.KelasRawatFeature;
using MediatR;

namespace AptOnline.Application.SepContext.JenisRawatFeature;

public record JenisRawatGetQuery(string JenisRawatId) : IRequest<JenisRawatGetResponse>;

public record JenisRawatGetResponse(string JenisRawatId, string JenisRawatName);

public class JenisRawatGetHandler : IRequestHandler<JenisRawatGetQuery, JenisRawatGetResponse>
{
    private readonly IJenisRawatDal _jenisRawatDal;

    public JenisRawatGetHandler(IJenisRawatDal jenisRawatDal)
    {
        _jenisRawatDal = jenisRawatDal;
    }

    public Task<JenisRawatGetResponse> Handle(JenisRawatGetQuery request, CancellationToken cancellationToken)
    {
        var result = _jenisRawatDal
            .GetData(JenisRawatType.Key(request.JenisRawatId))
            .Map(x => new JenisRawatGetResponse(x.JenisRawatId, x.JenisRawatName))
            .GetValueOrThrow($"Kelas Rawat '{request.JenisRawatId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }
}