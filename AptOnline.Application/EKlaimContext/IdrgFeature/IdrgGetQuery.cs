using AptOnline.Domain.EKlaimContext.IdrgFeature;
using MediatR;

namespace AptOnline.Application.EKlaimContext.IdrgFeature;

public record IdrgGetQuery(string IdrgId, int Im) : IRequest<IdrgGetResponse>;
public record IdrgGetResponse(string IdrgId,bool Im, string IdrgName, string Jenis, bool AllowPrimary);

public class IdrgGetQueryHandler : IRequestHandler<IdrgGetQuery, IdrgGetResponse>
{
    private readonly IIdrgDal _idrgDal;

    public IdrgGetQueryHandler(IIdrgDal idrgDal)
    {
        _idrgDal = idrgDal;
    }

    public Task<IdrgGetResponse> Handle(IdrgGetQuery request, CancellationToken cancellationToken)
    {
        var im = request.Im != 0;
        var idrg = _idrgDal.GetData(IdrgType.Key(request.IdrgId, im))
            .Map(x => new IdrgGetResponse(x.IdrgId, im, x.IdrgName, x.Kategori switch
            {
                IdrgKetegoriEnum.Diagnosa => "Diagnosa",
                IdrgKetegoriEnum.Prosedur => "Prosedur",
                IdrgKetegoriEnum.Morfologi => "Morfologi",
                _ => "Undefined"
            }, x.IsAllowPrimary))
            .GetValueOrThrow($"Idrg '{request.IdrgId}' (im: {im}) tidak ditemukan");
        
        return Task.FromResult(idrg);
    }
}