using AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;
using AptOnline.Domain.AptolCloudContext.PpkAgg;
using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.SepAgg;
using AptOnline.Domain.Helpers;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using GuardNet;
using Nuna.Lib.PatternHelper;
using Ulid = AptOnline.Domain.Helpers.Ulid;

namespace AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;

public class ResepMidwareModel : IResepMidwareKey
{
    private const string BRIDGE_STATE_CREATED = "CREATED";
    
    public ResepMidwareModel(DateTime resepDate,int iterasi, SepType sep, PpkRefference ppk, PoliBpjsType poliBpjs)
    {
        Guard.NotNull(sep, nameof(sep));
        Guard.NotNull(ppk, nameof(ppk));
        Guard.NotNull(poliBpjs, nameof(poliBpjs));

        ResepMidwareId = Ulid.NewUlid();
        ResepMidwareDate = resepDate;
        CreateTimestamp = DateTime.Now;
        SyncTimestamp = AppConst.DEF_DATE;
        UploadTimestamp = AppConst.DEF_DATE;
        BridgeState = BRIDGE_STATE_CREATED;

        ChartId = string.Empty;
        ResepRsId = string.Empty;
        ReffId = string.Empty;
        JenisObatId = string.Empty;
        Iterasi = iterasi;
        
        Sep = sep;
        Ppk = ppk;
        PoliBpjs = poliBpjs;
        ListItem = new List<ResepMidwareItemModel>();
    }

    private ResepMidwareModel()
    {
    }

    public static ResepMidwareModel Load(
        string resepMidwareId, DateTime resepMidwareDate, 
        string chartId, string resepRsId, 
        string reffId, string jenisObatId, int iterasi,        
        string sepId, DateTime sepDate, string sepNo, string noPeserta, 
        string regId, DateTime regDate, string pasienId, string pasienName,
        string dokterId, string dokterName,
        string ppkId, string ppkName,
        string poliBpjsId, string poliBpjsName,  
        string bridgeState, DateTime createTimestamp, 
        DateTime syncTimestamp, DateTime uploadTimestamp)
    {
        var reg = new RegType(regId, regDate, pasienId, pasienName);
        var dokter = new DokterType(dokterId, dokterName);
        return new ResepMidwareModel
        {
            ResepMidwareId = resepMidwareId,
            ResepMidwareDate = resepMidwareDate,

            ChartId = chartId,
            ResepRsId = resepRsId,

            ReffId = reffId,
            JenisObatId = jenisObatId,
            Iterasi = iterasi,

            Sep = new SepType(sepId, sepDate, sepNo, noPeserta, reg, dokter, false, ""),
            Ppk = new PpkRefference(ppkId, ppkName),
            PoliBpjs = new PoliBpjsType(poliBpjsId, poliBpjsName),
            
            BridgeState = bridgeState,
            CreateTimestamp = createTimestamp,
            SyncTimestamp = syncTimestamp,
            UploadTimestamp = uploadTimestamp,
        };
    }

#region KEY 
    public string ResepMidwareId { get; init; }
    public DateTime ResepMidwareDate { get; private set; }
    #endregion
    
#region EMR-RELATED
    public string ChartId { get; private set; }
    public string ResepRsId { get; private set; }
    #endregion
    
#region APTOL-RELATED
    public string ReffId { get; private set; }
    public string JenisObatId { get; private set;}
    public int Iterasi { get; private set;}
    #endregion

#region SEP-RELATED
    public SepType Sep { get; private  set; }
    public PpkRefference Ppk { get; private set; }
    public PoliBpjsType PoliBpjs { get; private set; }
    #endregion

#region BRIDGING-STATE
    public string BridgeState { get; private set; }
    public DateTime CreateTimestamp { get; private set;}
    public DateTime SyncTimestamp { get; private set;}
    public DateTime UploadTimestamp { get; private set;}
    #endregion

    public List<ResepMidwareItemModel> ListItem { get; set;}

    
#region METHODS-HEADER-RELATED
    public void SetSep(SepType sep)
    {
        Guard.NotNull(sep, nameof(sep));
        Sep = sep;
    }
    public void SetPoliBpjs(LayananType layanan)
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
    public NunaResult<string> AddObat(MapDphoType mapBrgDpho, string signa, int qty)
    {
        var no = ListItem.Any() ? ListItem.Max(x => x.NoUrut) + 1 : 1;
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
    public NunaResult<string> AddRacik(MapDphoType mapBrgDpho, 
        string signa, int qty, string jenisRacik)
    {
        var no = ListItem.Any() ? ListItem.Max(x => x.NoUrut) + 1 : 1;
        var newItem = new ResepMidwareItemModel(no, mapBrgDpho, signa, qty);
        newItem.SetAsRacik(jenisRacik);
        ListItem.Add(newItem);
        
        return NunaResult<string>.Success("Success");
    }
    #endregion
}