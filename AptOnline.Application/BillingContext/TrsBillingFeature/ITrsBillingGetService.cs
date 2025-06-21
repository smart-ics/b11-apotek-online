using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.TrsBillingFeature;
using Nuna.Lib.CleanArchHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.BillingContext.TrsBillingFeature;

public interface ITrsBillingGetService : INunaService<MayBe<TrsBillingModel>, IRegKey>
{
}