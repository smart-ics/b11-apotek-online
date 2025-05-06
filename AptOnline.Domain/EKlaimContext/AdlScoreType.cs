namespace AptOnline.Domain.EKlaimContext;

public record AdlScoreType // Activity Daily Living Score
{

    private AdlScoreType(int value) => Value = value;

    public static AdlScoreType Create(int value)
    {
        if (value is < 12 or > 60)
            throw new ArgumentException("ADL Score invalid. Harus berada antara 12 s/d 60");
        return new AdlScoreType(value);
    }

    public static AdlScoreType Load(int value) => new AdlScoreType(value);
    public static AdlScoreType Default => new AdlScoreType(60);
    public int Value { get; init; }
}