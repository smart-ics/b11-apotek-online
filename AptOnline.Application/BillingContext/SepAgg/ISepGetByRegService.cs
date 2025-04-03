using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.SepAgg;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.BillingContext.SepAgg;

public interface ISepGetByRegService : INunaService<SepType, IRegKey>
{
}