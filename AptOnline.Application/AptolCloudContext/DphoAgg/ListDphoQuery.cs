using AptOnline.Domain.AptolCloudContext.DphoAgg;
using MediatR;

namespace AptOnline.Application.AptolCloudContext.DphoAgg;

public class ListDphoQuery : IRequest<IEnumerable<DphoModel>>
{
}

public class ListDphoHandler : IRequestHandler<ListDphoQuery, IEnumerable<DphoModel>>
{
    private readonly IListDphoService _listDphoService;
    public ListDphoHandler(IListDphoService listDphoService)
    {
        _listDphoService = listDphoService;
    }

    public Task<IEnumerable<DphoModel>> Handle(ListDphoQuery request, CancellationToken cancellationToken)
    {
        var x = _listDphoService.Execute();
        return Task.FromResult(x);
    }
}