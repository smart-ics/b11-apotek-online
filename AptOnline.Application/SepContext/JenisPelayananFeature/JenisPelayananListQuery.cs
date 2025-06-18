using AptOnline.Domain.SepContext.JenisPelayananFeature;
using MediatR;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.SepContext.JenisPelayananFeature;

public record JenisPelayananListQuery() : IRequest<List<JenisPelayananListResponse>>;

public record JenisPelayananListResponse(string JenisPelayananId, string JenisPelayananName);

public class JenisPelayananListHandler : IRequestHandler<JenisPelayananListQuery, List<JenisPelayananListResponse>>
{
    public Task<List<JenisPelayananListResponse>> Handle(JenisPelayananListQuery request, CancellationToken cancellationToken)
    {
        var result = MayBe
            .From(JenisPelayananType.ListData())
            .Map(x => x.Select(y => new JenisPelayananListResponse(y.JenisPelayananId, y.JenisPelayananName)).ToList())
            .GetValueOrThrow("Jenis Pelayanan tidak ditemukan");

        return Task.FromResult(result);
    }
}