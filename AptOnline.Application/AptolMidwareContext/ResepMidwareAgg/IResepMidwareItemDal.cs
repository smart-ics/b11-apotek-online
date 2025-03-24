using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using Nuna.Lib.DataAccessHelper;

namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg;

public interface IResepMidwareItemDal :
    IInsertBulk<ResepMidwareItemModel>,
    IDelete<IResepMidwareKey>,
    IListData<ResepMidwareItemModel, IResepMidwareKey>
{
}
