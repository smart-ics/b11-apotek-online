using AptOnline.Api.Infrastructures.Services;
using AptOnline.Api.Models;
using MediatR;

namespace AptOnline.Api.Usecases
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