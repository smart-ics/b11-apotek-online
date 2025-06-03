using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;

namespace AptOnline.Infrastructure.AptolMidwareContext.ResepMidwareAgg;

public class ResepMidwareDto
{
    // Properties
    public string ResepMidwareId { get; set; }
    public DateTime ResepMidwareDate { get; set; }
    public string ChartId { get; set; }
    public string ResepRsId { get; set; }
    public string ReffId { get; set; }
    public string JenisObatId { get; set; }
    public int Iterasi { get; set; }

    public string SepId { get; set; }
    public DateTime SepDate { get; set; }
    public string SepNo { get; set; }
    public string NoPeserta { get; set; }
    public string RegId { get; set; }
    public DateTime RegDate { get; set; }
    public string PasienId { get; set; }
    public string PasienName { get; set; }
    public string DokterId { get; set; }
    public string DokterName { get; set; }

    public string PpkId { get; set; }
    public string PpkName { get; set; }
    public string PoliBpjsId { get; set; }
    public string PoliBpjsName { get; set; }

    public string BridgeState { get; set; }
    public DateTime CreateTimestamp { get; set; }
    public DateTime ConfirmTimeStamp { get; set; }
    public DateTime SyncTimestamp { get; set; }
    public DateTime UploadTimestamp { get; set; }

    public ResepMidwareModel ToModel()
    {
        var result = ResepMidwareModel.Load(ResepMidwareId, ResepMidwareDate,
            ChartId, ResepRsId, ReffId, JenisObatId, Iterasi, 
            SepId, SepDate, SepNo, NoPeserta, 
            RegId, RegDate, PasienId, PasienName, 
            DokterId, DokterName, 
            PpkId, PpkName, PoliBpjsId, PoliBpjsName, BridgeState, 
            CreateTimestamp, ConfirmTimeStamp, SyncTimestamp, UploadTimestamp);
        return result;
    }
}