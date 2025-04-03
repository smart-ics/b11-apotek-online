using AptOnline.Domain.PharmacyContext.DphoAgg;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.PharmacyContext.DphoAgg;

public interface IDphoListService : INunaService<DphoModel, IEnumerable<IBrgKey>>
{
}