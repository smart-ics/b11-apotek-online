using MediatR;

namespace AptOnline.Application.EKlaimContext.HemodialisaFeature;

public record DializerUsageListQuery : IRequest<List<DializerUsageListResponse>>;

public record DializerUsageListResponse(string DializerUsageId, string DializerUsageName);

public class DializerUsageListHandler : IRequestHandler<DializerUsageListQuery, List<DializerUsageListResponse>>
{
    private readonly IDializerUsageDal _dializerUsageDal;

    public DializerUsageListHandler(IDializerUsageDal dializerUsageDal)
    {
        _dializerUsageDal = dializerUsageDal;
    }

    public Task<List<DializerUsageListResponse>> Handle(DializerUsageListQuery request, CancellationToken cancellationToken)
    {
        var result = _dializerUsageDal
            .ListData()
            .Map(x => x.Select(y => new DializerUsageListResponse(y.DializerUsageId, y.DializerUsageName)).ToList())
            .GetValueOrThrow("Cara Masuk tidak ditemukan");

        return Task.FromResult(result);
    }
}