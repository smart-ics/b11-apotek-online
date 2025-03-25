using AptOnline.Domain.AptolCloudContext.PpkAgg;
using MediatR;

namespace AptOnline.Application.AptolCloudContext.PpkAgg
{
    public class GetSettingPpkBpjsQuery : IRequest<PpkSettingModel>
    {

    }
    public class GetSettingPpkBpjsQueryHandler : IRequestHandler<GetSettingPpkBpjsQuery, PpkSettingModel>
    {
        private readonly IGetSettingPpkBpjsService _getSettingPpkBpjsSvc;
        public GetSettingPpkBpjsQueryHandler(IGetSettingPpkBpjsService getSettingPpkBpjsSvc)
        {
            _getSettingPpkBpjsSvc = getSettingPpkBpjsSvc;
        }
        public Task<PpkSettingModel> Handle(GetSettingPpkBpjsQuery request, CancellationToken cancellationToken)
        {
            var result = _getSettingPpkBpjsSvc.Execute();
            return Task.FromResult(result);
        }
    }
}