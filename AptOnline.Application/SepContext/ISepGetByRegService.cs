using AptOnline.Application.Helpers;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.SepContext.SepFeature;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.SepContext;

public interface ISepGetByRegService : INunaService<MayBe<SepType>, IRegKey>
{
}