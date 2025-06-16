using MediatR;

namespace AptOnline.Application.SepContext.JenisPelayananFeature;

public record JenisPelayananListQuery : IRequest<List<JenisPelayananListResponse>>;

public record JenisPelayananListResponse(string JenisPelayananId, string JenisPelayananName);

public class JenisPelayananListHandler : IRequestHandler<JenisPelayananListQuery, List<JenisPelayananListResponse>>
{
    private readonly IJenisPelayananDal _jenisPelayananDal;

    public JenisPelayananListHandler(IJenisPelayananDal jenisPelayananDal)
    {
        _jenisPelayananDal = jenisPelayananDal;
    }

    public Task<List<JenisPelayananListResponse>> Handle(JenisPelayananListQuery request, CancellationToken cancellationToken)
    {
        var result = _jenisPelayananDal
            .ListData()
            .Map(x => x.Select(y => new JenisPelayananListResponse(y.JenisPelayananId, y.JenisPelayananName)).ToList())
            .GetValueOrThrow("Jenis Pelayanan tidak ditemukan");

        return Task.FromResult(result);
    }
}