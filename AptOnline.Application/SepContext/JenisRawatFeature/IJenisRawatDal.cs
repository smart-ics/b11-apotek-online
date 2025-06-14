using AptOnline.Domain.SepContext.JenisRawatFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.SepContext.JenisRawatFeature;

public interface IJenisRawatDal :
    IInsert<JenisRawatType>,
    IUpdate<JenisRawatType>,
    IDelete<IJenisRawatKey>,
    IGetData<MayBe<JenisRawatType>, IJenisRawatKey>
{
    MayBe<IEnumerable<JenisRawatType>> ListData();
}