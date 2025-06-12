using AptOnline.Application.AptolCloudContext.ObatBpjsAgg;
using AptOnline.Domain.AptolCloudContext.ResepBpjsAgg;
using MediatR;

namespace AptOnline.Application.AptolCloudContext.ResepBpjsAgg;

public record ResepBpjsListQuery(string KodeJenisObat, string TglAwal, string TglAkhir) : IRequest<IEnumerable<ResepBpjsModel>>;

public class ResepBpjsListHandler : IRequestHandler<ResepBpjsListQuery, IEnumerable<ResepBpjsModel>>
{
    private readonly IResepBpjsListService _resepBpjsListService;
    public ResepBpjsListHandler(IResepBpjsListService resepBpjsListService)
    {
        _resepBpjsListService = resepBpjsListService;
    }

    public Task<IEnumerable<ResepBpjsModel>> Handle(ResepBpjsListQuery request, CancellationToken cancellationToken)
    {
        var reqParam = new ResepBpjsListServiceParam(request.KodeJenisObat, request.TglAwal, request.TglAkhir);
        var list = _resepBpjsListService.Execute(reqParam);
        return Task.FromResult(list);
    }
}