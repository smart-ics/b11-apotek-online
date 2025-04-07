using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;

namespace AptOnline.Infrastructure.AptolMidwareContext.ResepMidwareAgg;

public record ResepMidwareDto(
    string ResepMidwareId,
    DateTime ResepMidwareDate,
    string ChartId,
    string ResepRsId,
    string ReffId,
    string JenisObatId,
    int Iterasi,
    
    string SepId,
    DateTime SepDate,
    string SepNo,
    string NoPeserta,
    string RegId,
    DateTime RegDate,
    string PasienId,
    string PasienName,
    string DokterId,
    string DokterName,

    string PpkId,
    string PpkName,
    string PoliBpjsId,
    string PoliBpjsName,
    
    string BridgeState,
    DateTime CreateTimestamp,
    DateTime SyncTimestamp,
    DateTime UploadTimestamp)
{
    public ResepMidwareModel ToModel()
    {
        var result = ResepMidwareModel.Load(ResepMidwareId, ResepMidwareDate,
            ChartId, ResepRsId, ReffId, JenisObatId, Iterasi, 
            SepId, SepDate, SepNo, NoPeserta, 
            RegId, RegDate, PasienId, PasienName, 
            DokterId, DokterName, 
            PpkId, PpkName, PoliBpjsId, PoliBpjsName,
            BridgeState, CreateTimestamp, SyncTimestamp, UploadTimestamp);
        return result;
    }
}