using AptOnline.Domain.AptolCloudContext.FaskesAgg;
using MediatR;

namespace AptOnline.Application.AptolCloudContext.FaskesAgg; 

public class ListFaskesQuery : IRequest<IEnumerable<FaskesModel>>
{
    public ListFaskesQuery(string jenisFaskes, string keyword)
    {
        JenisFaskes = jenisFaskes;
        Keyword = keyword;
    }

    public string JenisFaskes { get; set; }
    public string Keyword { get; set; }
}
public class ListFaskesQuery :  IRequestHandler<ListFaskesQuery, IEnumerable<FaskesModel>>
{
    private readonly IListRefFaskesBpjsService _listRefFaskesBpjsService;
    public ListRefFaskesBpjsQueryHandler(IListRefFaskesBpjsService listRefFaskesBpjsService)
    {
        _listRefFaskesBpjsService = listRefFaskesBpjsService;
    }
    public Task<ListRefFaskesBpjsRespDto> Handle(ListFaskesQuery request, 
        CancellationToken cancellationToken)
    {
        var x = _listRefFaskesBpjsService.Execute(request.JenisFaskes, request.Keyword);
        return Task.FromResult(x);
    }
}