using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.BillingContext.PegFeature;
using AptOnline.Domain.BillingContext.PegFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.BillingContext.PegFeature;

public class PegDal : IPegDal
{
    private readonly IDbConnection _conn;

    public PegDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }
    
    public void Insert(PegType model)
    {
        const string sql = @"
            INSERT INTO JKNMW_Peg(PegId, PegName, Nik)
            VALUES(@PegId, @PegName, @Nik)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@PegId", model.PegId, SqlDbType.VarChar);
        dp.AddParam("@PegName", model.PegName, SqlDbType.VarChar);
        dp.AddParam("@Nik", model.Nik, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp);
    }

    public void Update(PegType model)
    {
        const string sql = @"
            UPDATE JKNMW_Peg
            SET PegName = @PegName
            WHERE PegId = @PegId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@PegId", model.PegId, SqlDbType.VarChar);
        dp.AddParam("@PegName", model.PegName, SqlDbType.VarChar);
        dp.AddParam("@Nik", model.Nik, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public void Delete(IPegKey key)
    {
        const string sql = @"
            DELETE FROM JKNMW_Peg
            WHERE PegId = @PegId";

        var dp = new DynamicParameters();
        dp.AddParam("@PegId", key.PegId, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public MayBe<PegType> GetData(IPegKey key)
    {
        const string sql = @"
            SELECT PegId, PegName, Nik 
            FROM JKNMW_Peg 
            WHERE PegId = @PegId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@PegId", key.PegId, SqlDbType.VarChar);

        var dto = _conn.ReadSingle<PegType>(sql, dp);
        return MayBe.From(dto);
    }

    public MayBe<IEnumerable<PegType>> ListData()
    {
        const string sql = @"
            SELECT PegId, PegName, Nik 
            FROM JKNMW_Peg ";

        var dto = _conn.Read<PegType>(sql);
        return MayBe.From(dto);
    }

    public MayBe<PegType> GetData(string key)
    {
        const string sql = @"
            SELECT PegId, PegName, Nik 
            FROM JKNMW_Peg 
            WHERE Nik = @Nik";
        
        var dp = new DynamicParameters();
        dp.AddParam("@Nik", key, SqlDbType.VarChar);

        var dto = _conn.ReadSingle<PegType>(sql, dp);
        return MayBe.From(dto);
    }
}