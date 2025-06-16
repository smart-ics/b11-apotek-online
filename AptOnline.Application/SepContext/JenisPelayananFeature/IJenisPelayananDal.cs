using AptOnline.Domain.SepContext.JenisPelayananFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.SepContext.JenisPelayananFeature;

public interface IJenisPelayananDal :
    IInsert<JenisPelayananType>,
    IUpdate<JenisPelayananType>,
    IDelete<IJenisPelayananKey>,
    IGetData<MayBe<JenisPelayananType>, IJenisPelayananKey>
{
    MayBe<IEnumerable<JenisPelayananType>> ListData();
}