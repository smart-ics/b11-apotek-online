using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.EKlaimContext.PayorFeature;
using AptOnline.Domain.EKlaimContext.PayorFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.EKlaimContext.PayorFeature;

public class PayorDal : IPayorDal
{
    private readonly IDbConnection _conn;

    public PayorDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }
    
    public void Insert(PayorType model)
    {
        const string sql = @"
            INSERT INTO JKNMW_Payor(PayorId, PayorName, Code)
            VALUES(@PayorId, @PayorName, @Code)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@PayorId", model.PayorId, SqlDbType.VarChar);
        dp.AddParam("@PayorName", model.PayorName, SqlDbType.VarChar);
        dp.AddParam("@Code", model.Code, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp);
    }

    public void Update(PayorType model)
    {
        const string sql = @"
            UPDATE JKNMW_Payor
            SET PayorName = @PayorName,
                Code = @Code
            WHERE PayorId = @PayorId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@PayorId", model.PayorId, SqlDbType.VarChar);
        dp.AddParam("@PayorName", model.PayorName, SqlDbType.VarChar);
        dp.AddParam("@Code", model.Code, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public void Delete(IPayorKey key)
    {
        const string sql = @"
            DELETE FROM JKNMW_Payor
            WHERE PayorId = @PayorId";

        var dp = new DynamicParameters();
        dp.AddParam("@PayorId", key.PayorId, SqlDbType.VarChar);
        _conn.Execute(sql, dp);
    }

    public MayBe<PayorType> GetData(IPayorKey key)
    {
        const string sql = @"
            SELECT PayorId, PayorName, Code 
            FROM JKNMW_Payor 
            WHERE PayorId = @PayorId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@PayorId", key.PayorId, SqlDbType.VarChar);

        var dto = _conn.ReadSingle<PayorType>(sql, dp);
        return MayBe.From(dto);
    }

    public MayBe<IEnumerable<PayorType>> ListData()
    {
        const string sql = @"
            SELECT PayorId, PayorName, Code
            FROM JKNMW_Payor ";

        var dto = _conn.Read<PayorType>(sql);
        return MayBe.From(dto);
    }
}