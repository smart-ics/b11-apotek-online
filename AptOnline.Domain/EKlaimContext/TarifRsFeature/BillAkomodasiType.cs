using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public record BillAkomodasiType(decimal Kamar, decimal RawatIntensif)
{
    public static BillAkomodasiType Create(decimal kamar, decimal rawatIntensif)
    {
        Guard.Against.Negative(kamar, nameof(kamar));
        Guard.Against.Negative(rawatIntensif, nameof(rawatIntensif));
        return new BillAkomodasiType(kamar, rawatIntensif);
    } 
    public static BillAkomodasiType Default => new BillAkomodasiType(0, 0); 
}