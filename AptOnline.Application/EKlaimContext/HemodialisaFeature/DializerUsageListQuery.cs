using AptOnline.Domain.EKlaimContext.PelayananDarahFeature;
using MediatR;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.HemodialisaFeature;

public record DializerUsageListQuery() : IRequest<List<DializerUsageListResponse>>;

public record DializerUsageListResponse(string DializerUsageId, string DializerUsageName);

public class DializerUsageListHandler : IRequestHandler<DializerUsageListQuery, List<DializerUsageListResponse>>
{
    public Task<List<DializerUsageListResponse>> Handle(DializerUsageListQuery request, CancellationToken cancellationToken)
    {
        var result = MayBe
            .From(DializerUsageType.ListData())
            .Map(x => x.Select(y => new DializerUsageListResponse(y.DializerUsageId, y.DializerUsageName)).ToList())
            .GetValueOrThrow("Dializer Usage tidak ditemukan");

        return Task.FromResult(result);
    }
}