namespace AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;

public class ResepMidwareModel : IResepMidwareKey
{
    //  Key and Metadata Prop 
    public string ResepMidwareId { get; set; }
    public DateTime ResepMidwareDate { get; set; }
    public string BridgeState { get; set; }
    public DateTime CreateTimestamp { get; set;}
    public DateTime SyncTimestamp { get; set;}
    public DateTime UploadTimestamp { get; set;}

    //  Billing-EMR Related Prop
    public string ChartId { get; set; }
    public string ResepRsId { get; set; }
    public string RegId { get; set; }
    public string PasienId { get; set; }
    public string PasienName { get; set; }
    
    //  SEP Related Prop
    public string SepId { get;  set; }
    public DateTime SepDate { get; set;}
    public string NoPeserta { get; set;}
    public string FaskesId { get; set;}
    public string FaskesName { get; set;}
    public string PoliBpjsId { get; set;}
    public string PoliBpjsName { get; set;}
    public string DokterId { get; set;}
    public string DokterName { get; set;}

    //  APTOL Related Prop
    public string ReffId { get; set; }
    public string JenisObatId { get; set;}
    public int Iterasi { get; set;}
    public List<ResepMidwareItemModel> ListItem { get; set;}
}