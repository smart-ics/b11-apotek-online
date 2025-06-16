using AptOnline.Domain.EKlaimContext.JenisRawatFeature;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Application.EKlaimContext.JenisRawatFeature;

public interface IJenisRawatDal :
    IInsert<JenisRawatType>,
    IUpdate<JenisRawatType>,
    IDelete<IJenisRawatKey>,
    IGetData<MayBe<JenisRawatType>, IJenisRawatKey>
{
    MayBe<IEnumerable<JenisRawatType>> ListData();
}