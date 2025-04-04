using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.AptolCloudContext.PpkAgg;

public record VerifikatorType(string Nama, string Npp)
{
    public static VerifikatorType Default => new VerifikatorType(AppConst.DASH, AppConst.DASH); 
}