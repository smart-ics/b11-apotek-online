using AptOnline.Domain.BillingContext.TipeLayananDkFeature;
using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.BillingContext.TipeLayananDkFeature;

public interface ITipeLayananDkDal :
    IInsert<TipeLayananDkType>,
    IUpdate<TipeLayananDkType>,
    IDelete<ITipeLayananDkKey>,
    IGetData<MayBe<TipeLayananDkType>, ITipeLayananDkKey>
{
    MayBe<IEnumerable<TipeLayananDkType>> ListData();
}