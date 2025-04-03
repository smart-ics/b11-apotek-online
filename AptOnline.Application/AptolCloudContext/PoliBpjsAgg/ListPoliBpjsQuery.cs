using AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;
using MediatR;

namespace AptOnline.Application.AptolCloudContext.PoliBpjsAgg;

public record ListPoliBpjsQuery(string Keyword) : IRequest<IEnumerable<PoliBpjsType>>;

public class ListPoliBpjsHandler : IRequestHandler<ListPoliBpjsQuery, IEnumerable<PoliBpjsType>>
{
    private readonly IListPoliBpjsService _listRefPoliBpjsService;

    public ListPoliBpjsHandler(IListPoliBpjsService listRefPoliBpjsService)
    {
        _listRefPoliBpjsService = listRefPoliBpjsService;
    }
    public Task<IEnumerable<PoliBpjsType>> Handle(ListPoliBpjsQuery request, CancellationToken cancellationToken)
    {
        var result = _listRefPoliBpjsService.Execute(request.Keyword);
        return Task.FromResult(result);
    }
}