using AptOnline.Domain.BillingContext.ParamSistemFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.BillingContext.ParamSistemFeature;

public interface IParamSistemDal :
    IGetData<MayBe<ParamSistemType>, IParamSistemKey>
{
    MayBe<IEnumerable<ParamSistemType>> ListData();
}