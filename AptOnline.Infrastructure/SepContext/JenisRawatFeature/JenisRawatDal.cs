using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.SepContext.JenisRawatFeature;
using AptOnline.Application.SepContext.KelasRawatFeature;
using AptOnline.Domain.SepContext.JenisRawatFeature;
using AptOnline.Domain.SepContext.KelasRawatFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.SepContext.JenisRawatFeature;

public class JenisRawatDal : IJenisRawatDal
{
    private readonly IDbConnection _conn;

    public JenisRawatDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }
    public void Insert(JenisRawatType model)
    {
        const string sql = @"
            INSERT INTO JKNMW_JenisRawat(JenisRawatId, JenisRawatName)
            VALUES(@JenisRawatId, @JenisRawatName)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@JenisRawatId", model.JenisRawatId, SqlDbType.VarChar);
        dp.AddParam("@JenisRawatName", model.JenisRawatName, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp);
    }

    public void Update(JenisRawatType model)
    {
        const string sql = @"
            UPDATE JKNMW_JenisRawat
            SET JenisRawatName = @JenisRawatName
            WHERE JenisRawatId = @JenisRawatId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@JenisRawatId", model.JenisRawatId, SqlDbType.VarChar);
        dp.AddParam("@JenisRawatName", model.JenisRawatName, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public void Delete(IJenisRawatKey key)
    {
        const string sql = @"
            DELETE FROM JKNMW_JenisRawat
            WHERE JenisRawatId = @JenisRawatId";

        var dp = new DynamicParameters();
        dp.AddParam("@JenisRawatId", key.JenisRawatId, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public MayBe<JenisRawatType> GetData(IJenisRawatKey key)
    {
        const string sql = @"
            SELECT JenisRawatId, JenisRawatName 
            FROM JKNMW_JenisRawat 
            WHERE JenisRawatId = @JenisRawatId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@JenisRawatId", key.JenisRawatId, SqlDbType.VarChar);

        var dto = _conn.ReadSingle<JenisRawatType>(sql, dp);
        return MayBe.From(dto);
    }

    public MayBe<IEnumerable<JenisRawatType>> ListData()
    {
        const string sql = @"
            SELECT JenisRawatId, JenisRawatName 
            FROM JKNMW_JenisRawat ";

        var dto = _conn.Read<JenisRawatType>(sql);
        return MayBe.From(dto);
    }

}