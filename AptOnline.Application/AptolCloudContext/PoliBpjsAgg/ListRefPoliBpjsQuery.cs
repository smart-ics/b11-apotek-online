using AptOnline.Api.Infrastructures.Services;
using AptOnline.Api.Models;
using MediatR;

namespace AptOnline.Api.Usecases
{
    public class ListRefPoliBpjsQuery : IRequest<ListRefPoliBpjsRespDto>
    {
        public ListRefPoliBpjsQuery(string keyword)
        {
            Keyword = keyword;
        }

        public string JenisFaskes { get; set; }
        public string Keyword { get; set; }
    }
    public class ListRefPoliBpjsQueryHandler : IRequestHandler<ListRefPoliBpjsQuery, ListRefPoliBpjsRespDto>
    {
        private readonly IListRefPoliBpjsService _listRefPoliBpjsService;
        public ListRefPoliBpjsQueryHandler(IListRefPoliBpjsService listRefPoliBpjsService)
        {
            _listRefPoliBpjsService = listRefPoliBpjsService;
        }
        public Task<ListRefPoliBpjsRespDto> Handle(ListRefPoliBpjsQuery request, CancellationToken cancellationToken)
        {
            var x = _listRefPoliBpjsService.Execute(request.Keyword);
            return Task.FromResult(x);
        }
    }
}