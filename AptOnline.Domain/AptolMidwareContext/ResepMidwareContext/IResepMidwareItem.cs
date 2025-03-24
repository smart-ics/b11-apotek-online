namespace AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;

public class ResepMidwareItemModel : IResepMidwareKey
{
    public string ResepMidwareId { get; set; }
    public int NoUrut { get; set; }

    public string BarangId { get; set; }
    public string BarangName { get; set; }
    public string DphoId { get; set; }
    public string DphoName { get; set; }

    public int Signa1 { get; set; }
    public int Signa2 { get; set; }
    public int Jumlah { get; set; }
    public int Jho { get; set; }

    public bool IsRacik { get; set; }
    public string JenisRacikan { get; set; }
    public int Permintaan { get; set; }
    
    public string Note { get; set; }
}