using AptOnline.Api.Models;
using AptOnline.Api.Services;
using MediatR;

namespace AptOnline.Api.Usecases
{
    public class ListRefDphoBpjsQuery : IRequest<ListRefDphoBpjsRespDto>
    {
        
    }
    public class ListRefDphoBpjsQueryResponse
    {
        public string kodeobat { get; set; }
        public string namaobat { get; set; }
        public string prb { get; set; }
        public string kronis { get; set; }
        public string kemo { get; set; }
        public string harga { get; set; }
        public string restriksi { get; set; }
        public string generik { get; set; }
        public object aktif { get; set; }
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