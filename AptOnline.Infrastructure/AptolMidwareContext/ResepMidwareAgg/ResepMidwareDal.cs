using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.AptolMidwareContext.ResepMidwareAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Domain.EmrContext.ResepRsAgg;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.TransactionHelper;
using Nuna.Lib.ValidationHelper;
using Xunit;

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
                ResepMidwareId, ResepMidwareDate, 
                ChartId, ResepRsId, ReffId, ResepBpjsNo, JenisObatId, Iterasi, 
                SepId, SepDate, SepNo, NoPeserta, 
                RegId, RegDate, PasienId, PasienName, DokterId, DokterName,
                PpkId, PpkName, PoliBpjsId, PoliBpjsName,   
                BridgeState, CreateTimestamp, ConfirmTimeStamp, SyncTimestamp, UploadTimestamp)
            VALUES(
                @ResepMidwareId, @ResepMidwareDate, 
                @ChartId, @ResepRsId, @ReffId, @JenisObatId, @Iterasi, 
                @SepId, @SepDate, @SepNo, @NoPeserta, 
                @RegId, @RegDate, @PasienId, @PasienName, @DokterId, @DokterName,
                @PpkId, @PpkName, @PoliBpjsId, @PoliBpjsName,   
                @BridgeState, @CreateTimestamp, @ConfirmTimeStamp, @SyncTimestamp, @UploadTimestamp)";

        var dp = new DynamicParameters();
        dp.AddParam("@ResepMidwareId", resepMidware.ResepMidwareId, SqlDbType.VarChar);
        dp.AddParam("@ResepMidwareDate", resepMidware.ResepMidwareDate, SqlDbType.DateTime);

        dp.AddParam("@ChartId", resepMidware.ChartId, SqlDbType.VarChar); 
        dp.AddParam("@ResepRsId", resepMidware.ResepRsId, SqlDbType.VarChar); 
        dp.AddParam("@JenisObatId", resepMidware.JenisObatId, SqlDbType.VarChar);
        dp.AddParam("@ReffId", resepMidware.ReffId, SqlDbType.VarChar);
        dp.AddParam("@ResepBpjsNo", resepMidware.ResepBpjsNo, SqlDbType.VarChar);
        dp.AddParam("@Iterasi", resepMidware.Iterasi, SqlDbType.Int);
        
        dp.AddParam("@SepId", resepMidware.Sep.SepId, SqlDbType.VarChar);
        dp.AddParam("@SepDate", resepMidware.Sep.SepDateTime, SqlDbType.DateTime);
        dp.AddParam("@SepNo", resepMidware.Sep.SepNo, SqlDbType.VarChar);
        dp.AddParam("@NoPeserta", resepMidware.Sep.NoPeserta, SqlDbType.VarChar);
        dp.AddParam("@RegId", resepMidware.Sep.Reg.RegId, SqlDbType.VarChar); 
        dp.AddParam("@RegDate", resepMidware.Sep.Reg.RegDate, SqlDbType.DateTime); 
        dp.AddParam("@PasienId", resepMidware.Sep.Reg.PasienId, SqlDbType.VarChar); 
        dp.AddParam("@PasienName", resepMidware.Sep.Reg.PasienName, SqlDbType.VarChar);
        dp.AddParam("@DokterId", resepMidware.Sep.Dpjp.DokterId, SqlDbType.VarChar);
        dp.AddParam("@DokterName", resepMidware.Sep.Dpjp.DokterName, SqlDbType.VarChar);

        dp.AddParam("@PpkId", resepMidware.Ppk.PpkId, SqlDbType.VarChar);
        dp.AddParam("@PpkName", resepMidware.Ppk.PpkName, SqlDbType.VarChar);
        dp.AddParam("@PoliBpjsId", resepMidware.PoliBpjs.PoliBpjsId, SqlDbType.VarChar);
        dp.AddParam("@PoliBpjsName", resepMidware.PoliBpjs.PoliBpjsName, SqlDbType.VarChar);
        
        dp.AddParam("@BridgeState", resepMidware.BridgeState, SqlDbType.VarChar);
        dp.AddParam("@CreateTimestamp", resepMidware.CreateTimestamp, SqlDbType.DateTime);
        dp.AddParam("@ConfirmTimeStamp", resepMidware.ConfirmTimeStamp, SqlDbType.DateTime);
        dp.AddParam("@SyncTimestamp", resepMidware.SyncTimestamp, SqlDbType.DateTime);
        dp.AddParam("@UploadTimestamp", resepMidware.UploadTimestamp, SqlDbType.DateTime);

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

                ChartId = @ChartId,
                ResepRsId = @ResepRsId,
                ReffId = @ReffId,
                ResepBpjsNo = @ResepBpjsNo,
                JenisObatId = @JenisObatId, 
                Iterasi = @Iterasi,

                SepId = @SepId,
                SepDate = @SepDate,
                SepNo = @SepNo,
                NoPeserta = @NoPeserta,
                RegId = @RegId,
                RegDate = @RegDate,
                PasienId = @PasienId,
                PasienName = @PasienName,
                DokterId = @DokterId,
                DokterName = @DokterName, 
                
                PpkId = @PpkId,
                PpkName = @PpkName,
                PoliBpjsId = @PoliBpjsId,
                PoliBpjsName = @PoliBpjsName,

                BridgeState = @BridgeState,
                CreateTimestamp = @CreateTimestamp,
                ConfirmTimeStamp = @ConfirmTimeStamp,
                SyncTimestamp = @SyncTimestamp,
                UploadTimestamp = @UploadTimestamp
            WHERE
                ResepMidwareId = @ResepMidwareId ";

        var dp = new DynamicParameters();
        dp.AddParam("@ResepMidwareId", resepMidware.ResepMidwareId, SqlDbType.VarChar);
        dp.AddParam("@ResepMidwareDate", resepMidware.ResepMidwareDate, SqlDbType.DateTime);

        dp.AddParam("@ChartId", resepMidware.ChartId, SqlDbType.VarChar); 
        dp.AddParam("@ResepRsId", resepMidware.ResepRsId, SqlDbType.VarChar); 
        dp.AddParam("@JenisObatId", resepMidware.JenisObatId, SqlDbType.VarChar);
        dp.AddParam("@ReffId", resepMidware.ReffId, SqlDbType.VarChar);
        dp.AddParam("@ResepBpjsNo", resepMidware.ResepBpjsNo, SqlDbType.VarChar);
        dp.AddParam("@Iterasi", resepMidware.Iterasi, SqlDbType.Int);
        
        dp.AddParam("@SepId", resepMidware.Sep.SepId, SqlDbType.VarChar);
        dp.AddParam("@SepDate", resepMidware.Sep.SepDateTime, SqlDbType.DateTime);
        dp.AddParam("@SepNo", resepMidware.Sep.SepNo, SqlDbType.VarChar);
        dp.AddParam("@NoPeserta", resepMidware.Sep.NoPeserta, SqlDbType.VarChar);
        dp.AddParam("@RegId", resepMidware.Sep.Reg.RegId, SqlDbType.VarChar); 
        dp.AddParam("@RegDate", resepMidware.Sep.Reg.RegDate, SqlDbType.DateTime); 
        dp.AddParam("@PasienId", resepMidware.Sep.Reg.PasienId, SqlDbType.VarChar); 
        dp.AddParam("@PasienName", resepMidware.Sep.Reg.PasienName, SqlDbType.VarChar);
        dp.AddParam("@DokterId", resepMidware.Sep.Dpjp.DokterId, SqlDbType.VarChar);
        dp.AddParam("@DokterName", resepMidware.Sep.Dpjp.DokterName, SqlDbType.VarChar);

        dp.AddParam("@PpkId", resepMidware.Ppk.PpkId, SqlDbType.VarChar);
        dp.AddParam("@PpkName", resepMidware.Ppk.PpkName, SqlDbType.VarChar);
        dp.AddParam("@PoliBpjsId", resepMidware.PoliBpjs.PoliBpjsId, SqlDbType.VarChar);
        dp.AddParam("@PoliBpjsName", resepMidware.PoliBpjs.PoliBpjsName, SqlDbType.VarChar);
        
        dp.AddParam("@BridgeState", resepMidware.BridgeState, SqlDbType.VarChar);
        dp.AddParam("@CreateTimestamp", resepMidware.CreateTimestamp, SqlDbType.DateTime);
        dp.AddParam("@ConfirmTimeStamp", resepMidware.ConfirmTimeStamp, SqlDbType.DateTime);
        dp.AddParam("@SyncTimestamp", resepMidware.SyncTimestamp, SqlDbType.DateTime);
        dp.AddParam("@UploadTimestamp", resepMidware.UploadTimestamp, SqlDbType.DateTime);

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
                ResepMidwareId, ResepMidwareDate, 
                ChartId, ResepRsId, ReffId, ResepBpjsNo, JenisObatId, Iterasi, 
                SepId, SepDate, SepNo, NoPeserta, 
                RegId, RegDate, PasienId, PasienName, DokterId, DokterName,
                PpkId, PpkName, PoliBpjsId, PoliBpjsName,   
                BridgeState, CreateTimestamp, ConfirmTimeStamp, SyncTimestamp, UploadTimestamp            
            FROM
                APTOL_ResepMidware
            WHERE
                ResepMidwareId = @ResepMidwareId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@ResepMidwareId", key.ResepMidwareId, SqlDbType.VarChar);
        
        using var conn = new SqlConnection(ConnStringHelper.Get(_options));
        var result =  conn.ReadSingle<ResepMidwareDto>(sql, dp);
        return result?.ToModel();
    }

    public IEnumerable<ResepMidwareModel> ListData(Periode filter)
    {
        const string sql = @"
            SELECT 
                ResepMidwareId, ResepMidwareDate, 
                ChartId, ResepRsId, ReffId, ResepBpjsNo, JenisObatId, Iterasi, 
                SepId, SepDate, SepNo, NoPeserta, 
                RegId, RegDate, PasienId, PasienName, DokterId, DokterName,
                PpkId, PpkName, PoliBpjsId, PoliBpjsName,   
                BridgeState, CreateTimestamp, ConfirmTimeStamp, SyncTimestamp, UploadTimestamp          
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

    public ResepMidwareModel GetData(IResepRsKey key)
    {
        const string sql = @"
            SELECT 
                ResepMidwareId, ResepMidwareDate, 
                ChartId, ResepRsId, ReffId, ResepBpjsNo, JenisObatId, Iterasi, 
                SepId, SepDate, SepNo, NoPeserta, 
                RegId, RegDate, PasienId, PasienName, DokterId, DokterName,
                PpkId, PpkName, PoliBpjsId, PoliBpjsName,   
                BridgeState, CreateTimestamp, ConfirmTimeStamp, SyncTimestamp, UploadTimestamp            
            FROM
                APTOL_ResepMidware
            WHERE
                ResepRsId = @ResepRsId";

        var dp = new DynamicParameters();
        dp.AddParam("@ResepRsId", key.ResepId, SqlDbType.VarChar);

        using var conn = new SqlConnection(ConnStringHelper.Get(_options));
        var result = conn.ReadSingle<ResepMidwareDto>(sql, dp);
        return result?.ToModel();
    }
}

