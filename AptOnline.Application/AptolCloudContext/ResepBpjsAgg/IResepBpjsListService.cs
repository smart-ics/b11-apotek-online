using AptOnline.Domain.AptolCloudContext.ResepBpjsAgg;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.AptolCloudContext.ObatBpjsAgg
{
    public interface IResepBpjsListService : INunaService<IEnumerable<ResepBpjsModel>, ResepBpjsListServiceParam>
    {
    }
    public record ResepBpjsListServiceParam(string KodeJenisObat, string TglAwal, string TglAkhir);
}
