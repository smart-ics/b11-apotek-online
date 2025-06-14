using AptOnline.Domain.EKlaimContext.Covid19Feature;
using MediatR;

namespace AptOnline.Application.EKlaimContext.Covid19Feature;

public record TipeNoKartuGetQuery(string TipeNoKartuId) : IRequest<TipeNoKartuGetResponse>;

public record TipeNoKartuGetResponse(string TipeNoKartuId, string TipeNoKartuName);

public class TipeNoKartuGet : IRequestHandler<TipeNoKartuGetQuery, TipeNoKartuGetResponse>
{
    private readonly ITipeNoKartuDal _tipeNoKartuDal;

    public TipeNoKartuGet(ITipeNoKartuDal tipeNoKartuDal)
    {
        _tipeNoKartuDal = tipeNoKartuDal;
    }

    public Task<TipeNoKartuGetResponse> Handle(TipeNoKartuGetQuery request, CancellationToken cancellationToken)
    {
        var result = _tipeNoKartuDal
            .GetData(TipeNoKartuType.Key(request.TipeNoKartuId))
            .Map(x => new TipeNoKartuGetResponse(x.TipeNoKartuId, x.TipeNoKartuName))
            .GetValueOrThrow($"Tipe Nomor Kartu '{request.TipeNoKartuId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }
}