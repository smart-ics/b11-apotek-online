using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.SepContext.AssesmentPelayananFeature;
using AptOnline.Application.SepContext.JenisPelayananFeature;
using AptOnline.Domain.SepContext.AssesmentPelayananFeature;
using AptOnline.Domain.SepContext.JenisPelayananFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.SepContext.AssesmentPelayananFeature;

public class AssesmentPelayananDal : IAssesmentPelayananDal
{
    private readonly IDbConnection _conn;

    public AssesmentPelayananDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }
    public void Insert(AssesmentPelayananType model)
    {
        const string sql = @"
            INSERT INTO JKNMW_AssesmentPelayanan(AssesmentPelayananId, AssesmentPelayananName)
            VALUES(@AssesmentPelayananId, @AssesmentPelayananName)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@AssesmentPelayananId", model.AssesmentPelayananId, SqlDbType.VarChar);
        dp.AddParam("@AssesmentPelayananName", model.AssesmentPelayananName, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp);
    }

    public void Update(AssesmentPelayananType model)
    {
        const string sql = @"
            UPDATE JKNMW_AssesmentPelayanan
            SET AssesmentPelayananName = @AssesmentPelayananName
            WHERE AssesmentPelayananId = @AssesmentPelayananId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@AssesmentPelayananId", model.AssesmentPelayananId, SqlDbType.VarChar);
        dp.AddParam("@AssesmentPelayananName", model.AssesmentPelayananName, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public void Delete(IAssesmentPelayananKey key)
    {
        const string sql = @"
            DELETE FROM JKNMW_AssesmentPelayanan
            WHERE AssesmentPelayananId = @AssesmentPelayananId";

        var dp = new DynamicParameters();
        dp.AddParam("@AssesmentPelayananId", key.AssesmentPelayananId, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public MayBe<AssesmentPelayananType> GetData(IAssesmentPelayananKey key)
    {
        const string sql = @"
            SELECT AssesmentPelayananId, AssesmentPelayananName 
            FROM JKNMW_AssesmentPelayanan 
            WHERE AssesmentPelayananId = @AssesmentPelayananId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@AssesmentPelayananId", key.AssesmentPelayananId, SqlDbType.VarChar);

        var dto = _conn.ReadSingle<AssesmentPelayananType>(sql, dp);
        return MayBe.From(dto);
    }

    public MayBe<IEnumerable<AssesmentPelayananType>> ListData()
    {
        const string sql = @"
            SELECT AssesmentPelayananId, AssesmentPelayananName 
            FROM JKNMW_AssesmentPelayanan ";

        var dto = _conn.Read<AssesmentPelayananType>(sql);
        return MayBe.From(dto);
    }

}