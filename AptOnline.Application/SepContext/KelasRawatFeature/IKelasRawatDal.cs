using AptOnline.Domain.SepContext.KelasRawatFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.SepContext.KelasRawatFeature;

public interface IKelasRawatDal :
    IInsert<KelasRawatType>,
    IUpdate<KelasRawatType>,
    IDelete<IKelasRawatKey>,
    IGetData<MayBe<KelasRawatType>, IKelasRawatKey>
{
    MayBe<IEnumerable<KelasRawatType>> ListData();
}