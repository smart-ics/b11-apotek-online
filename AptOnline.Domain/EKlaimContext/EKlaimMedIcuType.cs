using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext;

public record EKlaimMedIcuType
{
    public EKlaimMedIcuType(AdlScoreType adlSubAcute, AdlScoreType adlChronic, 
        YesNoIndikatorValType icuIndikator, int icuLos)
    {
        Guard.Against.Null(adlSubAcute, nameof(adlSubAcute), "ADL Sub Acute Tidak boleh kosong");
        Guard.Against.Null(adlChronic, nameof(adlChronic), "ADL Chronic Tidak boleh kosong");
        Guard.Against.Null(icuIndikator, nameof(icuIndikator), "ICU Indikator Tidak boleh kosong");
        
        AdlSubAcute = adlSubAcute;
        AdlChronic = adlChronic;
        IcuIndikator = icuIndikator;
        IcuLos = icuLos;
    }

    public static EKlaimMedIcuType Create(AdlScoreType adlSubAcute, AdlScoreType adlChronic,
        YesNoIndikatorValType icuIndikator, int icuLos)
    {
        Guard.Against.Negative(icuLos, nameof(icuLos), "ICU LOS minimal 0");
        return new EKlaimMedIcuType(adlSubAcute, adlChronic, icuIndikator, icuLos);
    } 
    
    public static EKlaimMedIcuType Load(AdlScoreType adlSubAcute, AdlScoreType adlChronic, 
        YesNoIndikatorValType icuIndikator, int icuLos) 
        => new EKlaimMedIcuType(adlSubAcute, adlChronic, icuIndikator, icuLos);
    
    public static EKlaimMedIcuType Default 
        => new EKlaimMedIcuType(AdlScoreType.Default, AdlScoreType.Default, YesNoIndikatorValType.Default, 0);
    
    public AdlScoreType AdlSubAcute {get; init;} 
    public AdlScoreType AdlChronic {get; init;} 
    public YesNoIndikatorValType IcuIndikator {get; init;} 
    public int IcuLos {get; init;}
}