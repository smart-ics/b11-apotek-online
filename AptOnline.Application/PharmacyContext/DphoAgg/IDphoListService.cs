using AptOnline.Domain.PharmacyContext.BrgAgg;
using AptOnline.Domain.PharmacyContext.DphoAgg;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.PharmacyContext.DphoAgg;

public interface IDphoListService : INunaService<DphoType, IEnumerable<IBrgKey>>
{
}