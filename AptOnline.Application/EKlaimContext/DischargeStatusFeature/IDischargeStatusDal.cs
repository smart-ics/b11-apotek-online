using AptOnline.Domain.EKlaimContext.DischargeStatusFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.DischargeStatusFeature;

public interface IDischargeStatusDal :
    IInsert<DischargeStatusType>,
    IUpdate<DischargeStatusType>,
    IDelete<IDischargeStatusKey>,
    IGetData<MayBe<DischargeStatusType>, IDischargeStatusKey>
{
    MayBe<IEnumerable<DischargeStatusType>> ListData();
}