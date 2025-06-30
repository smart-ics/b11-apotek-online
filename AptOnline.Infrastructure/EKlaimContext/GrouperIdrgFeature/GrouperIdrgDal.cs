using System.Data;
using System.Data.SqlClient;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;
using AptOnline.Infrastructure.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.PatternHelper;
using Nuna.Lib.ValidationHelper;

namespace AptOnline.Infrastructure.EKlaimContext.GrouperIdrgFeature;

public interface IGrouperIdrgDal :
    IInsert<GrouperIdrgDto>,
    IUpdate<GrouperIdrgDto>,
    IDelete<IEKlaimKey>,
    IGetDataMayBe<GrouperIdrgDto, IEKlaimKey>,
    IListDataMayBe<GrouperIdrgDto, Periode>
{
}
public class GrouperIdrgDal : IGrouperIdrgDal
{
    private readonly IDbConnection _conn;

    public GrouperIdrgDal(IOptions<DatabaseOptions> opt)
    {
        _conn = new SqlConnection(ConnStringHelper.Get(opt.Value));
    }
    public void Insert(GrouperIdrgDto dto)
    {
        const string sql = @"
            INSERT INTO JKNMW_GrouperIdrg(
                EKlaimId, GrouperIdrgDate, SepNo, InfoResult, JenisRawat,
                MdcId, MdcName, DrgId, DrgName, StatusResult,
                Phase, FinalTimestamp)
            VALUES(
                @EKlaimId, @GrouperIdrgDate, @SepNo, @InfoResult, @JenisRawat,
                @MdcId, @MdcName, @DrgId, @DrgName, @StatusResult,
                @Phase, @FinalTimestamp)";
        
        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", dto.EKlaimId, SqlDbType.VarChar);
        dp.AddParam("@GrouperIdrgDate", dto.GrouperIdrgDate, SqlDbType.DateTime);
        dp.AddParam("@SepNo", dto.SepNo, SqlDbType.VarChar);
        dp.AddParam("@InfoResult", dto.InfoResult, SqlDbType.VarChar);
        dp.AddParam("@JenisRawat", dto.JenisRawat, SqlDbType.VarChar);
        dp.AddParam("@MdcId", dto.MdcId, SqlDbType.VarChar);
        dp.AddParam("@MdcName", dto.MdcName, SqlDbType.VarChar);
        dp.AddParam("@DrgId", dto.DrgId, SqlDbType.VarChar);
        dp.AddParam("@DrgName", dto.DrgName, SqlDbType.VarChar);
        dp.AddParam("@StatusResult", dto.StatusResult, SqlDbType.VarChar);
        dp.AddParam("@Phase", dto.Phase, SqlDbType.Int);
        dp.AddParam("@FinalTimestamp", dto.FinalTimestamp, SqlDbType.DateTime);
        
        _conn.Execute(sql, dp);
    }

    public void Update(GrouperIdrgDto dto)
    {
        const string sql = @"
            UPDATE 
                JKNMW_GrouperIdrg
            SET
                GrouperIdrgDate = @GrouperIdrgDate,
                SepNo = @SepNo, 
                InfoResult = @InfoResult, 
                JenisRawat = @JenisRawat,
                MdcId = @MdcId, 
                MdcName = @MdcName, 
                DrgId = @DrgId, 
                DrgName = @DrgName, 
                StatusResult = @StatusResult,
                Phase = @Phase, 
                FinalTimestamp = @FinalTimestamp
            WHERE
                EKlaimId = @EKlaimId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", dto.EKlaimId, SqlDbType.VarChar);
        dp.AddParam("@GrouperIdrgDate", dto.GrouperIdrgDate, SqlDbType.DateTime);
        dp.AddParam("@SepNo", dto.SepNo, SqlDbType.VarChar);
        dp.AddParam("@InfoResult", dto.InfoResult, SqlDbType.VarChar);
        dp.AddParam("@JenisRawat", dto.JenisRawat, SqlDbType.VarChar);
        dp.AddParam("@MdcId", dto.MdcId, SqlDbType.VarChar);
        dp.AddParam("@MdcName", dto.MdcName, SqlDbType.VarChar);
        dp.AddParam("@DrgId", dto.DrgId, SqlDbType.VarChar);
        dp.AddParam("@DrgName", dto.DrgName, SqlDbType.VarChar);
        dp.AddParam("@StatusResult", dto.StatusResult, SqlDbType.VarChar);
        dp.AddParam("@Phase", dto.Phase, SqlDbType.Int);
        dp.AddParam("@FinalTimestamp", dto.FinalTimestamp, SqlDbType.DateTime);
        
        _conn.Execute(sql, dp);
    }

    public void Delete(IEKlaimKey key)
    {
        const string sql = @"
            DELETE FROM 
                JKNMW_GrouperIdrg
            WHERE
                EKlaimId = @EKlaimId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", key.EKlaimId, SqlDbType.VarChar);
        
        _conn.Execute(sql, dp);
    }

    public MayBe<GrouperIdrgDto> GetData(IEKlaimKey key)
    {
        const string sql = @"
            SELECT
                aa.EKlaimId, aa.GrouperIdrgDate, aa.SepNo, aa.InfoResult, aa.JenisRawat,
                aa.MdcId, aa.MdcName, aa.DrgId, aa.DrgName, aa.StatusResult,
                aa.Phase, aa.FinalTimestamp,
                ISNULL(bb.fs_kd_trs, '-') AS SepId,
                ISNULL(bb.fd_tgl_trs, '3000-01-01') AS SepDate
            FROM
                JKNMW_GrouperIdrg aa
                LEFT JOIN VCLAIM_Sep bb ON aa.SepNo = bb.fs_no_peserta
            WHERE
                aa.EKlaimId = @EKlaimId";
        
        var dp = new DynamicParameters();
        dp.AddParam("@EKlaimId", key.EKlaimId, SqlDbType.VarChar);
        
        return MayBe.From(_conn.ReadSingle<GrouperIdrgDto>(sql, dp));
    }

    public MayBe<IEnumerable<GrouperIdrgDto>> ListData(Periode filter)
    {
        const string sql = @"
            SELECT
                aa.EKlaimId, aa.GrouperIdrgDate, aa.SepNo, aa.InfoResult, aa.JenisRawat,
                aa.MdcId, aa.MdcName, aa.DrgId, aa.DrgName, aa.StatusResult,
                aa.Phase, aa.FinalTimestamp,
                ISNULL(bb.fs_kd_trs, '-') AS SepId,
                ISNULL(bb.fd_tgl_trs, '3000-01-01') AS SepDate
            FROM
                JKNMW_GrouperIdrg aa
                LEFT JOIN VCLAIM_Sep bb ON aa.SepNo = bb.fs_no_peserta
            WHERE
                aa.GrouperIdrgDate BETWEEN @Tgl1 AND @Tgl2";
        
        var dp = new DynamicParameters();
        dp.AddParam("@Tgl1", filter.Tgl1, SqlDbType.DateTime); 
        dp.AddParam("@Tgl2", filter.Tgl2, SqlDbType.DateTime);

        return MayBe.From(_conn.Read<GrouperIdrgDto>(sql, dp));
    }
}