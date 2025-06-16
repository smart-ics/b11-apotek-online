using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.EKlaimContext.PayorFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.PayorFeature;

public interface IPayorDal :
    IInsert<PayorType>,
    IUpdate<PayorType>,
    IDelete<IPayorKey>,
    IGetData<MayBe<PayorType>, IPayorKey>
{
    MayBe<IEnumerable<PayorType>> ListData();
}