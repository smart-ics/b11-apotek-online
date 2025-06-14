using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public record BillProcedureType(decimal NonBedah, decimal Bedah)
{
    public static BillProcedureType Create(decimal nonBedah, decimal bedah)
    {
        Guard.Against.Negative(nonBedah, nameof(nonBedah));
        Guard.Against.Negative(bedah, nameof(bedah));
        
        return new BillProcedureType(nonBedah, bedah);
    }
    public static BillProcedureType Default => new BillProcedureType(0, 0);
}