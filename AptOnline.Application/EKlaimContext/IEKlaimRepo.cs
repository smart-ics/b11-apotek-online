using AptOnline.Application.Helpers;
using AptOnline.Domain.EKlaimContext;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Application.EKlaimContext;

public interface IEKlaimRepo :
    IInsert<EKlaimModel>,
    IUpdate<EKlaimModel>,
    IDelete<IEKlaimKey>,
    IGetData<MayBe<EKlaimModel>, IEKlaimKey>
{
    MayBe<IEnumerable<EKlaimModel>> ListData(Periode filter);
}