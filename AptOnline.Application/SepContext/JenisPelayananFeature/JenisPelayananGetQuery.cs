using AptOnline.Application.SepContext.KelasRawatFeature;
using AptOnline.Domain.SepContext.JenisPelayananFeature;
using AptOnline.Domain.SepContext.KelasRawatFeature;
using MediatR;

namespace AptOnline.Application.SepContext.JenisPelayananFeature;

public record JenisPelayananGetQuery(string JenisPelayananId) : IRequest<JenisPelayananGetResponse>;

public record JenisPelayananGetResponse(string JenisPelayananId, string JenisPelayananName);

public class JenisPelayananGetHandler : IRequestHandler<JenisPelayananGetQuery, JenisPelayananGetResponse>
{
    private readonly IJenisPelayananDal _jenisPelayananDal;

    public JenisPelayananGetHandler(IJenisPelayananDal jenisPelayananDal)
    {
        _jenisPelayananDal = jenisPelayananDal;
    }

    public Task<JenisPelayananGetResponse> Handle(JenisPelayananGetQuery request, CancellationToken cancellationToken)
    {
        var result = _jenisPelayananDal
            .GetData(JenisPelayananType.Key(request.JenisPelayananId))
            .Map(x => new JenisPelayananGetResponse(x.JenisPelayananId, x.JenisPelayananName))
            .GetValueOrThrow($"Jenis Pelayanan '{request.JenisPelayananId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }
}