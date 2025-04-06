using AptOnline.Domain.BillingContext.LayananAgg;
using MediatR;

namespace AptOnline.Application.BillingContext.LayananAgg;

public record LayananGetQuery(string LayananId) : IRequest<LayananType>, ILayananKey;

public class LayananGetHandler : IRequestHandler<LayananGetQuery, LayananType>
{
    private readonly ILayananGetService _layananGetService;

    public LayananGetHandler(ILayananGetService layananGetService)
    {
        _layananGetService = layananGetService;
    }

    public Task<LayananType> Handle(LayananGetQuery request, CancellationToken cancellationToken)
    {
        var result = _layananGetService.Execute(request);
        return Task.FromResult(result);
    }
}