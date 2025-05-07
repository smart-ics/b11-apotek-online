namespace AptOnline.Domain.EKlaimContext;

public record EklaimVitalSignType(decimal BirthWeight, int Sistole, int Diastole)
{
    public static EklaimVitalSignType Default => new EklaimVitalSignType(0, 0, 0);
}