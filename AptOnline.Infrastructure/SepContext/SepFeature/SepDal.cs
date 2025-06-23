using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.SepContext.SepFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.SepContext.SepFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.SepContext.SepFeature;

public class SepDal : ISepDal
{
    private readonly IDbConnection _conn;

    public SepDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }
    public void Insert(SepType model)
    {
        const string sql = @"
           INSERT INTO VCLAIM_Sep(
                fs_kd_trs, fd_tgl_jam_trs, fs_no_sep, fs_no_peserta, 
                fs_kd_kelas_peserta, fs_nm_kelas_peserta,
                fs_kd_ppk_rujukan, fs_nm_ppk_rujukan, fs_kd_jns_pelayanan,  
                fs_kd_tipe_faskes, fs_kd_assesment_pel, fs_no_skdp,
                fs_kd_reg, fs_no_mr, fs_nm_peserta, 
                fs_kd_dpjp, fs_nm_dpjp, fs_kd_dpjp_layanan, 
                fb_prb, fs_prb)
            VALUES(
                @fs_kd_trs, @fd_tgl_jam_trs, @fs_no_sep, @fs_no_peserta, 
                @fs_kd_kelas_peserta, @fs_nm_kelas_peserta,
                @fs_kd_ppk_rujukan, @fs_nm_ppk_rujukan, @fs_kd_jns_pelayanan, 
                @fs_kd_tipe_faskes, @fs_kd_assesment_pel, @fs_no_skdp,
                @fs_kd_reg, @fs_no_mr, @fs_nm_peserta, 
                @fs_kd_dpjp, @fs_nm_dpjp, @fs_kd_dpjp_layanan, 
                @fb_prb, @fs_prb)";

        var dp = new DynamicParameters();
        dp.AddParam("@fs_kd_trs", model.SepId, SqlDbType.VarChar);
        dp.AddParam("@fd_tgl_jam_trs", model.SepDateTime.ToString("yyyy-MM-dd HH:mm:ss"), SqlDbType.VarChar);
        
        dp.AddParam("@fs_no_sep", model.SepNo, SqlDbType.VarChar);
        dp.AddParam("@fs_no_peserta", model.PesertaBpjs.PesertaBpjsNo, SqlDbType.VarChar);
        
        dp.AddParam("@fs_kd_kelas_peserta", model.KelasHak.KelasJknId, SqlDbType.VarChar);
        dp.AddParam("@fs_nm_kelas_peserta", model.KelasHak.KelasJknName, SqlDbType.VarChar);
        
        dp.AddParam("@fs_kd_ppk_rujukan", model.FaskesPerujuk.FaskesId, SqlDbType.VarChar);        
        dp.AddParam("@fs_nm_ppk_rujukan", model.FaskesPerujuk.FaskesName, SqlDbType.VarChar);
        dp.AddParam("@fs_kd_tipe_faskes", model.FaskesPerujuk.TipeFaskes.TipeFaskesId, SqlDbType.VarChar);
        dp.AddParam("@fs_kd_jns_pelayanan", model.JenisPelayanan.JenisPelayananId, SqlDbType.VarChar);
        dp.AddParam("@fs_kd_assesment_pel", model.AssesmentPelayanan.AssesmentPelayananId, SqlDbType.VarChar);
        dp.AddParam("@fs_no_skdp", model.Skdp.SkdpNo, SqlDbType.VarChar);
        
        dp.AddParam("@fs_kd_reg", model.Reg.RegId, SqlDbType.VarChar);
        dp.AddParam("@fs_no_mr", model.Reg.Pasien.PasienId, SqlDbType.VarChar);
        dp.AddParam("@fs_nm_peserta", model.Reg.Pasien.PasienName, SqlDbType.VarChar);
        dp.AddParam("@fs_kd_dpjp", model.Dpjp.DokterId, SqlDbType.VarChar);
        dp.AddParam("@fs_nm_dpjp", model.Dpjp.DokterName, SqlDbType.VarChar);
        dp.AddParam("@fs_kd_dpjp_layanan", model.DpjpLayanan.DokterId, SqlDbType.VarChar);
        dp.AddParam("@fb_prb", model.IsPrb, SqlDbType.Bit);
        dp.AddParam("@fs_prb", model.Prb, SqlDbType.VarChar);

        _conn.Execute(sql, dp);
    }

    public void Update(SepType model)
    {
        throw new NotImplementedException();
    }

    public void Delete(ISepKey key)
    {
        throw new NotImplementedException();
    }

    public MayBe<SepType> GetData(IRegKey key)
    {
        const string sql = @"
           SELECT
                aa.fs_kd_trs SepId, aa.fd_tgl_jam_trs SepDateTime,
                aa.fs_no_sep SepNo, aa.fs_no_peserta PesertaJaminanid,
                aa.fs_kd_kelas_peserta KelasPesertaId,
                aa.fs_nm_kelas_peserta KelasPesertaName,
                
                aa.fs_kd_ppk_rujukan FaskesPerujukId ,
                aa.fs_nm_ppk_rujukan FaskesPerujukName,
                aa.fs_kd_tipe_faskes TipeFaskesPerujukId,
                aa.fs_kd_jns_pelayanan JenisPelayananId,
                aa.fs_kd_assesment_pel AssesmentPelayananId,
                aa.fs_no_skdp SkdpNo,

                aa.fs_kd_reg RegId, aa.fs_no_mr PasienId,
                aa.fs_nm_peserta PasienName, 
                aa.fs_kd_dpjp DpjpId, aa.fs_nm_dpjp DpjpName, 
                aa.fb_prb IsPrb, aa.fs_prb Prb, 
                aa.fs_kd_dpjp_layanan DpjpLayananId,
                
                ISNULL(bb.fs_nm_dpjp,'-') DpjpLayananName,
                ISNULL(ee.AssesmentPelayananName, '-') AssesmentPelayananName
            FROM 
                VCLAIM_Sep aa
                LEFT JOIN VCLAIM_Dpjp bb ON aa.fs_kd_dpjp_layanan = bb.fs_kd_dpjp
                LEFT JOIN JKNMW_AssesmentPelayanan ee ON aa.fs_kd_assesment_pel = ee.AssesmentPelayananId
            WHERE
                aa.fs_kd_reg = @RegID
                AND aa.fd_tgl_void = '3000-01-01'";

        var dp = new DynamicParameters();
        dp.AddParam("@RegId", key.RegId, SqlDbType.VarChar);

        var dto = _conn.ReadSingle<SepDto>(sql, dp);
        var result = MayBe.From(dto).Map(x => x.ToSepType());
        return result;
    }

    public MayBe<IEnumerable<SepType>> ListData()
    {
        throw new NotImplementedException();
    }
}