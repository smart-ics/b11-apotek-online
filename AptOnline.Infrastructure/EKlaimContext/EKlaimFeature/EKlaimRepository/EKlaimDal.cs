using System.Data;
using System.Data.SqlClient;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.EKlaimRepository;

public interface IEKlaimDal :
    IInsert<EKlaimDto>,
    IUpdate<EKlaimDto>,
    IDelete<IEKlaimKey>,
    IGetDataMayBe<EKlaimDto, IEKlaimKey>,
    IListDataMayBe<EKlaimDto, Periode>
{
}

public class EKlaimDal : IEKlaimDal
{
    private readonly IDbConnection _conn;

    public EKlaimDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));    
    }
    
    public void Insert(EKlaimDto model)
    {
        const string sql = @"
            INSERT INTO JKNMW_EKlaim(
                EKlaimId, EKlaimDate, SepNo, KartuBpjsNo, RegId, RegDate, 
                PasienId, PasienName, BirthDate, Gender,
                DpjpId, DpjpName, CaraMasukId, CaraMasukName,
                KelasJknId, KelasJknName, KelasJknValue, KelasTarifRsId,
                TarifPoliEksekutif, UpgradeIndikator, AddPaymentProcentage,
                DischargeStatusId, DischargeStatusName, PayorId, PayorName,
                CoderPegId, CoderPegName, CoderNik, Los)
            VALUES(
                @EKlaimId, @EKlaimDate, @SepNo, @KartuBpjsNo, @RegId, @RegDate, 
                @PasienId, @PasienName, @BirthDate, @Gender,
                @DpjpId, @DpjpName, @CaraMasukId, @CaraMasukName,
                @KelasJknId, @KelasJknName, @KelasJknValue, @KelasTarifRsId,
                @TarifPoliEksekutif, @UpgradeIndikator, @AddPaymentProcentage,
                @DischargeStatusId, @DischargeStatusName, @PayorId, @PayorName,
                @CoderPegId, @CoderPegName, @CoderNik, @Los)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", model.EKlaimId, SqlDbType.VarChar);
        dp.AddParam("@EKlaimDate", model.EKlaimDate, SqlDbType.DateTime);
        dp.AddParam("@SepNo", model.SepNo, SqlDbType.VarChar);
        dp.AddParam("@KartuBpjsNo", model.KartuBpjsNo, SqlDbType.VarChar);
        dp.AddParam("@RegId", model.RegId, SqlDbType.VarChar);
        dp.AddParam("@RegDate", model.RegDate, SqlDbType.DateTime);
        dp.AddParam("@PasienId", model.PasienId, SqlDbType.VarChar);
        dp.AddParam("@PasienName", model.PasienName, SqlDbType.VarChar);
        dp.AddParam("@BirthDate", model.BirthDate, SqlDbType.DateTime);
        dp.AddParam("@Gender", model.Gender, SqlDbType.VarChar);
        dp.AddParam("@DpjpId", model.DpjpId, SqlDbType.VarChar);
        dp.AddParam("@DpjpName", model.DpjpName, SqlDbType.VarChar);
        dp.AddParam("@CaraMasukId", model.CaraMasukId, SqlDbType.VarChar);
        dp.AddParam("@CaraMasukName", model.CaraMasukName, SqlDbType.VarChar);
        dp.AddParam("@KelasJknId", model.KelasJknId, SqlDbType.VarChar);
        dp.AddParam("@KelasJknName", model.KelasJknName, SqlDbType.VarChar);
        dp.AddParam("@KelasJknValue", model.KelasJknValue, SqlDbType.VarChar);
        dp.AddParam("@KelasTarifRsId", model.KelasTarifRsId, SqlDbType.VarChar);
        dp.AddParam("@TarifPoliEksekutif", model.TarifPoliEksekutif, SqlDbType.VarChar);
        dp.AddParam("@UpgradeIndikator", model.UpgradeIndikator, SqlDbType.VarChar);
        dp.AddParam("@AddPaymentProcentage", model.AddPaymentProcentage, SqlDbType.VarChar);
        dp.AddParam("@DischargeStatusId", model.DischargeStatusId, SqlDbType.VarChar);
        dp.AddParam("@DischargeStatusName", model.DischargeStatusName, SqlDbType.VarChar);
        dp.AddParam("@PayorId", model.PayorId, SqlDbType.VarChar);
        dp.AddParam("@PayorName", model.PayorName, SqlDbType.VarChar);
        dp.AddParam("@CoderPegId", model.CoderPegId, SqlDbType.VarChar);
        dp.AddParam("@CoderPegName", model.CoderPegName, SqlDbType.VarChar);
        dp.AddParam("@CoderNik", model.CoderNik, SqlDbType.VarChar);
        dp.AddParam("@Los", model.Los, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp);
    }

    public void Update(EKlaimDto model)
    {
        const string sql = @"
            UPDATE 
                JKNMW_EKlaim
            SET            
                EKlaimId = @EKlaimId,  
                EKlaimDate = @EKlaimDate,  
                SepNo = @SepNo,  
                KartuBpjsNo = @KartuBpjsNo,  
                RegId = @RegId,  
                RegDate = @RegDate,  
                PasienId = @PasienId,  
                PasienName = @PasienName,  
                BirthDate = @BirthDate,  
                Gender = @Gender, 
                DpjpId = @DpjpId,  
                DpjpName = @DpjpName,  
                CaraMasukId = @CaraMasukId,  
                CaraMasukName = @CaraMasukName, 
                KelasJknId = @KelasJknId,  
                KelasJknName = @KelasJknName,  
                KelasJknValue = @KelasJknValue,  
                KelasTarifRsId = @KelasTarifRsId, 
                TarifPoliEksekutif = @TarifPoliEksekutif,  
                UpgradeIndikator = @UpgradeIndikator,  
                AddPaymentProcentage = @AddPaymentProcentage, 
                DischargeStatusId = @DischargeStatusId,  
                DischargeStatusName = @DischargeStatusName,  
                PayorId = @PayorId,  
                PayorName = @PayorName, 
                CoderPegId = @CoderPegId,  
                CoderPegName = @CoderPegName,  
                CoderNik = @CoderNik,  
                Los = @Los
            WHERE
                EKlaimId = @EKlaimId ";
        
        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", model.EKlaimId, SqlDbType.VarChar);
        dp.AddParam("@EKlaimDate", model.EKlaimDate, SqlDbType.DateTime);
        dp.AddParam("@SepNo", model.SepNo, SqlDbType.VarChar);
        dp.AddParam("@KartuBpjsNo", model.KartuBpjsNo, SqlDbType.VarChar);
        dp.AddParam("@RegId", model.RegId, SqlDbType.VarChar);
        dp.AddParam("@RegDate", model.RegDate, SqlDbType.DateTime);
        dp.AddParam("@PasienId", model.PasienId, SqlDbType.VarChar);
        dp.AddParam("@PasienName", model.PasienName, SqlDbType.VarChar);
        dp.AddParam("@BirthDate", model.BirthDate, SqlDbType.DateTime);
        dp.AddParam("@Gender", model.Gender, SqlDbType.VarChar);
        dp.AddParam("@DpjpId", model.DpjpId, SqlDbType.VarChar);
        dp.AddParam("@DpjpName", model.DpjpName, SqlDbType.VarChar);
        dp.AddParam("@CaraMasukId", model.CaraMasukId, SqlDbType.VarChar);
        dp.AddParam("@CaraMasukName", model.CaraMasukName, SqlDbType.VarChar);
        dp.AddParam("@KelasJknId", model.KelasJknId, SqlDbType.VarChar);
        dp.AddParam("@KelasJknName", model.KelasJknName, SqlDbType.VarChar);
        dp.AddParam("@KelasJknValue", model.KelasJknValue, SqlDbType.VarChar);
        dp.AddParam("@KelasTarifRsId", model.KelasTarifRsId, SqlDbType.VarChar);
        dp.AddParam("@TarifPoliEksekutif", model.TarifPoliEksekutif, SqlDbType.VarChar);
        dp.AddParam("@UpgradeIndikator", model.UpgradeIndikator, SqlDbType.VarChar);
        dp.AddParam("@AddPaymentProcentage", model.AddPaymentProcentage, SqlDbType.VarChar);
        dp.AddParam("@DischargeStatusId", model.DischargeStatusId, SqlDbType.VarChar);
        dp.AddParam("@DischargeStatusName", model.DischargeStatusName, SqlDbType.VarChar);
        dp.AddParam("@PayorId", model.PayorId, SqlDbType.VarChar);
        dp.AddParam("@PayorName", model.PayorName, SqlDbType.VarChar);
        dp.AddParam("@CoderPegId", model.CoderPegId, SqlDbType.VarChar);
        dp.AddParam("@CoderPegName", model.CoderPegName, SqlDbType.VarChar);
        dp.AddParam("@CoderNik", model.CoderNik, SqlDbType.VarChar);
        dp.AddParam("@Los", model.Los, SqlDbType.VarChar);

        _conn.Execute(sql, dp);
    }

    public void Delete(IEKlaimKey key)
    {
        const string sql = @"
            DELETE FROM 
                JKNMW_EKlaim
            WHERE
                EKlaimId = @EKlaimId ";
        
        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", key.EKlaimId, SqlDbType.VarChar);

        _conn.Execute(sql, dp);
    }

    public MayBe<EKlaimDto> GetData(IEKlaimKey key)
    {
        const string sql = @"
            SELECT
                EKlaimId, EKlaimDate, SepNo, KartuBpjsNo, RegId, RegDate, 
                PasienId, PasienName, BirthDate, Gender,
                DpjpId, DpjpName, CaraMasukId, CaraMasukName,
                KelasJknId, KelasJknName, KelasJknValue, KelasTarifRsId,
                TarifPoliEksekutif, UpgradeIndikator, AddPaymentProcentage,
                DischargeStatusId, DischargeStatusName, PayorId, PayorName,
                CoderPegId, CoderPegName, CoderNik, Los
            FROM
                JKNMW_EKlaim aa
            WHERE
                EKlaimId = @EKlaimId ";
        
        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", key.EKlaimId, SqlDbType.VarChar);

        var result = _conn.ReadSingle<EKlaimDto>(sql, dp);
        return MayBe.From(result);
    }

    public MayBe<IEnumerable<EKlaimDto>> ListData(Periode filter)
    {
        const string sql = @"
            SELECT
                EKlaimId, EKlaimDate, SepNo, KartuBpjsNo, RegId, RegDate, 
                PasienId, PasienName, BirthDate, Gender,
                DpjpId, DpjpName, CaraMasukId, CaraMasukName,
                KelasJknId, KelasJknName, KelasJknValue, KelasTarifRsId,
                TarifPoliEksekutif, UpgradeIndikator, AddPaymentProcentage,
                DischargeStatusId, DischargeStatusName, PayorId, PayorName,
                CoderPegId, CoderPegName, CoderNik, Los
            FROM
                JKNMW_EKlaim aa
            WHERE
                EKlaimDate BETWEEN @Tgl1 AND @Tgl2";
        
        var dp = new DynamicParameters();
        dp.AddParam("@Tgl1", filter.Tgl1, SqlDbType.DateTime); 
        dp.AddParam("@Tgl2", filter.Tgl2, SqlDbType.DateTime);

        var result = _conn.Read<EKlaimDto>(sql, dp);
        return MayBe.From(result);
    }
}