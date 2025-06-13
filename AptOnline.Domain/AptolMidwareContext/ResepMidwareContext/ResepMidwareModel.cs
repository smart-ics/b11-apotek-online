using AptOnline.Domain.AptolCloudContext.PoliBpjsAgg;
using AptOnline.Domain.AptolCloudContext.PpkAgg;
using AptOnline.Domain.BillingContext.DokterAgg;
using AptOnline.Domain.BillingContext.PasienFeature;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.Helpers;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using AptOnline.Domain.SepContext.PesertaBpjsFeature;
using AptOnline.Domain.SepContext.SepFeature;
using Nuna.Lib.PatternHelper;
using Ardalis.GuardClauses;
namespace AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;

public class ResepMidwareModel : IResepMidwareKey
{
    public const string BRIDGE_STATE_CREATED = "CREATED";
    public const string BRIDGE_STATE_CONFIRMED = "CONFIRMED";
    public const string BRIDGE_STATE_SYNCED = "SYNCED";
    public const string BRIDGE_STATE_UPLOADED = "UPLOADED";
    
    public ResepMidwareModel(DateTime resepDate,int iterasi, SepType sep, PpkRefference ppk, PoliBpjsType poliBpjs)
    {
        Guard.Against.Null(sep, nameof(sep));
        Guard.Against.Null(ppk, nameof(ppk));
        Guard.Against.Null(poliBpjs, nameof(poliBpjs));

        ResepMidwareId = Ulid.NewUlid().ToString();
        ResepMidwareDate = resepDate;

        ChartId = string.Empty;
        ResepRsId = string.Empty;
        ReffId = string.Empty;
        ResepBpjsNo = string.Empty;
        JenisObatId = poliBpjs.PoliBpjsId == "KEM" ? "3" : "2";
        // : sep.IsPrb ? "1" [prb sementara disabled (dari bpjs kc jogja)

        Iterasi = iterasi;
        Sep = sep;
        Ppk = ppk;
        PoliBpjs = poliBpjs;

        BridgeState = BRIDGE_STATE_CREATED;
        CreateTimestamp = DateTime.Now;
        ConfirmTimeStamp = AppConst.DEF_DATE;
        SyncTimestamp = AppConst.DEF_DATE;
        UploadTimestamp = AppConst.DEF_DATE;
        
        ListItem = new List<ResepMidwareItemModel>();
    }

    private ResepMidwareModel()
    {
    }

    public static ResepMidwareModel Load(
        string resepMidwareId, DateTime resepMidwareDate, 
        string chartId, string resepRsId, string reffId,
        string resepBpjsNo, string jenisObatId, int iterasi,         
        string sepId, DateTime sepDate, string sepNo, string noPeserta,
        string regId, DateTime regDate, string pasienId, string pasienName,
        string dokterId, string dokterName,
        string ppkId, string ppkName,
        string poliBpjsId, string poliBpjsName,  
        string bridgeState, DateTime createTimestamp, DateTime confirmTimestamp, 
        DateTime syncTimestamp, DateTime uploadTimestamp)
    {
        var pasien = PasienType.Load(pasienId, pasienName, new DateTime(3000, 1, 1), GenderType.Default);
        var reg = RegType.Load(regId, regDate, new DateTime(3000,1,1), pasien, JenisRegEnum.Unknown, KelasRawatType.Default);
        var dokter = new DokterType(dokterId, dokterName);
        var pesertaBpjs = new PesertaBpjsRefference(noPeserta, pasienName);
        return new ResepMidwareModel
        {
            ResepMidwareId = resepMidwareId,
            ResepMidwareDate = resepMidwareDate,

            ChartId = chartId,
            ResepRsId = resepRsId,
            Registrasi = reg,
            ReffId = reffId,
            ResepBpjsNo = resepBpjsNo,
            JenisObatId = jenisObatId,
            Iterasi = iterasi,

            Sep = new SepType(sepId, sepDate, sepNo, pesertaBpjs, reg, dokter, false, "", ""),
            Ppk = new PpkRefference(ppkId, ppkName),
            PoliBpjs = new PoliBpjsType(poliBpjsId, poliBpjsName),
            BridgeState = bridgeState,
            CreateTimestamp = createTimestamp,
            ConfirmTimeStamp = confirmTimestamp,
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
    public RegType Registrasi { get; private set; }
    #endregion
    
#region APTOL-RELATED
    public string ReffId { get; private set; }
    public string ResepBpjsNo { get; set; }
    public string JenisObatId { get; private set;}
    public int Iterasi { get; private set;}
    #endregion

    #region SEP-RELATED
    public SepType Sep { get; private  set; }
    public PpkRefference Ppk { get; private set; }
    public PoliBpjsType PoliBpjs { get; private set; }
    #endregion

#region BRIDGING-STATE
    public string BridgeState { get; private set; } // CREATED, CONFIRMED, SYNCED, UPLOADED
    public DateTime ConfirmTimeStamp { get; private set; }
    public DateTime CreateTimestamp { get; private set;}
    public DateTime SyncTimestamp { get; private set;}
    public DateTime UploadTimestamp { get; private set;}
    #endregion

    public List<ResepMidwareItemModel> ListItem { get; set;}

    
#region METHODS-HEADER-RELATED

    public void Confirm(DateTime confirmTimeStamp)
    {
        BridgeState = BRIDGE_STATE_CONFIRMED;
        ConfirmTimeStamp = confirmTimeStamp;
        SyncTimestamp = AppConst.DEF_DATE;
        UploadTimestamp = AppConst.DEF_DATE;
    }
    public void ChartRefference(string chartId, string resepRsId)
    {
        ChartId = chartId;
        ResepRsId = resepRsId;
    }
    public void Sent(string reffId, DateTime sentTimeStamp)
    {
        ReffId = reffId;
        UploadTimestamp = sentTimeStamp;
        BridgeState = BRIDGE_STATE_UPLOADED;
    }
    #endregion
    
#region METHODS-ITEM-RELATED
    public Result<string> AddObat(MapDphoType mapBrgDpho, string signa, int qty)
    {
        var no = ListItem.Any() ? ListItem.Max(x => x.NoUrut) + 1 : 1;
        var newItem = new ResepMidwareItemModel(no, mapBrgDpho, signa, qty);
        ListItem.Add(newItem);
        
        return Result<string>.Success("Success");
    }
 
    public Result<string> AddRacik(MapDphoType mapBrgDpho, 
        string signa, int qty, string jenisRacik)
    {
        var no = ListItem.Any() ? ListItem.Max(x => x.NoUrut) + 1 : 1;
        var newItem = new ResepMidwareItemModel(no, mapBrgDpho, signa, qty);
        newItem.SetAsRacik(jenisRacik);
        ListItem.Add(newItem);
        
        return Result<string>.Success("Success");
    }
    #endregion
}