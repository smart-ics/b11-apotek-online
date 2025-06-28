using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.EKlaimContext.IdrgFeature;
using AptOnline.Domain.EKlaimContext.IdrgFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.EKlaimContext.IdrgFeature;

public class IdrgDal : IIdrgDal
{
    private readonly IDbConnection _conn;
    public IdrgDal(IOptions<DatabaseOptions> opt) 
        => _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    
    public MayBe<IdrgType> GetData(IIdrgKey key)
    {
        const string sql = @"
            SELECT
                IdrgId, Code2, IdrgName, StdSystem,
                ValidCode, Accpdx, Asterisk, Im
            FROM
                STD_Idrg
            WHERE
                IdrgId = @IdrgId
                AND Im = @Im";
        
        var dp = new DynamicParameters();
        dp.AddParam("@IdrgId", key.IdrgId, SqlDbType.VarChar);
        dp.AddParam("@Im", key.Im, SqlDbType.Bit);
        
        return MayBe
            .From(_conn.ReadSingle<IdrgDto>(sql, dp))
            .Map(dto => dto.ToModel());
    }

    public MayBe<IEnumerable<IdrgType>> SearchDiagnosa(string keyword)
    {
        const string sql = @"
            SELECT
                IdrgId, Code2, IdrgName, StdSystem,
                ValidCode, Accpdx, Asterisk, Im
            FROM
                STD_Idrg
            WHERE
                IdrgName LIKE '%' + @Keyword + '%'
                AND StdSystem = 'ICD_10_2010_IM'

            UNION
            SELECT
                IdrgId, Code2, IdrgName, StdSystem,
                ValidCode, Accpdx, Asterisk, Im
            FROM
                STD_Idrg
            WHERE
                Code2 = @Keyword
                AND StdSystem = 'ICD_10_2010_IM'

            UNION
            SELECT
                IdrgId, Code2, IdrgName, StdSystem,
                ValidCode, Accpdx, Asterisk, Im
            FROM
                STD_Idrg
            WHERE
                IdrgId =  @Keyword
                AND StdSystem = 'ICD_10_2010_IM'";
        
        var dp = new DynamicParameters();
        dp.AddParam("@keyword", keyword, SqlDbType.VarChar);
        
        return MayBe
            .From(_conn.Read<IdrgDto>(sql, dp))
            .Map(dto => dto.Select(x => x.ToModel()));  
    }

    public MayBe<IEnumerable<IdrgType>> SearchProsedur(string keyword)
    {
        const string sql = @"
            SELECT
                IdrgId, Code2, IdrgName, StdSystem,
                ValidCode, Accpdx, Asterisk, Im
            FROM
                STD_Idrg
            WHERE
                IdrgName LIKE '%' + @Keyword + '%'
                AND StdSystem = 'ICD_9CM_2010_IM'
            
            UNION
            SELECT
                IdrgId, Code2, IdrgName, StdSystem,
                ValidCode, Accpdx, Asterisk, Im
            FROM
                STD_Idrg
            WHERE
                Code2 = @Keyword
                AND StdSystem = 'ICD_9CM_2010_IM'
                
            UNION
            SELECT
                IdrgId, Code2, IdrgName, StdSystem,
                ValidCode, Accpdx, Asterisk, Im
            FROM
                STD_Idrg
            WHERE
                IdrgId = @Keyword
                AND StdSystem = 'ICD_9CM_2010_IM' ";
        
        var dp = new DynamicParameters();
        dp.AddParam("@keyword", keyword, SqlDbType.VarChar);
        
        return MayBe
            .From(_conn.Read<IdrgDto>(sql, dp))
            .Map(dto => dto.Select(x => x.ToModel()));  
    }
}