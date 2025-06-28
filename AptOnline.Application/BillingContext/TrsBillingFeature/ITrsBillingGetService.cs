using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext.TarifRsFeature;
using Nuna.Lib.CleanArchHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.BillingContext.TrsBillingFeature;

public interface ITrsBillingGetService : INunaService<MayBe<TarifRsModel>, IRegKey>
{
}