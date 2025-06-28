using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.RoomChargeFeature;
using Nuna.Lib.CleanArchHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.BillingContext.RoomChargeFeature;

public interface IRoomChargeGetService : INunaService<MayBe<RoomChargeModel>, IRegKey>
{
    
}