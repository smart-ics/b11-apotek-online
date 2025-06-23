using AptOnline.Domain.SepContext.KelasJknFeature;
using MediatR;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.SepContext.KelasJknFeature;

public record KelasJknListQuery : IRequest<List<KelasJknListResponse>>;

public record KelasJknListResponse(string KelasJknId, string KelasJknName);

public class KelasJknListHandler : IRequestHandler<KelasJknListQuery, List<KelasJknListResponse>>
{
    public Task<List<KelasJknListResponse>> Handle(KelasJknListQuery request, CancellationToken cancellationToken)
    {
        var result = MayBe
            .From(KelasJknType.ListData())
            .Map(x => x.Select(y => new KelasJknListResponse(y.KelasJknId, y.KelasJknName)).ToList())
            .GetValueOrThrow("Kelas JKN tidak ditemukan");

        return Task.FromResult(result);
    }
}