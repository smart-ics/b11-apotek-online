using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.EKlaimContext.BayiLahirFeature;
using AptOnline.Application.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.EKlaimContext.BayiLahirFeature;
using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.EKlaimContext.BayiLahirFeature;

public class BayiLahirStatusDal : IBayiLahirStatusDal
{
    private readonly IDbConnection _conn;

    public BayiLahirStatusDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }
    
    public void Insert(BayiLahirStatusType model)
    {
        const string sql = @"
            INSERT INTO JKNMW_BayiLahirStatus(BayiLahirStatusId, BayiLahirStatusName)
            VALUES(@BayiLahirStatusId, @BayiLahirStatusName)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@BayiLahirStatusId", model.BayiLahirStatusId, SqlDbType.VarChar);
        dp.AddParam("@BayiLahirStatusName", model.BayiLahirStatusName, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp);
    }

    public void Update(BayiLahirStatusType model)
    {
        const string sql = @"
            UPDATE JKNMW_BayiLahirStatus
            SET BayiLahirStatusName = @BayiLahirStatusName
            WHERE BayiLahirStatusId = @BayiLahirStatusId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@BayiLahirStatusId", model.BayiLahirStatusId, SqlDbType.VarChar);
        dp.AddParam("@BayiLahirStatusName", model.BayiLahirStatusName, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public void Delete(IBayiLahirStatusKey key)
    {
        const string sql = @"
            DELETE FROM JKNMW_BayiLahirStatus
            WHERE BayiLahirStatusId = @BayiLahirStatusId";

        var dp = new DynamicParameters();
        dp.AddParam("@BayiLahirStatusId", key.BayiLahirStatusId, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public MayBe<BayiLahirStatusType> GetData(IBayiLahirStatusKey key)
    {
        const string sql = @"
            SELECT BayiLahirStatusId, BayiLahirStatusName 
            FROM JKNMW_BayiLahirStatus 
            WHERE BayiLahirStatusId = @BayiLahirStatusId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@BayiLahirStatusId", key.BayiLahirStatusId, SqlDbType.VarChar);

        var dto = _conn.ReadSingle<BayiLahirStatusType>(sql, dp);
        return MayBe.From(dto);
    }

    public MayBe<IEnumerable<BayiLahirStatusType>> ListData()
    {
        const string sql = @"
            SELECT BayiLahirStatusId, BayiLahirStatusName 
            FROM JKNMW_BayiLahirStatus ";

        var dto = _conn.Read<BayiLahirStatusType>(sql);
        return MayBe.From(dto);
    }
}