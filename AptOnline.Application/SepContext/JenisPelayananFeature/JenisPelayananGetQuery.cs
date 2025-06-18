using AptOnline.Domain.EKlaimContext.BayiLahirFeature;
using AptOnline.Domain.SepContext.JenisPelayananFeature;
using MediatR;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.SepContext.JenisPelayananFeature;

public record JenisPelayananGetQuery(string JenisPelayananId) : IRequest<JenisPelayananGetResponse>;

public record JenisPelayananGetResponse(string JenisPelayananId, string JenisPelayananName);

public class JenisPelayananGet : IRequestHandler<JenisPelayananGetQuery, JenisPelayananGetResponse>
{
    public Task<JenisPelayananGetResponse> Handle(JenisPelayananGetQuery request, CancellationToken cancellationToken)
    {
        var result = GetData(request.JenisPelayananId)
            .Map(x => new JenisPelayananGetResponse(x.JenisPelayananId, x.JenisPelayananName))
            .GetValueOrThrow($"Jenis Pelayanan '{request.JenisPelayananId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }

    private static MayBe<JenisPelayananType> GetData(string id)
    {
        var result = JenisPelayananType.ListData().FirstOrDefault(x => x.JenisPelayananId == id);
        return MayBe.From(result!);
    }
}