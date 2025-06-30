using System.Data;
using System.Data.SqlClient;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.EKlaimContext.GrouperIdrgFeature;

public interface IGrouperIdrgDiagDal:
    IInsertBulk<GrouperIdrgDiagDto>,
    IDelete<IEKlaimKey>,
    IListDataMayBe<GrouperIdrgDiagDto, IEKlaimKey> {}

public class GrouperIdrgDiagDal : IGrouperIdrgDiagDal
{
    private readonly IDbConnection _conn;
    public GrouperIdrgDiagDal(IOptions<DatabaseOptions> options) 
        => _conn = new SqlConnection(ConnStringHelper.Get(options.Value));
    
    public void Insert(IEnumerable<GrouperIdrgDiagDto> listDto)
    {
        using var bcp = new SqlBulkCopy((SqlConnection)_conn);
        if (_conn.State != ConnectionState.Open)
            _conn.Open();
        
        bcp.AddMap("EKlaimId", "EKlaimId");
        bcp.AddMap("NoUrut", "NoUrut");
        bcp.AddMap("IdrgId", "IdrgId");
        bcp.AddMap("Im", "Im");
        bcp.AddMap("IdrgName", "IdrgName");

        var fetched = listDto.ToList();
        bcp.DestinationTableName = "dbo.JKNMW_GrouperIdrgDiag";
        bcp.WriteToServer(fetched.AsDataTable());    }

    public void Delete(IEKlaimKey key)
    {
        const string sql = @"
            DELETE FROM JKNMW_GrouperIdrgDiag
            WHERE EKlaimId = @EklaimId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@EklaimId", key.EKlaimId, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp);
    }

    public MayBe<IEnumerable<GrouperIdrgDiagDto>> ListData(IEKlaimKey filter)
    {
        const string sql = @"
            SELECT
                EKlaimId, NoUrut, IdrgId, Im, IdrgName
            FROM
                JKNMW_GrouperIdrgDiag
            WHERE
                EKlaimId = @EKlaimId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", filter.EKlaimId, SqlDbType.VarChar);
        var result = _conn.Read<GrouperIdrgDiagDto>(sql, dp);
        return MayBe.From(result);  
    }
}