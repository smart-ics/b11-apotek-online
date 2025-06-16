using AptOnline.Application.SepContext.KelasRawatFeature;
using MediatR;

namespace AptOnline.Application.SepContext.TipeFaskesFeature;

public record TipeFaskesListQuery : IRequest<List<TipeFaskesListResponse>>;

public record TipeFaskesListResponse(string TipeFaskesId, string TipeFaskesName);

public class TipeFaskesListHandler : IRequestHandler<TipeFaskesListQuery, List<TipeFaskesListResponse>>
{
    private readonly ITipeFaskesDal _tipeFaskesDal;

    public TipeFaskesListHandler(ITipeFaskesDal tipeFaskesDal)
    {
        _tipeFaskesDal = tipeFaskesDal;
    }

    public Task<List<TipeFaskesListResponse>> Handle(TipeFaskesListQuery request, CancellationToken cancellationToken)
    {
        var result = _tipeFaskesDal
            .ListData()
            .Map(x => x.Select(y => new TipeFaskesListResponse(y.TipeFaskesId, y.TipeFaskesName)).ToList())
            .GetValueOrThrow("Tipe Faskes tidak ditemukan");

        return Task.FromResult(result);
    }
}