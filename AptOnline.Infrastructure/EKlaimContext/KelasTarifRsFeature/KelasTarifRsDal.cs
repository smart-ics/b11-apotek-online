using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.EKlaimContext.CaraMasukFeature;
using AptOnline.Application.EKlaimContext.KelasTarifRsFeature;
using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.EKlaimContext.KelasTarifRsFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.EKlaimContext.KelasTarifRsFeature;

public class KelasTarifRsDal : IKelasTarifRsDal
{
    private readonly IDbConnection _conn;

    public KelasTarifRsDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }
    
    public void Insert(KelasTarifRsType model)
    {
        const string sql = @"
            INSERT INTO JKNMW_KelasTarifRs(KelasTarifRsId, KelasTarifRsName)
            VALUES(@KelasTarifRsId, @KelasTarifRsName)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@KelasTarifRsId", model.KelasTarifRsId, SqlDbType.VarChar);
        dp.AddParam("@KelasTarifRsName", model.KelasTarifRsName, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp);
    }

    public void Update(KelasTarifRsType model)
    {
        const string sql = @"
            UPDATE JKNMW_KelasTarifRs
            SET KelasTarifRsName = @KelasTarifRsName
            WHERE KelasTarifRsId = @KelasTarifRsId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@KelasTarifRsId", model.KelasTarifRsId, SqlDbType.VarChar);
        dp.AddParam("@KelasTarifRsName", model.KelasTarifRsName, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public void Delete(IKelasTarifRsKey key)
    {
        const string sql = @"
            DELETE FROM JKNMW_KelasTarifRs
            WHERE KelasTarifRsId = @KelasTarifRsId";

        var dp = new DynamicParameters();
        dp.AddParam("@KelasTarifRsId", key.KelasTarifRsId, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public MayBe<KelasTarifRsType> GetData(IKelasTarifRsKey key)
    {
        const string sql = @"
            SELECT KelasTarifRsId, KelasTarifRsName 
            FROM JKNMW_KelasTarifRs 
            WHERE KelasTarifRsId = @KelasTarifRsId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@KelasTarifRsId", key.KelasTarifRsId, SqlDbType.VarChar);

        var dto = _conn.ReadSingle<KelasTarifRsType>(sql, dp);
        return MayBe.From(dto);
    }

    public MayBe<IEnumerable<KelasTarifRsType>> ListData()
    {
        const string sql = @"
            SELECT KelasTarifRsId, KelasTarifRsName 
            FROM JKNMW_KelasTarifRs ";

        var dto = _conn.Read<KelasTarifRsType>(sql);
        return MayBe.From(dto);
    }
}