namespace AptOnline.Domain.EKlaimContext;

public record ApgarType
{
    public ApgarType(ApgarScoreType minute1, ApgarScoreType minute5)
    {
        Minute1 = minute1;
        Minute5 = minute5;
    }

    public ApgarScoreType Minute1 { get; init; }
    public ApgarScoreType Minute5 { get; init; }

    public static ApgarType Default => new ApgarType(ApgarScoreType.Default, ApgarScoreType.Default);
}