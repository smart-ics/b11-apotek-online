namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public record ReffBiayaType(string ReffBiayaId, JenisReffBiayaEnum ReffClass)
{
    public static ReffBiayaType Default => new("", JenisReffBiayaEnum.Jasa);
}