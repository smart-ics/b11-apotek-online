using AptOnline.Api.Infrastructures.Services;
using AptOnline.Api.Models;
using MediatR;

namespace AptOnline.Api.Usecases
{
    public class ListRefDphoBpjsQuery : IRequest<ListRefDphoBpjsRespDto>
    {
    }
    public class ListRefDphoBpjsQueryHandler : IRequestHandler<ListRefDphoBpjsQuery, ListRefDphoBpjsRespDto>
    {
        private readonly IListRefDphoBpjsService _listRefDphoBpjsService;
        public ListRefDphoBpjsQueryHandler(IListRefDphoBpjsService listRefDphoBpjsService)
        {
            _listRefDphoBpjsService = listRefDphoBpjsService;
        }
        public Task<ListRefDphoBpjsRespDto> Handle(ListRefDphoBpjsQuery request, CancellationToken cancellationToken)
        {
            var x = _listRefDphoBpjsService.Execute();
            return Task.FromResult(x);
        }
    }
}