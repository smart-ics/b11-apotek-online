using AptOnline.Domain.EKlaimContext.IdrgFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.IdrgFeature;

public interface IIdrgDal :
    IGetDataMayBe<IdrgAbstract, IIdrgKey>
{
    MayBe<IEnumerable<IdrgDiagnosaType>> SearchDiagnosa(string keyword);
    MayBe<IEnumerable<IdrgProsedurType>> SearchProsedur(string keyword);
}