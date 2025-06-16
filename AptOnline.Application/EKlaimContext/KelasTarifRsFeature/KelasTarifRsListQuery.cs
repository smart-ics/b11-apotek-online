using MediatR;

namespace AptOnline.Application.EKlaimContext.KelasTarifRsFeature;

public record KelasTarifRsListQuery : IRequest<List<KelasTarifRsListResponse>>;

public record KelasTarifRsListResponse(string KelasTarifRsId, string KelasTarifRsName);

public class KelasTarifRsListHandler : IRequestHandler<KelasTarifRsListQuery, List<KelasTarifRsListResponse>>
{
    private readonly IKelasTarifRsDal _kelasTarifRsDal;

    public KelasTarifRsListHandler(IKelasTarifRsDal kelasTarifRsDal)
    {
        _kelasTarifRsDal = kelasTarifRsDal;
    }

    public Task<List<KelasTarifRsListResponse>> Handle(KelasTarifRsListQuery request, CancellationToken cancellationToken)
    {
        var result = _kelasTarifRsDal
            .ListData()
            .Map(x => x.Select(y => new KelasTarifRsListResponse(y.KelasTarifRsId, y.KelasTarifRsName)).ToList())
            .GetValueOrThrow("Kelas Tarif RS tidak ditemukan");

        return Task.FromResult(result);
    }
}