using AptOnline.Domain.EKlaimContext.KelasTarifRsFeature;
using MediatR;

namespace AptOnline.Application.EKlaimContext.KelasTarifRsFeature;

public record KelasTarifRsGetQuery(string KelasTarifRsId) : IRequest<KelasTarifRsGetResponse>;

public record KelasTarifRsGetResponse(string KelasTarifRsId, string KelasTarifRsName);

public class KelasTarifRsGet : IRequestHandler<KelasTarifRsGetQuery, KelasTarifRsGetResponse>
{
    private readonly IKelasTarifRsDal _kelasTarifRsDal;

    public KelasTarifRsGet(IKelasTarifRsDal kelasTarifRsDal)
    {
        _kelasTarifRsDal = kelasTarifRsDal;
    }

    public Task<KelasTarifRsGetResponse> Handle(KelasTarifRsGetQuery request, CancellationToken cancellationToken)
    {
        var result = _kelasTarifRsDal
            .GetData(KelasTarifRsType.Key(request.KelasTarifRsId))
            .Map(x => new KelasTarifRsGetResponse(x.KelasTarifRsId, x.KelasTarifRsName))
            .GetValueOrThrow($"Kelas Tarif RS '{request.KelasTarifRsId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }
}