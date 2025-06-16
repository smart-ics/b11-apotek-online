using AptOnline.Domain.SepContext.TipeFaskesFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.SepContext.TipeFaskesFeature;

public interface ITipeFaskesDal :
    IInsert<TipeFaskesType>,
    IUpdate<TipeFaskesType>,
    IDelete<ITipeFaskesKey>,
    IGetData<MayBe<TipeFaskesType>, ITipeFaskesKey>
{
    MayBe<IEnumerable<TipeFaskesType>> ListData();
}