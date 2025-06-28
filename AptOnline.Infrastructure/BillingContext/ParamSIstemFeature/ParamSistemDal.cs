using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.BillingContext.ParamSistemFeature;
using AptOnline.Domain.BillingContext.ParamSistemFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.BillingContext.ParamSIstemFeature;

public class ParamSistemDal : IParamSistemDal
{
    private readonly IDbConnection _conn;

    public ParamSistemDal(IOptions<DatabaseOptions> conn)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(conn.Value));
    }

    public MayBe<ParamSistemType> GetData(IParamSistemKey key)
    {
        const string sql = @"
            SELECT fs_kd_parameter AS ParamSistemId, 
                   fs_nm_parameter AS ParamSistemName,
                   fs_value AS ParamValue
            FROM tz_parameter_sistem
            WHERE fs_kd_parameter = @key";
        
        var dp = new DynamicParameters();
        dp.AddParam("@key", key.ParamSistemId, SqlDbType.VarChar);

        var dto = _conn.ReadSingle<ParamSistemType>(sql, dp);
        return MayBe.From(dto);
    }

    public MayBe<IEnumerable<ParamSistemType>> ListData()
    {
        const string sql = @"
            SELECT fs_kd_parameter AS ParamSistemId, 
                   fs_nm_parameter AS ParamSistemName,
                   fs_value AS ParamValue
            FROM tz_parameter_sistem";
        
        var dto = _conn.Read<ParamSistemType>(sql);
        return MayBe.From(dto);
    }
}