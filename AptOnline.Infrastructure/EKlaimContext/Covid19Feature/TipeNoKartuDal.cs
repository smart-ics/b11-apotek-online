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

public class TipeNoKartuDal : ITipeNoKartuDal
{
    private readonly IDbConnection _conn;

    public TipeNoKartuDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }
    
    public void Insert(TipeNoKartuType model)
    {
        const string sql = @"
            INSERT INTO JKNMW_TipeNoKartu(TipeNoKartuId, TipeNoKartuName)
            VALUES(@TipeNoKartuId, @TipeNoKartuName)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@TipeNoKartuId", model.TipeNoKartuId, SqlDbType.VarChar);
        dp.AddParam("@TipeNoKartuName", model.TipeNoKartuName, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp);
    }

    public void Update(TipeNoKartuType model)
    {
        const string sql = @"
            UPDATE JKNMW_TipeNoKartu
            SET TipeNoKartuName = @TipeNoKartuName
            WHERE TipeNoKartuId = @TipeNoKartuId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@TipeNoKartuId", model.TipeNoKartuId, SqlDbType.VarChar);
        dp.AddParam("@TipeNoKartuName", model.TipeNoKartuName, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public void Delete(ITipeNoKartuKey key)
    {
        const string sql = @"
            DELETE FROM JKNMW_TipeNoKartu
            WHERE TipeNoKartuId = @TipeNoKartuId";

        var dp = new DynamicParameters();
        dp.AddParam("@TipeNoKartuId", key.TipeNoKartuId, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public MayBe<TipeNoKartuType> GetData(ITipeNoKartuKey key)
    {
        const string sql = @"
            SELECT TipeNoKartuId, TipeNoKartuName 
            FROM JKNMW_TipeNoKartu 
            WHERE TipeNoKartuId = @TipeNoKartuId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@TipeNoKartuId", key.TipeNoKartuId, SqlDbType.VarChar);

        var dto = _conn.ReadSingle<TipeNoKartuType>(sql, dp);
        return MayBe.From(dto);
    }

    public MayBe<IEnumerable<TipeNoKartuType>> ListData()
    {
        const string sql = @"
            SELECT TipeNoKartuId, TipeNoKartuName 
            FROM JKNMW_TipeNoKartu ";

        var dto = _conn.Read<TipeNoKartuType>(sql);
        return MayBe.From(dto);
    }
}