using AptOnline.Domain.AptolCloudContext.DphoAgg;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.AptolCloudContext.DphoAgg;

public interface IListDphoService : INunaService<IEnumerable<DphoModel>>
{
}
