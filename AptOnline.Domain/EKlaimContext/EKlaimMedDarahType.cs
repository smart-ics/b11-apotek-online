using GuardNet;

namespace AptOnline.Domain.EKlaimContext;

public record EKlaimMedDarahType
{
    public EKlaimMedDarahType(YesNoIndikatorValType dializerSingleUse, int kantongDarah, YesNoIndikatorValType alteplaseIndikator)
    {
        Guard.NotNull(dializerSingleUse, nameof(dializerSingleUse), "Dializer Single Use Tidak boleh kosong");
        Guard.NotNull(alteplaseIndikator, nameof(alteplaseIndikator), "Alteplase Indikator Tidak boleh kosong");
        
        DializerSingleUse = dializerSingleUse;
        KantongDarah = kantongDarah;
        AlteplaseIndikator = alteplaseIndikator;
    }

    public static EKlaimMedDarahType Create(YesNoIndikatorValType dializerSingleUse, int kantongDarah,
        YesNoIndikatorValType alteplaseIndikator)
    {
        Guard.NotLessThan(kantongDarah, 0, nameof(kantongDarah), "Kantong Darah minimal 0");
        return new EKlaimMedDarahType(dializerSingleUse, kantongDarah, alteplaseIndikator);
    }
    
    public static EKlaimMedDarahType Load(YesNoIndikatorValType dializerSingleUse, int kantongDarah, 
        YesNoIndikatorValType alteplaseIndikator) 
        => new(dializerSingleUse, kantongDarah, alteplaseIndikator);
    
    public static EKlaimMedDarahType Default
    => new EKlaimMedDarahType(YesNoIndikatorValType.Default, 0, YesNoIndikatorValType.Default);
    
    public YesNoIndikatorValType DializerSingleUse { get; init; } 
    public int KantongDarah { get; init; }
    public YesNoIndikatorValType AlteplaseIndikator { get; init; }
}
