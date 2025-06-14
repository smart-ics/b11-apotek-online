using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext;

public record IcuIndikatorType(int IcuFlag, int Los)
{
    public static IcuIndikatorType Create(int icuFlag, int los)
    {
        Guard.Against.OutOfRange(icuFlag, nameof(icuFlag), 0, 1);
        Guard.Against.OutOfRange(los, nameof(los), 0, 31);
        return new  IcuIndikatorType(icuFlag, los);
    }
    public string Description => IcuFlag switch
    {
        0 => "Non ICU",
        1 => "ICU",
        _ => throw new ArgumentOutOfRangeException(nameof(IcuFlag), IcuFlag, null)
    };
}