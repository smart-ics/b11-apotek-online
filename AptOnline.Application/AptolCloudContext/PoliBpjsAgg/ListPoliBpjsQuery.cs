using AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;
using MediatR;

namespace AptOnline.Application.AptolCloudContext.PoliBpjsAgg;

public record ListPoliBpjsQuery(string Keyword) : IRequest<IEnumerable<PoliBpjsModel>>;

public class ListPoliBpjsHandler : IRequestHandler<ListPoliBpjsQuery, IEnumerable<PoliBpjsModel>>
{
    private readonly IListPoliBpjsService _listRefPoliBpjsService;

    public ListPoliBpjsHandler(IListPoliBpjsService listRefPoliBpjsService)
    {
        _listRefPoliBpjsService = listRefPoliBpjsService;
    }
    public Task<IEnumerable<PoliBpjsModel>> Handle(ListPoliBpjsQuery request, CancellationToken cancellationToken)
    {
        var x = _listRefPoliBpjsService.Execute(request.Keyword);
        return Task.FromResult(x);
    }
}