using MediatR;

namespace AptOnline.Infrastructure.AptolCloudContext.PkpAgg
{
    public class GetSettingPpkBpjsQuery : IRequest<GetSettingPpkBpjsRespDto>
    {

    }
    public class GetSettingPpkBpjsQueryHandler : IRequestHandler<GetSettingPpkBpjsQuery, GetSettingPpkBpjsRespDto>
    {
        private readonly IGetSettingPpkBpjsService _getSettingPpkBpjsSvc;
        public GetSettingPpkBpjsQueryHandler(IGetSettingPpkBpjsService getSettingPpkBpjsSvc)
        {
            _getSettingPpkBpjsSvc = getSettingPpkBpjsSvc;
        }
        public Task<GetSettingPpkBpjsRespDto> Handle(GetSettingPpkBpjsQuery request, CancellationToken cancellationToken)
        {
            var x = _getSettingPpkBpjsSvc.Execute();
            return Task.FromResult(x);
        }
    }
}