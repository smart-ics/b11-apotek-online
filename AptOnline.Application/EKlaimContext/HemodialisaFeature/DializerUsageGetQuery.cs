using AptOnline.Domain.EKlaimContext.HemodialisaFeature;
using MediatR;

namespace AptOnline.Application.EKlaimContext.HemodialisaFeature;

public record DializerUsageGetQuery(string DializerUsageId) : IRequest<DializerUsageGetResponse>;

public record DializerUsageGetResponse(string DializerUsageId, string DializerUsageName);

public class DializerUsageGet : IRequestHandler<DializerUsageGetQuery, DializerUsageGetResponse>
{
    private readonly IDializerUsageDal _dializerUsageDal;

    public DializerUsageGet(IDializerUsageDal dializerUsageDal)
    {
        _dializerUsageDal = dializerUsageDal;
    }

    public Task<DializerUsageGetResponse> Handle(DializerUsageGetQuery request, CancellationToken cancellationToken)
    {
        var result = _dializerUsageDal
            .GetData(DializerUsageType.Key(request.DializerUsageId))
            .Map(x => new DializerUsageGetResponse(x.DializerUsageId, x.DializerUsageName))
            .GetValueOrThrow($"Cara Masuk '{request.DializerUsageId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }
}