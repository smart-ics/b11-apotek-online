namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public record MapSkemaJknType(ReffBiayaType ReffBiaya, SkemaJknType SkemaJkn)
{
    public static MapSkemaJknType Default => new (ReffBiayaType.Default, SkemaJknType.Default);
} 
