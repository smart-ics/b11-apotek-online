using AptOnline.Domain.EKlaimContext.PelayananDarahFeature;
using MediatR;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.HemodialisaFeature;

public record DializerUsageGetQuery(string DializerUsageId) : IRequest<DializerUsageGetResponse>;

public record DializerUsageGetResponse(string DializerUsageId, string DializerUsageName);

public class DializerUsageGet : IRequestHandler<DializerUsageGetQuery, DializerUsageGetResponse>
{
    public Task<DializerUsageGetResponse> Handle(DializerUsageGetQuery request, CancellationToken cancellationToken)
    {
        var result = GetData(request.DializerUsageId)
            .Map(x => new DializerUsageGetResponse(x.DializerUsageId, x.DializerUsageName))
            .GetValueOrThrow($"Cara Masuk '{request.DializerUsageId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }

    private static MayBe<DializerUsageType> GetData(string id)
    {
        var result = DializerUsageType.ListData().FirstOrDefault(x => x.DializerUsageId == id);
        return MayBe.From(result!);
    }
}