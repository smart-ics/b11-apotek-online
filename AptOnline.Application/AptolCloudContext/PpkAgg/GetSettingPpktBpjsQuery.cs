using AptOnline.Domain.AptolCloudContext.PpkAgg;
using MediatR;

namespace AptOnline.Application.AptolCloudContext.PpkAgg
{
    public class GetSettingPpkBpjsQuery : IRequest<PpkType>
    {

    }
    public class GetSettingPpkBpjsQueryHandler : IRequestHandler<GetSettingPpkBpjsQuery, PpkType>
    {
        private readonly IPpkGetService _getSettingPpkBpjsSvc;
        public GetSettingPpkBpjsQueryHandler(IPpkGetService getSettingPpkBpjsSvc)
        {
            _getSettingPpkBpjsSvc = getSettingPpkBpjsSvc;
        }
        public Task<PpkType> Handle(GetSettingPpkBpjsQuery request, CancellationToken cancellationToken)
        {
            var result = _getSettingPpkBpjsSvc.Execute();
            return Task.FromResult(result);
        }
    }
}