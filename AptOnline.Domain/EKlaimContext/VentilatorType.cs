namespace AptOnline.Domain.EKlaimContext;

public record VentilatorType
{
    public VentilatorType(string usageIndicator, DateTime start, DateTime stop)
    {
        //      GUARD
        if (usageIndicator != "0" && usageIndicator != "1")
            throw new ArgumentException("Usage indicator must be either '0' or '1'", nameof(usageIndicator));
        if (stop < start)
            throw new ArgumentException("Stop time must be after start time", nameof(stop));

        //      ASSIGNMENT
        UsageIndicator = usageIndicator;
        UsageStart = start;
        UsageStop = stop;
    }
    public string UsageIndicator { get; init; }     //  <- use_ind
    public DateTime UsageStart { get; init; }       //  <- start_dttm 
    public DateTime UsageStop { get; init; }        //  <- stop_dttm
    
    public static VentilatorType Default => new VentilatorType("0", new DateTime(3000,1,1), new DateTime(3000,1,1));
}