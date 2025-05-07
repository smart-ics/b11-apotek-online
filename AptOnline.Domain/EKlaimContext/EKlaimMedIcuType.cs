namespace AptOnline.Domain.EKlaimContext;

public record EKlaimMedIcuType(AdlScoreType AdlSubAcute, AdlScoreType AdlChronic, YesNoIndikatorValType IcuIndikatorVal, int IcuLos)
{
    public static EKlaimMedIcuType Default 
        => new EKlaimMedIcuType(AdlScoreType.Default, AdlScoreType.Default, YesNoIndikatorValType.Default, 0);
}