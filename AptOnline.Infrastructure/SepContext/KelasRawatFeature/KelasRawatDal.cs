using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.SepContext.KelasRawatFeature;
using AptOnline.Domain.SepContext.KelasRawatFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.SepContext.KelasRawatFeature;

public class KelasRawatDal : IKelasRawatDal
{
    private readonly IDbConnection _conn;

    public KelasRawatDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }
    public void Insert(KelasRawatType model)
    {
        const string sql = @"
            INSERT INTO JKNMW_KelasRawat(KelasRawatId, KelasRawatName)
            VALUES(@KelasRawatId, @KelasRawatName)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@KelasRawatId", model.KelasRawatId, SqlDbType.VarChar);
        dp.AddParam("@KelasRawatName", model.KelasRawatName, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp);
    }

    public void Update(KelasRawatType model)
    {
        const string sql = @"
            UPDATE JKNMW_KelasRawat
            SET KelasRawatName = @KelasRawatName
            WHERE KelasRawatId = @KelasRawatId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@KelasRawatId", model.KelasRawatId, SqlDbType.VarChar);
        dp.AddParam("@KelasRawatName", model.KelasRawatName, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public void Delete(IKelasRawatKey key)
    {
        const string sql = @"
            DELETE FROM JKNMW_KelasRawat
            WHERE KelasRawatId = @KelasRawatId";

        var dp = new DynamicParameters();
        dp.AddParam("@KelasRawatId", key.KelasRawatId, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public MayBe<KelasRawatType> GetData(IKelasRawatKey key)
    {
        const string sql = @"
            SELECT KelasRawatId, KelasRawatName 
            FROM JKNMW_KelasRawat 
            WHERE KelasRawatId = @KelasRawatId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@KelasRawatId", key.KelasRawatId, SqlDbType.VarChar);

        var dto = _conn.ReadSingle<KelasRawatType>(sql, dp);
        return MayBe.From(dto);
    }

    public MayBe<IEnumerable<KelasRawatType>> ListData()
    {
        const string sql = @"
            SELECT KelasRawatId, KelasRawatName 
            FROM JKNMW_KelasRawat ";

        var dto = _conn.Read<KelasRawatType>(sql);
        return MayBe.From(dto);
    }

}