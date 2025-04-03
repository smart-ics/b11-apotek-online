using AptOnline.Domain.PharmacyContext.BrgAgg;
using AptOnline.Domain.PharmacyContext.DphoAgg;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using GuardNet;

namespace AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;

public class ResepMidwareItemModel : IResepMidwareKey
{
    #region CONSTRUCTOR
    public ResepMidwareItemModel(int no, MapDphoModel mapDpho, string signa, int qty)
    {
        NoUrut = no;
        SetBrg(mapDpho);
        SetSigna(signa, qty);

        ResepMidwareId = string.Empty;
        IsRacik = false;
        JenisRacikan = string.Empty;
        Permintaan = 0;
        Note = string.Empty;
    }
    #endregion
    
    #region PROPERTIES
    public string ResepMidwareId { get; private set; }
    public int NoUrut { get; private set; }

    public BrgType Brg { get; private set; }
    public DphoRefference Dpho { get; private set; }

    public int Signa1 { get; private set; }
    public decimal Signa2 { get; private set; }
    public int Jumlah { get; private set; }
    public int Jho { get; private set; }

    public bool IsRacik { get; private set; }
    public string JenisRacikan { get; private set; }
    public int Permintaan { get; private set; }
    public string Note { get; private set; }
    #endregion
    
    
    #region METHODS
    public void SetId(string id) => ResepMidwareId = id;

    private void SetBrg(MapDphoModel mapDpho)
    {
        Guard.NotNull(mapDpho, nameof(mapDpho));
        Brg = mapDpho.Brg;
        Dpho = mapDpho.Dpho;
    }

    private void SetSigna(string signa, int qty)
    {
        var signaResult = SignaParser.Parse(signa);
        Signa1 = signaResult.DailyDose;
        Signa2 = signaResult.ConsumeAmount;
        
        Jumlah = qty;
        Jho = Convert.ToInt16(Math.Ceiling(qty / (Signa1 * Signa2)));
    }

    public void SetAsRacik(string jenisRacik)
    {
        JenisRacikan = jenisRacik;
        IsRacik = true;
    }

    public void SetAsObat()
    {
        JenisRacikan = string.Empty;
        IsRacik = false;
    }
    #endregion
}
