using System.Globalization;
using AptOnline.Domain.AptolCloudContext.FaskesAgg;
using AptOnline.Domain.BillingContext.LayananAgg;
using AptOnline.Domain.BillingContext.RegAgg;
using AptOnline.Domain.BillingContext.SepAgg;
using AptOnline.Domain.PharmacyContext.MapDphoAgg;
using GuardNet;
using Nuna.Lib.DataTypeExtension;
using Nuna.Lib.PatternHelper;
using Nuna.Lib.ValidationHelper;

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
    public string RegId { get; set; }
    public string PasienId { get; set; }
    public string PasienName { get; set; }
    #endregion
    
    #region SEP-RELATED
    public string SepId { get;  set; }
    public DateTime SepDate { get; set;}
    public string NoPeserta { get; set;}
    
    
    public string FaskesId { get; set;}
    public string FaskesName { get; set;}
    public string PoliBpjsId { get; set;}
    public string PoliBpjsName { get; set;}
    public string DokterId { get; set;}
    public string DokterName { get; set;}
    #endregion

    #region APTOL-RELATED
    public string ReffId { get; set; }
    public string JenisObatId { get; set;}
    public int Iterasi { get; set;}
    public List<ResepMidwareItemModel> ListItem { get; set;}
    #endregion
    
    #region METHODS-HEADER-RELATED
    public void SetRegister(RegModel reg)
    {
        //  GUARD
        Guard.NotNull(reg, nameof(reg));
        Guard.NotNullOrWhitespace(reg.RegId, nameof(reg.RegId));
        Guard.NotNullOrWhitespace(reg.PasienId, nameof(reg.PasienId));
        Guard.NotNullOrWhitespace(reg.PasienName, nameof(reg.PasienName));
        
        //  BUILD      
        RegId = reg.RegId;
        PasienId = reg.PasienId;
        PasienName = reg.PasienName;
    }
    public void SetSep(SepModel sep)
    {
        //  GUARD
        Guard.NotNull(sep, nameof(sep));
        Guard.NotNullOrWhitespace(sep.SepId, nameof(sep.SepId));
        Guard.For(() => IsValidDate(sep.SepDateTime), new ArgumentException(nameof(sep.SepDateTime)));
        Guard.NotNullOrWhitespace(sep.PesertaJaminanId, nameof(sep.PesertaJaminanId));
        Guard.NotNullOrWhitespace(sep.DpjpId, nameof(sep.DpjpId));
        Guard.NotNullOrWhitespace(sep.DpjpName, nameof(sep.DpjpName));
        
        //  BUILD 
        SepId = sep.SepId;
        var sepDateStr = sep.SepDateTime.Left(10);
        SepDate = sepDateStr.ToDate(DateFormatEnum.YMD);
        NoPeserta = sep.PesertaJaminanId;
        DokterId = sep.DpjpId;
        DokterName = sep.DpjpName;
        return;
        
        //  INNER HELPER
        bool IsValidDate(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 10)
                return false;

            var datePart = input[..10];
            return DateTime.TryParseExact(
                datePart,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _
            );
        }        
    }

    public void SetPoliBpjs(LayananModel layanan)
    {
        //  GUARD
        Guard.NotNull(layanan, nameof(layanan));
        Guard.NotNullOrWhitespace(layanan.LayananBpjsId, nameof(layanan.LayananBpjsId));
        Guard.NotNullOrWhitespace(layanan.LayananBpjsName, nameof(layanan.LayananBpjsName));
        
        //  BUILD
        PoliBpjsId = layanan.LayananBpjsId;
        PoliBpjsName = layanan.LayananBpjsName;
    }

    public void SetFaskes(FaskesModel faskes)
    {
        FaskesId = faskes.FaskesId;
        FaskesName = faskes.FaskesName;
    }
    #endregion
    
    #region METHODS-ITEM-RELATED

    public NunaResult<string> AddObat(MapDphoModel mapBrgDpho, string signa, int qty)
    {
        // Guard.NotNull(mapBrgDpho, nameof(mapBrgDpho));
        // Guard.NotNullOrWhitespace(mapBrgDpho.BrgId, nameof(mapBrgDpho.BrgId));
        // Guard.NotNullOrWhitespace(mapBrgDpho.BrgName, nameof(mapBrgDpho.BrgName));
        // Guard.NotNullOrWhitespace(mapBrgDpho.DphoId, nameof(mapBrgDpho.DphoId));
        // Guard.NotNullOrWhitespace(mapBrgDpho.DphoName, nameof(mapBrgDpho.DphoName));
        // Guard.NotNullOrWhitespace(signa, nameof(signa));
        // Guard.NotLessThan(qty, 1, nameof(qty));
        //
        var no = ListItem.Max(x => x.NoUrut+1);
        var newItem = new ResepMidwareItemModel(no, mapBrgDpho, signa, qty);
        

        //resultType = newItem.SetBrg(mapBrgDpho, qty);
        
        
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
    public NunaResult<string> AddRacik(MapDphoModel brgDpho, string signa, int qty)
    {
        // var no = ListItem.Max(x => x.NoUrut+1);
        // var newItem = new ResepMidwareItemModel(no);
        // var result = newItem.SetSigna(signa, qty);
        // if (!result.IsSuccess)
        //     return NunaResult<string>.Failure(result.ErrorMessage);
        //
        // return NunaResult<string>.Success("Success");
        throw new NotImplementedException();
    }

    #endregion
}