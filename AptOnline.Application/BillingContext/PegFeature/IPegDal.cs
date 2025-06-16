using AptOnline.Domain.BillingContext.PegFeature;
using AptOnline.Domain.EKlaimContext.BayiLahirFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.BillingContext.PegFeature;

public interface IPegDal :
    IInsert<PegType>,
    IUpdate<PegType>,
    IDelete<IPegKey>,
    IGetData<MayBe<PegType>, IPegKey>
{
    MayBe<IEnumerable<PegType>> ListData();
}