using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext;

public record EklaimMedVitalSignType
{
    private EklaimMedVitalSignType(decimal birthWeight, int sistole, int diastole)
    {
        BirthWeight = birthWeight;
        Sistole = sistole;
        Diastole = diastole;
    }

    public static EklaimMedVitalSignType Create(decimal birthWeight, int sistole, int diastole)
    {
        Guard.Against.Negative(birthWeight,  nameof(birthWeight), "Berat lahir minimal 0");
        Guard.Against.Negative(sistole, nameof(sistole), "Sistol minimal 0"); 
        Guard.Against.Negative(diastole,  nameof(diastole), "Diastol minimal 0");
        
        return new EklaimMedVitalSignType(birthWeight, sistole, diastole);
    }
    
    public static EklaimMedVitalSignType Load(decimal birthWeight, int sistole, int diastole)
        => new EklaimMedVitalSignType(birthWeight, sistole, diastole);

    public static EklaimMedVitalSignType Default 
        => new EklaimMedVitalSignType(0, 0, 0);
    
    public decimal BirthWeight { get; init; } 
    public int Sistole { get; init; }
    public int Diastole { get; init; }
}