public class ResepMidwareDalTest
{
    private readonly ResepMidwareDal _sut;

    public ResepMidwareDalTest()
    {
        _sut = new ResepMidwareDal(FakeAppSetting.GetDatabaseOptions());
    }

    private static ResepMidwareModel ResepMidwareFaker()
        => ResepMidwareModel.Load("A", new DateTime(2025,4,1), "B",
            "C","D","E","12345",2,"F", new DateTime(2025,4,2), "G", "H",
            "I", new DateTime(2025,4,3), "J", "K", "L", "M", "N", "O", "P", "Q", "R", 
            new DateTime(2024,4,4),
            new DateTime(2024,4,5),
            new DateTime(2024,4,6),
            new DateTime(2024,4,7));
    
    [Fact]
    public void UT1_Insert()
    {
        using var trans = TransHelper.NewScope();
        _sut.Insert(ResepMidwareFaker());
    }
    [Fact]
    public void UT2_Update()
    {
        using var trans = TransHelper.NewScope();
        _sut.Update(ResepMidwareFaker());
    }
    [Fact]
    public void UT3_Delete()
    {
        using var trans = TransHelper.NewScope();
        _sut.Delete(ResepMidwareFaker());
    }
    [Fact]
    public void UT4_GetData()
    {
        using var trans = TransHelper.NewScope();
        var expected = ResepMidwareFaker(); 
        _sut.Insert(expected);
        var actual = _sut.GetData(expected);
        actual.Should().BeEquivalentTo(expected);
    }
    [Fact]
    public void UT5_ListData()
    {
        using var trans = TransHelper.NewScope();
        var expected = ResepMidwareFaker(); 
        _sut.Insert(expected);
        var actual = _sut.ListData(new Periode(new DateTime(2025,4,1)));
        actual.Should().BeEquivalentTo(new List<ResepMidwareModel>{expected});
    }
}