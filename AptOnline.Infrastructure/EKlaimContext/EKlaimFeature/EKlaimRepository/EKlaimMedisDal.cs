using System.Data;
using System.Data.SqlClient;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.EKlaimRepository;

public interface IEKlaimMedisDal:
    IInsert<EKlaimMedisDto>,
    IUpdate<EKlaimMedisDto>,
    IDelete<IEKlaimKey>,
    IGetDataMayBe<EKlaimMedisDto, IEKlaimKey>
{}

public class EKlaimMedisDal : IEKlaimMedisDal
{
    private readonly IDbConnection _conn;

    public EKlaimMedisDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }

    public void Insert(EKlaimMedisDto model)
    {
        const string sql = @"
            INSERT INTO JKNMW_EKlaimMedis(
                EKlaimId,   AdlSubAcuteScore, AdlChronicScore, IcuFlag, IcuLos, IcuDescription,  
                Covid19StatusId, Covid19StatusName, Covid19TipeNoKartuId, Covid19TipeNoKartuName,  
                IsPemulasaranJenazah, IsKantongJenazah, IsPetiJenazah, IsPlastikErat, IsDesinfeksiJenazah, 
                IsMobilJenazah, IsDesinfektanMobilJenazah, IsIsoman, Episodes, AksesNaat,  
                DializerUsageId, DializerUsageName, JumKantongDarah, AlteplaseIndikator,  
                Sistole, Diastole, BodyWeight, TbIndikatorId, TbIndikatorName)  
            VALUES(
                @EKlaimId, @AdlSubAcuteScore, @AdlChronicScore, @IcuFlag, @IcuLos, @IcuDescription,  
                @Covid19StatusId, @Covid19StatusName, @Covid19TipeNoKartuId, @Covid19TipeNoKartuName,  
                @IsPemulasaranJenazah, @IsKantongJenazah, @IsPetiJenazah, @IsPlastikErat, @IsDesinfeksiJenazah, 
                @IsMobilJenazah, @IsDesinfektanMobilJenazah, @IsIsoman, @Episodes, @AksesNaat,  
                @DializerUsageId, @DializerUsageName, @JumKantongDarah, @AlteplaseIndikator,  
                @Sistole, @Diastole, @BodyWeight, @TbIndikatorId, @TbIndikatorName)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", model.EKlaimId, SqlDbType.VarChar);
        dp.AddParam("@AdlSubAcuteScore", model.AdlSubAcuteScore, SqlDbType.VarChar);
        dp.AddParam("@AdlChronicScore", model.AdlChronicScore, SqlDbType.VarChar);
        dp.AddParam("@IcuFlag", model.IcuFlag, SqlDbType.VarChar);
        dp.AddParam("@IcuLos", model.IcuLos, SqlDbType.VarChar);
        dp.AddParam("@IcuDescription", model.IcuDescription, SqlDbType.VarChar);
        dp.AddParam("@Covid19StatusId", model.Covid19StatusId, SqlDbType.VarChar);
        dp.AddParam("@Covid19StatusName", model.Covid19StatusName, SqlDbType.VarChar);
        dp.AddParam("@Covid19TipeNoKartuId", model.Covid19TipeNoKartuId, SqlDbType.VarChar);
        dp.AddParam("@Covid19TipeNoKartuName", model.Covid19TipeNoKartuName, SqlDbType.VarChar);
        dp.AddParam("@IsPemulasaranJenazah", model.IsPemulasaranJenazah, SqlDbType.VarChar);        
        dp.AddParam("@IsKantongJenazah", model.IsKantongJenazah, SqlDbType.VarChar);
        dp.AddParam("@IsPetiJenazah", model.IsPetiJenazah, SqlDbType.VarChar);
        dp.AddParam("@IsPlastikErat", model.IsPlastikErat, SqlDbType.VarChar);
        dp.AddParam("@IsDesinfeksiJenazah", model.IsDesinfeksiJenazah, SqlDbType.VarChar);
        dp.AddParam("@IsMobilJenazah", model.IsMobilJenazah, SqlDbType.VarChar);
        dp.AddParam("@IsDesinfektanMobilJenazah", model.IsDesinfektanMobilJenazah, SqlDbType.VarChar);
        dp.AddParam("@IsIsoman", model.IsIsoman, SqlDbType.VarChar);
        dp.AddParam("@Episodes", model.Episodes, SqlDbType.VarChar);
        dp.AddParam("@AksesNaat", model.AksesNaat, SqlDbType.VarChar);
        dp.AddParam("@DializerUsageId", model.DializerUsageId, SqlDbType.VarChar);
        dp.AddParam("@DializerUsageName", model.DializerUsageName, SqlDbType.VarChar);
        dp.AddParam("@JumKantongDarah", model.JumKantongDarah, SqlDbType.VarChar);
        dp.AddParam("@AlteplaseIndikator", model.AlteplaseIndikator, SqlDbType.VarChar);
        dp.AddParam("@Sistole", model.Sistole, SqlDbType.VarChar);
        dp.AddParam("@Diastole", model.Diastole, SqlDbType.VarChar);
        dp.AddParam("@BodyWeight", model.BodyWeight, SqlDbType.VarChar);
        dp.AddParam("@TbIndikatorId", model.TbIndikatorId, SqlDbType.VarChar);
        dp.AddParam("@TbIndikatorName", model.TbIndikatorName, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp); 
    }

    public void Update(EKlaimMedisDto model)
    {
        const string sql = @"
            UPDATE JKNMW_EKlaimMedis
            SET AdlSubAcuteScore = @AdlSubAcuteScore,
                AdlChronicScore = @AdlChronicScore,
                IcuFlag = @IcuFlag,
                IcuLos = @IcuLos,
                IcuDescription = @IcuDescription,
                Covid19StatusId = @Covid19StatusId,
                Covid19StatusName = @Covid19StatusName,
                Covid19TipeNoKartuId = @Covid19TipeNoKartuId,
                Covid19TipeNoKartuName = @Covid19TipeNoKartuName,
                IsPemulasaranJenazah = @IsPemulasaranJenazah,
                IsKantongJenazah = @IsKantongJenazah,
                IsPetiJenazah = @IsPetiJenazah,
                IsPlastikErat = @IsPlastikErat,
                IsDesinfeksiJenazah = @IsDesinfeksiJenazah,
                IsMobilJenazah = @IsMobilJenazah,
                IsDesinfektanMobilJenazah = @IsDesinfektanMobilJenazah,
                IsIsoman = @IsIsoman,
                Episodes = @Episodes,
                AksesNaat = @AksesNaat,
                DializerUsageId = @DializerUsageId,
                DializerUsageName = @DializerUsageName,
                JumKantongDarah = @JumKantongDarah,
                AlteplaseIndikator = @AlteplaseIndikator,       
                Sistole = @Sistole,
                Diastole = @Diastole,
                BodyWeight = @BodyWeight,
                TbIndikatorId = @TbIndikatorId,
                TbIndikatorName = @TbIndikatorName
            WHERE EKlaimId = @EKlaimId ";
        
        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", model.EKlaimId, SqlDbType.VarChar);
        dp.AddParam("@AdlSubAcuteScore", model.AdlSubAcuteScore, SqlDbType.VarChar);
        dp.AddParam("@AdlChronicScore", model.AdlChronicScore, SqlDbType.VarChar);
        dp.AddParam("@IcuFlag", model.IcuFlag, SqlDbType.VarChar);
        dp.AddParam("@IcuLos", model.IcuLos, SqlDbType.VarChar);
        dp.AddParam("@IcuDescription", model.IcuDescription, SqlDbType.VarChar);
        dp.AddParam("@Covid19StatusId", model.Covid19StatusId, SqlDbType.VarChar);
        dp.AddParam("@Covid19StatusName", model.Covid19StatusName, SqlDbType.VarChar);
        dp.AddParam("@Covid19TipeNoKartuId", model.Covid19TipeNoKartuId, SqlDbType.VarChar);
        dp.AddParam("@Covid19TipeNoKartuName", model.Covid19TipeNoKartuName, SqlDbType.VarChar);
        dp.AddParam("@IsPemulasaranJenazah", model.IsPemulasaranJenazah, SqlDbType.VarChar);
        dp.AddParam("@IsKantongJenazah", model.IsKantongJenazah, SqlDbType.VarChar);
        dp.AddParam("@IsPetiJenazah", model.IsPetiJenazah, SqlDbType.VarChar);
        dp.AddParam("@IsPlastikErat", model.IsPlastikErat, SqlDbType.VarChar);
        dp.AddParam("@IsDesinfeksiJenazah", model.IsDesinfeksiJenazah, SqlDbType.VarChar);
        dp.AddParam("@IsMobilJenazah", model.IsMobilJenazah, SqlDbType.VarChar);
        dp.AddParam("@IsDesinfektanMobilJenazah", model.IsDesinfektanMobilJenazah, SqlDbType.VarChar);
        dp.AddParam("@IsIsoman", model.IsIsoman, SqlDbType.VarChar);
        dp.AddParam("@Episodes", model.Episodes, SqlDbType.VarChar);
        dp.AddParam("@AksesNaat", model.AksesNaat, SqlDbType.VarChar);
        dp.AddParam("@DializerUsageId", model.DializerUsageId, SqlDbType.VarChar);
        dp.AddParam("@DializerUsageName", model.DializerUsageName, SqlDbType.VarChar);
        dp.AddParam("@JumKantongDarah", model.JumKantongDarah, SqlDbType.VarChar);
        dp.AddParam("@AlteplaseIndikator", model.AlteplaseIndikator, SqlDbType.VarChar);
        dp.AddParam("@Sistole", model.Sistole, SqlDbType.VarChar);
        dp.AddParam("@Diastole", model.Diastole, SqlDbType.VarChar);
        dp.AddParam("@BodyWeight", model.BodyWeight, SqlDbType.VarChar);
        dp.AddParam("@TbIndikatorId", model.TbIndikatorId, SqlDbType.VarChar);
        dp.AddParam("@TbIndikatorName", model.TbIndikatorName, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp); 
    }   

    public void Delete(IEKlaimKey key)
    {
        const string sql = @"
            DELETE FROM JKNMW_EKlaimMedis 
            WHERE EKlaimId = @EKlaimId";
        _conn.Execute(sql, key);       
    }

    public MayBe<EKlaimMedisDto> GetData(IEKlaimKey key)
    {
        const string sq = @"
            SELECT 
                EKlaimId, AdlSubAcuteScore, AdlChronicScore, IcuFlag, IcuLos, IcuDescription, 
                Covid19StatusId, Covid19StatusName, Covid19TipeNoKartuId, Covid19TipeNoKartuName, 
                IsPemulasaranJenazah, IsKantongJenazah, IsPetiJenazah, IsPlastikErat, IsDesinfeksiJenazah, 
                IsMobilJenazah, IsDesinfektanMobilJenazah, IsIsoman, Episodes, AksesNaat, 
                DializerUsageId, DializerUsageName, JumKantongDarah, AlteplaseIndikator, 
                Sistole, Diastole, BodyWeight, TbIndikatorId, TbIndikatorName
            FROM 
                JKNMW_EKlaimMedis
            WHERE 
                EKlaimId = @EKlaimId";
        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", key.EKlaimId, SqlDbType.VarChar);
        var result = _conn.ReadSingle<EKlaimMedisDto>(sq, dp);
        return MayBe.From(result);  
    }
}