using AptOnline.Api.Infrastructures.Services;
using AptOnline.Api.Models;
using MediatR;

namespace AptOnline.Api.Usecases
{
    public class ListRefObatBpjsQuery : IRequest<ListRefObatBpjsRespDto>
    {
        public ListRefObatBpjsQuery(string kodeJenisObat, string tglResep, string keyword)
        {
            KodeJenisObat = kodeJenisObat;
            TglResep = tglResep;
            Keyword = keyword;
        }

        public string KodeJenisObat { get; set; }
        public string TglResep { get; set; }
        public string Keyword { get; set; }
    }
    public class ListRefObatBpjsQueryHandler : IRequestHandler<ListRefObatBpjsQuery, ListRefObatBpjsRespDto>
    {
        private readonly IListRefObatBpjsService _listRefObatBpjsService;
        public ListRefObatBpjsQueryHandler(IListRefObatBpjsService listRefObatBpjsService)
        {
            _listRefObatBpjsService = listRefObatBpjsService;
        }
        public Task<ListRefObatBpjsRespDto> Handle(ListRefObatBpjsQuery request, CancellationToken cancellationToken)
        {
            var x = _listRefObatBpjsService.Execute(request.KodeJenisObat, request.TglResep, request.Keyword);
            return Task.FromResult(x);
        }
    }
}