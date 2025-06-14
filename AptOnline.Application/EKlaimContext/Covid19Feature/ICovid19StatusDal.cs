using AptOnline.Domain.EKlaimContext.Covid19Feature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.Covid19Feature;

public interface ICovid19StatusDal :
    IInsert<Covid19StatusType>,
    IUpdate<Covid19StatusType>,
    IDelete<ICovid19StatusKey>,
    IGetData<MayBe<Covid19StatusType>, ICovid19StatusKey>
{
    MayBe<IEnumerable<Covid19StatusType>> ListData();
}