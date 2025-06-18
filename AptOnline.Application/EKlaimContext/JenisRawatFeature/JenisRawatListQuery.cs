using AptOnline.Domain.EKlaimContext.JenisRawatFeature;
using MediatR;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.JenisRawatFeature;

public record JenisRawatListQuery() : IRequest<List<JenisRawatListResponse>>;

public record JenisRawatListResponse(string JenisRawatId, string JenisRawatName);

public class JenisRawatListHandler : IRequestHandler<JenisRawatListQuery, List<JenisRawatListResponse>>
{
    public Task<List<JenisRawatListResponse>> Handle(JenisRawatListQuery request, CancellationToken cancellationToken)
    {
        var result = MayBe
            .From(JenisRawatType.ListData())
            .Map(x => x.Select(y => new JenisRawatListResponse(y.JenisRawatId, y.JenisRawatName)).ToList())
            .GetValueOrThrow("Jenis Rawat tidak ditemukan");

        return Task.FromResult(result);
    }
}