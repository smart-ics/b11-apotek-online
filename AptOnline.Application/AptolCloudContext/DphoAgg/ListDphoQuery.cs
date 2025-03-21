using AptOnline.Domain.AptolCloudContext.DphoAgg;
using MediatR;

namespace AptOnline.Application.AptolCloudContext.DphoAgg;

public class ListDphoQuery : IRequest<IEnumerable<DphoModel>>
{
}

public class ListDphoHandler : IRequestHandler<ListDphoQuery, IEnumerable<DphoModel>>
{
    private readonly IListDphoService _listRefDphoBpjsService;
    public ListDphoHandler(IListDphoService listRefDphoBpjsService)
    {
        _listRefDphoBpjsService = listRefDphoBpjsService;
    }

    public Task<IEnumerable<DphoModel>> Handle(ListDphoQuery request, CancellationToken cancellationToken)
    {
        var x = _listRefDphoBpjsService.Execute();
        return Task.FromResult(x);
    }
}