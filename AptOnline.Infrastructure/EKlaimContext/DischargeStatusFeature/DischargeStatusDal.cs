using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.EKlaimContext.DischargeStatusFeature;
using AptOnline.Domain.EKlaimContext.DischargeStatusFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.EKlaimContext.DischargeStatusFeature;

public class DischargeStatusDal : IDischargeStatusDal
{
    private readonly IDbConnection _conn;

    public DischargeStatusDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }
    
    public void Insert(DischargeStatusType model)
    {
        const string sql = @"
            INSERT INTO JKNMW_DischargeStatus(DischargeStatusId, DischargeStatusName)
            VALUES(@DischargeStatusId, @DischargeStatusName)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@DischargeStatusId", model.DischargeStatusId, SqlDbType.VarChar);
        dp.AddParam("@DischargeStatusName", model.DischargeStatusName, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp);
    }

    public void Update(DischargeStatusType model)
    {
        const string sql = @"
            UPDATE JKNMW_DischargeStatus
            SET DischargeStatusName = @DischargeStatusName
            WHERE DischargeStatusId = @DischargeStatusId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@DischargeStatusId", model.DischargeStatusId, SqlDbType.VarChar);
        dp.AddParam("@DischargeStatusName", model.DischargeStatusName, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public void Delete(IDischargeStatusKey key)
    {
        const string sql = @"
            DELETE FROM JKNMW_DischargeStatus
            WHERE DischargeStatusId = @DischargeStatusId";

        var dp = new DynamicParameters();
        dp.AddParam("@DischargeStatusId", key.DischargeStatusId, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public MayBe<DischargeStatusType> GetData(IDischargeStatusKey key)
    {
        const string sql = @"
            SELECT DischargeStatusId, DischargeStatusName 
            FROM JKNMW_DischargeStatus 
            WHERE DischargeStatusId = @DischargeStatusId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@DischargeStatusId", key.DischargeStatusId, SqlDbType.VarChar);

        var dto = _conn.ReadSingle<DischargeStatusType>(sql, dp);
        return MayBe.From(dto);
    }

    public MayBe<IEnumerable<DischargeStatusType>> ListData()
    {
        const string sql = @"
            SELECT DischargeStatusId, DischargeStatusName 
            FROM JKNMW_DischargeStatus ";

        var dto = _conn.Read<DischargeStatusType>(sql);
        return MayBe.From(dto);
    }
}