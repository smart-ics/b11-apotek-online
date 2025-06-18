using AptOnline.Domain.EKlaimContext.BayiLahirFeature;
using AptOnline.Domain.SepContext.TipeFaskesFeature;
using MediatR;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.SepContext.TipeFaskesFeature;

public record TipeFaskesListQuery() : IRequest<List<TipeFaskesListResponse>>;

public record TipeFaskesListResponse(string TipeFaskesId, string TipeFaskesName);

public class TipeFaskesListHandler : IRequestHandler<TipeFaskesListQuery, List<TipeFaskesListResponse>>
{
    public Task<List<TipeFaskesListResponse>> Handle(TipeFaskesListQuery request, CancellationToken cancellationToken)
    {
        var result = MayBe
            .From(TipeFaskesType.ListData())
            .Map(x => x.Select(y => new TipeFaskesListResponse(y.TipeFaskesId, y.TipeFaskesName)).ToList())
            .GetValueOrThrow("Status Bayi Lahir tidak ditemukan");

        return Task.FromResult(result);
    }
}