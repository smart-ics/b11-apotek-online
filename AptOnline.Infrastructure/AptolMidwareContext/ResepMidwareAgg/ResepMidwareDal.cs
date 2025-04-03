using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.AptolMidwareContext.ResepMidwareAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Infrastructure.AptolMidwareContext.ResepMidwareAgg;

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
                ResepMidwareId, ResepMidwareDate, BridgeState, 
                CreateTimestamp, SyncTimestamp, UploadTimestamp,
                ChartId, ResepRsId, RegId, PasienId, PasienName,
                SepId, SepDate, NoPeserta, FaskesId, FaskesAsal, 
                PoliBpjsId, PoliBpjsName, JenisObatId, DokterId, DokterName, 
                ReffId, JenisObatId, Iterasi)
            VALUES(
                @ResepMidwareId, @ResepMidwareDate, @BridgeState, 
                @CreateTimestamp, @SyncTimestamp, @UploadTimestamp,
                @ChartId, @ResepRsId, @RegId, @PasienId, @PasienName,
                @SepId, @SepDate, @NoPeserta, @FaskesId, @FaskesAsal, 
                @PoliBpjsId, @PoliBpjsName, @JenisObatId, @DokterId, @DokterName, 
                @ReffId, @JenisObatId, @Iterasi)";

        var dp = new DynamicParameters();
        dp.AddParam("@ResepMidwareId", resepMidware.ResepMidwareId, SqlDbType.VarChar);
        dp.AddParam("@ResepMidwareDate", resepMidware.ReffId, SqlDbType.DateTime);
        dp.AddParam("@BridgeState", resepMidware.BridgeState, SqlDbType.VarChar);
        dp.AddParam("@CreateTimestamp", resepMidware.CreateTimestamp, SqlDbType.DateTime);
        dp.AddParam("@SyncTimestamp", resepMidware.SyncTimestamp, SqlDbType.DateTime);
        dp.AddParam("@UploadTimestamp", resepMidware.UploadTimestamp, SqlDbType.DateTime);

        dp.AddParam("@ChartId", resepMidware.ChartId, SqlDbType.VarChar); 
        dp.AddParam("@ResepRsId", resepMidware.ResepRsId, SqlDbType.VarChar); 
        dp.AddParam("@RegId", resepMidware.RegId, SqlDbType.VarChar); 
        dp.AddParam("@PasienId", resepMidware.PasienId, SqlDbType.VarChar); 
        dp.AddParam("@PasienName", resepMidware.PasienName, SqlDbType.VarChar);
        
        dp.AddParam("@SepId", resepMidware.SepId, SqlDbType.VarChar);
        dp.AddParam("@SepDate", resepMidware.SepId, SqlDbType.DateTime);
        dp.AddParam("@NoPeserta", resepMidware.SepId, SqlDbType.VarChar);
        dp.AddParam("@FaskesId", resepMidware.Faskes.FaskesId, SqlDbType.VarChar);
        dp.AddParam("@FaskesName", resepMidware.Faskes.FaskesName, SqlDbType.VarChar);
        dp.AddParam("@PoliBpjsId", resepMidware.PoliBpjs.PoliBpjsId, SqlDbType.VarChar);
        dp.AddParam("@PoliBpjsName", resepMidware.PoliBpjs.PoliBpjsName, SqlDbType.VarChar);
        dp.AddParam("@DokterId", resepMidware.DokterId, SqlDbType.VarChar);
        dp.AddParam("@DokterName", resepMidware.DokterName, SqlDbType.VarChar);
        
        dp.AddParam("@JenisObatId", resepMidware.JenisObatId, SqlDbType.VarChar);
        dp.AddParam("@ReffId", resepMidware.ReffId, SqlDbType.VarChar);
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
                ResepMidwareDate = @ResepMidwareDate,
                BridgeState = @BridgeState,
                CreateTimestamp = @CreateTimestamp,
                SyncTimestamp = @SyncTimestamp,
                UploadTimestamp = @UploadTimestamp,

                ChartId = @ChartId,
                SepId = @SepId,
                SepDate = @SepDate,
                NoPeserta = @NoPeserta,
                FaskesId = @FaskesAsal,
                FaskesName = @FaskesName,
                PoliBpjsId = @PoliBpjsId,
                PoliBpjsName = @PoliBpjsName,
                DokterId = @DokterId,
                DokterName = @DokterName, 

                JenisObatId = @JenisObatId, 
                ReffId = @ReffId,
                Iterasi = @Iterasi
            WHERE
                ResepMidwareId = @ResepMidwareId ";

        var dp = new DynamicParameters();
        dp.AddParam("@ResepMidwareId", resepMidware.ResepMidwareId, SqlDbType.VarChar);
        dp.AddParam("@ResepMidwareDate", resepMidware.ReffId, SqlDbType.DateTime);
        dp.AddParam("@BridgeState", resepMidware.BridgeState, SqlDbType.VarChar);
        dp.AddParam("@CreateTimestamp", resepMidware.CreateTimestamp, SqlDbType.DateTime);
        dp.AddParam("@SyncTimestamp", resepMidware.SyncTimestamp, SqlDbType.DateTime);
        dp.AddParam("@UploadTimestamp", resepMidware.UploadTimestamp, SqlDbType.DateTime);

        dp.AddParam("@ChartId", resepMidware.ChartId, SqlDbType.VarChar); 
        dp.AddParam("@ResepRsId", resepMidware.ResepRsId, SqlDbType.VarChar); 
        dp.AddParam("@RegId", resepMidware.RegId, SqlDbType.VarChar); 
        dp.AddParam("@PasienId", resepMidware.PasienId, SqlDbType.VarChar); 
        dp.AddParam("@PasienName", resepMidware.PasienName, SqlDbType.VarChar);
        
        dp.AddParam("@SepId", resepMidware.SepId, SqlDbType.VarChar);
        dp.AddParam("@SepDate", resepMidware.SepId, SqlDbType.DateTime);
        dp.AddParam("@NoPeserta", resepMidware.SepId, SqlDbType.VarChar);
        dp.AddParam("@FaskesId", resepMidware.Faskes.FaskesId, SqlDbType.VarChar);
        dp.AddParam("@FaskesName", resepMidware.Faskes.FaskesName, SqlDbType.VarChar);
        dp.AddParam("@PoliBpjsId", resepMidware.PoliBpjs.PoliBpjsId, SqlDbType.VarChar);
        dp.AddParam("@PoliBpjsName", resepMidware.PoliBpjs.PoliBpjsName, SqlDbType.VarChar);
        dp.AddParam("@DokterId", resepMidware.DokterId, SqlDbType.VarChar);
        dp.AddParam("@DokterName", resepMidware.DokterName, SqlDbType.VarChar);
        
        dp.AddParam("@JenisObatId", resepMidware.JenisObatId, SqlDbType.VarChar);
        dp.AddParam("@ReffId", resepMidware.ReffId, SqlDbType.VarChar);
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
                ResepMidwareId, ResepMidwareDate, BridgeState, 
                CreateTimestamp, SyncTimestamp, UploadTimestamp,
                ChartId, ResepRsId, RegId, PasienId, PasienName,
                SepId, SepDate, NoPeserta, FaskesId, FaskesAsal, 
                PoliBpjsId, PoliBpjsName, DokterId, DokterName, 
                ReffId, JenisObatId, Iterasi            
            FROM
                APTOL_ResepMidware
            WHERE
                ResepMidwareId = @ResepMidwareId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@ResepMidwareId", key.ResepMidwareId, SqlDbType.VarChar);
        
        using var conn = new SqlConnection(ConnStringHelper.Get(_options));
        var result =  conn.ReadSingle<ResepMidwareDto>(sql, dp);
        return result.ToModel();

    }

    public IEnumerable<ResepMidwareModel> ListData(Periode filter)
    {
        const string sql = @"
            SELECT 
                ResepMidwareId, ResepMidwareDate, BridgeState, 
                CreateTimestamp, SyncTimestamp, UploadTimestamp,
                ChartId, ResepRsId, RegId, PasienId, PasienName,
                SepId, SepDate, NoPeserta, FaskesId, FaskesAsal, 
                PoliBpjsId, PoliBpjsName, DokterId, DokterName, 
                ReffId, JenisObatId, Iterasi            
            FROM
                APTOL_ResepMidware
            WHERE
                ResepMidwareDate BETWEEN @StartDate AND @EndDate";

        var dp = new DynamicParameters();
        dp.AddParam("@StartDate", filter.Tgl1, SqlDbType.DateTime);
        dp.AddParam("@EndDate", filter.Tgl2, SqlDbType.DateTime);
        
        using var conn = new SqlConnection(ConnStringHelper.Get(_options));
        var result = conn.Read<ResepMidwareDto>(sql, dp);
        var response = result?.Select(x => x.ToModel());
        return response;
    }
}

internal record ResepMidwareDto(
    string ResepMidwareId,
    DateTime ResepMidwareDate,
    DateTime BridgeState,
    DateTime CreateTimestamp,
    DateTime SyncTimestamp,
    DateTime UploadTimestamp,
    string ChartId,
    string ResepRsId,
    string RegId,
    string PasienId,
    string PasienName,
    string SepId,
    DateTime SepDate,
    string NoPeserta,
    string FaskesId,
    string FaskesAsal,
    string PoliBpjsId,
    string PoliBpjsName,
    string JenisObatId,
    string DokterId,
    string DokterName,
    string ReffId,
    int Iterasi)
{
    public ResepMidwareModel ToModel()
    {
        var result = new ResepMidwareModel
        {

        };
        return result;
    }
}
