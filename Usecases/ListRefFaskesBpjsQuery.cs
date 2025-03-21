using AptOnline.Api.Infrastructures.Services;
using AptOnline.Api.Models;
using MediatR;

namespace AptOnline.Api.Usecases
{
    public class ListRefFaskesBpjsQuery : IRequest<ListRefFaskesBpjsRespDto>
    {
        public ListRefFaskesBpjsQuery(string jenisFaskes, string keyword)
        {
            JenisFaskes = jenisFaskes;
            Keyword = keyword;
        }

        public string JenisFaskes { get; set; }
        public string Keyword { get; set; }
    }
    public class ListRefFaskesBpjsQueryHandler : 
        IRequestHandler<ListRefFaskesBpjsQuery, ListRefFaskesBpjsRespDto>
    {
        private readonly IListRefFaskesBpjsService _listRefFaskesBpjsService;
        public ListRefFaskesBpjsQueryHandler(IListRefFaskesBpjsService listRefFaskesBpjsService)
        {
            _listRefFaskesBpjsService = listRefFaskesBpjsService;
        }
        public Task<ListRefFaskesBpjsRespDto> Handle(ListRefFaskesBpjsQuery request, 
            CancellationToken cancellationToken)
        {
            var x = _listRefFaskesBpjsService.Execute(request.JenisFaskes, request.Keyword);
            return Task.FromResult(x);
        }
    }
}