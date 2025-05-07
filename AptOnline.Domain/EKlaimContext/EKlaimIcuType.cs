namespace AptOnline.Domain.EKlaimContext;

public record EKlaimIcuType(AdlScoreType AdlSubAcute, AdlScoreType AdlChronic, YesNoIndikatorValType IcuIndikatorVal, int IcuLos)
{
    public static EKlaimIcuType Default 
        => new EKlaimIcuType(AdlScoreType.Default, AdlScoreType.Default, YesNoIndikatorValType.Default, 0);
}