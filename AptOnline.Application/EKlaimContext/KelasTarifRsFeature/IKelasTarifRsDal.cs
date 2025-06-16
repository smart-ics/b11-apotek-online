using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.EKlaimContext.KelasTarifRsFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.KelasTarifRsFeature;

public interface IKelasTarifRsDal :
    IInsert<KelasTarifRsType>,
    IUpdate<KelasTarifRsType>,
    IDelete<IKelasTarifRsKey>,
    IGetData<MayBe<KelasTarifRsType>, IKelasTarifRsKey>
{
    MayBe<IEnumerable<KelasTarifRsType>> ListData();
}