namespace AptOnline.Domain.EKlaimContext;

public record AdlScoreValType // Activity Daily Living Score
{

    private AdlScoreValType(int value) => Value = value;

    public static AdlScoreValType Create(int value)
    {
        if (value is < 12 or > 60)
            throw new ArgumentException("ADL Score invalid. Harus berada antara 12 s/d 60");
        return new AdlScoreValType(value);
    }
    public static AdlScoreValType Load(int value) => new AdlScoreValType(value);

    public static AdlScoreValType Default => new AdlScoreValType(60);
    public int Value { get; init; }
}