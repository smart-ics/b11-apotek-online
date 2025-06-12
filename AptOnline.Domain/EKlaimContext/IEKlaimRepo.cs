using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.Helpers;
using AptOnline.Domain.SepContext.SepFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Domain.EKlaimContext;

public interface IEKlaimRepo :
    IInsert<EKlaimModel>,
    IUpdate<EKlaimModel>,
    IDelete<IEKlaimKey>,
    IGetData<MayBe<EKlaimModel>, IEKlaimKey>,
    IGetData<MayBe<EKlaimModel>, ISepKey>,
    IGetData<MayBe<EKlaimModel>, IRegKey>
{
    MayBe<IEnumerable<EKlaimModel>> ListData(Periode filter);
}