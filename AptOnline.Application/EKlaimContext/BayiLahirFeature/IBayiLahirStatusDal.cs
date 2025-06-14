using AptOnline.Domain.EKlaimContext.BayiLahirFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.BayiLahirFeature;

public interface IBayiLahirStatusDal :
    IInsert<BayiLahirStatusType>,
    IUpdate<BayiLahirStatusType>,
    IDelete<IBayiLahirStatusKey>,
    IGetData<MayBe<BayiLahirStatusType>, IBayiLahirStatusKey>
{
    MayBe<IEnumerable<BayiLahirStatusType>> ListData();
}