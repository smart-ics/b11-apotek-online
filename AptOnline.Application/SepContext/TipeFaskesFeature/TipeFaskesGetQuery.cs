using AptOnline.Domain.EKlaimContext.BayiLahirFeature;
using AptOnline.Domain.SepContext.TipeFaskesFeature;
using MediatR;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.SepContext.TipeFaskesFeature;

public record TipeFaskesGetQuery(string TipeFaskesId) : IRequest<TipeFaskesGetResponse>;

public record TipeFaskesGetResponse(string TipeFaskesId, string TipeFaskesName);

public class TipeFaskesGet : IRequestHandler<TipeFaskesGetQuery, TipeFaskesGetResponse>
{
    public Task<TipeFaskesGetResponse> Handle(TipeFaskesGetQuery request, CancellationToken cancellationToken)
    {
        var result = GetData(request.TipeFaskesId)
            .Map(x => new TipeFaskesGetResponse(x.TipeFaskesId, x.TipeFaskesName))
            .GetValueOrThrow($"Tipe Faskes '{request.TipeFaskesId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }

    private static MayBe<TipeFaskesType> GetData(string id)
    {
        var result = TipeFaskesType.ListData().FirstOrDefault(x => x.TipeFaskesId == id);
        return MayBe.From(result!);
    }
}