using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.EKlaimContext.CaraMasukFeature;
using AptOnline.Domain.EKlaimContext.CaraMasukFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.EKlaimContext.CaraMasukFeature;

public class CaraMasukDal : ICaraMasukDal
{
    private readonly IDbConnection _conn;

    public CaraMasukDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }
    
    public void Insert(CaraMasukType model)
    {
        const string sql = @"
            INSERT INTO JKNMW_CaraMasuk(CaraMasukId, CaraMasukName)
            VALUES(@CaraMasukId, @CaraMasukName)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@CaraMasukId", model.CaraMasukId, SqlDbType.VarChar);
        dp.AddParam("@CaraMasukName", model.CaraMasukName, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp);
    }

    public void Update(CaraMasukType model)
    {
        const string sql = @"
            UPDATE JKNMW_CaraMasuk
            SET CaraMasukName = @CaraMasukName
            WHERE CaraMasukId = @CaraMasukId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@CaraMasukId", model.CaraMasukId, SqlDbType.VarChar);
        dp.AddParam("@CaraMasukName", model.CaraMasukName, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public void Delete(ICaraMasukKey key)
    {
        const string sql = @"
            DELETE FROM JKNMW_CaraMasuk
            WHERE CaraMasukId = @CaraMasukId";

        var dp = new DynamicParameters();
        dp.AddParam("@CaraMasukId", key.CaraMasukId, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public MayBe<CaraMasukType> GetData(ICaraMasukKey key)
    {
        const string sql = @"
            SELECT CaraMasukId, CaraMasukName 
            FROM JKNMW_CaraMasuk 
            WHERE CaraMasukId = @CaraMasukId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@CaraMasukId", key.CaraMasukId, SqlDbType.VarChar);

        var dto = _conn.ReadSingle<CaraMasukType>(sql, dp);
        return MayBe.From(dto);
    }

    public MayBe<IEnumerable<CaraMasukType>> ListData()
    {
        const string sql = @"
            SELECT CaraMasukId, CaraMasukName 
            FROM JKNMW_CaraMasuk ";

        var dto = _conn.Read<CaraMasukType>(sql);
        return MayBe.From(dto);
    }
}