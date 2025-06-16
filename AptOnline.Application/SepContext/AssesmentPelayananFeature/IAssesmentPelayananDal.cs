using AptOnline.Domain.SepContext.AssesmentPelayananFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.SepContext.AssesmentPelayananFeature;

public interface IAssesmentPelayananDal :
    IInsert<AssesmentPelayananType>,
    IUpdate<AssesmentPelayananType>,
    IDelete<IAssesmentPelayananKey>,
    IGetData<MayBe<AssesmentPelayananType>, IAssesmentPelayananKey>
{
    MayBe<IEnumerable<AssesmentPelayananType>> ListData();
}