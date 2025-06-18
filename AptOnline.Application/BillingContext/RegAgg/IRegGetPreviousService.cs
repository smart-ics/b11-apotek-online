using AptOnline.Domain.BillingContext.RegAgg;
using Nuna.Lib.CleanArchHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.BillingContext.RegAgg;

public interface IRegGetPreviousService : INunaService<MayBe<RegType>, IRegKey>
{}
