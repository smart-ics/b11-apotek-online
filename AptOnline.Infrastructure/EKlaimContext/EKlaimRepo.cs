using System.Data;
using System.Data.SqlClient;
using AptOnline.Application.EKlaimContext;
using AptOnline.Application.Helpers;
using AptOnline.Domain.EKlaimContext;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Infrastructure.EKlaimContext;

public class EKlaimRepo : IEKlaimRepo
{
    private readonly DatabaseOptions _opt;

    public EKlaimRepo(IOptions<DatabaseOptions> opt)
    {
        _opt = opt.Value;
    }

    public void Insert(EKlaimModel model)
    {
        const string sql = @"
           INSERT INTO JKNMW_EKlaim(
                EKlaimId, EKlaimDate, SepNo, KartuBpjsNo, 
                PasienId, PasienName, BirthDate, Gender)
            VALUES(
                @EKlaimId, @EKlaimDate, @SepNo, @KartuBpjsNo, 
                @PasienId, @PasienName, @BirthDate, @Gender)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", model.EKlaimId, SqlDbType.VarChar);
        dp.AddParam("@EKlaimDate", model.EKlaimDate, SqlDbType.DateTime);
        dp.AddParam("@SepNo", model.Sep.SepNo, SqlDbType.VarChar);
        dp.AddParam("@KartuBpjsNo", model.PesertaBpjs.PesertaBpjsNo, SqlDbType.VarChar);
        dp.AddParam("@PasienId", model.Pasien.PasienId, SqlDbType.VarChar);
        dp.AddParam("@PasienName", model.Pasien.PasienName, SqlDbType.VarChar);
        dp.AddParam("@BirthDate", model.Pasien.BirthDate, SqlDbType.DateTime);
        dp.AddParam("@Gender", model.Pasien.Gender.Value, SqlDbType.VarChar);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);
    }
    //  TODO: TABLE JKNMW SepNo kurang panjang
    public void Update(EKlaimModel model)
    {
        const string sql = @"
            UPDATE JKNMW_EKlaim
            SET EKlaimDate = @EKlaimDate, 
                SepNo = @SepNo, 
                KartuBpjsNo = @KartuBpjsNo, 
                PasienId = @PasienId, 
                PasienName = @PasienName, 
                BirthDate = @BirthDate, 
                Gender = @Gender
            WHERE EKlaimId = @EKlaimId";

        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", model.EKlaimId, SqlDbType.VarChar);
        dp.AddParam("@EKlaimDate", model.EKlaimDate, SqlDbType.DateTime);
        dp.AddParam("@SepNo", model.Sep.SepNo, SqlDbType.VarChar);
        dp.AddParam("@KartuBpjsNo", model.PesertaBpjs.PesertaBpjsNo, SqlDbType.VarChar);
        dp.AddParam("@PasienId", model.Pasien.PasienId, SqlDbType.VarChar);
        dp.AddParam("@PasienName", model.Pasien.PasienName, SqlDbType.VarChar);
        dp.AddParam("@BirthDate", model.Pasien.BirthDate, SqlDbType.DateTime);
        dp.AddParam("@Gender", model.Pasien.Gender.Value, SqlDbType.VarChar);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);
    }

    public void Delete(IEKlaimKey key)
    {
        const string sql = @"
            DELETE FROM JKNMW_EKlaim
            WHERE EKlaimId = @EKlaimId";
        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", key.EKlaimId, SqlDbType.VarChar);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        conn.Execute(sql, dp);
    }

    public MayBe<EKlaimModel> GetData(IEKlaimKey key)
    {
        const string sql = @"
            SELECT 
                aa.EKlaimId, aa.EKlaimDate, aa.SepNo, aa.KartuBpjsNo, 
                aa.PasienId, aa.PasienName, aa.BirthDate, aa.Gender,
                ISNULL(bb.fs_kd_trs, '') AS SepId,
                ISNULL(bb.fs_no_sep, '') AS SepNo,
                ISNULL(bb.fd_tgl_trs, '3000-01-01') AS SepDateTime
            FROM 
                JKNMW_EKlaim aa
                LEFT JOIN VCLAIM_Sep bb ON aa.SepNo = bb.fs_no_sep
            WHERE 
                EKlaimId = @EKlaimId";
        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", key.EKlaimId, SqlDbType.VarChar);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        var dto = conn.QuerySingleOrDefault<EKlaimDto>(sql, dp);
        return MayBe
            .From(dto)
            .Map(x => x.ToModel());
    }

    public MayBe<IEnumerable<EKlaimModel>> ListData(Periode filter)
    {
        const string sql = @"
            SELECT 
                aa.EKlaimId, aa.EKlaimDate, aa.SepNo, aa.KartuBpjsNo, 
                aa.PasienId, aa.PasienName, aa.BirthDate, aa.Gender,
                ISNULL(bb.fs_kd_trs, '') AS SepId,
                ISNULL(bb.fs_no_sep, '') AS SepNo,
                ISNULL(bb.fd_tgl_trs, '3000-01-01') AS SepDateTime
            FROM 
                JKNMW_EKlaim aa
                LEFT JOIN VCLAIM_Sep bb ON aa.SepNo = bb.fs_no_sep
            WHERE 
                EKlaimDate BETWEEN @Tgl1 AND @Tgl2";
        var dp = new DynamicParameters();
        dp.AddParam("@Tgl1", filter.Tgl1, SqlDbType.DateTime); 
        dp.AddParam("@Tgl2", filter.Tgl2, SqlDbType.DateTime);

        using var conn = new SqlConnection(ConnStringHelper.Get(_opt));
        var dto = conn.Read<EKlaimDto>(sql, dp);
        return MayBe
            .From(dto)
            .Map(x => x
                .Select(y => y.ToModel()));
    }
}