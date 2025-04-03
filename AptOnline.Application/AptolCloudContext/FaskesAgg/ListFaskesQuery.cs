using AptOnline.Domain.AptolCloudContext.FaskesAgg;
using MediatR;

namespace AptOnline.Application.AptolCloudContext.FaskesAgg; 

public record ListFaskesQuery(string JenisFaskes, string Keyword) : IRequest<IEnumerable<FaskesType>>;

public class ListFaskesHandler : IRequestHandler<ListFaskesQuery, IEnumerable<FaskesType>>
{
    private readonly IListFaskesService _listFaskesService;
    public ListFaskesHandler(IListFaskesService listFaskesService)
    {
        _listFaskesService = listFaskesService;
    }
    public Task<IEnumerable<FaskesType>> Handle(ListFaskesQuery request, CancellationToken cancellationToken)
    {
        var x = _listFaskesService.Execute(new ListFaskesQueryParam(request.JenisFaskes, request.Keyword));
        return Task.FromResult(x);
    }
}