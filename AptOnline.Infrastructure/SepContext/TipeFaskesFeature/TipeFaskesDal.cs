using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.SepContext.KelasRawatFeature;
using AptOnline.Application.SepContext.TipeFaskesFeature;
using AptOnline.Domain.SepContext.KelasRawatFeature;
using AptOnline.Domain.SepContext.TipeFaskesFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.SepContext.TipeFaskesFeature;

public class TipeFaskesDal : ITipeFaskesDal
{
    private readonly IDbConnection _conn;

    public TipeFaskesDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }
    public void Insert(TipeFaskesType model)
    {
        const string sql = @"
            INSERT INTO JKNMW_TipeFaskes(TipeFaskesId, TipeFaskesName)
            VALUES(@TipeFaskesId, @TipeFaskesName)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@TipeFaskesId", model.TipeFaskesId, SqlDbType.VarChar);
        dp.AddParam("@TipeFaskesName", model.TipeFaskesName, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp);
    }

    public void Update(TipeFaskesType model)
    {
        const string sql = @"
            UPDATE JKNMW_TipeFaskes
            SET TipeFaskesName = @TipeFaskesName
            WHERE TipeFaskesId = @TipeFaskesId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@TipeFaskesId", model.TipeFaskesId, SqlDbType.VarChar);
        dp.AddParam("@TipeFaskesName", model.TipeFaskesName, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public void Delete(ITipeFaskesKey key)
    {
        const string sql = @"
            DELETE FROM JKNMW_TipeFaskes
            WHERE TipeFaskesId = @TipeFaskesId";

        var dp = new DynamicParameters();
        dp.AddParam("@TipeFaskesId", key.TipeFaskesId, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public MayBe<TipeFaskesType> GetData(ITipeFaskesKey key)
    {
        const string sql = @"
            SELECT TipeFaskesId, TipeFaskesName 
            FROM JKNMW_TipeFaskes 
            WHERE TipeFaskesId = @TipeFaskesId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@TipeFaskesId", key.TipeFaskesId, SqlDbType.VarChar);

        var dto = _conn.ReadSingle<TipeFaskesType>(sql, dp);
        return MayBe.From(dto);
    }

    public MayBe<IEnumerable<TipeFaskesType>> ListData()
    {
        const string sql = @"
            SELECT TipeFaskesId, TipeFaskesName 
            FROM JKNMW_TipeFaskes ";

        var dto = _conn.Read<TipeFaskesType>(sql);
        return MayBe.From(dto);
    }

}