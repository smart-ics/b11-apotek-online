using AptOnline.Domain.AptolCloudContext.ObatBpjsAgg;
using AptOnline.Domain.AptolCloudContext.ResepBpjsAgg;
using MediatR;

namespace AptOnline.Application.AptolCloudContext.ResepBpjsAgg;

public record ObatBpjsPerPesertaListQuery(string NoPeserta, string TglAwal, string TglAkhir) : IRequest<RiwayatAptolBpjsModel>;

public class ObatBpjsPerPesertaListHandler : IRequestHandler<ObatBpjsPerPesertaListQuery, RiwayatAptolBpjsModel>
{
    private readonly IObatBpjsPerPesertaListService _obatBpjsPerPesertaListService;
    public ObatBpjsPerPesertaListHandler(IObatBpjsPerPesertaListService obatBpjsPerPesertaListService)
    {
        _obatBpjsPerPesertaListService = obatBpjsPerPesertaListService;
    }

    public Task<RiwayatAptolBpjsModel> Handle(ObatBpjsPerPesertaListQuery request, CancellationToken cancellationToken)
    {
        var reqParam = new ObatBpjsPerPesertaListServiceParam(request.NoPeserta, request.TglAwal, request.TglAkhir);
        var list = _obatBpjsPerPesertaListService.Execute(reqParam);
        return Task.FromResult(list);
    }
}