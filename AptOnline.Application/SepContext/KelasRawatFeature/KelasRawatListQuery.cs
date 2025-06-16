using MediatR;

namespace AptOnline.Application.SepContext.KelasRawatFeature;

public record KelasRawatListQuery : IRequest<List<KelasRawatListResponse>>;

public record KelasRawatListResponse(string KelasRawatId, string KelasRawatName);

public class KelasRawatListHandler : IRequestHandler<KelasRawatListQuery, List<KelasRawatListResponse>>
{
    private readonly IKelasRawatDal _kelasRawatDal;

    public KelasRawatListHandler(IKelasRawatDal kelasRawatDal)
    {
        _kelasRawatDal = kelasRawatDal;
    }

    public Task<List<KelasRawatListResponse>> Handle(KelasRawatListQuery request, CancellationToken cancellationToken)
    {
        var result = _kelasRawatDal
            .ListData()
            .Map(x => x.Select(y => new KelasRawatListResponse(y.KelasRawatId, y.KelasRawatName)).ToList())
            .GetValueOrThrow("Kelas Rawat tidak ditemukan");

        return Task.FromResult(result);
    }
}