using AptOnline.Domain.PharmacyContext.BrgAgg;
using AptOnline.Domain.PharmacyContext.DphoAgg;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using GuardNet;

namespace AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;

public class ResepMidwareItemModel : IResepMidwareKey
{
    #region CONSTRUCTOR
    public ResepMidwareItemModel(int no, MapDphoType mapDpho, string signa, int qty)
    {
        NoUrut = no;
        SetBrg(mapDpho);
        SetSigna(signa, qty);

        ResepMidwareId = string.Empty;
        IsRacik = false;
        RacikId = string.Empty;
        //Permintaan = 0;
        Note = string.Empty;
    }

    private ResepMidwareItemModel()
    {
    }

    public static ResepMidwareItemModel Load(
        string respeMidwareId, int noUrut, bool isRacik, string racikId,
        string barangId, string barangName, string dphoId, string dphoName,
        int signa1, decimal signa2, int permintaan, int jho, int jumlah, string note, bool isUploaded)
        => new ResepMidwareItemModel
        {
            ResepMidwareId = respeMidwareId,
            NoUrut = noUrut,
            IsRacik = isRacik,
            RacikId = racikId,
            Brg = new BrgType(barangId, barangName),
            Dpho = new DphoRefference(dphoId, dphoName),
            Signa1 = signa1,
            Signa2 = signa2,
            Permintaan = permintaan,
            Jho = jho,
            Jumlah = jumlah,
            Note = note,
            IsUploaded = isUploaded
        };
    #endregion
    
    #region PROPERTIES
    public string ResepMidwareId { get; private set; }
    public int NoUrut { get; private set; }
    public bool IsRacik { get; private set; }
    public string RacikId { get; private set; }

    public BrgType Brg { get; private set; }
    public DphoRefference Dpho { get; private set; }

    public int Signa1 { get; private set; }
    public decimal Signa2 { get; private set; }
    public int Permintaan { get; private set; }
    public int Jho { get; private set; }
    public int Jumlah { get; private set; }

    public string Note { get; private set; }

    public bool IsUploaded { get; private set; }
    #endregion


    #region METHODS
    public void SetId(string id) => ResepMidwareId = id;

    private void SetBrg(MapDphoType mapDpho)
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
        Permintaan = qty;
        Jumlah = qty;
        Jho = Convert.ToInt16(Math.Ceiling(qty / (Signa1 * Signa2)));
    }

    public void SetAsRacik(string jenisRacik)
    {
        RacikId = jenisRacik;
        IsRacik = true;
    }

    public void SetAsObat()
    {
        RacikId = string.Empty;
        IsRacik = false;
    }

    public void SetUploaded()
    {
        IsUploaded = true;
    }

    public void SetNote(string note)
    {
        Note = note;
    }
    #endregion
}
