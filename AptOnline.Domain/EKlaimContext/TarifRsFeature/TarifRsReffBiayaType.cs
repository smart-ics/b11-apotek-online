namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public class TarifRsReffBiayaType
{
    public TarifRsReffBiayaType(int noUrut, string trsId, 
        ReffBiayaType reffBiaya, string ketBiaya, decimal nilai)
    {
        NoUrut = noUrut;
        TrsId = trsId;
        ReffBiaya = reffBiaya;
        KetBiaya = ketBiaya;
        Nilai = nilai;
        SkemaJkn = SkemaJknType.Default;
    }
    public int NoUrut { get; init; } 
    public string TrsId { get; init; } 
    public ReffBiayaType ReffBiaya { get; init; } 
    public string KetBiaya { get; init; }
    public decimal Nilai { get; init; }
    public SkemaJknType SkemaJkn { get; private set; }
    public void SetSkemaJkn(SkemaJknType skemaJkn) => SkemaJkn = skemaJkn;
}