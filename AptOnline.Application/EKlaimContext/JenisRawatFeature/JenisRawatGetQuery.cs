using AptOnline.Domain.EKlaimContext.JenisRawatFeature;
using MediatR;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.JenisRawatFeature;

public record JenisRawatGetQuery(string JenisRawatId) : IRequest<JenisRawatGetResponse>;

public record JenisRawatGetResponse(string JenisRawatId, string JenisRawatName);

public class JenisRawatGet : IRequestHandler<JenisRawatGetQuery, JenisRawatGetResponse>
{
    public Task<JenisRawatGetResponse> Handle(JenisRawatGetQuery request, CancellationToken cancellationToken)
    {
        var result = GetData(request.JenisRawatId)
            .Map(x => new JenisRawatGetResponse(x.JenisRawatId, x.JenisRawatName))
            .GetValueOrThrow($"Jenis Rawat '{request.JenisRawatId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }

    private static MayBe<JenisRawatType> GetData(string id)
    {
        var result = JenisRawatType.ListData().FirstOrDefault(x => x.JenisRawatId == id);
        return MayBe.From(result!);
    }
}