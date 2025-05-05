namespace AptOnline.Domain.EKlaimContext;

public record ApgarScoreType
{
    public ApgarScoreType(int appearance, int pulse, int grimace, int activity, int respiration)
    {
        //      GUARD
        if (appearance is < 0 or > 2)
            throw new ArgumentException("Appearance must be between 0 and 2", nameof(appearance));
        if (pulse is < 0 or > 2)
            throw new ArgumentException("Pulse must be between 0 and 2", nameof(pulse));
        if (grimace is < 0 or > 2)
            throw new ArgumentException("Grimace must be between 0 and 2", nameof(grimace));
        if (activity is < 0 or > 2)
            throw new ArgumentException("Activity must be between 0 and 2", nameof(activity));
        if (respiration is < 0 or > 2)
            throw new ArgumentException("Respiration must be between 0 and 2", nameof(respiration));
        
        //      ASSIGNMENT
        Appearance = appearance;
        Pulse = pulse;
        Grimace = grimace;
        Activity = activity;
        Respiration = respiration;
    }

    public int Appearance { get; init; }
    public int Pulse { get; init; }
    public int Grimace { get; init; }
    public int Activity { get; init; }
    public int Respiration { get; init; }
    
    public static ApgarScoreType Default => new(0, 0, 0, 0, 0);
}