using AptOnline.Domain.AptolCloudContext.ResepBpjsAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.AptolCloudContext.ResepBpjsAgg
{
    public interface IResepBpjsSaveService : INunaService<ResepBpjsModel, ResepMidwareModel>
    {
    }
}
