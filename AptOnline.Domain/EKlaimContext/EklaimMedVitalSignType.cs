namespace AptOnline.Domain.EKlaimContext;

public record EklaimMedVitalSignType(decimal BirthWeight, int Sistole, int Diastole)
{
    public static EklaimMedVitalSignType Default => new EklaimMedVitalSignType(0, 0, 0);
}