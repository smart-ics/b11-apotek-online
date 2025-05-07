using GuardNet;

namespace AptOnline.Domain.EKlaimContext;

public record EKlaimMedIcuType
{
    public EKlaimMedIcuType(AdlScoreValType adlSubAcute, AdlScoreValType adlChronic, 
        YesNoIndikatorValType icuIndikator, int icuLos)
    {
        Guard.NotNull(adlSubAcute, nameof(adlSubAcute), "ADL Sub Acute Tidak boleh kosong");
        Guard.NotNull(adlChronic, nameof(adlChronic), "ADL Chronic Tidak boleh kosong");
        Guard.NotNull(icuIndikator, nameof(icuIndikator), "ICU Indikator Tidak boleh kosong");
        
        AdlSubAcute = adlSubAcute;
        AdlChronic = adlChronic;
        IcuIndikator = icuIndikator;
        IcuLos = icuLos;
    }

    public static EKlaimMedIcuType Create(AdlScoreValType adlSubAcute, AdlScoreValType adlChronic,
        YesNoIndikatorValType icuIndikator, int icuLos)
    {
        Guard.NotLessThan(icuLos, 0, nameof(icuLos), "ICU LOS minimal 0");
        return new EKlaimMedIcuType(adlSubAcute, adlChronic, icuIndikator, icuLos);
    } 
    
    public static EKlaimMedIcuType Load(AdlScoreValType adlSubAcute, AdlScoreValType adlChronic, 
        YesNoIndikatorValType icuIndikator, int icuLos) 
        => new EKlaimMedIcuType(adlSubAcute, adlChronic, icuIndikator, icuLos);
    
    public static EKlaimMedIcuType Default 
        => new EKlaimMedIcuType(AdlScoreValType.Default, AdlScoreValType.Default, YesNoIndikatorValType.Default, 0);
    
    public AdlScoreValType AdlSubAcute {get; init;} 
    public AdlScoreValType AdlChronic {get; init;} 
    public YesNoIndikatorValType IcuIndikator {get; init;} 
    public int IcuLos {get; init;}
}