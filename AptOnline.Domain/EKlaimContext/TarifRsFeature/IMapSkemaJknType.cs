using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public interface IMapSkemaJknDal
{
    MayBe<MapSkemaJknType> GetData(ReffBiayaType reffBiaya);
}