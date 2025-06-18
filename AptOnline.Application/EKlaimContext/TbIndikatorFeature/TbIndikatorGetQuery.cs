using AptOnline.Domain.EKlaimContext.TbIndikatorFeature;
using MediatR;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.TbIndikatorFeature;

public record TbIndikatorGetQuery(string TbIndikatorId) : IRequest<TbIndikatorGetResponse>;

public record TbIndikatorGetResponse(string TbIndikatorId, string TbIndikatorName);

public class TbIndikatorGet : IRequestHandler<TbIndikatorGetQuery, TbIndikatorGetResponse>
{
    public Task<TbIndikatorGetResponse> Handle(TbIndikatorGetQuery request, CancellationToken cancellationToken)
    {
        var result = GetData(request.TbIndikatorId)
            .Map(x => new TbIndikatorGetResponse(x.TbIndikatorId, x.TbIndikatorName))
            .GetValueOrThrow($"TbIndikator '{request.TbIndikatorId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }

    private static MayBe<TbIndikatorType> GetData(string id)
    {
        var result = TbIndikatorType.ListData().FirstOrDefault(x => x.TbIndikatorId == id);
        return MayBe.From(result!);
    }
}
