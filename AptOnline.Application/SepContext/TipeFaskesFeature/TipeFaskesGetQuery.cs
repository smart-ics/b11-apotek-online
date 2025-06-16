using AptOnline.Domain.SepContext.TipeFaskesFeature;
using MediatR;

namespace AptOnline.Application.SepContext.TipeFaskesFeature;

public record TipeFaskesGetQuery(string TipeFaskesId) : IRequest<TipeFaskesGetResponse>;

public record TipeFaskesGetResponse(string TipeFaskesId, string TipeFaskesName);

public class TipeFaskesGetHandler : IRequestHandler<TipeFaskesGetQuery, TipeFaskesGetResponse>
{
    private readonly ITipeFaskesDal _tipeFaskesDal;

    public TipeFaskesGetHandler(ITipeFaskesDal tipeFaskesDal)
    {
        _tipeFaskesDal = tipeFaskesDal;
    }

    public Task<TipeFaskesGetResponse> Handle(TipeFaskesGetQuery request, CancellationToken cancellationToken)
    {
        var result = _tipeFaskesDal
            .GetData(TipeFaskesType.Key(request.TipeFaskesId))
            .Map(x => new TipeFaskesGetResponse(x.TipeFaskesId, x.TipeFaskesName))
            .GetValueOrThrow($"Tipe Faskes '{request.TipeFaskesId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }
}