using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.PelayananDarahFeature;

public record PelayananDarahType
{
    public PelayananDarahType(DializerUsageType dializerUsage, int jumlahKantongDarah, bool alteplaseIndikator)
    {
        Guard.Against.Null(dializerUsage, nameof(dializerUsage));
        
        DializerUsage = dializerUsage;
        JumlahKantongDarah = jumlahKantongDarah;
        AlteplaseIndikator = alteplaseIndikator;
    }    
    public DializerUsageType DializerUsage { get; init; }
    public int JumlahKantongDarah { get; init; }
    public bool AlteplaseIndikator { get; init; }

    public static PelayananDarahType Create(DializerUsageType dializerUsage, 
        int jumlahKantongDarah, bool alteplaseIndikator)
    {
        Guard.Against.Null(dializerUsage, nameof(dializerUsage));
        Guard.Against.Negative(jumlahKantongDarah,  nameof(jumlahKantongDarah));    

        return new PelayananDarahType(dializerUsage, jumlahKantongDarah, alteplaseIndikator);
    }
    
    public static PelayananDarahType Default => new PelayananDarahType(DializerUsageType.Default, 0, false);
    
}