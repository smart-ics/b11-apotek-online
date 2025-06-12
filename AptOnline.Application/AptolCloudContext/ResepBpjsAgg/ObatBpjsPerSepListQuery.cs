using AptOnline.Application.AptolCloudContext.ObatBpjsAgg;
using AptOnline.Domain.AptolCloudContext.ObatBpjsAgg;
using AptOnline.Domain.AptolCloudContext.ResepBpjsAgg;
using MediatR;

namespace AptOnline.Application.AptolCloudContext.ResepBpjsAgg;

public record ObatBpjsPerSepListQuery(string NoSep) : IRequest<IEnumerable<ResepBpjsItemModel>>;

public class ObatBpjsPerSepListHandler : IRequestHandler<ObatBpjsPerSepListQuery, IEnumerable<ResepBpjsItemModel>>
{
    private readonly IObatBpjsPerSepListService _obatBpjsPerSepListService;
    public ObatBpjsPerSepListHandler(IObatBpjsPerSepListService obatBpjsPerSepListService)
    {
        _obatBpjsPerSepListService = obatBpjsPerSepListService;
    }

    public Task<IEnumerable<ResepBpjsItemModel>> Handle(ObatBpjsPerSepListQuery request, CancellationToken cancellationToken)
    {
        var reqParam = new ObatBpjsPerSepListServiceParam(request.NoSep);
        var list = _obatBpjsPerSepListService.Execute(reqParam);
        return Task.FromResult(list);
    }
}