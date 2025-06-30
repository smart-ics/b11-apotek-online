using AptOnline.Application.Helpers;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;

public interface IGrouperIdrgRepo :
    ISaveChange<GrouperIdrgModel>,
    IDeleteEntity<IEKlaimKey>,
    ILoadEntity<GrouperIdrgModel, IEKlaimKey>,
    ILoadEntity<GrouperIdrgModel, IRegKey>
{
}