using AptOnline.Domain.EKlaimContext.HemodialisaFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.HemodialisaFeature;

public interface IDializerUsageDal :
    IInsert<DializerUsageType>,
    IUpdate<DializerUsageType>,
    IDelete<IDializerUsageKey>,
    IGetData<MayBe<DializerUsageType>, IDializerUsageKey>
{
    MayBe<IEnumerable<DializerUsageType>> ListData();
}