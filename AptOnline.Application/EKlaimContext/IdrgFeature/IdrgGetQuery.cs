using AptOnline.Domain.EKlaimContext.IdrgFeature;
using MediatR;

namespace AptOnline.Application.EKlaimContext.IdrgFeature;

public record IdrgGetQuery(string IdrgId, int Im) : IRequest<IdrgGetResponseBase>;
public record IdrgGetResponseBase(string IdrgId,bool Im, string IdrgName, string Jenis);
public record IdrgGetResponseDiag(string IdrgId,bool Im, string IdrgName, string Jenis,
    bool AllowPrimary, bool IsAstersk) : IdrgGetResponseBase(IdrgId, Im, IdrgName, Jenis);

public class IdrgGetQueryHandler : IRequestHandler<IdrgGetQuery, IdrgGetResponseBase>
{
    private readonly IIdrgDal _idrgDal;

    public IdrgGetQueryHandler(IIdrgDal idrgDal)
    {
        _idrgDal = idrgDal;
    }

    public Task<IdrgGetResponseBase> Handle(IdrgGetQuery request, CancellationToken cancellationToken)
    {
        var im = request.Im != 0;
        var idrg = _idrgDal.GetData(IdrgAbstract.Key(request.IdrgId, im))
            .Map(x => x is IdrgDiagnosaType dx
                ? new IdrgGetResponseDiag(dx.IdrgId, dx.Im, dx.IdrgName, "Diagnosa", dx.IsAllowPrimary, dx.IsAsterisk) 
                : new IdrgGetResponseBase(x.IdrgId, x.Im, x.IdrgName, x is IdrgProsedurType ? "Prosedur" : "Morfologi"))
            .GetValueOrThrow($"Idrg '{request.IdrgId}' (im: {im}) tidak ditemukan");
        
        return Task.FromResult(idrg);
    }
}