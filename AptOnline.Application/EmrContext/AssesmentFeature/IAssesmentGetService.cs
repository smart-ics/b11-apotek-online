using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EmrContext.AssesmentFeature;
using Nuna.Lib.CleanArchHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EmrContext.AssesmentFeature;

public interface IAssesmentGetService : INunaService<MayBe<AssesmentModel>, IRegKey>
{
   
}