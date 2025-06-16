using MediatR;

namespace AptOnline.Application.EKlaimContext.Covid19Feature;

public record TipeNoKartuListQuery : IRequest<List<TipeNoKartuListResponse>>;

public record TipeNoKartuListResponse(string TipeNoKartuId, string TipeNoKartuName);

public class TipeNoKartuListHandler : IRequestHandler<TipeNoKartuListQuery, List<TipeNoKartuListResponse>>
{
    private readonly ITipeNoKartuDal _tipeNoKartuDal;

    public TipeNoKartuListHandler(ITipeNoKartuDal tipeNoKartuDal)
    {
        _tipeNoKartuDal = tipeNoKartuDal;
    }

    public Task<List<TipeNoKartuListResponse>> Handle(TipeNoKartuListQuery request, CancellationToken cancellationToken)
    {
        var result = _tipeNoKartuDal
            .ListData()
            .Map(x => x.Select(y => new TipeNoKartuListResponse(y.TipeNoKartuId, y.TipeNoKartuName)).ToList())
            .GetValueOrThrow("Tipe No Kartu tidak ditemukan");

        return Task.FromResult(result);
    }
}