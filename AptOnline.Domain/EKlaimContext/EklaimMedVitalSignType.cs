using GuardNet;

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
        Guard.NotLessThan(birthWeight, 0, nameof(birthWeight), "Berat lahir minimal 0");
        Guard.NotLessThan(sistole, 0, nameof(sistole), "Sistol minimal 0"); 
        Guard.NotLessThan(diastole, 0, nameof(diastole), "Diastol minimal 0");
        
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