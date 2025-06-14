using AptOnline.Domain.EKlaimContext.Covid19Feature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.Covid19Feature;

public interface ITipeNoKartuDal :
    IInsert<TipeNoKartuType>,
    IUpdate<TipeNoKartuType>,
    IDelete<ITipeNoKartuKey>,
    IGetData<MayBe<TipeNoKartuType>, ITipeNoKartuKey>
{
    MayBe<IEnumerable<TipeNoKartuType>> ListData();
}