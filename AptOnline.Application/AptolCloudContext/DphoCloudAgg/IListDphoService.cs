using AptOnline.Domain.PharmacyContext.DphoAgg;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.AptolCloudContext.DphoCloudAgg;

public interface IDphoCloudListService : INunaService<IEnumerable<DphoType>>
{
}
