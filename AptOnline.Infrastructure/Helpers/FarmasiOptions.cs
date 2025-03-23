namespace AptOnline.Infrastructure.Helpers;

public class FarmasiOptions
{
    public const string SECTION_NAME = "Farmasi";

    public string BaseApiUrl { get; set; }
    public string ConsId { get; set; }
    public string SecretKey { get; set; }
}