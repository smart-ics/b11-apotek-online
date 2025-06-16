using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.SepContext.JenisPelayananFeature;
using AptOnline.Domain.SepContext.JenisPelayananFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.SepContext.JenisPelayananFeature;

public class JenisPelayananDal : IJenisPelayananDal
{
    private readonly IDbConnection _conn;

    public JenisPelayananDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }
    public void Insert(JenisPelayananType model)
    {
        const string sql = @"
            INSERT INTO JKNMW_JenisPelayanan(JenisPelayananId, JenisPelayananName)
            VALUES(@JenisPelayananId, @JenisPelayananName)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@JenisPelayananId", model.JenisPelayananId, SqlDbType.VarChar);
        dp.AddParam("@JenisPelayananName", model.JenisPelayananName, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp);
    }

    public void Update(JenisPelayananType model)
    {
        const string sql = @"
            UPDATE JKNMW_JenisPelayanan
            SET JenisPelayananName = @JenisPelayananName
            WHERE JenisPelayananId = @JenisPelayananId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@JenisPelayananId", model.JenisPelayananId, SqlDbType.VarChar);
        dp.AddParam("@JenisPelayananName", model.JenisPelayananName, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public void Delete(IJenisPelayananKey key)
    {
        const string sql = @"
            DELETE FROM JKNMW_JenisPelayanan
            WHERE JenisPelayananId = @JenisPelayananId";

        var dp = new DynamicParameters();
        dp.AddParam("@JenisPelayananId", key.JenisPelayananId, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public MayBe<JenisPelayananType> GetData(IJenisPelayananKey key)
    {
        const string sql = @"
            SELECT JenisPelayananId, JenisPelayananName 
            FROM JKNMW_JenisPelayanan 
            WHERE JenisPelayananId = @JenisPelayananId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@JenisPelayananId", key.JenisPelayananId, SqlDbType.VarChar);

        var dto = _conn.ReadSingle<JenisPelayananType>(sql, dp);
        return MayBe.From(dto);
    }

    public MayBe<IEnumerable<JenisPelayananType>> ListData()
    {
        const string sql = @"
            SELECT JenisPelayananId, JenisPelayananName 
            FROM JKNMW_JenisPelayanan ";

        var dto = _conn.Read<JenisPelayananType>(sql);
        return MayBe.From(dto);
    }

}