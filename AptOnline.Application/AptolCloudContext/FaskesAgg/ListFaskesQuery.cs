using AptOnline.Domain.AptolCloudContext.FaskesAgg;
using MediatR;

namespace AptOnline.Application.AptolCloudContext.FaskesAgg; 

public record ListFaskesQuery(string JenisFaskes, string Keyword) : IRequest<IEnumerable<FaskesModel>>;

public class ListFaskesHandler : IRequestHandler<ListFaskesQuery, IEnumerable<FaskesModel>>
{
    private readonly IListFaskesService _listFaskesService;
    public ListFaskesHandler(IListFaskesService listFaskesService)
    {
        _listFaskesService = listFaskesService;
    }
    public Task<IEnumerable<FaskesModel>> Handle(ListFaskesQuery request, CancellationToken cancellationToken)
    {
        var x = _listFaskesService.Execute(new ListFaskesQueryParam(request.JenisFaskes, request.Keyword));
        return Task.FromResult(x);
    }
}