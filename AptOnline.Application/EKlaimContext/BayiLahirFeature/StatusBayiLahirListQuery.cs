using AptOnline.Domain.EKlaimContext.BayiLahirFeature;
using MediatR;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.BayiLahirFeature;

public record StatusBayiLahirListQuery() : IRequest<List<StatusBayiLahirListResponse>>;

public record StatusBayiLahirListResponse(string StatusBayiLahirId, string StatusBayiLahirName);

public class StatusBayiLahirListHandler : IRequestHandler<StatusBayiLahirListQuery, List<StatusBayiLahirListResponse>>
{
    public Task<List<StatusBayiLahirListResponse>> Handle(StatusBayiLahirListQuery request, CancellationToken cancellationToken)
    {
        var result = MayBe
            .From(StatusBayiLahirType.ListData())
            .Map(x => x.Select(y => new StatusBayiLahirListResponse(y.StatusBayiLahirId, y.StatusBayiLahirName)).ToList())
            .GetValueOrThrow("Status Bayi Lahir tidak ditemukan");

        return Task.FromResult(result);
    }
}