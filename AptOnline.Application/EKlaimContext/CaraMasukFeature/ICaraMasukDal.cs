using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.CaraMasukFeature;

public interface ICaraMasukDal :
    IInsert<CaraMasukType>,
    IUpdate<CaraMasukType>,
    IDelete<ICaraMasukKey>,
    IGetData<MayBe<CaraMasukType>, ICaraMasukKey>
{
    MayBe<IEnumerable<CaraMasukType>> ListData();
}