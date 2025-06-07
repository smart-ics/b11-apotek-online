using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Domain.EmrContext.ResepRsAgg;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg;

public interface IResepMidwareDal :
    IInsert<ResepMidwareModel>,
    IUpdate<ResepMidwareModel>,
    IDelete<IResepMidwareKey>,
    IGetData<ResepMidwareModel, IResepMidwareKey>,
    IGetData<ResepMidwareModel, IResepRsKey>,
    IListData<ResepMidwareModel, Periode>
{
}
