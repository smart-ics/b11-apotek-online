using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.EKlaimContext.Covid19Feature;
using AptOnline.Domain.EKlaimContext.Covid19Feature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.EKlaimContext.Covid19Feature;

public class Covid19StatusDal : ICovid19StatusDal
{
    private readonly IDbConnection _conn;

    public Covid19StatusDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }
    
    public void Insert(Covid19StatusType model)
    {
        const string sql = @"
            INSERT INTO JKNMW_Covid19Status(Covid19StatusId, Covid19StatusName)
            VALUES(@Covid19StatusId, @Covid19StatusName)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@Covid19StatusId", model.Covid19StatusId, SqlDbType.VarChar);
        dp.AddParam("@Covid19StatusName", model.Covid19StatusName, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp);
    }

    public void Update(Covid19StatusType model)
    {
        const string sql = @"
            UPDATE JKNMW_Covid19Status
            SET Covid19StatusName = @Covid19StatusName
            WHERE Covid19StatusId = @Covid19StatusId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@Covid19StatusId", model.Covid19StatusId, SqlDbType.VarChar);
        dp.AddParam("@Covid19StatusName", model.Covid19StatusName, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public void Delete(ICovid19StatusKey key)
    {
        const string sql = @"
            DELETE FROM JKNMW_Covid19Status
            WHERE Covid19StatusId = @Covid19StatusId";

        var dp = new DynamicParameters();
        dp.AddParam("@Covid19StatusId", key.Covid19StatusId, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public MayBe<Covid19StatusType> GetData(ICovid19StatusKey key)
    {
        const string sql = @"
            SELECT Covid19StatusId, Covid19StatusName 
            FROM JKNMW_Covid19Status 
            WHERE Covid19StatusId = @Covid19StatusId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@Covid19StatusId", key.Covid19StatusId, SqlDbType.VarChar);

        var dto = _conn.ReadSingle<Covid19StatusType>(sql, dp);
        return MayBe.From(dto);
    }

    public MayBe<IEnumerable<Covid19StatusType>> ListData()
    {
        const string sql = @"
            SELECT Covid19StatusId, Covid19StatusName 
            FROM JKNMW_Covid19Status ";

        var dto = _conn.Read<Covid19StatusType>(sql);
        return MayBe.From(dto);
    }
}