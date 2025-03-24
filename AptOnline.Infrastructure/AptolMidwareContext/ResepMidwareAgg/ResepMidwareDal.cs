using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.AptolMidwareContext.ResepMidwareAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Infrastructure.AptolMidwareContext.ResepRsAgg;

public class ResepMidwareDal : IResepMidwareDal
{
    private readonly DatabaseOptions _options;
    public ResepMidwareDal(IOptions<DatabaseOptions> options)
    {
        _options = options.Value;
    }

    public void Insert(ResepMidwareModel resepMidware)
    {
        const string sql = @"
            INSERT INTO APTOL_ResepMidware(
                ResepMidwareId, ReffId, ResepMidwareDate, EntryDate, 
                SepId, SepDate, NoPeserta, FaskesId, FaskesAsal, 
                PoliBpjsId, PoliBpjsName, JenisObatId, 
                DokterId, DokterName, Iterasi)
            VALUES(
                @ResepMidwareId, @ReffId, @ResepMidwareDate, @EntryDate, 
                @SepId, @SepDate, @NoPeserta, @FaskesId, @FaskesAsal, 
                @PoliBpjsId, @PoliBpjsName, @JenisObatId, 
                @DokterId, @DokterName, @Iterasi)";

        var dp = new DynamicParameters();
        dp.AddParam("@ResepMidwareId", resepMidware.ResepMidwareId, SqlDbType.VarChar);
        dp.AddParam("@ReffId", resepMidware.ReffId, SqlDbType.VarChar);
        dp.AddParam("@ResepMidwareDate", resepMidware.ReffId, SqlDbType.DateTime);
        dp.AddParam("@EntryDate", resepMidware.ReffId, SqlDbType.DateTime);

        dp.AddParam("@SepId", resepMidware.SepId, SqlDbType.VarChar);
        dp.AddParam("@SepDate", resepMidware.SepId, SqlDbType.DateTime);
        dp.AddParam("@NoPeserta", resepMidware.SepId, SqlDbType.VarChar);
        dp.AddParam("@FaskesId", resepMidware.FaskesId, SqlDbType.VarChar);
        dp.AddParam("@FaskesName", resepMidware.FaskesName, SqlDbType.VarChar);
        dp.AddParam("@PoliBpjsId", resepMidware.PoliBpjsId, SqlDbType.VarChar);
        dp.AddParam("@PoliBpjsName", resepMidware.PoliBpjsName, SqlDbType.VarChar);

        dp.AddParam("@JenisObatId", resepMidware.JenisObatId, SqlDbType.VarChar);
        dp.AddParam("@DokterId", resepMidware.DokterId, SqlDbType.VarChar);
        dp.AddParam("@DokterName", resepMidware.DokterName, SqlDbType.VarChar);
        dp.AddParam("@Iterasi", resepMidware.Iterasi, SqlDbType.Int);

        //  EXECUTE
        using var conn = new SqlConnection(ConnStringHelper.Get(_options));
        conn.Execute(sql, dp);
    }

    public void Update(ResepMidwareModel resepMidware)
    {
        const string sql = @"
            UPDATE
                APTOL_ResepMidware
            SET     
                ReffId = @ReffId,
                ResepMidwareDate = @ResepMidwareDate,
                EntryDate = @EntryDate,
                SepId = @SepId,
                SepDate = @SepDate,
                NoPeserta = @NoPeserta,
                FaskesId = @FaskesAsal,
                FaskesName = @FaskesName,
                PoliBpjsId = @PoliBpjsId,
                PoliBpjsName = @PoliBpjsName,
                JenisObatId = @JenisObatId, 
                DokterId = @DokterId,
                DokterName = @DokterName, 
                Iterasi = @Iterasi
            WHERE
                ResepMidwareId = @ResepMidwareId ";

        var dp = new DynamicParameters();
        dp.AddParam("@ResepMidwareId", resepMidware.ResepMidwareId, SqlDbType.VarChar);
        dp.AddParam("@ReffId", resepMidware.ReffId, SqlDbType.VarChar);
        dp.AddParam("@ResepMidwareDate", resepMidware.ReffId, SqlDbType.DateTime);
        dp.AddParam("@EntryDate", resepMidware.ReffId, SqlDbType.DateTime);

        dp.AddParam("@SepId", resepMidware.SepId, SqlDbType.VarChar);
        dp.AddParam("@SepDate", resepMidware.SepId, SqlDbType.DateTime);
        dp.AddParam("@NoPeserta", resepMidware.SepId, SqlDbType.VarChar);
        dp.AddParam("@FaskesId", resepMidware.FaskesId, SqlDbType.VarChar);
        dp.AddParam("@FaskesName", resepMidware.FaskesName, SqlDbType.VarChar);
        dp.AddParam("@PoliBpjsId", resepMidware.PoliBpjsId, SqlDbType.VarChar);
        dp.AddParam("@PoliBpjsName", resepMidware.PoliBpjsName, SqlDbType.VarChar);

        dp.AddParam("@JenisObatId", resepMidware.JenisObatId, SqlDbType.VarChar);
        dp.AddParam("@DokterId", resepMidware.DokterId, SqlDbType.VarChar);
        dp.AddParam("@DokterName", resepMidware.DokterName, SqlDbType.VarChar);
        dp.AddParam("@Iterasi", resepMidware.Iterasi, SqlDbType.Int);

        //  EXECUTE
        using var conn = new SqlConnection(ConnStringHelper.Get(_options));
        conn.Execute(sql, dp);
    }
    public void Delete(IResepMidwareKey resepMidwareKey)
    {
        const string sql = @"
            DELETE FROM
                APTOL_ResepMidware
            WHERE
                ResepMidwareId = @ResepMidwareId ";

        var dp = new DynamicParameters();
        dp.AddParam("ResepMidwareId", resepMidwareKey.ResepMidwareId, SqlDbType.VarChar);

        //  EXECUTE
        using var conn = new SqlConnection(ConnStringHelper.Get(_options));
        conn.Execute(sql, dp);
    }

    public ResepMidwareModel GetData(IResepMidwareKey key)
    {
        const string sql = @"
            SELECT 
                ResepMidwareId, ReffId, ResepMidwareDate, EntryDate, 
                SepId, SepDate, NoPeserta, FaskesId, FaskesAsal, 
                PoliBpjsId, PoliBpjsName, JenisObatId, 
                DokterId, DokterName, Iterasi            
            FROM
                APTOL_ResepMidware
            WHERE
                ResepMidwareId = @ResepMidwareId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@ResepMidwareId", key.ResepMidwareId, SqlDbType.VarChar);
        
        using var conn = new SqlConnection(ConnStringHelper.Get(_options));
        return conn.ReadSingle<ResepMidwareModel>(sql, dp);
    }


    public IEnumerable<ResepMidwareModel> ListData(Periode filter)
    {
        const string sql = @"
            SELECT 
                ResepMidwareId, ReffId, ResepMidwareDate, EntryDate, 
                SepId, SepDate, NoPeserta, FaskesId, FaskesAsal, 
                PoliBpjsId, PoliBpjsName, JenisObatId, 
                DokterId, DokterName, Iterasi            
            FROM
                APTOL_ResepMidware
            WHERE
                ResepDate BETWEEN @StartDate AND @EndDate";

        var dp = new DynamicParameters();
        dp.AddParam("@StartDate", filter.Tgl1, SqlDbType.DateTime);
        dp.AddParam("@EndDate", filter.Tgl2, SqlDbType.DateTime);
        
        using var conn = new SqlConnection(ConnStringHelper.Get(_options));
        return conn.Read<ResepMidwareModel>(sql, dp);
    }
}
