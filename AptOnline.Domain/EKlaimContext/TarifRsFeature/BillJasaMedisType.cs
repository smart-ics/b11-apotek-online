using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public record BillJasaMedisType(decimal Konsultasi, decimal TenagaAhli, decimal Keperawatan)
{
    public static BillJasaMedisType Create(decimal konsultasi, decimal tenagaAhli, decimal keperawatan)
    {
        Guard.Against.Negative(konsultasi, nameof(konsultasi));
        Guard.Against.Negative(tenagaAhli, nameof(tenagaAhli));
        Guard.Against.Negative(keperawatan, nameof(keperawatan));
        
        return new BillJasaMedisType(konsultasi, tenagaAhli, keperawatan);
    }
    public static BillJasaMedisType Default => new BillJasaMedisType(0, 0, 0);
}