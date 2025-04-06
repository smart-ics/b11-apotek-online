using AptOnline.Application.AptolCloudContext.DphoCloudAgg;
using AptOnline.Domain.PharmacyContext.DphoAgg;
using MediatR;

namespace AptOnline.Application.AptolCloudContext.DphoAgg;

public class ListDphoQuery : IRequest<IEnumerable<DphoType>>
{
}

public class ListDphoHandler : IRequestHandler<ListDphoQuery, IEnumerable<DphoType>>
{
    private readonly IDphoCloudListService _listDphoService;
    public ListDphoHandler(IDphoCloudListService listDphoService)
    {
        _listDphoService = listDphoService;
    }

    public Task<IEnumerable<DphoType>> Handle(ListDphoQuery request, CancellationToken cancellationToken)
    {
        var x = _listDphoService.Execute();
        return Task.FromResult(x);
    }
}