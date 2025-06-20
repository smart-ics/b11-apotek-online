
using System.Data;
using System.Data.SqlClient;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.BillingContext.RoomChargeFeature;

public class RoomChargeListDal : IListDataMayBe<RoomChargeBedDto, IRegKey>
{
    private readonly IDbConnection _conn;

    public RoomChargeListDal(IOptions<DatabaseOptions> conn)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(conn.Value));
    }

    public MayBe<IEnumerable<RoomChargeBedDto>> ListData(IRegKey key)
    {
        const string sql = @"
            SELECT
                aa.fd_tgl_Trs Tgl, aa.fs_kd_bed BedId, aa.fs_kd_layanan LayananId,
                ISNULL(bb.fs_nm_bed, '') BedName,
                ISNULL(dd.fs_kd_kelas_dk, '') KelasDkId,
                ISNULL(ee.fs_nm_kelas_dk, '') KelasDkName,
                ISNULL(ff.fs_nm_layanan, '') LayananName,
                ISNULL(ff.fs_kd_layanan_dk, '') LayananDkId,
                ISNULL(gg.fs_nm_layanan_dk, '') LayananDkName
            FROM 
                ta_trs_roomcharge aa
                LEFT JOIN ta_bed bb ON aa.fs_kd_bed = bb.fs_Kd_bed
                LEFT JOIN ta_kamar cc ON bb.fs_kd_kamar = cc.fs_kd_kamar
                LEFT JOIN ta_kelas dd ON cc.fs_kd_kelas = dd.fs_kd_kelas
                LEFT JOIN ta_kelas_dk ee ON dd.fs_kd_kelas_dk = ee.fs_kd_kelas_dk
                LEFT JOIN ta_layanan ff ON aa.fs_kd_layanan = ff.fs_kd_layanan
                LEFT JOIN ta_layanan_dk gg ON ff.fs_kd_layanan_dk = gg.fs_kd_layanan_dk
            WHERE
                aa.fs_kd_reg = @regId";

        var dp = new DynamicParameters();
        dp.Add("@regId", key.RegId, DbType.String, ParameterDirection.Input);
        var result = _conn.Read<RoomChargeBedDto>(sql, dp);
        return MayBe.From(result);        
    }
}