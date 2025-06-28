using AptOnline.Application.Helpers;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.SepContext.SepFeature;

namespace AptOnline.Application.EKlaimContext.EKlaimFeature;

public interface IEKlaimRepo :
    ISaveChange<EKlaimModel>,
    IDeleteEntity<IEKlaimKey>,
    ILoadEntity<EKlaimModel, IEKlaimKey>,
    ILoadEntity<EKlaimModel, IRegKey>
{
}