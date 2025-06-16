using AptOnline.Application.EKlaimContext.CaraMasukFeature;
using MediatR;

namespace AptOnline.Application.BillingContext.PegFeature;

public record PegListQuery : IRequest<List<PegListResponse>>;

public record PegListResponse(string PegId, string PegName, string Nik);

public class PegListHandler : IRequestHandler<PegListQuery, List<PegListResponse>>
{
    private readonly IPegDal _pegDal;

    public PegListHandler(IPegDal pegDal)
    {
        _pegDal = pegDal;
    }

    public Task<List<PegListResponse>> Handle(PegListQuery request, CancellationToken cancellationToken)
    {
        var result = _pegDal
            .ListData()
            .Map(x => x.Select(y => new PegListResponse(y.PegId, y.PegName, y.Nik)).ToList())
            .GetValueOrThrow("Pegawai tidak ditemukan");

        return Task.FromResult(result);
    }
}