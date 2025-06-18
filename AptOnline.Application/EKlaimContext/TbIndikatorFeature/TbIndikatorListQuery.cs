using AptOnline.Application.EKlaimContext.BayiLahirFeature;
using AptOnline.Domain.EKlaimContext.TbIndikatorFeature;
using MediatR;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.TbIndikatorFeature;

public record TbIndikatorListQuery() : IRequest<List<TbIndikatorListResponse>>;

public record TbIndikatorListResponse(string TbIndikatorId, string TbIndikatorName);

public class TbIndikatorListHandler : IRequestHandler<TbIndikatorListQuery, List<TbIndikatorListResponse>>
{
    public Task<List<TbIndikatorListResponse>> Handle(TbIndikatorListQuery request, CancellationToken cancellationToken)
    {
        var result = MayBe
            .From(TbIndikatorType.ListData())
            .Map(x => x.Select(y => new TbIndikatorListResponse(y.TbIndikatorId, y.TbIndikatorName)).ToList())
            .GetValueOrThrow("Status Bayi Lahir tidak ditemukan");

        return Task.FromResult(result);
    }
}