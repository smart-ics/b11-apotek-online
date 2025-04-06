using AptOnline.Domain.PharmacyContext.BrgAgg;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.PharmacyContext.MapDphoAgg;

public interface IMapDphoGetService : INunaService<MapDphoType, IBrgKey>
{
}