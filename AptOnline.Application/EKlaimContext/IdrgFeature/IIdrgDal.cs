using AptOnline.Domain.EKlaimContext.IdrgFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.IdrgFeature;

public interface IIdrgDal :
    IGetDataMayBe<IdrgType, IIdrgKey>
{
    MayBe<IEnumerable<IdrgType>> SearchDiagnosa(string keyword);
    MayBe<IEnumerable<IdrgType>>  SearchProsedur(string keyword);
}