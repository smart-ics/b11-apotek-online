using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.AptolMidwareContext.ResepMidwareAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using AptOnline.Domain.PharmacyContext.BrgAgg;
using AptOnline.Domain.PharmacyContext.DphoAgg;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;

namespace AptOnline.Infrastructure.AptolMidwareContext.ResepMidwareAgg;

public class ResepMidwareItemDal : IResepMidwareItemDal
{
    private readonly DatabaseOptions _opt;
    public ResepMidwareItemDal(IOptions<DatabaseOptions> options)
    {
        _opt = options.Value;
    }

    public void Insert(IEnumerable<ResepMidwareItemModel> listModel)
    {
        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        using var bcp = new SqlBulkCopy(conn);
        
        var listModelDto = listModel.Select(x => new ResepMidwareItemDto(x));
        
        bcp.AddMap("ResepMidwareId", "ResepMidwareId");
        bcp.AddMap("NoUrut", "NoUrut");
        bcp.AddMap("IsRacik", "IsRacik");
        bcp.AddMap("RacikId", "RacikId");
        bcp.AddMap("BarangId", "BarangId");
        bcp.AddMap("BarangName", "BarangName");
        bcp.AddMap("DphoId", "DphoId");
        bcp.AddMap("DphoName", "DphoName");
        bcp.AddMap("Signa1", "Signa1");
        bcp.AddMap("Signa2", "Signa2");
        bcp.AddMap("Permintaan", "Permintaan");
        bcp.AddMap("Jho", "Jho");
        bcp.AddMap("Jumlah", "Jumlah");
        bcp.AddMap("Note", "Note");
 
        var fetched = listModelDto.ToList();
        bcp.BatchSize = fetched.Count;
        bcp.DestinationTableName = "APTOL_ResepMidwareItem";

        conn.Open();
        bcp.WriteToServer(fetched.AsDataTable());
    }

    public void Delete(IResepMidwareKey key)
    {
        const string sql = @"
            DELETE FROM
                APTOL_ResepMidwareItem
            WHERE
                ResepMidwareId = @resepMidwareId";

        var dp = new DynamicParameters();
        dp.AddParam("@resepMidwareId", key.ResepMidwareId, SqlDbType.VarChar);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);
    }

    public IEnumerable<ResepMidwareItemModel> ListData(IResepMidwareKey filter)
    {
        const string sql = @"
            SELECT
                ResepMidwareId, NoUrut, IsRacik, RacikId,
                BarangId, BarangName, DphoId, DphoName,
                Signa1, Signa2, Permintaan,
                Jho, Jumlah, Note
            FROM
                APTOL_ResepMidwareItem
            WHERE
                ResepMidwareId = @resepMidwareId";

        var dp = new DynamicParameters();
        dp.AddParam("@resepMidwareId", filter.ResepMidwareId, SqlDbType.VarChar);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        var result =  conn.Read<ResepMidwareItemDto>(sql, dp);
        return result?.Select(x => x.ToModel());
    }
}