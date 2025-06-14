using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public record BillPenunjangType(
    decimal Penunjang,
    decimal Radiologi,
    decimal Laborat,
    decimal BankDarah,
    decimal Rehabilitasi)
{
    public static BillPenunjangType Create(decimal penunjang, decimal radiologi, 
        decimal laborat, decimal bankDarah, decimal rehabilitasi)
    {
        Guard.Against.Negative(penunjang, nameof(penunjang));
        Guard.Against.Negative(radiologi, nameof(radiologi));
        Guard.Against.Negative(laborat, nameof(laborat));
        Guard.Against.Negative(bankDarah, nameof(bankDarah));
        Guard.Against.Negative(rehabilitasi, nameof(rehabilitasi));
        return new BillPenunjangType(penunjang, radiologi, laborat, bankDarah, rehabilitasi);
    }
    public static BillPenunjangType Default => new BillPenunjangType(0, 0, 0, 0, 0); 
}