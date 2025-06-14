using MediatR;

namespace AptOnline.Application.SepContext.JenisRawatFeature;

public record JenisRawatListQuery() : IRequest<List<JenisRawatGetResponse>>;

public record JenisRawatListResponse(string JenisRawatId, string JenisRawatName);

public class JenisRawatListHandler : IRequestHandler<JenisRawatListQuery, List<JenisRawatGetResponse>>
{
    private readonly IJenisRawatDal _jenisRawatDal;

    public JenisRawatListHandler(IJenisRawatDal jenisRawatDal)
    {
        _jenisRawatDal = jenisRawatDal;
    }

    public Task<List<JenisRawatGetResponse>> Handle(JenisRawatListQuery request, CancellationToken cancellationToken)
    {
        var result = _jenisRawatDal
            .ListData()
            .Map(x => x.Select(y => new JenisRawatGetResponse(y.JenisRawatId, y.JenisRawatName)).ToList())
            .GetValueOrThrow("Jenis Rawat tidak ditemukan");

        return Task.FromResult(result);
    }
}