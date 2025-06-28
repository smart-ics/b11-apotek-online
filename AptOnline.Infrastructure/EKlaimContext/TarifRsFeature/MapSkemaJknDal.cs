using System.Data;
using System.Data.SqlClient;
using AptOnline.Domain.EKlaimContext.TarifRsFeature;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.EKlaimContext.TarifRsFeature;

public class MapSkemaJknDal : IMapSkemaJknDal
{
    private readonly IDbConnection _conn;
    private readonly Dictionary<ReffBiayaType, MapSkemaJknType> _mapDictionary = new();
    
    public MapSkemaJknDal(IOptions<DatabaseOptions> opt)
    {   
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value)); 

        const string sql = @"
            SELECT fs_kd_tarif, fs_kd_grup_tarif, 0 as flag  FROM EKLAIM_tarif_maps UNION ALL
            SELECT fs_kd_barang, fs_kd_grup_tarif, 1 as flag FROM EKLAIM_barang_maps UNION ALL
            SELECT fs_kd_bed, fs_kd_grup_tarif, 2 as flag    FROM EKLAIM_akomodasi_maps";
        
        var dto = _conn.Read<MapSkemaJknDto>(sql);
        var list = dto.Select(x => x.ToModel()).ToList();
        _mapDictionary = list.ToDictionary(x => x.ReffBiaya, x => x);
    }
    public MayBe<MapSkemaJknType> GetData(ReffBiayaType key)
    {
        var result = MayBe.From(_mapDictionary.GetValueOrDefault(key));
        return result!;
    }
}