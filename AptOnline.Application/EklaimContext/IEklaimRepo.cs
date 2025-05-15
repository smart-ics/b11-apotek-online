using AptOnline.Domain.EKlaimContext;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Application.EKlaimContext;

public interface IEklaimRepo :
    IInsert<EklaimModel>,
    IUpdate<EklaimModel>,
    IDelete<IEklaimKey>,
    IGetData<IEklaimKey, EklaimModel>,
    IListData<EklaimModel, Periode>
{
}