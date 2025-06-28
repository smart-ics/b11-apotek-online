using AptOnline.Domain.EKlaimContext.IdrgFeature;
using MediatR;

namespace AptOnline.Application.EKlaimContext.IdrgFeature;

public record IdrgSearchDiagQuery(string Keyword) : IRequest<IEnumerable<IdrgSearchDiagResponse>>;
public record IdrgSearchDiagResponse(string IdrgId,bool Im, string IdrgName, bool AllowPrimary, bool IsAsterks);

public class IdrgSearchDiagHandler : IRequestHandler<IdrgSearchDiagQuery, IEnumerable<IdrgSearchDiagResponse>>
{
    private readonly IIdrgDal _idrgDal;

    public IdrgSearchDiagHandler(IIdrgDal idrgDal)
    {
        _idrgDal = idrgDal;
    }

    public Task<IEnumerable<IdrgSearchDiagResponse>> Handle(IdrgSearchDiagQuery request, CancellationToken cancellationToken)
    {
        var idrg = _idrgDal.SearchDiagnosa(request.Keyword)
            .Map(x => x.Select(y => new IdrgSearchDiagResponse(y.IdrgId, y.Im, y.IdrgName, y.IsAllowPrimary, y.IsAsterisk)))
            .GetValueOrThrow($"Diagnosa {request.Keyword}' tidak ditemukan");
        
        return Task.FromResult(idrg);
    }
}