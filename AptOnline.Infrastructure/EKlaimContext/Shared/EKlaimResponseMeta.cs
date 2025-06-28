namespace AptOnline.Infrastructure.EKlaimContext.Shared;

public class EKlaimResponseMeta
{
    public string code { get; set; }
    public string message { get; set; }
    public static EKlaimResponseMeta Default => new EKlaimResponseMeta
    {
        code = "500",
        message = "Internal Server Error: Default Response created"
    };
}
