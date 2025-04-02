using AptOnline.Domain.AptolCloudContext.DphoAgg;
using AptOnline.Domain.Helpers;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using GuardNet;
using Nuna.Lib.PatternHelper;

namespace AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;

public class ResepMidwareItemModel : IResepMidwareKey
{
    #region CONSTRUCTOR
    public ResepMidwareItemModel(int no)
    {
        NoUrut = no;
        BarangId = string.Empty;
        BarangName = string.Empty;
        DphoId = string.Empty;
        DphoName = string.Empty;

        Signa1 = 0;
        Signa2 = 0;
        Jumlah = 0;
        Jho = 0;
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

    public string BarangId { get; private set; }
    public string BarangName { get; private set; }
    public string DphoId { get; private set; }
    public string DphoName { get; private set; }

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

    public NunaResult<string> SetBrg(MapDphoModel mapDpho)
    {
        Guard.NotNull(mapDpho, nameof(mapDpho));
        Guard.NotNullOrWhitespace(mapDpho.BrgId, nameof(mapDpho.BrgId));
        Guard.NotNullOrWhitespace(mapDpho.BrgName, nameof(mapDpho.BrgName));
        Guard.NotNullOrWhitespace(mapDpho.DphoId, nameof(mapDpho.DphoId));
        Guard.NotNullOrWhitespace(mapDpho.DphoName, nameof(mapDpho.DphoName));

        BarangId = mapDpho.BrgId;
        BarangName = mapDpho.BrgName;
        DphoId = mapDpho.DphoId;
        DphoName = mapDpho.DphoName;

        return NunaResult<string>.Success("Success");
    }

    public NunaResult<SignaParser.SignaType> SetSigna(string signa, int qty)
    {
        SignaParser.SignaType signaResult;
        try
        {
            signaResult = SignaParser.Parse(signa);
        }
        catch (FormatException ex)
        {
            return NunaResult<SignaParser.SignaType>.Failure(ex.Message);
        }
        Signa1 = signaResult.DailyDose;
        Signa2 = signaResult.ConsumeAmount;
        
        Jumlah = qty;
        Jho = Convert.ToInt16(Math.Ceiling(qty / (Signa1 * Signa2)));
        return NunaResult<SignaParser.SignaType>.Success(signaResult);
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
