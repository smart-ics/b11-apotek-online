using AptOnline.Domain.AptolCloudContext.ObatBpjsAgg;
using AptOnline.Domain.AptolCloudContext.ResepBpjsAgg;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.AptolCloudContext.ResepBpjsAgg
{
    public interface IObatBpjsPerSepListService : INunaService<IEnumerable<ResepBpjsItemModel>, ObatBpjsPerSepListServiceParam>
    {
    }
    public record ObatBpjsPerSepListServiceParam(string NoSep);
}
