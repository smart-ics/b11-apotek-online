using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.BillingContext.PegFeature;
using AptOnline.Application.BillingContext.TipeLayananDkFeature;
using AptOnline.Domain.BillingContext.PegFeature;
using AptOnline.Domain.BillingContext.TipeLayananDkFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.BillingContext.TipeLayananDkFeature;

public class TipeLayananDkDal : ITipeLayananDkDal
{
    private readonly IDbConnection _conn;

    public TipeLayananDkDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }
    
    public void Insert(TipeLayananDkType model)
    {
        const string sql = @"
            INSERT INTO ta_layanan_tipe_dk(fs_kd_layanan_tipe_dk, fs_nm_layanan_tipe_dk)
            VALUES(@fs_kd_layanan_tipe_dk, @fs_nm_layanan_tipe_dk)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@fs_kd_layanan_tipe_dk", model.TipeLayananDkId, SqlDbType.VarChar);
        dp.AddParam("@fs_nm_layanan_tipe_dk", model.TipeLayananDkName, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp);
    }

    public void Update(TipeLayananDkType model)
    {
        const string sql = @"
            UPDATE ta_layanan_tipe_dk
            SET fs_nm_layanan_tipe_dk = @fs_nm_layanan_tipe_dk
            WHERE fs_kd_layanan_tipe_dk = @fs_kd_layanan_tipe_dk";
        
        var dp = new DynamicParameters();
        dp.AddParam("@fs_kd_layanan_tipe_dk", model.TipeLayananDkId, SqlDbType.VarChar);
        dp.AddParam("@fs_nm_layanan_tipe_dk", model.TipeLayananDkName, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public void Delete(ITipeLayananDkKey key)
    {
        const string sql = @"
            DELETE FROM ta_layanan_tipe_dk
            WHERE fs_kd_layanan_tipe_dk = @fs_kd_layanan_tipe_dk";

        var dp = new DynamicParameters();
        dp.AddParam("@fs_kd_layanan_tipe_dk", key.TipeLayananDkId, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public MayBe<TipeLayananDkType> GetData(ITipeLayananDkKey key)
    {
        const string sql = @"
            SELECT fs_kd_layanan_tipe_dk AS TipeLayananDkId, 
                   fs_nm_layanan_tipe_dk AS TipeLayananDkName
            FROM ta_layanan_tipe_dk 
            WHERE fs_kd_layanan_tipe_dk = @fs_kd_layanan_tipe_dk";
        
        var dp = new DynamicParameters();
        dp.AddParam("fs_kd_layanan_tipe_dk", key.TipeLayananDkId, SqlDbType.VarChar);

        var dto = _conn.ReadSingle<TipeLayananDkType>(sql, dp);
        return MayBe.From(dto);
    }

    public MayBe<IEnumerable<TipeLayananDkType>> ListData()
    {
        const string sql = @"
            SELECT fs_kd_layanan_tipe_dk AS TipeLayananDkId, 
                   fs_nm_layanan_tipe_dk AS TipeLayananDkName
            FROM ta_layanan_tipe_dk ";

        var dto = _conn.Read<TipeLayananDkType>(sql);
        return MayBe.From(dto);
    }
}