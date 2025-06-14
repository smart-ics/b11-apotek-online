using MediatR;

namespace AptOnline.Application.SepContext.KelasRawatFeature;

public record KelasRawatListQuery() : IRequest<List<KelasRawatGetResponse>>;

public record KelasRawatListResponse(string KelasRawatId, string KelasRawatName);

public class KelasRawatListHandler : IRequestHandler<KelasRawatListQuery, List<KelasRawatGetResponse>>
{
    private readonly IKelasRawatDal _kelasRawatDal;

    public KelasRawatListHandler(IKelasRawatDal kelasRawatDal)
    {
        _kelasRawatDal = kelasRawatDal;
    }

    public Task<List<KelasRawatGetResponse>> Handle(KelasRawatListQuery request, CancellationToken cancellationToken)
    {
        var result = _kelasRawatDal
            .ListData()
            .Map(x => x.Select(y => new KelasRawatGetResponse(y.KelasRawatId, y.KelasRawatName)).ToList())
            .GetValueOrThrow("Kelas Rawat tidak ditemukan");

        return Task.FromResult(result);
    }
}