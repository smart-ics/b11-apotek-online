namespace AptOnline.Domain.SepContext.SkdpFeature;

public record SkdpRefference(string SkdpNo)
{
    public static SkdpRefference Default => new SkdpRefference("-");
}