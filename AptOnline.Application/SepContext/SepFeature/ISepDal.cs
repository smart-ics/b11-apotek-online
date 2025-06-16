using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.SepContext.SepFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.SepContext.SepFeature;

public interface ISepDal :
    IInsert<SepType>,
    IUpdate<SepType>,
    IDelete<ISepKey>,
    IGetData<MayBe<SepType>, IRegKey>
{
    MayBe<IEnumerable<SepType>> ListData();
}