using AptOnline.Domain.AptolCloudContext.ObatBpjsAgg;
using MediatR;

namespace AptOnline.Application.AptolCloudContext.ObatBpjsAgg;

public record ListObatBpjsQuery(string KodeJenisObat, string TglResep, string Keyword) : IRequest<IEnumerable<ObatBpjsModel>>;

public class ListObatBpjsHandler : IRequestHandler<ListObatBpjsQuery, IEnumerable<ObatBpjsModel>>
{
    private readonly IListObatBpjsService _listObatBpjsService;
    public ListObatBpjsHandler(IListObatBpjsService listObatBpjsService)
    {
        _listObatBpjsService = listObatBpjsService;
    }
    public Task<IEnumerable<ObatBpjsModel>> Handle(ListObatBpjsQuery request, CancellationToken cancellationToken)
    {
        var x = _listObatBpjsService.Execute(new ListObatBpjsServiceParam(request.KodeJenisObat, request.TglResep, request.Keyword));
        return Task.FromResult(x);
    }
}