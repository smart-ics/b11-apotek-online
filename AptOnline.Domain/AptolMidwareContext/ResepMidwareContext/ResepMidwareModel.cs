using AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;
using AptOnline.Domain.AptolCloudContext.PpkAgg;
using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.SepAgg;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using GuardNet;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;

public class ResepMidwareModel : IResepMidwareKey
{
    #region KEY-METADATA 
    public string ResepMidwareId { get; set; }
    public DateTime ResepMidwareDate { get; set; }
    public string BridgeState { get; set; }
    public DateTime CreateTimestamp { get; set;}
    public DateTime SyncTimestamp { get; set;}
    public DateTime UploadTimestamp { get; set;}
    #endregion
    
    #region BILLING-EMR-RELATED
    public string ChartId { get; set; }
    public string ResepRsId { get; set; }
    public SepType Sep { get; private set; }
    public PpkRefference Ppk { get; private set; }
    public PoliBpjsType PoliBpjs { get; private set; }
    #endregion

    #region APTOL-RELATED
    public string ReffId { get; set; }
    public string JenisObatId { get; set;}
    public int Iterasi { get; set;}
    public List<ResepMidwareItemModel> ListItem { get; set;}
    #endregion
    
    #region METHODS-HEADER-RELATED
    public void SetSep(SepType sep)
    {
        Guard.NotNull(sep, nameof(sep));
        Sep = sep;
    }
    public void SetPoliBpjs(LayananModel layanan)
    {
        Guard.NotNull(layanan, nameof(layanan));
        PoliBpjs = layanan.PoliBpjs;
    }

    public void SetPpk(PpkRefference ppk)
    {
        Guard.NotNull(ppk, nameof(ppk));
        Ppk = ppk;
    }
    #endregion
    
    #region METHODS-ITEM-RELATED

    public NunaResult<string> AddObat(MapDphoModel mapBrgDpho, string signa, int qty)
    {
        var no = ListItem.Max(x => x.NoUrut+1);
        var newItem = new ResepMidwareItemModel(no, mapBrgDpho, signa, qty);
        ListItem.Add(newItem);
        
        return NunaResult<string>.Success("Success");
    }
    private static ResepMidwareItemModel CreateResep(
        Func<ResepMidwareItemModel> primaryStrategy, 
        Func<ResepMidwareItemModel> fallbackStrategy)
    {
        var result = primaryStrategy() 
            ?? fallbackStrategy(); 
        return result;
    }    
    public NunaResult<string> AddRacik(MapDphoModel mapBrgDpho, 
        string signa, int qty, string jenisRacik)
    {
        var no = ListItem.Max(x => x.NoUrut+1);
        var newItem = new ResepMidwareItemModel(no, mapBrgDpho, signa, qty);
        newItem.SetAsRacik(jenisRacik);
        ListItem.Add(newItem);
        
        return NunaResult<string>.Success("Success");
    }
    #endregion
}