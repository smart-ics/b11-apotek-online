using AptOnline.Domain.BillingContext.PegFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.BillingContext.PegFeature;

public interface IPegDal :
    IInsert<PegType>,
    IUpdate<PegType>,
    IDelete<IPegKey>,
    IGetData<MayBe<PegType>, IPegKey>,
    IGetData<MayBe<PegType>, string>
{
    MayBe<IEnumerable<PegType>> ListData();
}