using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg;

public interface IResepMidwareDal :
    IInsert<ResepMidwareModel>,
    IUpdate<ResepMidwareModel>,
    IDelete<IResepMidwareKey>,
    IGetData<ResepMidwareModel, IResepMidwareKey>,
    IListData<ResepMidwareModel, Periode>
{
}
