using AptOnline.Domain.EKlaimContext.BayiLahirFeature;
using MediatR;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.BayiLahirFeature;

public record StatusBayiLahirGetQuery(string StatusBayiLahirId) : IRequest<StatusBayiLahirGetResponse>;

public record StatusBayiLahirGetResponse(string StatusBayiLahirId, string StatusBayiLahirName);

public class StatusBayiLahirGet : IRequestHandler<StatusBayiLahirGetQuery, StatusBayiLahirGetResponse>
{
    public Task<StatusBayiLahirGetResponse> Handle(StatusBayiLahirGetQuery request, CancellationToken cancellationToken)
    {
        var result = GetData(request.StatusBayiLahirId)
            .Map(x => new StatusBayiLahirGetResponse(x.StatusBayiLahirId, x.StatusBayiLahirName))
            .GetValueOrThrow($"Status Bayi Lahir '{request.StatusBayiLahirId}' tidak ditemukan");
        
        return Task.FromResult(result);
    }

    private static MayBe<StatusBayiLahirType> GetData(string id)
    {
        var result = StatusBayiLahirType.ListData().FirstOrDefault(x => x.StatusBayiLahirId == id);
        return MayBe.From(result!);
    }
}