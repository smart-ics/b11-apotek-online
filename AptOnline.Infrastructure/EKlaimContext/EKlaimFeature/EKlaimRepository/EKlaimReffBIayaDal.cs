using System.Data;
using System.Data.SqlClient;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.EKlaimRepository;

public interface IEKlaimReffBiayaDal :
    IInsertBulk<EKlaimReffBiayaDto>,
    IDelete<IEKlaimKey>,
    IListDataMayBe<EKlaimReffBiayaDto, IEKlaimKey>
{
}

public class EKlaimReffBIayaDal : IEKlaimReffBiayaDal
{
    private readonly IDbConnection _conn;

    public EKlaimReffBIayaDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }


    public void Insert(IEnumerable<EKlaimReffBiayaDto> listModel)
    {
        using var bcp = new SqlBulkCopy(_conn.ConnectionString);
        bcp.AddMap("EKlaimId", "EKlaimId");
        bcp.AddMap("NoUrut", "NoUrut");
        bcp.AddMap("TrsId", "TrsId");
        bcp.AddMap("ReffBiayaId", "ReffBiayaId");
        bcp.AddMap("KetBiaya", "KetBiaya");
        bcp.AddMap("Nilai", "Nilai");
        bcp.AddMap("SkemaTarifJknId", "SkemaTarifJknId");
        bcp.AddMap("SkemaTarifJknName", "SkemaTarifJknName");

        var fetched = listModel.ToList();
        bcp.DestinationTableName = "dbo.JKNMW_EKlaimReffBiaya";
        bcp.WriteToServer(fetched.AsDataTable());
    }

    public void Delete(IEKlaimKey key)
    {
        const string sql = @"
            DELETE FROM JKNMW_EKlaimReffBiaya
            WHERE EKlaimId = @EKlaimId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", key.EKlaimId, SqlDbType.VarChar);
        _conn.Execute(sql, dp); 
    }

    public MayBe<IEnumerable<EKlaimReffBiayaDto>> ListData(IEKlaimKey filter)
    {
        const string sql = @"
            SELECT 
                EKlaimId, NoUrut, TrsId, ReffBiayaId, 
                KetBiaya, Nilai, SkemaTarifJknId, SkemaTarifJknName
            FROM 
                JKNMW_EKlaimReffBiaya
            WHERE 
                EKlaimId = @EKlaimId"; 
        
        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", filter.EKlaimId, SqlDbType.VarChar);
        var result = _conn.Read<EKlaimReffBiayaDto>(sql, dp);
        return MayBe.From(result);     
    }
}