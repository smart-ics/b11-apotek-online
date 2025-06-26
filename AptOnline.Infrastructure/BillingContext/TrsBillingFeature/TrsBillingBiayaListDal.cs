using System.Data;
using System.Data.SqlClient;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.BillingContext.TrsBillingFeature;

public class TrsBillingBiayaListDal : IListDataMayBe<TrsBillingBiayaDto, IRegKey>
{
    private readonly IDbConnection _conn;

    public TrsBillingBiayaListDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }

    public MayBe<IEnumerable<TrsBillingBiayaDto>> ListData(IRegKey key)
    {
        const string sql = @"
            SELECT
                aa.fs_kd_trs, fs_kd_ref_biaya, fs_keterangan, fn_total, fn_modul
            FROM
                ta_trs_billing aa
            WHERE
                aa.fs_kd_reg = @regId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@regId", key.RegId, SqlDbType.VarChar);
        var result = _conn.Read<TrsBillingBiayaDto>(sql, dp)?.ToList();
        if (result is null) 
            return MayBe<IEnumerable<TrsBillingBiayaDto>>.None;
        
        foreach (var item in result.Where(item => item.fs_kd_trs[0..2] == "BE"))
            item.fn_modul = 2;
        
        return MayBe.From<IEnumerable<TrsBillingBiayaDto>>(result);
    }
}