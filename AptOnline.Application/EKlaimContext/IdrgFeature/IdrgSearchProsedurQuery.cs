using AptOnline.Domain.EKlaimContext.IdrgFeature;
using MediatR;

namespace AptOnline.Application.EKlaimContext.IdrgFeature;

public record IdrgSearchProsedurQuery(string Keyword) : IRequest<IEnumerable<IdrgSearchProsedurResponse>>;
public record IdrgSearchProsedurResponse(string IdrgId,bool Im, string IdrgName, string Jenis, bool AllowPrimary);

public class IdrgSearchProsedurHandler : IRequestHandler<IdrgSearchProsedurQuery, IEnumerable<IdrgSearchProsedurResponse>>
{
    private readonly IIdrgDal _idrgDal;

    public IdrgSearchProsedurHandler(IIdrgDal idrgDal)
    {
        _idrgDal = idrgDal;
    }

    public Task<IEnumerable<IdrgSearchProsedurResponse>> Handle(IdrgSearchProsedurQuery request, CancellationToken cancellationToken)
    {
        var idrg = _idrgDal.SearchProsedur(request.Keyword)
            .Map(x => x.Select(y => new IdrgSearchProsedurResponse(y.IdrgId, y.Im, y.IdrgName, y.Kategori switch
            {
                IdrgKetegoriEnum.Diagnosa => "Diagnosa",
                IdrgKetegoriEnum.Prosedur => "Prosedur",
                IdrgKetegoriEnum.Morfologi => "Morfologi",
                _ => "Undefined"
            }, y.IsAllowPrimary)))
            .GetValueOrThrow($"Prosedur '{request.Keyword}' tidak ditemukan");
        
        return Task.FromResult(idrg);
    }
}