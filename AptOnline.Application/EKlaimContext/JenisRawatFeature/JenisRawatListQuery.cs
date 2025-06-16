using MediatR;

namespace AptOnline.Application.EKlaimContext.JenisRawatFeature;

public record JenisRawatListQuery : IRequest<List<JenisRawatListResponse>>;

public record JenisRawatListResponse(string JenisRawatId, string JenisRawatName);

public class JenisRawatListHandler : IRequestHandler<JenisRawatListQuery, List<JenisRawatListResponse>>
{
    private readonly IJenisRawatDal _jenisRawatDal;

    public JenisRawatListHandler(IJenisRawatDal jenisRawatDal)
    {
        _jenisRawatDal = jenisRawatDal;
    }

    public Task<List<JenisRawatListResponse>> Handle(JenisRawatListQuery request, CancellationToken cancellationToken)
    {
        var result = _jenisRawatDal
            .ListData()
            .Map(x => x.Select(y => new JenisRawatListResponse(y.JenisRawatId, y.JenisRawatName)).ToList())
            .GetValueOrThrow("Jenis Rawat tidak ditemukan");

        return Task.FromResult(result);
    }
}