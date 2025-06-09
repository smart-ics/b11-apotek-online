using AptOnline.Domain.AptolCloudContext.ObatBpjsAgg;
using AptOnline.Domain.AptolCloudContext.ResepBpjsAgg;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.AptolCloudContext.ResepBpjsAgg
{
    public interface IObatBpjsPerPesertaListService : INunaService<RiwayatAptolBpjsModel, ObatBpjsPerPesertaListServiceParam>
    {
    }
    public record ObatBpjsPerPesertaListServiceParam(string NoPeserta, string TglAwal, string TglAkhir);
}
